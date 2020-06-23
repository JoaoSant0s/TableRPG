using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class WallData
    {
        [SerializeField]
        private List<WallConfig> wallsDefinitions;

        public WallData(List<Wall> walls)
        {

            for (int i = 0; i < walls.Count; i++)
            {
                Wall wall = walls[i];

                WallConfig config = new WallConfig();

                config.position = wall.Position;
                config.rotation = wall.Quaternion;
                config.scale = wall.GetScale;

                WallDefinitions.Add(config);
            }
        }

        public List<WallConfig> WallDefinitions
        {
            get
            {
                if (this.wallsDefinitions == null)
                {
                    this.wallsDefinitions = new List<WallConfig>();
                }
                return this.wallsDefinitions;
            }
        }

    }

    [System.Serializable]
    public class WallConfig
    {
        public Vector3 position;
        public Quaternion rotation;
        public float scale;
    }
}