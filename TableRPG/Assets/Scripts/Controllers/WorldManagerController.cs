using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class WorldManagerController : SingletonBehaviour<WorldManagerController>
    {
        private string worldId;

        private List<WorldConfigData> worldsConfig;

        private WorldConfigData currentWorldConfig;

        #region Getters and Setters

        public string WorldId
        {
            get { return this.worldId; }
        }

        public string DirectoryName{
            get{
                return this.currentWorldConfig.DirectoryName;
            }
        }        

        public List<WorldConfigData> WorldsConfig
        {
            get
            {
                if (this.worldsConfig == null)
                {
                    this.worldsConfig = new List<WorldConfigData>();
                }
                return this.worldsConfig;
            }
        }

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            WorldButtonController.LoadWorld += LoadWorld;
            base.Awake();
            DontDestroyOnLoad(gameObject);

            WorldsConfig.Clear();
        }

        private void OnDestroy()
        {
            WorldButtonController.LoadWorld -= LoadWorld;
        }

        #endregion

        #region private methods

        private void LoadWorld(string _worldId)
        {
            this.worldId = _worldId;
            this.currentWorldConfig = WorldsConfig.Find(context => context.Id == this.worldId); ;
        }

        #endregion

        #region public methods
        public void AddWorldConfigData(WorldConfigData data)
        {
            WorldsConfig.Add(data);
        }
        #endregion

    }
}
