using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TableRPG
{
    public class CanvasLoadingController : SingletonBehaviour<CanvasLoadingController>
    {
        [SerializeField]
        private GameObject panelGameObject;

        #region behaviour methods

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        #endregion

        #region private methods
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this.panelGameObject.SetActive(false);
        }
        #endregion

        #region public methods
        public void EnableScene()
        {
            this.panelGameObject.SetActive(true);
        }
        #endregion
    }
}