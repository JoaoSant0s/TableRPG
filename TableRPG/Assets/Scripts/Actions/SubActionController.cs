using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public enum SubActionType
    {
        TEST_1,
        TEST_2
    }

    public class SubActionController : MonoBehaviour
    {
        [SerializeField]
        private SubActionType type;

        [Header("Components")]

        [SerializeField]
        private Animator animator;

        [Header("Animation names")]

        [SerializeField]
        private string closeAnimation;

        private bool isHiding;


        #region  Getters And Setters
        public SubActionType Type
        {
            get { return this.type; }
        }

        public bool IsHiding{
            get{return this.isHiding;}
        }
        #endregion

        #region public methods

        public void HideAndDestroySubAction()
        {
            if (this.isHiding) return;
            this.isHiding = true;

            this.animator.Play(this.closeAnimation);

        }
        #endregion

        #region Animation
        public void OnDestroyAnimation()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}