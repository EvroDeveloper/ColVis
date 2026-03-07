using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    [RegisterTypeInIl2Cpp]
    public class BoxColVis : ColVisBase<BoxCollider>
    {
        public BoxColVis(BoxCollider ptr) : base(ptr) { }

        public override PrimitiveType PrimType => PrimitiveType.Cube;

        public override void UpdateVis()
        {
            Vis.transform.localScale = Tar.extents * 2;
            Vis.transform.localPosition = Tar.center;

            if (Tar.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);
        }
    }
}