using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    [HideInInspector]
    public string _playerOneCharacter;
    [HideInInspector]
    public string _playerTwoCharacter;
    [HideInInspector]
    public string _playerOneName;
    [HideInInspector]
    public string _playerTwoName;
    [HideInInspector]
    public string _mapChoice;
    [HideInInspector]
    public string _winnerName;
    [HideInInspector]
    public Sprite _winnerSprite;

    public static GameManager _instance;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(gameObject);

        _playerOneName = "Player 1";
        _playerTwoName = "Player 2";
    }

    #endregion Methods
}
