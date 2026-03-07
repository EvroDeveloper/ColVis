#if BONELAB
using Il2CppSLZ.Marrow;
#elif BONEWORKS
using StressLevelZero.Rig;
#endif
using MelonLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class PhysVis : MonoBehaviour
    {
        public PhysVis(IntPtr ptr) : base(ptr) { }

        public static List<PhysVis> activeInstances = new List<PhysVis>();

        public SphereColVis locosphere;
        public SphereColVis fender;
        public CapsuleColVis legs;
#if BONELAB
        public MeshColVis pelvis;
        public MeshColVis spine;
        public MeshColVis spine2;
        public MeshColVis chest;
        public CapsuleColVis neck;
        public MeshColVis head;
#elif BONEWORKS
        public CapsuleColVis pelvis;
        public CapsuleColVis chest;
        public CapsuleColVis neck;
        public BoxColVis head;
#endif

#if BONELAB
        public MeshColVis shoulderLf;
        public MeshColVis elbowLf;
        public MeshColVis shoulderRt;
        public MeshColVis elbowRt;
#elif BONEWORKS
        public CapsuleColVis elbowLf;
        public CapsuleColVis elbowRt;
#endif
        public BoxColVis handLf;
        public BoxColVis fingersLf;
        public BoxColVis handRt;
        public BoxColVis fingersRt;

        // Use this for initialization
        void Awake()
        {
            PhysicsRig physicsRig = GetComponent<PhysicsRig>();

            if (physicsRig == null) return;

            activeInstances.Add(this);

#if BONELAB
            locosphere = new SphereColVis(physicsRig._football);
            fender = new SphereColVis(physicsRig._kneeFender);
            legs = new CapsuleColVis(physicsRig.kneePelvisCol);
#elif BONEWORKS
            locosphere = new SphereColVis(physicsRig.physBody._football);
            fender = new SphereColVis(physicsRig.physBody._kneeFender);
            legs = new CapsuleColVis(physicsRig.physBody.kneePelvisCol);
#endif
            locosphere.SetActive(Bone_Menu_Creator.locosphereEntry.Value);
            fender.SetActive(Bone_Menu_Creator.fenderEntry.Value);
            legs.SetActive(Bone_Menu_Creator.kneeEntry.Value);

#if BONELAB
            pelvis = new MeshColVis(physicsRig.torso.cPelvis);
            spine = new MeshColVis(physicsRig.torso.cSpineLow);
            spine.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            spine2 = new MeshColVis(physicsRig.torso.cSpine);
            spine2.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            chest = new MeshColVis(physicsRig.torso.cChest);
            neck = new CapsuleColVis(physicsRig.torso.neckCol);
            head = new MeshColVis(physicsRig.torso.cHead);
#elif BONEWORKS
            pelvis = new CapsuleColVis(physicsRig.physBody.pelvisCol);
            chest = new CapsuleColVis(physicsRig.physBody.chestCol);
            neck = new CapsuleColVis(physicsRig.physBody.neckCol);
            head = new BoxColVis(physicsRig.physBody.rbHead.GetComponent<BoxCollider>());
#endif
            pelvis.SetActive(Bone_Menu_Creator.pelvisEntry.Value);
            chest.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            neck.SetActive(Bone_Menu_Creator.headEntry.Value);
            head.SetActive(Bone_Menu_Creator.headEntry.Value);

#if BONELAB
            shoulderLf = new MeshColVis(physicsRig.leftHand.physHand.cUpper);
            shoulderLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            shoulderRt = new MeshColVis(physicsRig.rightHand.physHand.cUpper);
            shoulderRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowLf = new MeshColVis(physicsRig.leftHand.physHand.cLower);
            elbowRt = new MeshColVis(physicsRig.rightHand.physHand.cLower);
#elif BONEWORKS
            elbowLf = new CapsuleColVis(physicsRig.physBody.lfForearmCol);
            elbowRt = new CapsuleColVis(physicsRig.physBody.rtForearmCol);
#endif
            elbowLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowRt.SetActive(Bone_Menu_Creator.armsEntry.Value);

#if BONELAB
            handLf = new BoxColVis(physicsRig.leftHand.physHand.handCol);
            handRt = new BoxColVis(physicsRig.rightHand.physHand.handCol);
            fingersLf = new BoxColVis(physicsRig.leftHand.physHand.fingersCol);
            fingersRt = new BoxColVis(physicsRig.rightHand.physHand.fingersCol);
#elif BONEWORKS
            handLf = new BoxColVis((BoxCollider)physicsRig.leftHand.GetComponent<BoxCollider>());
            handRt = new BoxColVis((BoxCollider)physicsRig.rightHand.GetComponent<BoxCollider>());
            fingersLf = new BoxColVis(physicsRig.physBody.lfFingersCol);
            fingersRt = new BoxColVis(physicsRig.physBody.rtFingersCol);
#endif
            handLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            handRt.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersRt.SetActive(Bone_Menu_Creator.handsEntry.Value);
        }

        public void OnDestroy()
        {
            if(activeInstances.Contains(this))
                activeInstances.Remove(this);
        }

        public static void LocosphereSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.locosphere.SetActive(active);
            }
        }

        public static void FenderSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.fender.SetActive(active);
            }
        }

        public static void LegsSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.legs.SetActive(active);
            }
        }

        public static void PelvisSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.pelvis.SetActive(active);
            }
        }

        public static void TorsoSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
#if BONELAB
                instance.spine.SetActive(active);
                instance.spine2.SetActive(active);
#endif
                instance.chest.SetActive(active);
            }
        }

        public static void HeadSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.neck.SetActive(active);
                instance.head.SetActive(active);
            }
        }

        public static void ArmsSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
#if BONELAB
                instance.shoulderLf.SetActive(active);
                instance.shoulderRt.SetActive(active);
#endif
                instance.elbowLf.SetActive(active);
                instance.elbowRt.SetActive(active);
            }
        }

        public static void HandsSetActive(bool active)
        {
            foreach (var instance in activeInstances)
            {
                instance.handLf.SetActive(active);
                instance.handRt.SetActive(active);
                instance.fingersLf.SetActive(active);
                instance.fingersRt.SetActive(active);
            }
        }
    }
}