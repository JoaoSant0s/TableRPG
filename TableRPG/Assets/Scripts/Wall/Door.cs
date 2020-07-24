using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class Door : Wall
    {
        [Space]

        [Header("Door settings")]

        [Space]

        [SerializeField]
        private DoorButton doorButton;


        protected override void UpdateElementsPosition(float scale)
        {
            base.UpdateElementsPosition(scale);

            this.doorButton.UpdateLocalPosition(scale);
        }

        protected override void EnableInteractions(bool enable)
        {
            base.EnableInteractions(enable);
            this.doorButton.gameObject.SetActive(!enable);
        }
    }
}