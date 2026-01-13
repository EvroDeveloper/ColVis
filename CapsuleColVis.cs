using MelonLoader;
using System;
using System.Collections;
using UnityEngine;


namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class CapsuleColVis : MonoBehaviour, IColVisBase<CapsuleCollider>
    {
        public CapsuleColVis(IntPtr ptr) : base(ptr) { }
        public CapsuleCollider Col { get; set; }
        public GameObject Vis { get; set; }

        public PrimitiveType PrimType => PrimitiveType.Capsule;

        public Color VisColor => Color.white;

        public void Awake()
        {
            Col = GetComponent<CapsuleCollider>();
            CreateVis();
            UpdateVis();
        }

        public void Update()
        {
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

        public void UpdateVis()
        {
            if (Col.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);

            Vis.transform.localPosition = Col.center;
            Vis.transform.localScale = new Vector3(Col.radius * 2, Col.height / 2, Col.radius * 2);

            if (Col.direction == 2)
                Vis.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }

        public void SetActive(bool active)
        {
            Vis.SetActive(active);
            enabled = active;
        }
    }
}