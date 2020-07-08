using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MouseConfig
    {
        private static MouseTextures mouseTextures;

        public static void SetResizeMouse()
        {
            LoadTextures();
            Cursor.SetCursor(mouseTextures.resizeTexture, mouseTextures.hotSpot, mouseTextures.cursorMode);
        }

        public static void SetDragMouse()
        {
            LoadTextures();
            Cursor.SetCursor(mouseTextures.dragTexture, mouseTextures.hotSpot, mouseTextures.cursorMode);
        }

        public static void SetDefaultMouse()
        {
            LoadTextures();
            Cursor.SetCursor(mouseTextures.defaultTexture, Vector2.zero, mouseTextures.cursorMode);
        }

        private static void LoadTextures()
        {
            if (mouseTextures != null) return;

            mouseTextures = Resources.Load<MouseTextures>("MouseTextures");
        }
    }
}