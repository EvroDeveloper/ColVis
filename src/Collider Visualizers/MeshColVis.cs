using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    public class MeshColVis : ColVisBase<MeshCollider>
    {
        public MeshColVis(MeshCollider target) : base(target) { }

        public override void UpdateVis()
        {
            if (Tar.enabled) Vis.SetActive(true);
            else Vis.SetActive(false);

            Vis.GetComponent<MeshFilter>().sharedMesh = Tar.sharedMesh;
        }
    }
}