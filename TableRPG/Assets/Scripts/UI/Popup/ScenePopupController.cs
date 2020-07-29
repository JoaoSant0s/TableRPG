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

        [Header("General Buttons")]

        [SerializeField]
        private Button createScene;

        [SerializeField]
        private Button saveScene;

        private byte[] backgroundTextureBytes;

        private SceneController localSceneController;

        #region UI

        public void OnCreateSceneButton()
        {
            CanvasLoadingController.Instance.EnableScene();
            StartCoroutine(CreateSceneRoutine());
        }

        public void OnSaveSceneButton()
        {
            if (this.localSceneController == null)
            {
                Debugs.Log("No scene Controller selected");
                return;
            }

            var values = CreateSceneValues();
            var info = new SceneInfo(values);

            this.localSceneController.SetSceneInfo(info);

            StartCoroutine(CloseButtonRoutine());
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

        #region public methods
        public void Init(string sceneId)
        {
            this.localSceneController = SceneManagerController.Instance.FindSceneById(sceneId);

            this.createScene.gameObject.SetActive(false);
            this.saveScene.gameObject.SetActive(true);

            SetInputContent();
        }
        #endregion

        #region private methods    

        private IEnumerator CloseButtonRoutine()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            OnCloseButton();
        }

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

        private void SetInputContent()
        {
            var scene = this.localSceneController;

            this.sceneName.text = scene.SceneName;
            this.backgroundPixelPerUnit.text = scene.BackgroundData.PixelsPerUnits.ToString();
            this.backgroundTextureBytes = scene.BackgroundData.BackgroundSpriteBytes;

            this.gridType.value = scene.GridData.GridType;
            this.gridOffsetX.text = scene.GridData.GridOffset.x.ToString();
            this.gridOffsetY.text = scene.GridData.GridOffset.y.ToString();
            this.gridSize.text = scene.GridData.GridSize.ToString();
            this.gridExtent.text = scene.GridData.GridDrawExtent.ToString();

            var sprite = LoadTexture.LoadSpriteByBytes(this.backgroundTextureBytes);

            this.backgroundImage.sprite = sprite;
            this.backgroundSpriteWidth.text = string.Format("Width: {0}px", sprite.texture.width);
            this.backgroundSpriteHeight.text = string.Format("Height: {0}px", sprite.texture.height);

            RefreshComponentAlignment();
        }

        private void RefreshComponentAlignment()
        {
            this.sceneName.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
            this.backgroundPixelPerUnit.textComponent.alignment = TextAlignmentOptions.MidlineLeft;

            this.gridOffsetX.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
            this.gridOffsetY.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
            this.gridSize.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
            this.gridExtent.textComponent.alignment = TextAlignmentOptions.MidlineLeft;
        }
        #endregion
    }
}
