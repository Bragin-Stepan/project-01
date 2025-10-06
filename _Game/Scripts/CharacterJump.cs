using System;
using System.Collections;
using UnityEngine;

public class CharacterJump
{
    private CharacterVfx _vfx;
    private float _horizontalMultiplier;

    public CharacterJump(CharacterVfx vfx, Rigidbody rigidbody, Vector3 jumpForce, float horizontalMultiplier)
    {
        _vfx = vfx;
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
        _horizontalMultiplier = horizontalMultiplier;
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

        _vfx.Jump();
    }

    public void Horizontal(float forceX)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(forceX, _jumpForce.y * _horizontalMultiplier, 0), ForceMode.Impulse);

        HorizontalCounter++;

        _vfx.Jump();
    }

    public void ResetCounter()
    {
        VerticalCounter = 0;
        HorizontalCounter = 0;
    }
}
