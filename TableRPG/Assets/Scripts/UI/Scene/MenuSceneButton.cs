using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MenuSceneButton : SceneButton
    {
        public delegate void OnEnablePinnedSceneButton(string id);
        public static OnEnablePinnedSceneButton EnablePinnedSceneButton;
            
        #region UI        

        public void OnPinSceneButton()
        {
            if (EnablePinnedSceneButton != null) EnablePinnedSceneButton(this.id);
        }

        public void OnDeleteScene()
        {
            if (DeleteContent != null) DeleteContent(this.id);
        }

        #endregion
    }
}