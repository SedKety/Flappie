using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public Slider sensitivitySlider;

    public TextMeshProUGUI highscoreText;
    public int highscorePoint;
    private void Start()
    {
        highscorePoint = PlayerPrefs.GetInt("highscore");

        highscoreText.text = highscorePoint.ToString();
        Debug.Log(highscorePoint);
    }

    public void HighscoreButton()
    {
        highscorePoint = PlayerPrefs.GetInt("highscore");

        highscoreText.text = highscorePoint.ToString();
        Debug.Log(highscorePoint);
    }
    public void ClickButton(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void MenuDisable(GameObject menuToDisable)
    {
        menuToDisable.SetActive(false);
    }

    public void MenuEnable(GameObject menuToEnable)
    {
        menuToEnable.SetActive(true);
    }

    public void SetSensitivity()
    {
        //Movement.sensitivity = sensitivitySlider.value;

        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
