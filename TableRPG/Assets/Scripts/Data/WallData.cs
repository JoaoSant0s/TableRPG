using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public enum WallType
    {
        WALL,
        DOOR
    }
    [System.Serializable]
    public class WallData
    {
        [SerializeField]
        private List<WallConfig> wallsDefinitions;

        public WallData() { }

        public WallData(List<Wall> walls)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                Wall wall = walls[i];

                WallConfig config = new WallConfig();

                config.position = wall.Position;
                config.rotation = wall.Quaternion;
                config.scale = wall.GetScale;
                config.type = GetWallType(wall);

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

        public WallType GetWallType(Wall wall)
        {
            var localType = wall.GetType();

            var typeW = typeof(Wall);
            var typeD = typeof(Door);

            if (localType == typeW)
            {
                return WallType.WALL;

            }
            else if (localType == typeD)
            {
                return WallType.DOOR;

            }

            return WallType.WALL;
        }

    }

    [System.Serializable]
    public class WallConfig
    {
        public WallType type;
        public Vector3 position;
        public Quaternion rotation;
        public float scale;
    }
}