using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class SceneData
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private bool pinned;

        [SerializeField]
        private WallData wallData;

        public SceneData(SceneController controller)
        {
            this.id = controller.Id;
            this.pinned = controller.Pinned;
            this.wallData = controller.WallData;
        }

        #region getters and setters
        public string Id
        {
            get { return this.id; }
        }
        public bool Pinned
        {
            get { return this.pinned; }
        }
        public WallData WallData
        {
            get { return this.wallData; }
        }
        #endregion
    }
}