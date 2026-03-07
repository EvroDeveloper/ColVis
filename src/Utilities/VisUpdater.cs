using System;
using System.Collections.Generic;

namespace ColVis
{
    [Flags]
    public enum VisUpdateFlags
    {
        Update = 1,
        LateUpdate = 2,
        FixedUpdate = 4,
    }
    
    public static class VisUpdater
    {
        private static HashSet<IVisBase> _visBases = new();
        private static HashSet<IVisBase> _visBaseUpdate = new();
        private static HashSet<IVisBase> _visBaseFixedUpdate = new();
        private static HashSet<IVisBase> _visBaseLateUpdate = new();

        public static void RegisterVisBase(IVisBase visBase)
        {
            _visBases.Add(visBase);
            if(visBase.flags.HasFlag(VisUpdateFlags.Update)) _visBaseUpdate.Add(visBase);
            if(visBase.flags.HasFlag(VisUpdateFlags.FixedUpdate)) _visBaseFixedUpdate.Add(visBase);
            if(visBase.flags.HasFlag(VisUpdateFlags.LateUpdate)) _visBaseLateUpdate.Add(visBase);
        }

        public static void DeregisterVisBase(IVisBase visBase)
        {
            _visBases.Remove(visBase);
            _visBaseUpdate.Remove(visBase);
            _visBaseFixedUpdate.Remove(visBase);
            _visBaseLateUpdate.Remove(visBase);
        }

        public static void Update()
        {
            foreach(IVisBase visBase in _visBaseUpdate)
            {
                visBase.Update();
            }
        }

        public static void LateUpdate()
        {
            foreach(IVisBase visBase in _visBaseLateUpdate)
            {
                visBase.Update();
            }
        }

        public static void FixedUpdate()
        {
            foreach(IVisBase visBase in _visBaseFixedUpdate)
            {
                visBase.Update();
            }
        }
    }
}