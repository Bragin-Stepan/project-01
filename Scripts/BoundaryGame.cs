using UnityEngine;

public class BoundaryGame : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private Vector3 _defaultBirdPosition = new Vector3(0, 0.5f, 0);
    [SerializeField] private float _winScore;

    [SerializeField] private GameObject _upperBoundary;
    [SerializeField] private GameObject _lowerBoundary;
    [SerializeField] private GameObject _leftBoundary;
    [SerializeField] private GameObject _rightBoundary;

    [SerializeField] private float _verticalLimit = 5f;
    [SerializeField] private float _horizontalLimit = 8f;

    [SerializeField] private bool _showDebugGui;

    private bool _isRunning;
    private string _gameOverMessage;
    private Rigidbody _birdRigidbody;

    private static class Message
    {
        public const string Win = "You win";
        public const string Lose = "You lose";
        public const string Score = "Score:";
        public const string Controls = "[Space] / [A] / [D] - jump";
        public const string Restart = "[F] / [R] - restart";
    }

    private void Awake()
    {
        _birdRigidbody = _bird.GetComponent<Rigidbody>();
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.R))
            StartGame();

        if (_isRunning == false)
            return;

        if (_bird.JumpScore >= _winScore)
            WinGame();

        if (IsOutOfBoundary())
            LoseGame();

        _bird.InputJump();
    }

    private bool IsOutOfBoundary()
    {
        Vector3 targetPosition = _bird.transform.position;

        return targetPosition.x > _horizontalLimit ||
            targetPosition.x < -_horizontalLimit ||
            targetPosition.y > _verticalLimit ||
            targetPosition.y < -_verticalLimit;
    }

    private void StartGame()
    {
        SetupBird();
        SetupBoundaries();

        _bird.ResetScore();
        _gameOverMessage = string.Empty;
        _isRunning = true;
    }

    private void SetupBird()
    {
        Extensions.On(_bird.gameObject);

        _bird.gameObject.transform.position = _defaultBirdPosition;
        _birdRigidbody.velocity = Vector3.zero;
        _birdRigidbody.isKinematic = false;
    }

    private void SetupBoundaries()
    {
        _upperBoundary.transform.position = new Vector3(0, _verticalLimit, 0);
        _lowerBoundary.transform.position = new Vector3(0, -_verticalLimit, 0);
        _leftBoundary.transform.position = new Vector3(-_horizontalLimit, 0, 0);
        _rightBoundary.transform.position = new Vector3(_horizontalLimit, 0, 0);
    }

    private void WinGame()
    {
        _gameOverMessage = Message.Win;
        _birdRigidbody.isKinematic = true;
        _isRunning = false;
    }

    private void LoseGame()
    {
        _gameOverMessage = Message.Lose;
        Extensions.Off(_bird.gameObject);
        _isRunning = false;
    }

    public void OnGUI()
    {
        if (_showDebugGui)
        {
            GUI.Label(new Rect(20, 10, 200, 20), Message.Controls);
            GUI.Label(new Rect(20, 30, 200, 20), Message.Restart);
            GUI.Label(new Rect(20, 50, 200, 20), Message.Score + " " + _bird.JumpScore + " / " + _winScore);
            GUI.Label(new Rect(20, 70, 200, 20), _gameOverMessage);
        }
    }
}