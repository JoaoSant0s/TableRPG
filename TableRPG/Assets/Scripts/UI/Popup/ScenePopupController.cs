using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

namespace TableRPG
{
    public struct SceneValues
    {
        public string backgroundPixelsPerUnit;
        public string sceneName;
        public byte[] backgroundTextureBytes;

        public int gridType;
        public string gridOffsetX;
        public string gridOffsetY;
        public string gridSize;
        public string gridExtent;

    }
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

        [Header("Grid Info")]

        [SerializeField]
        private TMP_Dropdown gridType;

        [SerializeField]
        private TMP_InputField gridOffsetX;

        [SerializeField]
        private TMP_InputField gridOffsetY;

        [SerializeField]
        private TMP_InputField gridSize;

        [SerializeField]
        private TMP_InputField gridExtent;

        private byte[] backgroundTextureBytes;

        #region UI

        public void OnCreateSceneButton()
        {
            CanvasLoadingController.Instance.EnableScene();
            StartCoroutine(CreateSceneRoutine());
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

        #region private methods    

        private IEnumerator CreateSceneRoutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            
            var values = CreateSceneValues();

            if (!SceneInfo.CheckInvalidScene(values))
            {
                var info = new SceneInfo(values);

                if (CreateScene != null) CreateScene(info);
                OnCloseButton();
            }
            else
            {
                CanvasLoadingController.Instance.EnableScene(false);
            }
        }
        private SceneValues CreateSceneValues()
        {
            var value = new SceneValues();

            value.backgroundPixelsPerUnit = this.backgroundPixelPerUnit.text;
            value.sceneName = this.sceneName.text;
            value.backgroundTextureBytes = this.backgroundTextureBytes;

            value.gridType = this.gridType.value;
            value.gridOffsetX = this.gridOffsetX.text;
            value.gridOffsetY = this.gridOffsetY.text;
            value.gridSize = this.gridSize.text;
            value.gridExtent = this.gridExtent.text;

            return value;
        }
        #endregion
    }
}
