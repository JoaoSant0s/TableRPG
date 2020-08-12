using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TableRPG.Server;

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
            SignUpPopupController.SignUpPlayerEvent += SignUpPlayer;
        }

        private void OnDestroy()
        {
            LoginManagerViewer.SignInPlayer -= SignInPlayer;
            SettingsManagerViewer.LogoutPlayer -= LogoutPlayer;
            SignUpPopupController.SignUpPlayerEvent -= SignUpPlayer;
        }

        #endregion

        private void SignInPlayer(LoginStructure login, UnityAction<object> onSuccess = null, UnityAction<object> onFail = null)
        {            
            if (onSuccess != null) onSuccess("onSuccess");
        }

        private void SignUpPlayer(SignUpStructure login, UnityAction<object> onSuccess = null, UnityAction<object> onFail = null)
        {
            Debug.Log("SignInPlayer");
            if (login.IsValid())
            {
                ServerCommunicationManager.Instance.SignUp(login, onSuccess, onFail);
            }
            else
            {
                if (onFail != null) onFail("Error: password don't mathc or empty fields");
            }

        }

        private void LogoutPlayer(UnityAction callback)
        {
            Debug.Log("LogoutPlayer");

            if (callback != null) callback();
        }
    }
}