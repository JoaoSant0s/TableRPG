using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class WorldConfigData
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string worldName;        

        private bool isValid;

        #region Getters and Setters

        public string Id
        {
            get { return this.id; }
        }

        public string WorldName
        {
            get { return this.worldName; }
        }

        public string DirectoryName
        {
            get { return $"World_{this.id}"; }
        }

        public bool IsValid
        {
            get { return this.isValid; }
        }

        #endregion

        public WorldConfigData(string _worldName)
        {
            if (CheckValidScene(_worldName))
            {
                this.isValid = false;
                return;
            }

            this.isValid = true;
            this.worldName = _worldName;

            var hashDifference = Random.Range(int.MinValue, int.MaxValue);
            this.id = $"{GetHashCode()}_{hashDifference}";            
        }

        private bool CheckValidScene(string _worldName)
        {
            Debugs.Log(_worldName);

            return string.IsNullOrEmpty(_worldName);
        }
    }
}