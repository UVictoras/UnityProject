using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private GameObject[] _characters;
    [SerializeField]
    private Transform[] _playerOnePos;
    [SerializeField]
    private Transform[] _playerTwoPos;

    public int _playerOneChoice;
    public int _playerTwoChoice;

    public bool _playerOneLocked;
    public bool _playerTwoLocked;
    // Start is called before the first frame update
    void Start()
    {
        _playerOneChoice = 0;
        _playerTwoChoice = 0;

        _playerOneLocked = false;
        _playerTwoLocked = false;

        GameManager._instance._playerOneCharacter = "Zeus";
        GameManager._instance._playerTwoCharacter = "Zeus";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (_playerTwoChoice)
            {
                case 0:
                    _playerTwoChoice = 1;
                    GameManager._instance._playerTwoCharacter = "Odin";
                    break;
                case 1:
                    _playerTwoChoice = 0;
                    GameManager._instance._playerTwoCharacter = "Zeus";
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (_playerTwoChoice)
            {
                case 0:
                    _playerTwoChoice = 1;
                    GameManager._instance._playerTwoCharacter= "Odin";
                    break;
                case 1:
                    _playerTwoChoice = 0;
                    GameManager._instance._playerTwoCharacter = "Zeus";
                    break;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (_playerOneChoice)
            {
                case 0:
                    _playerOneChoice = 1;
                    GameManager._instance._playerOneCharacter = "Odin";
                    break;
                case 1:
                    _playerOneChoice = 0;
                    GameManager._instance._playerOneCharacter = "Zeus";
                    break;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (_playerOneChoice)
            {
                case 0:
                    _playerOneChoice = 1;
                    GameManager._instance._playerOneCharacter = "Odin";
                    break;
                case 1:
                    _playerOneChoice = 0;
                    GameManager._instance._playerOneCharacter = "Zeus";
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerOneLocked = !_playerOneLocked;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _playerTwoLocked = !_playerTwoLocked;
        }

        for (int i = 0; i < _players.Length; i++)
        {
            switch (i)
            {
                case 0:

                    _players[i].GetComponent<Transform>().position = _playerOnePos[_playerOneChoice].position;
                    _players[i].transform.GetChild(0).gameObject.SetActive(_playerOneLocked);
                    break;

                case 1:

                    _players[i].GetComponent<Transform>().position = _playerTwoPos[_playerTwoChoice].position;
                    _players[i].transform.GetChild(0).gameObject.SetActive(_playerTwoLocked);
                    break;

            }
        }

        if (_playerOneLocked == true && _playerTwoLocked == true)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
