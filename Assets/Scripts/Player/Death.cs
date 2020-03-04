using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {

    public Image endGameBackground;
    public Text endGameText;
    public Sprite victorySprite;
    public CanvasGroup wholeManager;
    public GameObject levelUpScreen;

    private PLAYER player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PLAYER>();
        wholeManager.alpha = 0f;
        wholeManager.blocksRaycasts = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.health.CurrentVal == 0 || player.victory)
        {
            FindObjectOfType<SFX_Manager>().daySound.Stop();
            FindObjectOfType<SFX_Manager>().nightSound.Stop();
            if (player.victory)
            {
                FindObjectOfType<SFX_Manager>().victorySound.Play();
                FindObjectOfType<ScoreManager>().scoreCount += 5000;
                FindObjectOfType<ScoreManager>().Scoring();
                endGameBackground.sprite = victorySprite;
                endGameText.text = "VICTORY";
            }
            else
            {
                FindObjectOfType<SFX_Manager>().gameOverSound.Play();
            }
            levelUpScreen.SetActive(false);
            player.gameObject.SetActive(false);
            wholeManager.alpha = 1f;
            wholeManager.blocksRaycasts = true;
        }
	}
}
