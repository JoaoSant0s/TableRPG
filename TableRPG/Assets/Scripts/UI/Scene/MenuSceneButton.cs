using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MenuSceneButton : SceneButton
    {
        public delegate void OnEnablePinnedSceneButton(string id);
        public static OnEnablePinnedSceneButton EnablePinnedSceneButton;

        [SerializeField]
        private GameObject showSettingsButton;

        #region public methods   

        public override void Init()
        {
            base.Init();
            EnableShowSettingsButton(false);
        }

        #endregion

        #region private methods


        private void EnableShowSettingsButton(bool value = true)
        {
            this.showSettingsButton.SetActive(value);
        }

        protected override void EnableSelection(string sceneId)
        {
            base.EnableSelection(sceneId);
            EnableShowSettingsButton(this.id.Equals(sceneId));
        }

        #endregion

        #region UI        

        public override void OnLoadScene()
        {
            base.OnLoadScene();
            EnableShowSettingsButton(true);
        }

        public void OnPinSceneButton()
        {
            if (EnablePinnedSceneButton != null) EnablePinnedSceneButton(this.id);
        }

        public void OnDeleteScene()
        {
            if (DeleteContent != null) DeleteContent(this.id);
        }

        public void OnShowSettings()
        {
            PopupManager.Instance.ShowScenePopup(this.id);
        }

        #endregion
    }
}