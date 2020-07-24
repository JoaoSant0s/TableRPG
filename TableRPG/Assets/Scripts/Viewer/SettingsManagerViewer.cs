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
            SceneManager.LoadScene(this.sceneName, LoadSceneMode.Single);
        }
        #endregion
    }
}