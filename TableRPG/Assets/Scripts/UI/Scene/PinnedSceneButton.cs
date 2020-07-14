using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class PinnedSceneButton : SceneButton
    {
        public delegate void OnRemovePinnecButton(string id);
        public static OnRemovePinnecButton RemovePinnecButton;

        #region monoBehaviour methods        

        protected void Awake()
        {
            SceneButton.LoadContent += EnableSelection;
            SceneManagerViewer.RefreshPinnedButtons += EnableSelection;
            SceneManagerViewer.CreateSceneButton += EnableSelection;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SceneManagerViewer.RefreshPinnedButtons -= EnableSelection;
            SceneManagerViewer.CreateSceneButton -= EnableSelection;
        }

        #endregion

        #region UI        

        public void OnUndoPinnedScene()
        {
            UndoPinnedScene();
        }

        #endregion

        #region public methods        

        public void UndoPinnedScene()
        {
            if (RemovePinnecButton != null) RemovePinnecButton(this.id);
        }

        #endregion
    }
}