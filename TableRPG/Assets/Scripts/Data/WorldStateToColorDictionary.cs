using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [CreateAssetMenu(fileName = "WorldStateToColorDictionary", menuName = "TableRPG/WorldStateToColorDictionary", order = 0)]
    public class WorldStateToColorDictionary : ScriptableObject
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