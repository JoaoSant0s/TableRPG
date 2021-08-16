using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class BackgroundData
    {
        [SerializeField]
        private byte[] backgroundSpriteBytes;

        [SerializeField]
        private float pixelsPerUnits;

        [SerializeField]
        private Color backgroundColor;

        public BackgroundData() { }

        public BackgroundData(byte[] _backgroundSpriteBytes, float _pixelsPerUnits)
        {
            UpdateValues(_backgroundSpriteBytes, _pixelsPerUnits);
        }

        public BackgroundData(byte[] _backgroundSpriteBytes, float _pixelsPerUnits, Color color)
        {
            UpdateValues(_backgroundSpriteBytes, _pixelsPerUnits, color);
        }

        public byte[] BackgroundSpriteBytes
        {
            get { return this.backgroundSpriteBytes; }
        }

        public float PixelsPerUnits
        {
            get { return this.pixelsPerUnits; }
        }

        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }

        public void UpdateValues(byte[] _backgroundSpriteBytes, float _pixelsPerUnits)
        {
            this.backgroundSpriteBytes = _backgroundSpriteBytes;
            this.pixelsPerUnits = _pixelsPerUnits;
        }

        public void UpdateValues(byte[] _backgroundSpriteBytes, float _pixelsPerUnits, Color color)
        {
            this.backgroundSpriteBytes = _backgroundSpriteBytes;
            this.pixelsPerUnits = _pixelsPerUnits;
            this.backgroundColor = color;
        }

    }
}