using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace TableRPG
{
    public class ScenePopupController : PopupController
    {
        public delegate void OnCreateScene(SceneInfo info);
        public static OnCreateScene CreateScene;

        [Header("Input infos")]

        [SerializeField]    
        private TMP_InputField input;

        #region UI

        public void OnCreateSceneButton()
        {            
            var info = new SceneInfo(this.input.text);
            if(!info.Valid) return;
            
            if (CreateScene != null) CreateScene(info);
            OnCloseButton();
        }

        public void OnFocusEditArea(bool value)
        {
            StaticState.InputFieldFocus = value;
        }

        #endregion
    }
}
