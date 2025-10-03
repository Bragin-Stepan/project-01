using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private float _horizontalJumpMultiplier;

    public int VerticalJumpCounter { get; private set; }
    public int HorizontalJumpCounter { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            BaseJump();

        if (Input.GetKeyDown(KeyCode.A))
            HorizontalJump(-_jumpForce.x);

        if (Input.GetKeyDown(KeyCode.D))
            HorizontalJump(_jumpForce.x);
    }

    public void ResetJumpCounter()
    {
        VerticalJumpCounter = 0;
        HorizontalJumpCounter = 0;
    }
    public void Teleport(Vector3 position) => transform.position = position;

    public void Freeze() => _rigidbody.isKinematic = true;

    public void Unfreeze()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private void BaseJump()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(0, _jumpForce.y, 0), ForceMode.Impulse);

        VerticalJumpCounter++;
    }

    private void HorizontalJump(float forceX)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(forceX, _jumpForce.y * _horizontalJumpMultiplier, 0), ForceMode.Impulse);

        HorizontalJumpCounter++;
    }
}
