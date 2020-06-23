using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{    
    public class SubActionController : MonoBehaviour
    {
        [SerializeField]
        private SubActionViewer viewerType;        

        [Header("Components")]

        [SerializeField]
        private Animator animator;

        [Header("Animation names")]

        [SerializeField]
        private string closeAnimation;

        private bool isHiding;


        #region  Getters And Setters
        public System.Type Type
        {
            get { return this.viewerType.GetType(); }
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