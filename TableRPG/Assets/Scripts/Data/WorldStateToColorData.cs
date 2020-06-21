using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [CreateAssetMenu(fileName = "WorldStateToColorData", menuName = "TableRPG/WorldStateToColorData", order = 0)]
    public class WorldStateToColorData : ScriptableObject
    {
        [SerializeField]
        private List<WorldStateColorObject> worldStatesColors;

        public Color GetColorByWorldState(WorldState state)
        {
            WorldStateColorObject obj = worldStatesColors.Find(local => local.state == state);

            if (obj != null)
            {
                return obj.color;
            }
            else
            {
                return Color.black;
            }
        }
    }

    [System.Serializable]
    public class WorldStateColorObject
    {
        public WorldState state;
        public Color color;
    }
}