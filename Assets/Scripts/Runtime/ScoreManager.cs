using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text score;
    public Text[] nickname = new Text[10];
    public Text[] highScore = new Text[10];
    public Text Username_field;

    string[] arr1 = new string[] { "h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8", "h9", "h10" };
    string[] arr2 = new string[] { "n1", "n2", "n3", "n4", "n5", "n6", "n7", "n8", "n9", "n10" };

    public int scoreCount;

	// Use this for initialization
	void Start () {
        SortArray();
        for (int i = 0; i < 10; i++)
        {
            highScore[i].text = PlayerPrefs.GetInt(arr1[i], 0).ToString();
            nickname[i].text = PlayerPrefs.GetString(arr2[i], "---");
        }
    }
	
	// Update is called once per frame
	public void Scoring() {

        score.text = "Score: " + scoreCount;

	}

    public void SortArray()
    {
        int[] highscores = new int[10];
        string[] nicknames = new string[10];
        for (int i = 0; i < 10; i++)
        {
            highscores[i] = PlayerPrefs.GetInt(arr1[i], 0);
            nicknames[i] = PlayerPrefs.GetString(arr2[i], "---");
        }

        Array.Sort(highscores, nicknames);
        Array.Reverse(highscores);
        Array.Reverse(nicknames);

        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt(arr1[i], highscores[i]);
            PlayerPrefs.SetString(arr2[i], nicknames[i]);
        }
    }

    public void AddHighscore()
    {
        
        string userID = Username_field.text.ToString();

        if (scoreCount > PlayerPrefs.GetInt(arr1[9], 0))
        {
            PlayerPrefs.SetInt(arr1[9], scoreCount);
            PlayerPrefs.SetString(arr2[9], userID);
        }

        SortArray();

        for (int i = 0; i < 10; i++)
        {
            highScore[i].text = PlayerPrefs.GetInt(arr1[i], 0).ToString();
            nickname[i].text = PlayerPrefs.GetString(arr2[i], "---");
        }
        gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
