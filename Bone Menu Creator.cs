using BoneLib;
using BoneLib.BoneMenu;
using MelonLoader;
using UnityEngine;

namespace ColVis
{
    public class Bone_Menu_Creator
    {
        private static MelonPreferences_Category category;

        public static MelonPreferences_Entry<bool> locosphereEntry, fenderEntry, kneeEntry, pelvisEntry, torsoEntry, armsEntry, handsEntry;
        public static RigVisType rigVisType = RigVisType.None;
        public static void OnInitialize()
        {
            category = MelonPreferences.CreateCategory("ColVis");

            locosphereEntry = category.CreateEntry<bool>("ShowLocosphere", true);
            fenderEntry = category.CreateEntry<bool>("ShowFender", true);
            kneeEntry = category.CreateEntry<bool>("ShowLegs", true);
            pelvisEntry = category.CreateEntry<bool>("ShowPelvis", false);
            torsoEntry = category.CreateEntry<bool>("ShowTorso", false);
            armsEntry = category.CreateEntry<bool>("ShowArms", false);
            handsEntry = category.CreateEntry<bool>("ShowHands", false);

            MelonPreferences.Save();

            CreateBoneMenu();
        }

        public static void CreateBoneMenu()
        {
            Page page = Page.Root.CreatePage("ColVis", Color.white);

            page.CreateBool("Show Locosphere", Color.white, locosphereEntry.Value, (b) => { locosphereEntry.Value = b; PhysVis.LocosphereSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Fender", Color.white, fenderEntry.Value, (b) => { fenderEntry.Value = b; PhysVis.FenderSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Legs", Color.white, kneeEntry.Value, (b) => { kneeEntry.Value = b; PhysVis.LegsSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Pelvis", Color.white, pelvisEntry.Value, (b) => { pelvisEntry.Value = b; PhysVis.PelvisSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Torso", Color.white, torsoEntry.Value, (b) => { torsoEntry.Value = b; PhysVis.TorsoSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Arms", Color.white, armsEntry.Value, (b) => { armsEntry.Value = b; PhysVis.ArmsSetActive(b); MelonPreferences.Save(); });
            page.CreateBool("Show Arms", Color.white, handsEntry.Value, (b) => { handsEntry.Value = b; PhysVis.HandsSetActive(b); MelonPreferences.Save(); });

            page.CreateEnum("Rig Visualization", Color.white, rigVisType, (e) => SetRigVis((RigVisType)e));

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
                    if (!Player.ControllerRig.TryGetComponent(out vis))
                        vis = Player.ControllerRig.gameObject.AddComponent<RigVis>();
                    vis.enabled = true;
                    break;
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
                default:
                    break;
            }
        }

        public static void TurnOffAllPlayerRigVis()
        {
            if (Player.ControllerRig.TryGetComponent(out RigVis control)) control.enabled = false;
            if (Player.RemapRig.TryGetComponent(out RigVis control1)) control1.enabled = false;
            if (Player.RigManager.animationRig.TryGetComponent(out RigVis control2)) control2.enabled = false;
            if (Player.RigManager.interpRig.TryGetComponent(out RigVis control3)) control3.enabled = false;
            if (Player.RigManager.virtualHeptaRig.TryGetComponent(out RigVis control4)) control4.enabled = false;
        }

        
    }
}
