using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

namespace TableRPG
{
    public class ScenePopupController : PopupController
    {
        public delegate void OnCreateScene(SceneInfo info);
        public static OnCreateScene CreateScene;

        [Header("General Info")]

        [SerializeField]
        private TMP_InputField sceneName;

        [Header("Visual Info")]

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private TMP_InputField backgroundPixelPerUnit;

        [SerializeField]
        private TMP_Text backgroundSpriteWidth;

        [SerializeField]
        private TMP_Text backgroundSpriteHeight;

        private byte[] backgroundTextureBytes;

        #region UI

        public void OnCreateSceneButton()
        {
            var info = new SceneInfo(this.sceneName.text, this.backgroundTextureBytes, this.backgroundPixelPerUnit.text);

            Debugs.Log("Check scene validation:", info.Valid);

            if (!info.Valid) return;

            if (CreateScene != null) CreateScene(info);
            OnCloseButton();
        }

        public void OnLoadBackgroundSprite()
        {
            string[] texturesPaths = LoadTexture.LoadTexturePersistence(new TextureExtensions[] { TextureExtensions.JPEG, TextureExtensions.JPG, TextureExtensions.PNG });

            for (int i = 0; i < texturesPaths.Length; i++)
            {
                var filePath = texturesPaths[i];

                this.backgroundTextureBytes = File.ReadAllBytes(filePath);                
                var sprite = LoadTexture.LoadSpriteByBytes(this.backgroundTextureBytes);

                this.backgroundImage.sprite = sprite;
                this.backgroundSpriteWidth.text = string.Format("Width: {0}px", sprite.texture.width);
                this.backgroundSpriteHeight.text = string.Format("Height: {0}px", sprite.texture.height);
            }
        }

        public void OnFocusEditArea(bool value)
        {
            StaticState.InputFieldFocus = value;
        }

        #endregion
    }
}
