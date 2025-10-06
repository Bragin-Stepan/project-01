using System.Collections;
using UnityEngine;

public class CharacterVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem _jumpEffect;
    [SerializeField] private ParticleSystem _dieEffect;

    public void Jump() => _jumpEffect.Play();

    public void Die(Vector3 position)
    {
        _dieEffect.transform.position = position;
        _dieEffect.Play();
    }
}
