#if BONELAB
using Il2CppSLZ.Marrow;
#elif BONEWORKS
using StressLevelZero.Rigs;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace ColVis
{
    public enum RigVisType
    {
        None,
        ControllerRig,
#if BONELAB
        RemapHeptaRig,
        AnimationRig,
        InterpRig,
        VirtualHeptaRig,
#elif BONEWORKS
        RealtimeSkeletonRig,
        GameWorldSkeletonRig,
#endif
    }

    [RegisterTypeInIl2Cpp]
    public class RigVis : MonoBehaviour
    {
        public RigVis(IntPtr ptr) : base(ptr) { }

        Rig _rig;
        public List<TranVis> _tranVis = new List<TranVis>();
        void Awake()
        {
            _rig = GetComponent<Rig>();
            CreateTranVis(_rig.m_pelvis);
#if BONELAB
            CreateTranVis(_rig.m_spine);
#endif
            CreateTranVis(_rig.m_chest);
#if BONELAB
            CreateTranVis(_rig.m_clavLf);
            CreateTranVis(_rig.m_clavRt);
            CreateTranVis(_rig.m_shoulderLf);
            CreateTranVis(_rig.m_shoulderRt);
            CreateTranVis(_rig.m_elbowLf);
            CreateTranVis(_rig.m_elbowRt);
#endif
            CreateTranVis(_rig.m_handLf);
            CreateTranVis(_rig.m_handRt);
#if BONELAB
            CreateTranVis(_rig.m_hipLf);
            CreateTranVis(_rig.m_hipRt);
            CreateTranVis(_rig.m_kneeLf);
            CreateTranVis(_rig.m_kneeRt);
#endif
            CreateTranVis(_rig.m_footLf);
            CreateTranVis(_rig.m_footRt);
        }

        void OnDisable()
        {
            foreach (var tran in _tranVis)
            {
                tran.enabled = false;
            }
        }

        void OnEnable()
        {
            foreach (var tran in _tranVis)
            {
                tran.enabled = true;
            }
        }

        void OnDestroy()
        {
            foreach (var tran in _tranVis)
            {
                tran.DestroyVis();
            }
        }

        void CreateTranVis(Transform t)
        {
            if (t != null)
                _tranVis.Add(new TranVis(t));
        }
    }
}
