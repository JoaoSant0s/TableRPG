using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TableRPG
{
    public class SettingsManagerViewer : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

        #region UI
        public void OnExitGame()
        {
            CanvasLoadingController.Instance.EnableScene();
            SceneManager.LoadSceneAsync(this.sceneName, LoadSceneMode.Single);
        }
        #endregion
    }
}