using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace TableRPG
{

    public struct SignUpStructure
    {
        public string email;
        public string password;

        public string repeatPassword;

        public SignUpStructure(string _email, string _password, string _repeatPassword)
        {
            this.email = _email;
            this.password = _password;
            this.repeatPassword = _repeatPassword;
        }

        public bool IsValid()
        {
            var empty = string.IsNullOrEmpty(this.email) || string.IsNullOrEmpty(this.password) || string.IsNullOrEmpty(this.repeatPassword);

            return !empty && this.password.Equals(this.repeatPassword);
        }
    }

    public class SignUpPopupController : PopupController
    {
        public delegate void OnSignUpPlayerEvent(SignUpStructure login, UnityAction<object> onSuccess = null, UnityAction<object> onFail = null);
        public static OnSignUpPlayerEvent SignUpPlayerEvent;

        [Header("Inputs")]
        [SerializeField]
        private TMP_InputField email;

        [Header("Inputs")]
        [SerializeField]
        private TMP_InputField password;

        [Header("Inputs")]
        [SerializeField]
        private TMP_InputField repeatPassword;

        #region UI

        public void OnSignUp()
        {
            CanvasLoadingController.Instance.EnableScene();

            var signUp = new SignUpStructure(this.email.text, this.password.text, this.repeatPassword.text);

            if (SignUpPlayerEvent != null) SignUpPlayerEvent(signUp, SuccessSignUpCallback, FailSignUpCallback);
        }

        #endregion

        #region private methods

        private void SuccessSignUpCallback(object result)
        {
            CanvasLoadingController.Instance.EnableScene(false);
        }

        private void FailSignUpCallback(object result)
        {
            Debugs.Log(result);
            CanvasLoadingController.Instance.EnableScene(false);
        }

        #endregion
    }
}