using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class AnimationHandler : MonoBehaviour
    {
        private static readonly int _isMoving = Animator.StringToHash("IsMove");

        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component is not found on " + gameObject.name);
            }
        }

        public void Move(Vector2 obj)
        {
            _animator.SetBool(_isMoving, obj.magnitude > 0.5f);
        }
    }
}
