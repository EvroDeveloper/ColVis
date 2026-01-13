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
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class ColVis : MelonMod
    {
        public static Shader litmas;
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
    }

    [HarmonyPatch(typeof(PhysicsRig), "OnAwake")]
    public static class Hb
    {
        public static void Postfix(PhysicsRig __instance)
        {
            foreach (Renderer renderer in GameObject.FindObjectsOfType<MeshRenderer>())
            {
                if (renderer.material.shader.name == "SLZ/LitMAS/LitMAS Standard")
                {
                    ColVis.litmas = renderer.material.shader;
                }
            }

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
