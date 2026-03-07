#if BONELAB
using BoneLib;
using BoneLib.BoneMenu;
using Il2CppSLZ.Marrow;
#elif BONEWORKS
using ModThatIsNotMod;
using ModThatIsNotMod.BoneMenu;
using StressLevelZero.Rig;
#endif
using MelonLoader;
using UnityEngine;
using System;

namespace ColVis
{
    public class Bone_Menu_Creator
    {
        private static MelonPreferences_Category category;

        public static MelonPreferences_Entry<bool> locosphereEntry, fenderEntry, kneeEntry, pelvisEntry, torsoEntry, headEntry, armsEntry, handsEntry;

        public static RigVisType rigVisType = RigVisType.None;

#if BONELAB
        static Page page;
#elif BONEWORKS
        static MenuCategory page;
#endif

        public static void OnInitialize()
        {
            category = MelonPreferences.CreateCategory("ColVis");

            locosphereEntry = category.CreateEntry<bool>("ShowLocosphere", true);
            fenderEntry = category.CreateEntry<bool>("ShowFender", true);
            kneeEntry = category.CreateEntry<bool>("ShowLegs", true);
            pelvisEntry = category.CreateEntry<bool>("ShowPelvis", false);
            torsoEntry = category.CreateEntry<bool>("ShowTorso", false);
            headEntry = category.CreateEntry<bool>("ShowHead", false);
            armsEntry = category.CreateEntry<bool>("ShowArms", false);
            handsEntry = category.CreateEntry<bool>("ShowHands", false);

            MelonPreferences.Save();

            CreateBoneMenu();
        }

        public static void CreateBoneMenu()
        {
#if BONELAB
            page = Page.Root.CreatePage("ColVis", Color.white);
#elif BONEWORKS
            page = MenuManager.CreateCategory("ColVis", Color.white);
#endif

            CreateBool("Show Locosphere", Color.white, locosphereEntry.Value, (b) => { locosphereEntry.Value = b; PhysVis.LocosphereSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Fender", Color.white, fenderEntry.Value, (b) => { fenderEntry.Value = b; PhysVis.FenderSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Legs", Color.white, kneeEntry.Value, (b) => { kneeEntry.Value = b; PhysVis.LegsSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Pelvis", Color.white, pelvisEntry.Value, (b) => { pelvisEntry.Value = b; PhysVis.PelvisSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Torso", Color.white, torsoEntry.Value, (b) => { torsoEntry.Value = b; PhysVis.TorsoSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Head", Color.white, headEntry.Value, (b) => { headEntry.Value = b; PhysVis.HeadSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Arms", Color.white, armsEntry.Value, (b) => { armsEntry.Value = b; PhysVis.ArmsSetActive(b); MelonPreferences.Save(); });
            CreateBool("Show Hands", Color.white, handsEntry.Value, (b) => { handsEntry.Value = b; PhysVis.HandsSetActive(b); MelonPreferences.Save(); });

#if BONELAB
            page.CreateEnum("Rig Visualization", Color.white, rigVisType, (e) => SetRigVis((RigVisType)e));
#elif BONEWORKS
            page.CreateEnumElement("Rig Visualization", Color.white, rigVisType, (e) => SetRigVis((RigVisType)e));
#endif
        }

        private static void CreateBool(string name, Color color, bool defaultValue, Action<bool> onChanged)
        {
#if BONELAB
            page.CreateBool(name, color, defaultValue, onChanged);
#elif BONEWORKS
            page.CreateBoolElement(name, color, defaultValue, onChanged);
#endif
        }

        public static void SetRigVis(RigVisType type)
        {
            rigVisType = type;

            TurnOffAllPlayerRigVis();
            RigVis vis;

            switch (type)
            {
                case RigVisType.None:
                    break;
                case RigVisType.ControllerRig:
                    if (!RigManager.ControllerRig.TryGetComponent(out vis))
                        vis = RigManager.ControllerRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
#if BONELAB
                case RigVisType.RemapHeptaRig:
                    if (!Player.RemapRig.TryGetComponent(out vis))
                        vis = Player.RemapRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
                case RigVisType.AnimationRig:
                    if (!Player.RigManager.animationRig.TryGetComponent(out vis))
                        vis = Player.RigManager.animationRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
                case RigVisType.InterpRig:
                    if (!Player.RigManager.interpRig.TryGetComponent(out vis))
                        vis = Player.RigManager.interpRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
                case RigVisType.VirtualHeptaRig:
                    if(!Player.RigManager.virtualHeptaRig.TryGetComponent(out vis))
                        vis = Player.RigManager.virtualHeptaRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
#elif BONEWORKS
                case RigVisType.RealtimeSkeletonRig:
                    if(!RigManager.realtimeSkeletonRig.TryGetComponent(out vis))
                        vis = RigManager.realtimeSkeletonRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
                case RigVisType.GameWorldSkeletonRig:
                    if(!RigManager.gameWorldSkeletonRig.TryGetComponent(out vis))
                        vis = RigManager.gameWorldSkeletonRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
#endif
                default:
                    break;
            }
        }

        public static void TurnOffAllPlayerRigVis()
        {
            if (RigManager.ControllerRig.TryGetComponent(out RigVis control)) control.enabled = false;
#if BONELAB
            if (Player.RemapRig.TryGetComponent(out RigVis control1)) control1.enabled = false;
            if (RigManager.animationRig.TryGetComponent(out RigVis control2)) control2.enabled = false;
            if (RigManager.interpRig.TryGetComponent(out RigVis control3)) control3.enabled = false;
            if (RigManager.virtualHeptaRig.TryGetComponent(out RigVis control4)) control4.enabled = false;
#elif BONEWORKS
            if (RigManager.realtimeSkeletonRig.TryGetComponent(out RigVis control1)) control1.enabled = false;
            if (RigManager.gameWorldSkeletonRig.TryGetComponent(out RigVis control2)) control2.enabled = false;
#endif
        }

        public static RigManager RigManager
        {
            get 
            {
#if BONELAB
                return Player.RigManager; 
#elif BONEWORKS
                return RigManager.Cache.Get(Player.GetRigManager());
#endif
            }
        }
    }

#if BONEWORKS
    public static class StupidExtensionBecauseOldUnity
    {
        public static bool TryGetComponent<T>(this Component c, out T comp) where T : Component
        {
            comp = c.GetComponent<T>();
            return comp != null;
        }
    }
#endif
}
