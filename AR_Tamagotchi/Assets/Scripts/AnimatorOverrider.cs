using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{

    public class AnimatorOverrider : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimations(AnimatorOverrideController overrideController)
        {
            _animator.runtimeAnimatorController = overrideController;
        }

        public void SetTrigger()
        {
            _animator.SetTrigger("Action");
        }
    }
}