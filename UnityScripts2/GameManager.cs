using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int score;
    public int topScore;
    void Awake()
    {

        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {

            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("TopScore"))
        {
            topScore = PlayerPrefs.GetInt("TopScore");
        }
    }

    private void Update()
    {
        if (score > topScore)
        {
            PlayerPrefs.SetInt("TopScore", score);
        }

        if (PlayerPrefs.HasKey("TopScore"))
        {
            topScore = PlayerPrefs.GetInt("TopScore");
        }
    }



}
