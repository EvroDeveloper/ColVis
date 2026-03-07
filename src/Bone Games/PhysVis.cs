using ColVis.Utilities;
#if BONELAB
using Il2CppSLZ.Marrow;
#elif BONEWORKS
using StressLevelZero.Rigs;
using StressLevelZero.VRMK;
#endif
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class PhysVis : MonoBehaviour
    {
        public PhysVis(IntPtr ptr) : base(ptr) { }

        public static List<PhysVis> activeInstances = new();

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
        public BoxColVis chest;
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

            locosphere = new SphereColVis(physicsRig._football);
            locosphere.SetActive(Bone_Menu_Creator.locosphereEntry.Value);
            fender = new SphereColVis(physicsRig._kneeFender);
            fender.SetActive(Bone_Menu_Creator.fenderEntry.Value);
            legs = new CapsuleColVis(physicsRig.kneePelvisCol);
            legs.SetActive(Bone_Menu_Creator.kneeEntry.Value);

#if BONELAB
            pelvis = new MeshColVis(physicsRig.torso.cPelvis);
            pelvis.SetActive(Bone_Menu_Creator.pelvisEntry.Value);
            spine = new MeshColVis(physicsRig.torso.cSpineLow);
            spine.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            spine2 = new MeshColVis(physicsRig.torso.cSpine);
            spine2.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            chest = new MeshColVis(physicsRig.torso.cChest);
            chest.SetActive(Bone_Menu_Creator.torsoEntry.Value);
#elif BONEWORKS
#endif

#if BONELAB
            shoulderLf = new MeshColVis(physicsRig.leftHand.physHand.cUpper);
            shoulderLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            shoulderRt = new MeshColVis(physicsRig.rightHand.physHand.cUpper);
            shoulderRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowLf = new MeshColVis(physicsRig.leftHand.physHand.cLower);
            elbowLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowRt = new MeshColVis(physicsRig.rightHand.physHand.cLower);
            elbowRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
#elif BONEWORKS
            elbowLf = new MeshColVis(physicsRig.leftHand.physHand.forearmCollider);
            elbowLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowRt = new MeshColVis(physicsRig.rightHand.physHand.forearmCollider);
            elbowRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
#endif

            handLf = new BoxColVis(physicsRig.leftHand.physHand.handCol);
            handLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            handRt = new BoxColVis(physicsRig.rightHand.physHand.handCol);
            handRt.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersLf = new BoxColVis(physicsRig.leftHand.physHand.fingersCol);
            fingersLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersRt = new BoxColVis(physicsRig.rightHand.physHand.fingersCol);
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
                instance.spine.SetActive(active);
                instance.spine2.SetActive(active);
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
                instance.shoulderLf.SetActive(active);
                instance.shoulderRt.SetActive(active);
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