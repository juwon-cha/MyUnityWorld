using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int _isMoving = Animator.StringToHash("IsMove");
    private static readonly int _isDamage = Animator.StringToHash("IsDamage");

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

    public void Damage()
    {
        _animator.SetBool(_isDamage, true);
    }

    public void InvincibilityEnd()
    {
        _animator.SetBool(_isDamage, false);
    }
}
