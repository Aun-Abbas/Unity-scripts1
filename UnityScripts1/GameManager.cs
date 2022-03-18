using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    #endregion

    public event Action spawnEnemy;
    [HideInInspector] public int kills;    
    [HideInInspector] public bool gameStarted;
    
    //public float highScore { get { return PlayerPrefs.GetFloat("highscore"); } set { PlayerPrefs.SetFloat("highscore", value); } }

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        //if (!PlayerPrefs.HasKey("highscore")) { PlayerPrefs.SetFloat("highscore", 0f); }          
    }

    // Update is called once per frame
    void Update()
    {        
        spawnEnemy?.Invoke();                
    }
}
