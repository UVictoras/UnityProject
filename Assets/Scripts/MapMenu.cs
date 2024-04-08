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
        GameManager._instance._mapChoice = "Athens";
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        UpdateMapSelectionUI();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _mapIndex = (_mapIndex + 1) % _mapNames.Length;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            string selectedMapName = _mapNames[_mapIndex].name;
            GameManager._instance._mapChoice = selectedMapName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterMenu");
        }
    }

    private void UpdateMapSelectionUI()
    {
        for (int i = 0; i < _mapNames.Length; i++)
        {
            bool isSelected = (i == _mapIndex);
            _mapNames[i].color = isSelected ? Color.green : Color.white;
        }
    }
}
