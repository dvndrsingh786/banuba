function Effect() {
    var self = this;

    this.meshes = [
        { file: "cut.bsm2", anims: [
                                { a: "static", t: 0 },
                        ],

                is_physics_applied: "False".toLowerCase() === true
        },
        { file: "earrings.bsm2", anims: [
                                { a: "Take 001", t: 3333 },
                        ],

                is_physics_applied: "True".toLowerCase() === true
        },
        { file: "necklace_l.bsm2", anims: [
                                { a: "static", t: 0 },
                        ],

                is_physics_applied: "False".toLowerCase() === true
        },
        { file: "necklace_r.bsm2", anims: [
                                { a: "static", t: 0 },
                        ],

                is_physics_applied: "False".toLowerCase() === true
        },
    ];

    this.play = function() {
        var now = (new Date()).getTime();
        for(var i = 0; i < self.meshes.length; i++) {
                if(now > self.meshes[i].endTime) {
                        self.meshes[i].animIdx = (self.meshes[i].animIdx + 1)%self.meshes[i].anims.length;
                        if (!self.meshes[i].is_physics_applied) {
                                Api.meshfxMsg("animOnce", i, 0, self.meshes[i].anims[self.meshes[i].animIdx].a);
                        }
                        self.meshes[i].endTime = now + self.meshes[i].anims[self.meshes[i].animIdx].t;
                }
        }

        // if(isMouthOpen(world.landmarks, world.latents)) {
        //  Api.hideHint();
        // }
    };

    this.init = function() {
        Api.meshfxMsg("spawn", 4, 0, "!glfx_FACE");

        Api.meshfxMsg("spawn", 0, 0, "cut.bsm2");
        // Api.meshfxMsg("animOnce", 0, 0, "static");

        Api.meshfxMsg("spawn", 1, 0, "earrings.bsm2");
        // Api.meshfxMsg("animOnce", 1, 0, "Take 001");

        Api.meshfxMsg("spawn", 2, 0, "necklace_l.bsm2");
        // Api.meshfxMsg("animOnce", 2, 0, "static");

        Api.meshfxMsg("spawn", 3, 0, "necklace_r.bsm2");
        // Api.meshfxMsg("animOnce", 3, 0, "static");






        Api.meshfxMsg("dynImass", 1, 0, "joint_earringL_root");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringL_second_0");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringL_third_0");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringL_third_1");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringL_first_0");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringL_second_1");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringL_first_1");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringL_second_2");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringL_first_2");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringL_third_2");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringL_second_3");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringR_root");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringR_second_0");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringR_first_0");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringR_second_1");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringR_first_1");
        Api.meshfxMsg("dynImass", 1, 0, "joint_earringR_third_0");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringR_second_2");
        Api.meshfxMsg("dynImass", 1, 7, "joint_earringR_third_1");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringR_first_2");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringR_second_3");
        Api.meshfxMsg("dynImass", 1, 5, "joint_earringR_third_2");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringL_first_end");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringL_third_end");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringL_second_end");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringR_first_end");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringR_second_end");
        Api.meshfxMsg("dynImass", 1, 3, "joint_earringR_third_end");

        Api.meshfxMsg("dynGravity", 1, 0, "0 -900.0 0");

        Api.meshfxMsg("dynDamping", 1, 98);

        Api.meshfxMsg("dynSphere", 1, 0, "0 -36 7 75");










        for(var i = 0; i < self.meshes.length; i++) {
            self.meshes[i].animIdx = -1;
            self.meshes[i].endTime = 0;
        }
        self.faceActions = [self.play];

        // Api.showHint("Open mouth");

        Api.showRecordButton();
    };

    this.restart = function() {
        Api.meshfxReset();


        self.init();
    };

    this.faceActions = [];
    this.noFaceActions = [];

    this.videoRecordStartActions = [];
    this.videoRecordFinishActions = [];
    this.videoRecordDiscardActions = [this.restart];
}

configure(new Effect());