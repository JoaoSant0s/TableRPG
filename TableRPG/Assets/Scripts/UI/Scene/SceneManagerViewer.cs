using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneManagerViewer : MonoBehaviour
    {
        public delegate void OnRefreshPinnedButtons(string id);
        public static OnRefreshPinnedButtons RefreshPinnedButtons;

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

        [Header("Controllers references")]
        [SerializeField]
        private SceneManagerController sceneManagerController;

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
            SceneManagerController.CreateSceneButton += CreateSceneButton;
            MenuSceneButton.EnablePinnedSceneButton += EnablePinnedSceneButton;
            PinnedSceneButton.RemovePinnecButton += RemovePinnedScene;
        }

        private void OnDestroy()
        {
            SceneManagerController.CreateSceneButton -= CreateSceneButton;
            MenuSceneButton.EnablePinnedSceneButton -= EnablePinnedSceneButton;
            PinnedSceneButton.RemovePinnecButton -= RemovePinnedScene;
        }

        #endregion        

        #region UI

        public void OnCreatScene()
        {
            SceneButton sceneButton = CreatScene();
            SceneController scene = this.sceneManagerController.Create();

            sceneButton.SceneControllerId(scene.Id);
        }

        #endregion

        #region private methods

        private SceneController FindSceneById(string id)
        {
            return this.sceneManagerController.FindSceneById(id);
        }

        private SceneController CurrentScene
        {
            get
            {
                return this.sceneManagerController.CurrentScene();
            }
        }

        private void EnablePinnedSceneButton(string id)
        {
            var scene = FindSceneById(id);
            scene.Pinned = !scene.Pinned;

            if (scene.Pinned)
            {
                CretePinnedSceneButton(id);
            }
            else
            {
                RemovePinnedSceneButton(id);
            }

            if(CurrentScene == scene) StartCoroutine(RefreshPinnedButtonsRoutine(id));

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

        private void CretePinnedSceneButton(string id)
        {
            var pinnedButton = Instantiate(this.scenePinnedButtonPrefab, this.scenePinnedButtonArea, false);
            pinnedButton.SceneControllerId(id);
            PinnedScenes.Add(pinnedButton);
        }
        private SceneButton CreatScene()
        {
            var sceneButton = Instantiate(this.sceneButtonPrefab, this.sceneButtonArea, false);
            sceneButton.Init();
            return sceneButton;
        }

        private void CreateSceneButton(SceneController scene)
        {
            SceneButton sceneButton = CreatScene();

            var id = scene.Id;

            sceneButton.SceneControllerId(id);

            if (scene.Pinned)
            {
                CretePinnedSceneButton(id);
            }
        }

        #endregion
    }
}
