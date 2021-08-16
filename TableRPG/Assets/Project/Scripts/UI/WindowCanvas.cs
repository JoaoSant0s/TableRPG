using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class WindowCanvas : MonoBehaviour
    {
        private Canvas canvas;
        private Vector2 offsetScreen = new Vector2(25, 25);

        private void Awake()
        {
            this.canvas = transform.GetComponentInParent<Canvas>();
        }

        protected Vector2 CorrectInpuLimits(Vector2 position)
        {
            Rect rect = this.canvas.pixelRect;

            var xPosition = Mathf.Max(offsetScreen.x, Mathf.Min(rect.width - offsetScreen.x, position.x));
            var yPosition = Mathf.Max(offsetScreen.y, Mathf.Min(rect.height - offsetScreen.y, position.y));

            return new Vector2(xPosition, yPosition);
        }
    }
}