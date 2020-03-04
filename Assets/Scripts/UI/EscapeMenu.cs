using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {

    public CanvasGroup escapeManager;
    public Text audioText;

	// Use this for initialization
	void Start () {
        if (AudioListener.pause)
        {
            audioText.text = "Audio: <color=red>OFF</color>";
        }
        else
        {
            audioText.text = "Audio: <color=green>ON</color>";
        }
        HideManager();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowManager();
        }

        if (escapeManager.alpha == 1f)
        {
            Time.timeScale = 0f;
        }
        if (escapeManager.alpha == 0f && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
	}

    public void ShowManager()
    {
        FindObjectOfType<SkillPointManager>().ManagerHide();
        FindObjectOfType<Bestiary>().ManagerHide();
        FindObjectOfType<ShardShop>().HideShardShop();
        FindObjectOfType<GoldShop>().HideGoldShop();
        FindObjectOfType<QuestLog>().ManagerHide();
        escapeManager.alpha = 1f;
        escapeManager.blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public void HideManager()
    {
        escapeManager.alpha = 0f;
        escapeManager.blocksRaycasts = false;
        Time.timeScale = 1f;
    }

    public void MuteSound()
    {
        if (AudioListener.pause)
        {
            audioText.text = "Audio: <color=green>ON</color>";
            AudioListener.pause = false;
        }
        else
        {
            audioText.text = "Audio: <color=red>OFF</color>";
            AudioListener.pause = true;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
