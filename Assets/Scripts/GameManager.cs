using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gems;
    [SerializeField]
    private GameObject[] hearts;

    [SerializeField]
    private GameObject endingText;
    [SerializeField]
    private GameObject tryAgainButton;
    [SerializeField]
    private GameObject menuButton;

    public bool disablePlayerInput = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gems.Length; i++)
            gems[i].SetActive(false);
        endingText.SetActive(false);
        tryAgainButton.SetActive(false);
        menuButton.SetActive(false);
    }

    public void UpdateGems(int gemsCollected)
    {
        if (gemsCollected < 3)
            gems[gemsCollected - 1].SetActive(true);
        else if (gemsCollected == 3)
        {
            gems[gemsCollected - 1].SetActive(true);
            endingText.GetComponent<Text>().text = "You won!";
            endingText.SetActive(true);
            tryAgainButton.SetActive(true);
            menuButton.SetActive(true);
            disablePlayerInput = true;
        }
    }

    public void UpdateHearts(int health)
    {
        if (disablePlayerInput)
            return;

        if (health > 0)
            hearts[health].SetActive(false);
        else if (health==0)
        {
            Debug.Log("You dead");
            hearts[health].SetActive(false);
            endingText.GetComponent<Text>().text = "You lost!";
            endingText.SetActive(true);
            tryAgainButton.SetActive(true);
            menuButton.SetActive(true);
            disablePlayerInput = true;
        }
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
