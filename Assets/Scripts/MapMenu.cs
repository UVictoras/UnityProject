using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MapMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _mapNames;

    private int _mapIndex;
    // Start is called before the first frame update
    void Start()
    {
        _mapIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (_mapIndex)
            {
                case 0:
                    _mapIndex = 1;
                    break;
                case 1:
                    _mapIndex = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (_mapIndex)
            {
                case 0:
                    _mapIndex = 1;
                    break;
                case 1:
                    _mapIndex = 0;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (_mapNames[_mapIndex].name)
            {
                case "Athens":
                    GameManager._instance._mapChoice = "Athens";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterMenu");
                    break;
                case "Yggdrasil":
                    GameManager._instance._mapChoice = "Yggdrasil";
                    UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterMenu");
                    break;
            }
        }

        for (int i = 0; i < _mapNames.Length; i++)
        {
            if (i == _mapIndex)
            {
                _mapNames[i].GetComponent<TextMeshProUGUI>().color = Color.green;
            }
            else
            {
                _mapNames[i].GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }
    }
}
