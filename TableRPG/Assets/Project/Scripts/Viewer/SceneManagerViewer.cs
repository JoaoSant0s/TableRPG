using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneManagerViewer : MonoBehaviour
    {
        public delegate void OnUpdateSceneButtons(string id);
        public static OnUpdateSceneButtons RefreshPinnedButtons;
        public static OnUpdateSceneButtons CreateSceneButton;

        [Header("Scene elements")]
        [SerializeField]
        private MenuSceneButton sceneButtonPrefab;

        [SerializeField]
        private PinnedSceneButton scenePinnedButtonPrefab;

        [Header("Objects references")]
        [SerializeField]
        private RectTransform sceneButtonArea;
        [SerializeField]
        private RectTransform scenePinnedButtonArea;

        private List<PinnedSceneButton> pinnedScenes;

        public List<PinnedSceneButton> PinnedScenes
        {
            get
            {
                if (this.pinnedScenes == null)
                {
                    this.pinnedScenes = new List<PinnedSceneButton>();
                }
                return this.pinnedScenes;
            }
        }

        #region MonoBehaviour methods

        private void Awake()
        {
            SceneManagerController.CreateSceneButton += CreateScene;
            MenuSceneButton.EnablePinnedSceneButton += EnablePinnedSceneButton;
            PinnedSceneButton.RemovePinnecButton += RemovePinnedScene;
            ScenePopupController.CreateScene += CreateScene;
        }

        private void OnDestroy()
        {
            SceneManagerController.CreateSceneButton -= CreateScene;
            MenuSceneButton.EnablePinnedSceneButton -= EnablePinnedSceneButton;
            PinnedSceneButton.RemovePinnecButton -= RemovePinnedScene;
            ScenePopupController.CreateScene -= CreateScene;
        }

        #endregion        

        #region UI

        public void OnShowCreateScene()
        {
            PopupManager.Instance.ShowScenePopup();
        }

        #endregion

        #region private methods

        private SceneController FindSceneById(string id)
        {
            return SceneManagerController.FindSceneById(id);
        }

        private SceneManagerController SceneManagerController{
            get{ return SceneManagerController.Instance;}
        }

        private SceneController CurrentScene
        {
            get
            {
                return SceneManagerController.CurrentScene;                
            }
        }

        private void EnablePinnedSceneButton(string id)
        {
            var scene = FindSceneById(id);
            scene.Pinned = !scene.Pinned;

            if (scene.Pinned)
            {
                CretePinnedSceneButton(scene);
            }
            else
            {
                RemovePinnedSceneButton(id);
            }

            if (CurrentScene == scene) StartCoroutine(RefreshPinnedButtonsRoutine(id));

            scene.SaveAllData();
        }

        IEnumerator RefreshPinnedButtonsRoutine(string id)
        {
            yield return new WaitForEndOfFrame();
            if (RefreshPinnedButtons != null) RefreshPinnedButtons(id);
        }

        private void RemovePinnedScene(string id)
        {
            var scene = FindSceneById(id);
            scene.Pinned = false;
            RemovePinnedSceneButton(id);
            scene.SaveAllData();
        }

        private void RemovePinnedSceneButton(string id)
        {
            var button = PinnedScenes.Find(context => context.EqualsId(id));
            PinnedScenes.Remove(button);
            Destroy(button.gameObject);
        }

        private void CretePinnedSceneButton(SceneController scene)
        {
            var pinnedButton = Instantiate(this.scenePinnedButtonPrefab, this.scenePinnedButtonArea, false);

            pinnedButton.SetSceneController(scene);
            PinnedScenes.Add(pinnedButton);
        }
        private SceneButton CreatSceneButton()
        {
            var sceneButton = Instantiate(this.sceneButtonPrefab, this.sceneButtonArea, false);
            sceneButton.Init();
            return sceneButton;
        }

        private void CreateScene(SceneInfo info)
        {
            SceneController scene = SceneManagerController.Create(info);

            SceneButton sceneButton = CreatSceneButton();
            sceneButton.SetSceneController(scene);

            StartCoroutine(LoadSceneContentRoutine(scene));
        }

        IEnumerator LoadSceneContentRoutine(SceneController scene)
        {
            yield return new WaitForEndOfFrame();

            SceneManagerController.LoadSceneContent(scene);
            if (CreateSceneButton != null) CreateSceneButton(scene.Id);
            
            CanvasLoadingController.Instance.EnableScene(false);
        }

        private void CreateScene(SceneController scene)
        {
            var id = scene.Id;

            SceneButton sceneButton = CreatSceneButton();
            sceneButton.SetSceneController(scene);

            if (scene.Pinned)
            {
                CretePinnedSceneButton(scene);
            }
        }

        #endregion
    }
}
