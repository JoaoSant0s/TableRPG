using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace TableRPG
{
    public class PickerColorPopupController : PopupController
    {
        [SerializeField]
        private ColorPicker colorPicker;

        private UnityAction<Color> updateColorAction;

        #region monobehaviour methods

        private void Start()
        {
            this.colorPicker.onValueChanged.AddListener(ChangeColor);
        }

        private void OnDestroy()
        {
            this.colorPicker.onValueChanged.RemoveListener(ChangeColor);
        }

        #endregion

        #region public methods

        public void SetUpdateColorAction(UnityAction<Color> _updateColorAction, Color defaultColor)
        {
            this.updateColorAction = _updateColorAction;
            this.colorPicker.AssignColor(defaultColor);
        }

        #endregion

        #region private methods

        private void ChangeColor(Color color)
        {
            if (this.updateColorAction != null) this.updateColorAction(color);
        }

        #endregion
    }
}