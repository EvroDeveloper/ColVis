using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class SphereColVis : MonoBehaviour, IColVisBase<SphereCollider>
    {
        public SphereColVis(IntPtr ptr) : base(ptr) { }

        public PrimitiveType PrimType => PrimitiveType.Sphere;

        public SphereCollider Col { get; set; }
        public GameObject Vis { get; set; }

        public Color VisColor => Color.white;

        public void Awake()
        {
            Col = GetComponent<SphereCollider>();
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
            Vis.transform.localPosition = Col.center;
            Vis.transform.localScale = Vector3.one * Col.radius * 2;
        }

        public void SetActive(bool active)
        {
            Vis.SetActive(active);
            enabled = active;
        }
    }
}