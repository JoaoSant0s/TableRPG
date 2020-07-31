using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [CreateAssetMenu(fileName = "MouseTextures", menuName = "TableRPG/MouseTextures", order = 0)]
    public class MouseTextures : ScriptableObject
    {
        [Header("Configs")]

        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        [Header("Textures")]
        public Texture2D defaultTexture;
        public Texture2D resizeTexture;
        public Texture2D dragTexture;
    }
}