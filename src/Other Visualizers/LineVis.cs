//using Boneworks;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class LineVis : MonoBehaviour
    {
        public Transform other;

        public LineRenderer lineRenderer;

        public LineVis(IntPtr ptr) : base(ptr) { }

        public void Awake()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.SetWidth(0.1f, 0.1f);

            //var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.GetComponent<Renderer>().material.shader = ColVis.litmas;

            //lineRenderer.material = sphere.GetComponent<Renderer>().material;

            //DestroyImmediate(sphere);

        }

        public void Update()
        {   
            if (lineRenderer == null) return;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, other.position);
        }
    }
}