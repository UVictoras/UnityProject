using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    [SerializeField]
    private GameObject[] _frames;
    [SerializeField]
    private GameObject _gameManager;

    private int _idSelectedFrame;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        _idSelectedFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (_idSelectedFrame)
            {
                case 0:
                    _idSelectedFrame = 1;
                    break;
                case 1:
                    _idSelectedFrame = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { 
            switch (_idSelectedFrame)
            {
                case 0:
                    _idSelectedFrame = 1;
                    break;
                case 1:
                    _idSelectedFrame = 0;
                    break;
            } 
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (_frames[_idSelectedFrame].name)
            {
                case "PlayFrame":
                    if (_gameManager != null)
                        DontDestroyOnLoad(_gameManager);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MapMenu");
                    break;
                case "QuitFrame":
                    Application.Quit();
                    break;
            }
        }

        for (int i = 0; i < _frames.Length; i++)
        {
            if (i == _idSelectedFrame)
            {
                _frames[i].GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                _frames[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    #endregion Methods
}
