using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace TableRPG
{
    public class LoginController : SingletonBehaviour<LoginController>
    {

        private const string sceneMenu = "Menu";


        #region MonoBehaviour methods

        protected override void Awake()
        {
            base.Awake();
            //DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoginManagerViewer.SignInPlayer += SignInPlayer;
            SettingsManagerViewer.LogoutPlayer += LogoutPlayer;
        }

        private void OnDestroy()
        {
            LoginManagerViewer.SignInPlayer -= SignInPlayer;
            SettingsManagerViewer.LogoutPlayer -= LogoutPlayer;
        }

        #endregion

        private void SignInPlayer(LoginStructure login, UnityAction callback = null)
        {
            Debug.Log("SignInPlayer");
            if (callback != null) callback();
        }

        private void LogoutPlayer(UnityAction callback)
        {
            Debug.Log("LogoutPlayer");

            if (callback != null) callback();
        }
    }
}