using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Boundary _boundary;

    [SerializeField] private float _winScore;
    [SerializeField] private int _verticalScoreAdd = 1;
    [SerializeField] private int _horizontalScoreAdd = 3;

    [SerializeField] private Vector3 _defaultCharacterPosition = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector2 _boundaryLimit = new Vector2(6f, 5f);

    [SerializeField] private bool _showDebugGui;

    private bool _isRunning;
    private string _gameOverMessage;

    private void Start()
    {
        StartGame();

        _character.Freeze();
        _isRunning = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.KeypadEnter))
            StartGame();

        if (_isRunning == false)
            return;

        if (Score.Value >= _winScore)
            WinGame();

        if (IsOutOfBoundary())
            LoseGame();

        UpdateScore();
    }

    private bool IsOutOfBoundary()
    {
        Vector3 targetPosition = _character.transform.position;

        return targetPosition.x > _boundaryLimit.x ||
            targetPosition.x < -_boundaryLimit.x ||
            targetPosition.y > _boundaryLimit.y ||
            targetPosition.y < -_boundaryLimit.y;
    }

    private void StartGame()
    {
        Score.Reset();

        SetupBird();
        _boundary.Setup(_boundaryLimit);

        _gameOverMessage = string.Empty;
        _isRunning = true;
    }

    private void SetupBird()
    {
        _character.gameObject.On();

        _character.Teleport(_defaultCharacterPosition);
        _character.ResetJumpCounter();
        _character.Unfreeze();
    }

    private void UpdateScore()
        => Score.SetValue(
            (_character.Jump.VerticalCounter * _verticalScoreAdd) +
            (_character.Jump.HorizontalCounter * _horizontalScoreAdd)
        );

    private void WinGame()
    {
        _gameOverMessage = Message.Win;
        _character.Freeze();
        _isRunning = false;
    }

    private void LoseGame()
    {
        _gameOverMessage = Message.Lose;
        _character.Kill();
        _isRunning = false;
    }

    public void OnGUI()
    {
        if (_showDebugGui)
        {
            GUI.Label(new Rect(20, 20, 200, 20), Message.Controls);
            GUI.Label(new Rect(20, 40, 200, 20), Message.Restart);
            GUI.Label(new Rect(20, 60, 200, 20), Message.Score + " " + Score.Value + " / " + _winScore);
            GUI.Label(new Rect(20, 80, 200, 20), _gameOverMessage);
        }
    }
}