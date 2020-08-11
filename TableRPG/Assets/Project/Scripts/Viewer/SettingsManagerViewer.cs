using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TableRPG
{
    public class SettingsManagerViewer : MonoBehaviour
    {
        public delegate void OnLogoutPlayer(UnityAction callback);
        public static OnLogoutPlayer LogoutPlayer;

        private const string sceneMenu = "Menu";
        private const string sceneLogin = "Login";

        #region UI
        public void OnReturnMenu()
        {
            CanvasLoadingController.Instance.EnableScene();
            SceneManager.LoadSceneAsync(sceneMenu, LoadSceneMode.Single);
        }

        public void OnQuitGame()
        {
            Application.Quit();
        }

        public void OnLogout()
        {
            CanvasLoadingController.Instance.EnableScene();
            if (LogoutPlayer != null) LogoutPlayer(LoadLoginScene);
        }
        #endregion

        #region private methods

        private void LoadLoginScene()
        {
            SceneManager.LoadSceneAsync(sceneLogin, LoadSceneMode.Single);
        }

        #endregion
    }
}