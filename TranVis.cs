using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class TranVis : MonoBehaviour
    {
        public TranVis(IntPtr ptr) : base(ptr) { }

        public static float Size = 0.1f;

        public GameObject Vis { get; private set; }

        // Use this for initialization
        void Awake()
        {
            Vis = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vis.GetComponent<Renderer>().material.shader = ColVis.litmas;
            Destroy(Vis.GetComponent<BoxCollider>());

            Vis.transform.position = transform.position;
            Vis.transform.rotation = transform.rotation;
            Vis.transform.localScale = Vector3.one / 20;
        }

        void OnDisable()
        {
            Vis.SetActive(false);
        }

        void OnEnable()
        {
            Vis.SetActive(true);
        }

        void OnDestroy()
        {
            Destroy(Vis);
        }

        // Update is called once per frame
        void Update()
        {
            Vis.transform.position = transform.position;
            Vis.transform.rotation = transform.rotation;
        }

        void FixedUpdate()
        {
            Vis.transform.position = transform.position;
            Vis.transform.rotation = transform.rotation;
        }
    }
}