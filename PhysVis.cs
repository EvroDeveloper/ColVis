using Il2CppSLZ.Marrow;
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

        public static List<PhysVis> activeInstances = new List<PhysVis>();

        public SphereColVis locosphere;
        public SphereColVis fender;
        public CapsuleColVis legs;
        public MeshColVis pelvis;
        public MeshColVis spine;
        public MeshColVis chest;

        public MeshColVis shoulderLf;
        public MeshColVis elbowLf;
        public BoxColVis handLf;
        public BoxColVis fingersLf;

        public MeshColVis shoulderRt;
        public MeshColVis elbowRt;
        public BoxColVis handRt;
        public BoxColVis fingersRt;

        // Use this for initialization
        void Awake()
        {
            PhysicsRig physicsRig = GetComponent<PhysicsRig>();

            if (physicsRig == null) return;

            activeInstances.Add(this);

            locosphere = physicsRig.feet.AddComponent<SphereColVis>();
            locosphere.SetActive(Bone_Menu_Creator.locosphereEntry.Value);
            fender = physicsRig.knee.AddComponent<SphereColVis>();
            fender.SetActive(Bone_Menu_Creator.fenderEntry.Value);
            legs = physicsRig.kneePelvisCol.gameObject.AddComponent<CapsuleColVis>();
            legs.SetActive(Bone_Menu_Creator.kneeEntry.Value);
            pelvis = physicsRig.m_pelvis.gameObject.AddComponent<MeshColVis>();
            pelvis.SetActive(Bone_Menu_Creator.pelvisEntry.Value);
            spine = physicsRig.m_spine.gameObject.AddComponent<MeshColVis>();
            spine.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            chest = physicsRig.m_chest.gameObject.AddComponent<MeshColVis>();
            chest.SetActive(Bone_Menu_Creator.torsoEntry.Value);
            shoulderLf = physicsRig.m_shoulderLf.gameObject.AddComponent<MeshColVis>();
            shoulderLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            shoulderRt = physicsRig.m_shoulderRt.gameObject.AddComponent<MeshColVis>();
            shoulderRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowLf = physicsRig.m_elbowLf.gameObject.AddComponent<MeshColVis>();
            elbowLf.SetActive(Bone_Menu_Creator.armsEntry.Value);
            elbowRt = physicsRig.m_elbowRt.gameObject.AddComponent<MeshColVis>();
            elbowRt.SetActive(Bone_Menu_Creator.armsEntry.Value);
            handLf = physicsRig.m_handLf.gameObject.AddComponent<BoxColVis>();
            handLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            handRt = physicsRig.m_handRt.gameObject.AddComponent<BoxColVis>();
            handRt.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersLf = physicsRig.m_handLf.transform.Find("l_fingers_col").gameObject.AddComponent<BoxColVis>();
            fingersLf.SetActive(Bone_Menu_Creator.handsEntry.Value);
            fingersRt = physicsRig.m_handRt.transform.Find("r_fingers_col").gameObject.AddComponent<BoxColVis>();
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
                instance.chest.SetActive(active);
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