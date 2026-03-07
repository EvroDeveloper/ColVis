using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    public class TranVis : VisBase<Transform, GameObject>
    {
        public TranVis(Transform target) : base(target) { }

        public const float Size = 0.05f;

        public override void CreateVis()
        {
            Vis = PrimitiveCreator.CreatePrimitive(PrimitiveType.Cube);

            Vis.transform.position = Tar.position;
            Vis.transform.rotation = Tar.rotation;
            Vis.transform.localScale = Vector3.one * Size;
        }

        public override void UpdateVis()
        {
            Vis.transform.position = Tar.position;
            Vis.transform.rotation = Tar.rotation;
        }

        protected override void OnDestroy()
        {
            GameObject.Destroy(Vis);
        }
    }
}