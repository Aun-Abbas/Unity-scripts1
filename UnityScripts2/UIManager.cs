using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject exitPanel;

    [SerializeField]
    Text topScore;

    GameManager manager;

    public void Start()
    {
        manager = GameManager.instance;
    }

    private void Update()
    {
        topScore.text= manager.topScore.ToString();
    }

    public void openExitMenu() {
        exitPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void exitMenu()
    {
        exitPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void exitGame()
    {
        manager.score = 0;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }



}
