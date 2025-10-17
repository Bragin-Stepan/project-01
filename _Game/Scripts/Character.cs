using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private float _horizontalJumpMultiplier;
    [SerializeField] private float _addScalePerJump = 0.1f;

    public Jump Jump { get; private set; }

    private CharacterVfx _vfx;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private bool _isFreezed;
    private float _defaultCharacterScale;
    private float _characterScale;
    private float MaxCharacterScale => _defaultCharacterScale * 1.5f;

    private void Awake()
    {
        _defaultCharacterScale = _characterScale = transform.localScale.x;

        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _vfx = GetComponentInChildren<CharacterVfx>();

        Jump = new Jump(_rigidbody, _jumpForce, _animator, _vfx);
    }

    private void Update()
    {
        UpdateScale();

        if (_isFreezed)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseCharacterScale();
            Jump.Base();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            IncreaseCharacterScale();
            Jump.Horizontal(false, _horizontalJumpMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            IncreaseCharacterScale();
            Jump.Horizontal(true, _horizontalJumpMultiplier);
        }
    }

    public void ResetJumpCounter() => Jump.ResetCounter();

    public void Teleport(Vector3 position) => transform.position = position;

    public void Freeze()
    {
        _isFreezed = true;
        _rigidbody.isKinematic = true;
    }

    public void Unfreeze()
    {
        _isFreezed = false;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Kill()
    {
        _vfx.Die(transform.position);

        gameObject.Off();
    }

    private void IncreaseCharacterScale()
    {
        _characterScale += _addScalePerJump;

        if (_characterScale > MaxCharacterScale)
            _characterScale = MaxCharacterScale;
    }

    private void UpdateScale()
    {
        if (_characterScale > _defaultCharacterScale)
            _characterScale -= Time.deltaTime;

        transform.localScale = new Vector3(_characterScale, _characterScale, _characterScale);
    }
}
