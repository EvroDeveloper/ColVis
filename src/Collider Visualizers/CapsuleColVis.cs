using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;


namespace ColVis
{
    public class CapsuleColVis : ColVisBase<CapsuleCollider>
    {
        public CapsuleColVis(CapsuleCollider collider) : base(collider) { }

        public override PrimitiveType PrimType => PrimitiveType.Cylinder;

        public GameObject sphereTop;
        public GameObject sphereBottom;


        public override void AfterCreateVis()
        {
            sphereTop = PrimitiveCreator.CreatePrimitive(PrimitiveType.Sphere);
            sphereTop.transform.parent = Tar.transform;
            sphereTop.transform.localPosition = Vector3.zero;
            sphereTop.transform.localRotation = Quaternion.identity;
            sphereTop.transform.localScale = Vector3.one;

            sphereBottom = PrimitiveCreator.CreatePrimitive(PrimitiveType.Sphere);
            sphereBottom.transform.parent = Tar.transform;
            sphereBottom.transform.localPosition = Vector3.zero;
            sphereBottom.transform.localRotation = Quaternion.identity;
            sphereBottom.transform.localScale = Vector3.one;
        }

        public override void UpdateVis()
        {
            if (Tar.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);

            Vis.transform.localPosition = Tar.center;
            Vis.transform.localScale = new Vector3(Tar.radius * 2, Tar.height / 2 - Tar.radius, Tar.radius * 2);

            sphereTop.transform.localScale = Vector3.one * Tar.radius * 2;
            sphereBottom.transform.localScale = Vector3.one * Tar.radius * 2;

            if (Tar.direction == 2)
                Vis.transform.localRotation = Quaternion.Euler(90, 0, 0);

            sphereTop.transform.position = Vis.transform.TransformPoint(new Vector3(0f, 1f, 0f));
            sphereTop.transform.rotation = Vis.transform.rotation;
            sphereBottom.transform.position = Vis.transform.TransformPoint(new Vector3(0f, -1f, 0f));
            sphereBottom.transform.rotation = Vis.transform.rotation;
        }

        public override void OnSetActive(bool active)
        {
            sphereTop.SetActive(active);
            sphereBottom.SetActive(active);
        }
    }
}