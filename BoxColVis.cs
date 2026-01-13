using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class BoxColVis : MonoBehaviour, IColVisBase<BoxCollider>
    {
        public BoxColVis(IntPtr ptr) : base(ptr) { }

        GameObject cube;

        PrimitiveType PrimType { get => PrimitiveType.Cube; }

        public BoxCollider Col { get; set; }
        public GameObject Vis { get; set; }

        PrimitiveType IColVisBase<BoxCollider>.PrimType => PrimitiveType.Cube;

        public Color VisColor => Color.white;

        public void Awake()
        {
            Col = GetComponent<BoxCollider>();
            CreateVis();
            UpdateVis();
        }

        public void CreateVis()
        {
            Vis = GameObject.CreatePrimitive(PrimType);
            Vis.GetComponent<Renderer>().material.shader = ColVis.litmas;
            Destroy(Vis.GetComponent<Collider>());

            Vis.transform.parent = transform;

            Vis.transform.localPosition = Vector3.zero;
            Vis.transform.localRotation = Quaternion.identity;
            Vis.transform.localScale = Vector3.one;
        }


        public void Update()
        {
            UpdateVis();
        }

        public void UpdateVis()
        {
            Vis.transform.localScale = Col.extents * 2;
            Vis.transform.localPosition = Col.center;

            if (Col.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);
        }

        public void SetActive(bool active)
        {
            Vis.SetActive(active);
            enabled = active;
        }
    }
}