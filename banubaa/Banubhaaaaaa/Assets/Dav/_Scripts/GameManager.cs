using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Debug Elements")]
    bool scaleInc, posInc;
    int scaleDir, posDir;
    [SerializeField] Transform objToChange;
    [SerializeField] TextMeshProUGUI scaleIncDec, posIncDec, scaleDirr, posDirr;

    [Header("Jewelery Elements")]
    public Jewelry[] jewelry;
    [SerializeField] int jewelryIndex = 0;
    [SerializeField] int jewelryType = 0;

    #region Debug

    public void ScaleIncDec()
    {
        scaleInc = !scaleInc;
        if (scaleInc) scaleIncDec.text = "Inc Scale";
        else scaleIncDec.text = "Dec Scale";
    }

    public void PosIncDec()
    {
        posInc = !posInc;
        if (posInc) posIncDec.text = "Inc Pos";
        else posIncDec.text = "Dec Pos";
    }

    public void SetScaleDir()
    {
        scaleDir++;
        if (scaleDir > 2) scaleDir = 0;
        if (scaleDir == 0) scaleDirr.text = "X";
        else if (scaleDir == 1) scaleDirr.text = "Y";
        else scaleDirr.text = "Z";
    }

    public void SetPosDir()
    {
        posDir++;
        if (posDir > 2) posDir = 0;
        if (posDir == 0) posDirr.text = "X";
        else if (posDir == 1) posDirr.text = "Y";
        else posDirr.text = "Z";
    }

    public void ChangePosition()
    {
        Vector3 deltaChangePos = Vector3.zero;
        if (posDir == 0) deltaChangePos = new Vector3(1, 0, 0);
        else if (posDir == 1) deltaChangePos = new Vector3(0, 1, 0);
        else deltaChangePos = new Vector3(0, 0, 1);
        if (!posInc) deltaChangePos = -deltaChangePos;
        objToChange.localPosition += deltaChangePos;
    }

    public void ChangeScale()
    {
        Vector3 deltaChangeScale = Vector3.zero;
        if (scaleDir == 0) deltaChangeScale = new Vector3(1, 0, 0);
        else if (scaleDir == 1) deltaChangeScale = new Vector3(0, 1, 0);
        else deltaChangeScale = new Vector3(0, 0, 1);
        if (!scaleInc) deltaChangeScale = -deltaChangeScale;
        objToChange.localScale += deltaChangeScale;
    }

    public void Print()
    {
        Debug.Log("Local Position: " + objToChange.localPosition);
        Debug.Log("Local Scale: " + objToChange.localScale);
    }

    #endregion

    public void SelectJeweleryType(int type)
    {
        jewelry[jewelryType].objects[jewelryIndex].SetActive(false);
        jewelryType = type;
        jewelryIndex = 0;
        jewelry[jewelryType].objects[jewelryIndex].SetActive(true);
    }

    public void EnableNextJewelery()
    {
        jewelryIndex++;
        if (jewelryIndex >= jewelry[jewelryType].objects.Length) jewelryIndex = 0;
        jewelry[jewelryType].objects[jewelryIndex].SetActive(true);
        foreach (GameObject g in jewelry[jewelryType].objects)
        {
            if (g != jewelry[jewelryType].objects[jewelryIndex]) g.SetActive(false);
        }
    }

}

[System.Serializable]
public class Jewelry
{
    public string name;
    public GameObject[] objects;
}
