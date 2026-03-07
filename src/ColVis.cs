#if BONEWORKS
#else
#define BONELAB
#endif

using Il2CppSLZ.Marrow;
using MelonLoader;
using UnityEngine;
using HarmonyLib;

namespace ColVis
{
    public static class BuildInfo
    {
        public const string Name = "ColVis"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "EvroDev"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.1.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class ColVis : MelonMod
    {
        private static Shader _shader;

        public static Shader Shader
        {
            get
            {
                if(_shader == null)
                {
                    foreach (Renderer renderer in GameObject.FindObjectsOfType<MeshRenderer>())
                    {
                        if (renderer.material.shader.name == "SLZ/LitMAS/LitMAS Standard")
                        {
                            _shader = renderer.material.shader;
                            break;
                        }
                    }
                }
                return _shader;
            }
        }
        public static Material cachedMat;
        public static Texture2D dummyMAS;

        public override void OnInitializeMelon()
        {
            Bone_Menu_Creator.OnInitialize();

            dummyMAS = new Texture2D(2, 2);

            dummyMAS.SetPixel(0, 0, Color.green);
            dummyMAS.SetPixel(0, 1, Color.green);
            dummyMAS.SetPixel(1, 0, Color.green);
            dummyMAS.SetPixel(1, 1, Color.green);

            dummyMAS.Apply();

            dummyMAS.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public static IVisBase CreateColVis(Collider c)
        {
            if(c is BoxCollider b)
            {
                return new BoxColVis(b);
            }
            else if(c is CapsuleCollider cap)
            {
                return new CapsuleColVis(cap);
            }
            else if(c is MeshCollider m)
            {
                return new MeshColVis(m);
            }
            else if(c is SphereCollider s)
            {
                return new SphereColVis(s);
            }
            return null;
        }
    }

    [HarmonyPatch(typeof(PhysicsRig), "OnAwake")]
    public static class PhysicsRigVisualizerPatch
    {
        public static void Postfix(PhysicsRig __instance)
        {
            __instance.gameObject.AddComponent<PhysVis>();
        }
    }

#if false
    [HarmonyLib.HarmonyPatch(typeof(GameWorldSkeletonRig), "OnAwake")]
    public static class Hh
    {
        public static void Postfix(GameWorldSkeletonRig __instance)
        {
            foreach (Renderer renderer in GameObject.FindObjectsOfType<MeshRenderer>())
            {
                if (renderer.material.shader.name == "SLZ/LitMAS/LitMAS Opaque")
                {
                    ColVis.litmas = renderer.material.shader;
                }
            }
            __instance.m_pelvis.gameObject.AddComponent<LineVis>().other = __instance.m_hipRt;
            __instance.m_pelvis.gameObject.AddComponent<LineVis>().other = __instance.m_hipLf;

            __instance.m_hipRt.gameObject.AddComponent<LineVis>().other = __instance.m_kneeRt;
            __instance.m_hipLf.gameObject.AddComponent<LineVis>().other = __instance.m_kneeLf;

            __instance.m_kneeRt.gameObject.AddComponent<LineVis>().other = __instance.m_footRt;
            __instance.m_kneeLf.gameObject.AddComponent<LineVis>().other = __instance.m_footLf;


            __instance.m_pelvis.gameObject.AddComponent<LineVis>().other = __instance.m_spine;
            __instance.m_spine.gameObject.AddComponent<LineVis>().other = __instance.m_chest;

            __instance.m_chest.gameObject.AddComponent<LineVis>().other = __instance.m_shoulderRt;
            __instance.m_chest.gameObject.AddComponent<LineVis>().other = __instance.m_shoulderLf;

            __instance.m_shoulderRt.gameObject.AddComponent<LineVis>().other = __instance.m_elbowRt;
            __instance.m_shoulderLf.gameObject.AddComponent<LineVis>().other = __instance.m_elbowLf;

            __instance.m_elbowRt.gameObject.AddComponent<LineVis>().other = __instance.m_handRt;
            __instance.m_elbowLf.gameObject.AddComponent<LineVis>().other = __instance.m_handLf;

        }
    }
#endif

}
