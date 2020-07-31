using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TableRPG
{
    public class SettingsManagerViewer : MonoBehaviour
    {
        private const string sceneMenu = "Menu";

        #region UI
        public void OnExitGame()
        {
            CanvasLoadingController.Instance.EnableScene();
            SceneManager.LoadSceneAsync(sceneMenu, LoadSceneMode.Single);
        }

        public void OnQuitGame()
        {
            Application.Quit();
        }
        #endregion
    }
}