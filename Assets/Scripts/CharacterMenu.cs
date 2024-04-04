using System.Collections;
using System.Collections.Generic;
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
        
    }
}
