using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace TableRPG
{
    public struct LoginStructure
    {
        public string email;
        public string password;

        public LoginStructure(string _email, string _password)
        {
            this.email = _email;
            this.password = _password;
        }        
    }
    public class LoginManagerViewer : MonoBehaviour
    {
        public delegate void OnSignInPlayer(LoginStructure login, UnityAction<object> onSuccess, UnityAction<object> onFail);
        public static OnSignInPlayer SignInPlayer;

        [Header("Inputs")]
        [SerializeField]
        private TMP_InputField email;

        [SerializeField]
        private TMP_InputField password;

        private const string sceneMenu = "Menu";

        #region UI

        public void SignIn()
        {
            //ToDo make login
            CanvasLoadingController.Instance.EnableScene();
            var login = new LoginStructure(this.email.text, this.password.text);

            if (SignInPlayer != null) SignInPlayer(login, LoadMenuScene, LoadMenuError);
        }

        public void SignUp()
        {
            Debug.Log("SingUp Popup");

            PopupManager.Instance.ShowSignUpPopup();
        }

        #endregion

        #region private methods

        private void LoadMenuScene(object result)
        {
            Debugs.Log(result);
            SceneManager.LoadSceneAsync(sceneMenu, LoadSceneMode.Single);
        }

        private void LoadMenuError(object result)
        {
            Debugs.Log(result);
        }

        #endregion

    }
}