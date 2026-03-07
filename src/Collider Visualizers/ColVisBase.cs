using ColVis.Utilities;
using MelonLoader;
using System;
using System.Collections;
using UnityEngine;

namespace ColVis
{
    public interface IVisBase
    {
        VisUpdateFlags flags { get; }
        void Update();
        void DestroyVis();
    }

    public class VisBase<T, V> : IVisBase
    {
        public T Tar { get; protected set; }
        public V Vis { get; protected set; }
        public VisUpdateFlags flags { get; } = VisUpdateFlags.Update | VisUpdateFlags.LateUpdate | VisUpdateFlags.FixedUpdate;
        public virtual bool enabled { get; set; }

        public VisBase(T target)
        {
            VisUpdater.RegisterVisBase(this);
            Tar = target;
            CreateVis();
            UpdateVis();
        }

        public virtual void CreateVis() { }

        public virtual void UpdateVis() { }

        public void Update()
        {
            if(Tar == null)
            {
                DestroyVis();
                return;
            }
            
            if(enabled) UpdateVis();
        }

        public void DestroyVis()
        {
            VisUpdater.DeregisterVisBase(this);
            OnDestroy();
        }

        protected virtual void OnDestroy() { }
    }

    public class ColVisBase<T> : VisBase<T, GameObject> where T : Collider
    {
        public virtual PrimitiveType PrimType => PrimitiveType.Cube;

        public ColVisBase(T collider) : base(collider) { }

        public override void CreateVis()
        {
            Vis = PrimitiveCreator.CreatePrimitive(this);

            Vis.transform.parent = Tar.transform;

            Vis.transform.localPosition = Vector3.zero;
            Vis.transform.localRotation = Quaternion.identity;
            Vis.transform.localScale = Vector3.one;

            AfterCreateVis();
        }

        public virtual void AfterCreateVis() { }

        public void SetActive(bool active)
        {
            Vis.SetActive(active);
            enabled = active;
            OnSetActive(active);
        }

        public virtual void OnSetActive(bool active) { }

        protected override void OnDestroy()
        {
            UnityEngine.Object.Destroy(Vis);
        }
    }
}