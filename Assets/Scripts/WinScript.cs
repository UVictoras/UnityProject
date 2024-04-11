using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    /* ----------------------------------------------------- *\
    |                                                         |
    |                          Field                          |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Field

    private bool _textCanBlink;
    private bool _godCanFlip;
    private bool _isFlipped;

    public TextMeshProUGUI _winText;
    public TextMeshProUGUI _playAgainText;

    #endregion Field

    /* ----------------------------------------------------- *\
    |                                                         |
    |                         Methods                         |
    |                                                         |
    \* ----------------------------------------------------- */
    #region Methods
    private void Start()
    {
        _isFlipped = false;
    }
    private void Update()
    {
        _winText.text = GameManager._instance._winnerName + " Wins !";
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager._instance._winnerSprite;

        CheckIfBlink();
        CheckIfFlip();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void CheckIfBlink()
    {
        if (_textCanBlink == false)
            return;

        StartCoroutine(Blink());
    }

    private void CheckIfFlip()
    {
        if (_godCanFlip == false)
            return;

        StartCoroutine(Flip());
    }

    private IEnumerator Blink()
    {
        _textCanBlink = false;

        yield return new WaitForSeconds(0.5f);

        _playAgainText.enabled = !_playAgainText.enabled;

        _textCanBlink = true;
    }

    private IEnumerator Flip()
    {
        _godCanFlip = false;

        yield return new WaitForSeconds(0.5f);

        transform.GetChild(1).transform.rotation = Quaternion.Euler(0, _isFlipped == false ? 180 : 0, 0);

        _godCanFlip = true;

        _isFlipped = !_isFlipped;
    }

    #endregion Methods
}
