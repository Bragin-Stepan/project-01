using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] private GameObject _upperBoundary;
    [SerializeField] private GameObject _lowerBoundary;
    [SerializeField] private GameObject _leftBoundary;
    [SerializeField] private GameObject _rightBoundary;

    public void Setup(Vector2 limit)
    {
        // _leftBoundary.transform.position = new Vector3(-limit.x, 0, 0);
        // _rightBoundary.transform.position = new Vector3(limit.x, 0, 0);

        _upperBoundary.transform.position = new Vector3(0, limit.y, 0);
        _lowerBoundary.transform.position = new Vector3(0, -limit.y, 0);
    }
}