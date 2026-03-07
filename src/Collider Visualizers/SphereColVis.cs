using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    public class SphereColVis : ColVisBase<SphereCollider>
    {
        public SphereColVis(SphereCollider collider) : base(collider) { }

        public override PrimitiveType PrimType => PrimitiveType.Sphere;

        public override void UpdateVis()
        {
            Vis.transform.localPosition = Tar.center;
            Vis.transform.localScale = Vector3.one * Tar.radius * 2;
        }
    }
}