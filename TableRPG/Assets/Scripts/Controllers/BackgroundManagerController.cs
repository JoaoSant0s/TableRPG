using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class BackgroundManagerController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer backgroundLayer;

        private void Awake()
        {
            SceneManagerController.UpdateSceneContent += ChangeBackground;
        }

        private void OnDestroy()
        {
            SceneManagerController.UpdateSceneContent -= ChangeBackground;
        }

        #region private methods

        private void ChangeBackground(SceneController scene)
        {
            if (scene == null) return;
            var data = scene.BackgroundData;
            var sprite = LoadTexture.LoadSpriteByBytes(data.BackgroundSpriteBytes, data.PixelsPerUnits);
            this.backgroundLayer.sprite = sprite;
        }

        #endregion        
    }
}