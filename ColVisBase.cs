using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    public interface IColVisBase<T> where T : Collider
    {

        T Col { get; set; }
        GameObject Vis { get; set; }

        PrimitiveType PrimType { get; }
        Color VisColor { get; }

        void Awake();

        void CreateVis();

        void Update();

        void UpdateVis();

        void SetActive(bool active);
    }
}