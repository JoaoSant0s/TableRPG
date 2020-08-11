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
        public delegate void OnSignInPlayer(LoginStructure login, UnityAction callback = null);
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

            if (SignInPlayer != null) SignInPlayer(login, LoadMenuScene);
        }

        private void LoadMenuScene()
        {
            SceneManager.LoadSceneAsync(sceneMenu, LoadSceneMode.Single);
        }

        public void SignUp()
        {
            Debug.Log("SingUp Popup");
            //ToDo show SignUp Popup
        }

        #endregion

    }
}