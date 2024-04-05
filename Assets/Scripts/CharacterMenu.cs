using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _players;
    [SerializeField]
    private GameObject[] _characters;

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
                    break;
                case 1:
                    _playerTwoChoice = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (_playerTwoChoice)
            {
                case 0:
                    _playerTwoChoice = 1;
                    break;
                case 1:
                    _playerTwoChoice = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (_playerOneChoice)
            {
                case 0:
                    _playerOneChoice = 1;
                    break;
                case 1:
                    _playerOneChoice = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (_playerOneChoice)
            {
                case 0:
                    _playerOneChoice = 1;
                    break;
                case 1:
                    _playerOneChoice = 0;
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
                    if (_playerOneChoice == 0)
                    {
                        _players[i].GetComponent<Transform>().position = new Vector2(-4.10f, _players[i].transform.position.y);
                        _players[i].GetComponentInChildren<Transform>().position = new Vector2(-4.12f, _players[i].GetComponentInChildren<Transform>().position.y);
                    }
                    else
                    {
                        _players[i].GetComponent<Transform>().position = new Vector2(3.10f, _players[i].transform.position.y);
                        _players[i].GetComponentInChildren<Transform>().position = new Vector2(3.12f, _players[i].GetComponentInChildren<Transform>().position.y);
                    }

                    if (_playerOneLocked == true)
                    {
                        _players[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        _players[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    break;
                case 1:
                    if (_playerTwoChoice == 0)
                    {
                        _players[i].GetComponent<Transform>().position = new Vector2(-2.50f, _players[i].transform.position.y);
                        _players[i].GetComponentInChildren<Transform>().position = new Vector2(-2.52f, _players[i].GetComponentInChildren<Transform>().position.y);
                    }
                    else
                    {
                        _players[i].GetComponent<Transform>().position = new Vector2(4.70f, _players[i].transform.position.y);
                        _players[i].GetComponentInChildren<Transform>().position = new Vector2(4.72f, _players[i].GetComponentInChildren<Transform>().position.y);
                    }

                    if (_playerTwoLocked == true)
                    {
                        _players[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        _players[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    break;
            }
        }

        if (_playerOneLocked == true && _playerTwoLocked == true)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
