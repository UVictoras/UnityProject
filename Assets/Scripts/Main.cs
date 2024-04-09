using TMPro;
using UnityEngine;

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
    private GameObject _deathParticles;
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

        playerFrame.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = characterInstance.GetComponent<SpriteRenderer>().sprite;
        playerFrame.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[characterIndex];

        _players[playerId - 1] = characterInstance;

        if (playerId == 2)
            playerFrame.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
    }

    public void CheckAlive(GameObject character)
    {
        if (character.GetComponent<Character>()._lifesRemaining == 0)
        { 
            Quaternion dir = new Quaternion(character.GetComponent<Rigidbody2D>().velocity.x, character.GetComponent<Rigidbody2D>().velocity.y, character.GetComponent<Rigidbody2D>().velocity.y, character.GetComponent<Rigidbody2D>().velocity.x);
            Instantiate(_deathParticles, character.transform.position, dir);
        }
    }

    #endregion Methods
}
