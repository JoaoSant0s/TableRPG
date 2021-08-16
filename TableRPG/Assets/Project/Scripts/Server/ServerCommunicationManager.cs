using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;
using TableRPG;

namespace TableRPG.Server
{
    public class ServerCommunicationManager : SingletonBehaviour<ServerCommunicationManager>
    {
        private ServerCommunicationSettings settings;

        #region Monobehaviour methods

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this.gameObject);
            SetConfig();
        }

        #endregion

        #region public methods

        public void SignUp(SignUpStructure login, UnityAction<object> onSuccess, UnityAction<object> onFail)
        {
            var form = new WWWForm();
            var jsonString = JsonConvert.SerializeObject(login);

            Debugs.Log(jsonString);

            form.AddField("login", jsonString);

            string urlRequest = this.settings.ServerUrl + FirebaseFunctions.LOGIN_SIGN_UP;

            StartCoroutine(UnityWebRequestRoutine(urlRequest, form, onSuccess, onFail));
        }
        public void SignIn(LoginStructure login, UnityAction<object> onSuccess, UnityAction<object> onFail)
        {
            var form = new WWWForm();
            var jsonString = JsonConvert.SerializeObject(login);

            Debugs.Log(jsonString);

            form.AddField("login", jsonString);

            string urlRequest = this.settings.ServerUrl + FirebaseFunctions.LOGIN_SIGN_IN;

            StartCoroutine(UnityWebRequestRoutine(urlRequest, form, onSuccess, onFail));
        }

        public void Logout(UnityAction<object> onSuccess, UnityAction<object> onFail)
        {
            var form = new WWWForm();

            //ToDo set logout parameters           

            string urlRequest = this.settings.ServerUrl + FirebaseFunctions.LOGIN_LOGOUT;

            StartCoroutine(UnityWebRequestRoutine(urlRequest, form, onSuccess, onFail));
        }

        #endregion

        #region private methods

        private void SetConfig()
        {
            this.settings = (ServerCommunicationSettings)Resources.Load("ServerCommunicationSettings");
        }

        private IEnumerator UnityWebRequestRoutine(string urlRequest, WWWForm form, UnityAction<object> onSuccess = null, UnityAction<object> onFail = null)
        {
            using (UnityWebRequest request = UnityWebRequest.Post(urlRequest, form))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debugs.Error(request.error);
                    if (onFail != null) onFail(request.error);
                }
                else
                {
                    if (request.responseCode != 200)
                    {
                        Debugs.Log(request.responseCode);
                        if (onFail != null) onFail(request.responseCode);
                    }
                    else
                    {
                        var text = request.downloadHandler.text;
                        Debugs.Log(text);
                        object data = ExtractObject(text);

                        if (onSuccess != null) onSuccess(data);
                    }
                }
            }
        }

        private object ExtractObject(string objectStringfy)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(objectStringfy);

            object value;
            if (dict != null && dict.TryGetValue("data", out value))
            {
                return value;
            }

            return null;
        }

        #endregion    
    }

    public struct FirebaseFunctions
    {
        public static string LOGIN_SIGN_IN
        {
            get { return LoginFunction.SIGN_IN; }
        }

        public static string LOGIN_SIGN_UP
        {
            get { return LoginFunction.SIGN_UP; }
        }

        public static string LOGIN_LOGOUT
        {
            get { return LoginFunction.LOGOUT; }
        }
        
        #region Login functions

        private struct LoginFunction
        {
            public static string SIGN_IN = "/signIn";
            public static string SIGN_UP = "/signUp";
            public static string LOGOUT = "/logout";
        }

        #endregion
    }


}