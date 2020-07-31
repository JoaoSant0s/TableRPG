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
        public Color backgroundColor;

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

        public delegate void OnUpdateBackground(BackgroundData data);
        public static OnUpdateBackground UpdateBackground;

        public delegate void OnUpdateGrid(GridData grid);
        public static OnUpdateGrid UpdateGrid;

        public delegate void OnUpdateSceneName(string sceneId, string sceneName);
        public static OnUpdateSceneName UpdateSceneName;

        public delegate void OnUpdateBackgrondColor(Color color);
        public static OnUpdateBackgrondColor UpdateBackgrondColor;

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

        [SerializeField]
        private Image backgroundImageColor;

        [SerializeField]
        private TMP_Text colorBackgroundLabel;

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

        private SceneController referencedSceneController;

        #region monobehaviour methods

        private void Start()
        {
            SetCharacterValidation();
        }

        #endregion

        #region UI

        public void OnCreateSceneButton()
        {
            CanvasLoadingController.Instance.EnableScene();
            StartCoroutine(CreateSceneRoutine());
        }

        public void OnSaveSceneButton()
        {
            if (this.referencedSceneController == null)
            {
                Debugs.Log("No scene Controller selected");
                return;
            }

            var values = CreateSceneValues();
            var info = new SceneInfo(values);

            this.referencedSceneController.SetSceneInfo(info);

            if (UpdateSceneName != null) UpdateSceneName(this.referencedSceneController.Id, this.sceneName.text);

            StartCoroutine(CloseButtonRoutine());
        }

        public void OnLoadBackgroundSprite()
        {
            string[] texturesPaths = LoadTexture.LoadTexturePersistence(new TextureExtensions[] { TextureExtensions.JPEG, TextureExtensions.JPG, TextureExtensions.PNG });

            if (texturesPaths.Length == 0) return;

            var filePath = texturesPaths[0];

            this.backgroundTextureBytes = File.ReadAllBytes(filePath);
            var sprite = LoadTexture.LoadSpriteByBytes(this.backgroundTextureBytes);

            this.backgroundImage.sprite = sprite;
            this.backgroundSpriteWidth.text = string.Format("Width: {0}px", sprite.texture.width);
            this.backgroundSpriteHeight.text = string.Format("Height: {0}px", sprite.texture.height);

            UpdateBackgroundValues();
        }        

        public void OnUpdateGridValues()
        {
            if (this.referencedSceneController == null) return;

            var gridOffset = new Vector2(float.Parse(this.gridOffsetX.text.Replace(".", ",")), float.Parse(this.gridOffsetY.text.Replace(".", ",")));

            this.referencedSceneController.GridData.UpdateValues(this.gridType.value, int.Parse(this.gridExtent.text), int.Parse(this.gridSize.text), gridOffset);

            UpdateGrid(this.referencedSceneController.GridData);
        }

        public void OnUpdateBackgroundVales()
        {
            UpdateBackgroundValues();
        }

        public void OnShowColorPicker()
        {
            var popup = PopupManager.Instance.ShowColorPickerPopup();            
            popup.SetUpdateColorAction(UpdateBackgroundColor, this.backgroundImageColor.color);
        }

        #endregion

        #region public methods
        public void Init(string sceneId)
        {
            this.referencedSceneController = SceneManagerController.Instance.FindSceneById(sceneId);

            this.createScene.gameObject.SetActive(false);
            this.saveScene.gameObject.SetActive(true);

            SetInputContent();
        }
        #endregion

        #region private methods  

        private void UpdateBackgroundValues()
        {
            if (this.referencedSceneController == null) return;

            this.referencedSceneController.BackgroundData.UpdateValues(this.backgroundTextureBytes, int.Parse(this.backgroundPixelPerUnit.text));

            if (UpdateBackground != null) UpdateBackground(this.referencedSceneController.BackgroundData);
        }

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
            value.backgroundColor = this.backgroundImageColor.color;

            value.gridType = this.gridType.value;
            value.gridOffsetX = this.gridOffsetX.text;
            value.gridOffsetY = this.gridOffsetY.text;
            value.gridSize = this.gridSize.text;
            value.gridExtent = this.gridExtent.text;

            return value;
        }

        private void SetInputContent()
        {
            var scene = this.referencedSceneController;

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

            var backgroundColor = scene.BackgroundData.BackgroundColor;

            this.backgroundImageColor.color = backgroundColor;
            this.colorBackgroundLabel.text = $"#{ColorUtility.ToHtmlStringRGB(backgroundColor)}";

            RefreshComponentAlignment();
        }

        private void RefreshComponentAlignment()
        {
            this.sceneName.AlignmentMidlineLeft();
            this.backgroundPixelPerUnit.AlignmentMidlineLeft();

            this.gridOffsetX.AlignmentMidlineLeft();
            this.gridOffsetY.AlignmentMidlineLeft();
            this.gridSize.AlignmentMidlineLeft();
            this.gridExtent.AlignmentMidlineLeft();
        }

        private void SetCharacterValidation()
        {
            this.backgroundPixelPerUnit.CharacterValidationDecimal();
            this.backgroundPixelPerUnit.CharacterValidationDecimal();

            this.gridOffsetX.CharacterValidationDecimal();
            this.gridOffsetY.CharacterValidationDecimal();
            this.gridSize.CharacterValidationDecimal();
            this.gridExtent.CharacterValidationDecimal();
        }

        private void UpdateBackgroundColor(Color color)
        {
            this.backgroundImageColor.color = color;

            this.colorBackgroundLabel.text = $"#{ColorUtility.ToHtmlStringRGB(color)}";

            if (this.referencedSceneController == null) return;

            if (UpdateBackgrondColor != null) UpdateBackgrondColor(color);
        }
        #endregion
    }
}
