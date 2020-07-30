using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TableRPG
{
    public enum DoorState
    {
        LOCKED,
        OPEN,
        CLOSE
    }

    public class DoorButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Animator animator;

        [Header("Animations Name")]

        [SerializeField]
        private string openAnimationName;

        [SerializeField]
        private string closeAnimationName;

        [SerializeField]
        private string lockedAnimationName;
        
        private DoorState state;

        public void OnPointerClick(PointerEventData eventData)
        {            
            switch (this.state)
            {
                case DoorState.LOCKED:
                    this.animator.Play(this.lockedAnimationName, -1, 0f);
                    break;

                case DoorState.OPEN:
                    this.animator.Play(this.openAnimationName);
                    this.state = DoorState.CLOSE;
                    break;

                case DoorState.CLOSE:
                    this.animator.Play(this.closeAnimationName);
                    this.state = DoorState.OPEN;
                    break;

                default:
                    break;
            }
        }

        public void UpdateLocalPosition(float scale)
        {
            var localPosition = transform.localPosition;
            localPosition.x = scale / 2;
            transform.localPosition = localPosition;
        }
    }
}