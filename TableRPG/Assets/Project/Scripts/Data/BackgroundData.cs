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

        public BackgroundData() { }

        public BackgroundData(byte[] _backgroundSpriteBytes, float _pixelsPerUnits)
        {
            UpdateValues(_backgroundSpriteBytes, _pixelsPerUnits);
        }

        public byte[] BackgroundSpriteBytes
        {
            get { return this.backgroundSpriteBytes; }
        }

        public float PixelsPerUnits
        {
            get { return this.pixelsPerUnits; }
        }

        public void UpdateValues(byte[] _backgroundSpriteBytes, float _pixelsPerUnits)
        {
            this.backgroundSpriteBytes = _backgroundSpriteBytes;
            this.pixelsPerUnits = _pixelsPerUnits;
        }

    }
}