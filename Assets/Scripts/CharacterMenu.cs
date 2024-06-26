using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    [SerializeField] 
    private GameObject[] _players;
    [SerializeField]
    private GameObject[] _inputFields;
    [SerializeField] 
    private Transform[] _playerOnePos;
    [SerializeField] 
    private Transform[] _playerTwoPos;

    public int _playerOneChoice = 0;
    public int _playerTwoChoice = 0;

    public bool _playerOneLocked = false;
    public bool _playerTwoLocked = false;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods

    private void Start()
    {
        GameManager._instance._playerOneCharacter = "Zeus";
        GameManager._instance._playerTwoCharacter = "Zeus";
    }

    private void Update()
    {
        HandleInput();
        MovePlayers();
        CheckLockAndLoadMainScene();
    }

    private void HandleInput()
    {
        if (!_playerOneLocked)
        {
            if (Input.GetKeyDown(KeyCode.A))
                _playerOneChoice = (_playerOneChoice + 1) % _players.Length;
            else if (Input.GetKeyDown(KeyCode.D))
                _playerOneChoice = (_playerOneChoice + 1) % _players.Length;
        }

        if (!_playerTwoLocked)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _playerTwoChoice = (_playerTwoChoice + 1) % _players.Length;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                _playerTwoChoice = (_playerTwoChoice + 1) % _players.Length;
        }

        if (Input.GetKeyDown(KeyCode.E))
            _playerOneLocked = !_playerOneLocked;

        if (Input.GetKeyDown(KeyCode.Backspace))
            _playerTwoLocked = !_playerTwoLocked;
    }

    private void MovePlayers()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].transform.position = (i == 0) ? _playerOnePos[_playerOneChoice].position : _playerTwoPos[_playerTwoChoice].position;
            _players[i].transform.GetChild(0).gameObject.SetActive((i == 0) ? _playerOneLocked : _playerTwoLocked);
        }
    }

    private void CheckLockAndLoadMainScene()
    {
        if (_playerOneLocked && _playerTwoLocked)
        {
            GameManager._instance._playerOneCharacter = _playerOneChoice == 0 ? "Zeus" : "Odin";
            GameManager._instance._playerTwoCharacter = _playerTwoChoice== 0 ? "Zeus" : "Odin";
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }

    public void WritePlayerOneName()
    {
        GameManager._instance._playerOneName = _inputFields[0].GetComponent<TMPro.TMP_InputField>().text;
        if (GameManager._instance._playerOneName == "")
            GameManager._instance._playerOneName = "Player 1";
    }

    public void WritePlayerTwoName()
    {
        GameManager._instance._playerTwoName = _inputFields[1].GetComponent<TMPro.TMP_InputField>().text;
        if (GameManager._instance._playerTwoName == "")
            GameManager._instance._playerTwoName = "Player 2";
    }

    #endregion Methods
}
