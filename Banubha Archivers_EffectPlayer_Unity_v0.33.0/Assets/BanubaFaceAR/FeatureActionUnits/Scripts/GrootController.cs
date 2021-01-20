using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BNB
{
    public class GrootController : MonoBehaviour
    {
        int faceIndex;

        float[] actionunits;

        // Start is called before the first frame update
        void Start()
        {
            // set actionunits feature for Groot
            var featuresId = BanubaSDKBridge.bnb_recognizer_get_features_id();

            var error = IntPtr.Zero;
            BanubaSDKBridge.bnb_recognizer_insert_feature(BanubaSDKManager.instance.recognizer, featuresId.action_units, out error);
            Utils.CheckError(error);

            actionunits = new float[(int) BanubaSDKBridge.bnb_action_units_mapping_t.BNB_AU_total_au_count];

            var faceController = gameObject.GetComponentInParent<FaceController>();
            faceIndex = faceController.faceIndex;

            BanubaSDKManager.instance.onRecognitionResult += onRecognitionResult;
        }

        void onRecognitionResult(FrameData frameData)
        {
            var error = IntPtr.Zero;
            var hasActionUnits = BanubaSDKBridge.bnb_frame_data_has_action_units(frameData, out error);
            Utils.CheckError(error);
            if (!hasActionUnits)
                return;

            var face = BanubaSDKBridge.bnb_frame_data_get_face(frameData, faceIndex, out error);
            Utils.CheckError(error);
            if (face.rectangle.hasFaceRectangle == 0)
                return;

            // now only first face supported
            if (faceIndex == 0)
                UpdateGroot(frameData);
            else
                gameObject.SetActive(false);
        }

        void UpdateGroot(FrameData frameData)
        {
            var error = IntPtr.Zero;

            var groot = gameObject;

            var headobj = groot.transform.Find("Head");
            var headSkinnedMeshRenderer = headobj.GetComponent<SkinnedMeshRenderer>();
            var headBlendShapeCount = headSkinnedMeshRenderer.sharedMesh.blendShapeCount;

            var teethobj = groot.transform.Find("teeth");
            var teethSkinnedMeshRenderer = teethobj.GetComponent<SkinnedMeshRenderer>();
            var teethBlendShapeCount = teethSkinnedMeshRenderer.sharedMesh.blendShapeCount;

            var au = BanubaSDKBridge.bnb_frame_data_get_action_units(frameData, faceIndex, out error);
            Utils.CheckError(error);

            Marshal.Copy(au.units, actionunits, 0, actionunits.Length);
            {
                int i = 0;
                int j = 0;
                while (i < headBlendShapeCount) {
                    // groot model have no this blendshapes
                    if (j == (int) BanubaSDKBridge.bnb_action_units_mapping_t.BNB_AU_MouthDimpleLeft || j == (int) BanubaSDKBridge.bnb_action_units_mapping_t.BNB_AU_MouthDimpleRight) {
                        j++;
                        continue;
                    }

                    headSkinnedMeshRenderer.SetBlendShapeWeight(i++, actionunits[j++] * 100F);
                }
            }

            if (teethBlendShapeCount > 0) {
                // teeth moves with jaw
                var teeth = actionunits[(int) BanubaSDKBridge.bnb_action_units_mapping_t.BNB_AU_JawOpen];
                teethSkinnedMeshRenderer.SetBlendShapeWeight(0, teeth * 100F);
            }
        }
    }

}
