using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class MeshColVis : MonoBehaviour, IColVisBase<MeshCollider>
    {
        public MeshColVis(IntPtr ptr) : base(ptr) { }

        GameObject sphere;

        public void Awake()
        {
            Col = GetComponent<MeshCollider>();
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
        public PrimitiveType PrimType => PrimitiveType.Sphere;

        public MeshCollider Col { get; set; }
        public GameObject Vis { get; set; }

        public Color VisColor => Color.white;

        public void UpdateVis()
        {
            if (Col.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);

            Vis.GetComponent<MeshFilter>().mesh = Col.sharedMesh;
        }

        public void Update()
        {
            UpdateVis();
        }

        public void SetActive(bool active)
        {
            Vis.SetActive(active);
            enabled = active;
        }
    }
}