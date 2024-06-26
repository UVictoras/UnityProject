using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    [SerializeField] 
    private Transform[] _playersPos;
    [SerializeField] 
    private GameObject[] _playersFrames;
    [SerializeField] 
    private GameObject[] _characters;
    [SerializeField] 
    private Sprite[] _backgrounds;
    [SerializeField] 
    private Sprite[] _charactersFrames;
    [SerializeField] 
    private TextMeshProUGUI[] _charactersPercentage;

    private GameObject[] _players;

    public GameObject[] _hearts;
    public static Main instance;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _players = new GameObject[2];

        SetBackgroundSprite();
        SetCharacters(1, _charactersPercentage[0], _playersPos[0], _playersFrames[0], GameManager._instance._playerOneCharacter);
        SetCharacters(2, _charactersPercentage[1], _playersPos[1], _playersFrames[1], GameManager._instance._playerTwoCharacter);
    }

    private void Update()
    {
        
        if (_players[0].GetComponent<Character>()._lifesRemaining > _players[1].GetComponent<Character>()._lifesRemaining)
        {
            if (_players[0].GetComponent<Character>()._characterName == "Zeus")
            {
                OdinFury.instance.Disable(_players[1].GetComponent<Character>());
                ZeusWrath.instance.Begin(_players[1].GetComponent<Character>());
            }
            else
            {
                ZeusWrath.instance.Disable();
                OdinFury.instance.Begin(_players[0].GetComponent<Character>(), _players[1].GetComponent<Character>());
            }
        }
        else if (_players[0].GetComponent<Character>()._lifesRemaining < _players[1].GetComponent<Character>()._lifesRemaining)
        {
            if (_players[1].GetComponent<Character>()._characterName == "Zeus")
            {
                OdinFury.instance.Disable(_players[0].GetComponent<Character>());
                ZeusWrath.instance.Begin(_players[0].GetComponent<Character>());
            }
            else
            {
                ZeusWrath.instance.Disable();
                OdinFury.instance.Begin(_players[1].GetComponent<Character>(), _players[0].GetComponent<Character>());
            }
        }
        else
        {
            if (_players[0].GetComponent<Character>()._percentage < _players[1].GetComponent<Character>()._percentage)
            {
                if (_players[0].GetComponent<Character>()._characterName == "Zeus")
                {
                    OdinFury.instance.Disable(_players[1].GetComponent<Character>());
                    ZeusWrath.instance.Begin(_players[1].GetComponent<Character>());
                }
                else
                {
                    ZeusWrath.instance.Disable();
                    OdinFury.instance.Begin(_players[0].GetComponent<Character>(), _players[1].GetComponent<Character>());
                }
            }
            else if (_players[0].GetComponent<Character>()._percentage > _players[1].GetComponent<Character>()._percentage)
            {
                if (_players[1].GetComponent<Character>()._characterName == "Zeus")
                {
                    OdinFury.instance.Disable(_players[0].GetComponent<Character>());
                    ZeusWrath.instance.Begin(_players[0].GetComponent<Character>());
                }
                else
                {
                    ZeusWrath.instance.Disable();
                    OdinFury.instance.Begin(_players[1].GetComponent<Character>(), _players[0].GetComponent<Character>());
                }
            }
            else
            {
                ZeusWrath.instance.Disable();
                OdinFury.instance.Disable(_players[0].GetComponent<Character>());
            }
        }

        CheckPlayerWins();
    }


    private void SetBackgroundSprite()
    {
        int backgroundIndex = (GameManager._instance._mapChoice == "Athens") ? 0 : 1;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _backgrounds[backgroundIndex];
    }

    private void SetCharacters(int playerId, TextMeshProUGUI percentageText, Transform playerPos, GameObject playerFrame, string characterName)
    {
        int characterIndex = (characterName == "Zeus") ? 0 : 1;
        GameObject characterInstance = Instantiate(_characters[characterIndex], playerPos.position, transform.rotation);

        characterInstance.GetComponent<Character>()._playerId = playerId;
        characterInstance.GetComponent<Character>()._percentageText = percentageText;
        characterInstance.GetComponent<Character>()._name = playerId == 1 ? GameManager._instance._playerOneName : GameManager._instance._playerTwoName;

        if (GameManager._instance._playerOneCharacter == GameManager._instance._playerTwoCharacter && playerId == 2) 
        {
            characterInstance.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        playerFrame.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterInstance.GetComponent<SpriteRenderer>().sprite;
        playerFrame.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[characterIndex];

        _players[playerId - 1] = characterInstance;

        if (playerId == 2)
            playerFrame.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
    }

    private void CheckPlayerWins()
    {
        if (_players[0].GetComponent<Character>()._lifesRemaining == 0 || _players[1].GetComponent<Character>()._lifesRemaining == 0)
        {
            GameManager._instance._winnerName = _players[0].GetComponent<Character>()._lifesRemaining != 0 ? _players[0].GetComponent<Character>()._name : _players[1].GetComponent<Character>()._name;
            GameManager._instance._winnerSprite = _players[0].GetComponent<Character>()._lifesRemaining != 0 ? _players[0].GetComponent<SpriteRenderer>().sprite : _players[1].GetComponent<SpriteRenderer>().sprite;
            SceneManager.LoadScene("WinScene");
        }
    }

    #endregion Methods
}
