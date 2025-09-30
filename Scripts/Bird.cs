using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private float _horizontalJumpMultiplier;
    [SerializeField] private int _baseScoreAdd = 1;
    [SerializeField] private int _extraScoreAdd = 2;

    private Rigidbody _rigidbody;

    private int _jumpScore;
    public int JumpScore => _jumpScore;

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

    private void BaseJump()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(0, _jumpForce.y, 0), ForceMode.Impulse);
        AddScore();
    }

    private void HorizontalJump(float forceX)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(forceX, _jumpForce.y * _horizontalJumpMultiplier, 0), ForceMode.Impulse);
        AddScore(_extraScoreAdd);
    }

    private void AddScore(int extraAmount = 0)
        => _jumpScore = _jumpScore + _baseScoreAdd + extraAmount;

    public void ResetScore()
        => _jumpScore = 0;
}
