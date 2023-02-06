using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace Freeheart.UI
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorWidget : WidgetNode
    {
        [ShowInInspector, ReadOnly, SerializeField]
        [InfoBox("The Animator Controller should have a \"Show\" state and a \"Hide\" state.")]
        private Animator animator;
        private static class AnimStates
        {
            public static readonly int Show = Animator.StringToHash(nameof(Show));
            public static readonly int Hide = Animator.StringToHash(nameof(Hide));
        }
        void Reset() 
        {
            animator = GetComponent<Animator>();
        }
        void Awake()
        {
            if(!animator) animator = GetComponent<Animator>();
            animator.Play(AnimStates.Hide, layer: 0, normalizedTime: 1);
        }
        void Start()
        {
        
        }
        void Update()
        {
            isIdle = animator.GetNextAnimatorStateInfo(0).normalizedTime <= 0
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
        }
        public override void Show()
        {
            if(isVisible) return;
            animator.CrossFade(AnimStates.Show, 1, 0, 0);
            isIdle = false;
            isVisible = true;
        }
        public override void Hide()
        {
            if(!isVisible) return;
            animator.CrossFade(AnimStates.Hide, 1, 0, 0);
            isIdle = false;
            isVisible = false;
        }
    }
}
