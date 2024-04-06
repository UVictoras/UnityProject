using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Transform[] _playersPos;
    [SerializeField]
    private GameObject[] _characters;
    [SerializeField]
    private Sprite[] _backgrounds;

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

        if (GameManager._instance._playerOneCharacter == "Zeus")
        {
            Instantiate(_characters[0], _playersPos[0].position, transform.rotation);
        }
        else
        {
            Instantiate(_characters[1], _playersPos[0].position, transform.rotation);
        }

        if (GameManager._instance._playerTwoCharacter == "Zeus")
        {
            Instantiate(_characters[0], _playersPos[1].position, transform.rotation);
        }
        else
        {
            Instantiate(_characters[1], _playersPos[1].position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
