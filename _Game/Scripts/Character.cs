using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private float _horizontalJumpMultiplier;
    [SerializeField] private float _addScalePerJump = 0.1f;

    public CharacterJump Jump { get; private set; }

    private CharacterVfx _vfx;
    private Rigidbody _rigidbody;

    private float _defaultScale;
    private float _targetScale;
    private float MaxScale => _defaultScale * 1.5f;

    private void Awake()
    {
        _defaultScale = _targetScale = transform.localScale.x;

        _rigidbody = GetComponent<Rigidbody>();
        _vfx = GetComponentInChildren<CharacterVfx>();

        Jump = new CharacterJump(_vfx, _rigidbody, _jumpForce, _horizontalJumpMultiplier);
    }

    public void InputJump()
    {
        UpdateScale();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump.Base();

        if (Input.GetKeyDown(KeyCode.A))
            Jump.Horizontal(-_jumpForce.x);

        if (Input.GetKeyDown(KeyCode.D))
            Jump.Horizontal(_jumpForce.x);
    }

    public void ResetJumpCounter() => Jump.ResetCounter();

    public void Teleport(Vector3 position) => transform.position = position;

    public void Freeze() => _rigidbody.isKinematic = true;

    public void Unfreeze()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Kill()
    {
        _vfx.Die(transform.position);

        gameObject.Off();
    }

    private void IncreaseTargetScale()
    {
        _targetScale += _addScalePerJump;

        if (_targetScale > MaxScale)
            _targetScale = MaxScale;
    }

    private void UpdateScale()
    {
        if (_targetScale > _defaultScale)
            _targetScale -= Time.deltaTime;

        transform.localScale = new Vector3(_targetScale, _targetScale, _targetScale);
    }
}
