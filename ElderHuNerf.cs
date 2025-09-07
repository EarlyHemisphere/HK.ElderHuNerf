using Modding;
using SFCore.Utils;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using HutongGames.PlayMaker;
using System.Linq;

namespace ElderHuNerf {
    public class ElderHuNerf : Mod, ITogglableMod {
        public static ElderHuNerf instance;

        public ElderHuNerf() : base("Elder Hu Nerf") => instance = this;

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public bool ToggleButtonInsideMenu => true;

        public override void Initialize() {
            Log("Initializing");

            On.PlayMakerFSM.OnEnable += OnFsmEnable;
            CheckForActiveFight();

            Log("Initialized");
        }

        private void OnFsmEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
            orig(self);

            if (self.gameObject.name == "Ghost Warrior Hu" && self.FsmName == "Attacking") {
                SetActivations(self);
            }
        }

        public void Unload() {
            On.PlayMakerFSM.OnEnable -= OnFsmEnable;
            CheckForActiveFight(true);
        }

        private void CheckForActiveFight(bool activate = false) {
            GameObject elderHu = GameObject.Find("Ghost Warrior Hu"); 
            if (elderHu != null) {
                SetActivations(elderHu.LocateMyFSM("Attacking"), activate);
            }
        }
        
        private void SetActivations(PlayMakerFSM fsm, bool activate = false) {
            fsm.GetAction<ActivateGameObject>("1 6", 5).activate = activate;
            fsm.GetAction<ActivateGameObject>("2 1", 1).activate = activate;
            fsm.GetAction<ActivateGameObject>("2 1", 8).activate = activate;
            fsm.GetAction<ActivateGameObject>("2 1", 15).activate = activate;
            fsm.GetAction<ActivateGameObject>("2 2", 7).activate = activate;
            fsm.GetAction<ActivateGameObject>("2 3", 6).activate = activate; // Ring 11
            fsm.GetAction<ActivateGameObject>("2 4", 1).activate = activate;
        }
    }
}
