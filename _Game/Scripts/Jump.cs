using System;
using System.Collections;
using UnityEngine;

public class Jump
{
    private CharacterVfx _vfx;
    private Animator _animator;

    public Jump(Rigidbody rigidbody, Vector3 jumpForce, Animator animator, CharacterVfx vfx)
    {
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
        _animator = animator;
        _vfx = vfx;
    }

    public int VerticalCounter { get; private set; }
    public int HorizontalCounter { get; private set; }

    private Rigidbody _rigidbody;
    private Vector3 _jumpForce;

    public void Base()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(0, _jumpForce.y, 0), ForceMode.Impulse);

        VerticalCounter++;

        _animator.SetTrigger(AnimationKey.Jump);
        _vfx.Jump();
    }

    public void Horizontal(bool isRight, float multiplier)
    {
        float forceX = isRight ? _jumpForce.x : -_jumpForce.x;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(forceX, _jumpForce.y * multiplier, 0), ForceMode.Impulse);

        HorizontalCounter++;

        _animator.SetTrigger(AnimationKey.Jump);
        _vfx.Jump();
    }

    public void ResetCounter()
    {
        VerticalCounter = 0;
        HorizontalCounter = 0;
    }
}
