using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instanec;
    [HideInInspector] public int score;
    [SerializeField] private Text ScoreText;
    void Awake()
    {
        if(instanec != null)
            Destroy(gameObject);
        else
        {
            instanec = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // ScoreText.text = score.ToString(); // ui 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreText == null)
        {
            ScoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
            ScoreText.text = score.ToString();
        }
            
    }
    public void AddScore(int amount)
    {
        score += amount;
        if(score>PlayerPrefs.GetInt("HighScore",0))
            PlayerPrefs.SetInt("HighScore",score);
        // Debug.Log(score);
        ScoreText.text = score.ToString(); // ui 

    }
    public void RemoveScore()
    {
        score =0;
    }
}
