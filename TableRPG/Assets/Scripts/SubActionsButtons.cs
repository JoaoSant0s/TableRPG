using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TableRPG
{
    [System.Serializable]
    public class SubActionsButtons
    {                
        public List<Image> selectImages;     

        #region public methods  

        public void SelectButton(int id, bool activing)
        {
            for (int i = 0; i < this.selectImages.Count; i++)
            {
                Image comparableImage = this.selectImages[i];

                comparableImage.enabled = (i == id && activing);
            }
        }

        public void DefaultStateButtons(){
            for (int i = 0; i < this.selectImages.Count; i++)
            {
                Image comparableImage = this.selectImages[i];

                comparableImage.enabled = false;
            }
        }

        #endregion
    }
}