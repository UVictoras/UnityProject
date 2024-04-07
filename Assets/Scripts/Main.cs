using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager._instance._mapChoice == "Athens")
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _backgrounds[0];
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _backgrounds[1];
        }

        _characters[0].GetComponent<Character>()._playerId = 1;
        _characters[1].GetComponent<Character>()._playerId = 1;
        _characters[0].GetComponent<Character>()._percentageText = _charactersPercentage[0];
        _characters[1].GetComponent<Character>()._percentageText = _charactersPercentage[0];

        if (GameManager._instance._playerOneCharacter == "Zeus")
        {
            Instantiate(_characters[0], _playersPos[0].position, transform.rotation);
            _playersFrames[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _characters[0].GetComponent<SpriteRenderer>().sprite;
            _playersFrames[0].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[0];
        }
        else
        {
            Instantiate(_characters[1], _playersPos[0].position, transform.rotation);
            _playersFrames[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _characters[1].GetComponent<SpriteRenderer>().sprite;
            _playersFrames[0].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[1];
        }

        _characters[1].GetComponent<Character>()._playerId = 2;
        _characters[0].GetComponent<Character>()._playerId = 2;
        _characters[0].GetComponent<Character>()._percentageText = _charactersPercentage[1];
        _characters[1].GetComponent<Character>()._percentageText = _charactersPercentage[1];

        if (GameManager._instance._playerTwoCharacter == "Zeus")
        {
            Instantiate(_characters[0], _playersPos[1].position, transform.rotation);
            _playersFrames[1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _characters[0].GetComponent<SpriteRenderer>().sprite;
            _playersFrames[1].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[0];
        }
        else
        {
            Instantiate(_characters[1], _playersPos[1].position, transform.rotation);
            _playersFrames[1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _characters[1].GetComponent<SpriteRenderer>().sprite;
            _playersFrames[1].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = _charactersFrames[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
