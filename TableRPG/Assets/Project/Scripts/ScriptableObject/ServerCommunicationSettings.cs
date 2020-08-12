using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [CreateAssetMenu(fileName = "ServerCommunicationSettings", menuName = "TableRPG/ServerCommunicationSettings", order = 1)]
    public class ServerCommunicationSettings : ScriptableObject
    {        
        [SerializeField]
        private bool usingProductionServer = true;

        [SerializeField]
        private string productionServerURL = "https://us-central1-vttrpg-9740c.cloudfunctions.net";
        [SerializeField]
        private string localServerURL = "http://localhost:5001/vttrpg/us-central1";       

        public string ServerUrl
        {
            get
            {
                if (this.usingProductionServer)
                {
                    return this.productionServerURL;

                }
                else
                {
                    return this.localServerURL;

                }
            }
        }
    }
}