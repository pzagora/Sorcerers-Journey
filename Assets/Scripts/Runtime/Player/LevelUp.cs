using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour {

    public Text gainText;
    public Image gainShade, gainOrnament;

    public SkillPointManager sManager;
    private SFX_Manager sfxMan;
    private PLAYER player;

    private int counter = 2;
    private int multi = 1;

    private float overExp;
    
    private bool flag;
    private Color colorShade, colorOrnament, colorText;

    public void Awake()
    {
        overExp = 0;
        sfxMan = FindObjectOfType<SFX_Manager>();
        colorShade = gainShade.color;
        colorText = gainText.color;
        player = FindObjectOfType<PLAYER>();
        sManager = FindObjectOfType<SkillPointManager>();
        gainShade.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        flag = false;
        player.health.MaxVal = SkillPointManager.health;
        player.mana.MaxVal = SkillPointManager.mana;
	}
	
	// Update is called once per frame
	void Update () {
        if (gainShade.gameObject.activeSelf)
        {
            if (flag)
            {
                colorShade.a = Mathf.Lerp(colorShade.a, 0.7f, 0.03f);
                gainShade.color = colorShade;
                colorText.a = Mathf.Lerp(colorText.a, 0.8f, 0.05f);
                gainText.color = colorText;
                colorOrnament.a = Mathf.Lerp(colorOrnament.a, 0.8f, 0.05f);
                gainOrnament.color = colorOrnament;
                gainOrnament.fillAmount = Mathf.Lerp(gainOrnament.fillAmount, 1f, 0.05f);
                if (colorShade.a >= 0.699f) flag = false;
            }
            else if (!flag)
            {
                colorShade.a = Mathf.Lerp(colorShade.a, 0f, 0.01f);
                gainShade.color = colorShade;
                colorText.a = Mathf.Lerp(colorText.a, 0f, 0.02f);
                gainText.color = colorText;
                colorOrnament.a = Mathf.Lerp(colorOrnament.a, 0f, 0.02f);
                gainOrnament.color = colorOrnament;
                gainOrnament.fillAmount = Mathf.Lerp(gainOrnament.fillAmount, 0f, 0.05f);
                if (colorShade.a <= 0.05f) gainShade.gameObject.SetActive(false);
            }
        }
    }

    public void LevelGain()
    {
        ShowGain();
        sfxMan.levelUp.Play();
        player.level++;

        overExp = (player.xp.CurrentVal + overExp) - player.xp.MaxVal;

        player.xp.MaxVal = (int)(player.xp.MaxVal * 1.4);
        player.xp.CurrentVal = 0;
        SkillPointManager.health += 10 * multi;
        player.health.MaxVal = SkillPointManager.health;
        player.health.CurrentVal = player.health.MaxVal;
        SkillPointManager.mana += 5 * multi;
        player.mana.MaxVal = SkillPointManager.mana;
        player.mana.CurrentVal = player.mana.MaxVal;
        player.levelText.text = player.level.ToString();
        SkillPointManager.skillPoints += 2;
        sManager.skillPointAmount.text = SkillPointManager.skillPoints.ToString();
        sManager.PlusHideShow();
        player.movementSpeed.CurrentVal += 1;
        counter++;
        if (counter > 10)
        {
            counter = 1;
            multi += 1;
            SkillPointManager.healthRegen += 2;
            SkillPointManager.manaRegen += 1;
        }
    }

    public void ExpPass(float mobExp)
    {
        //overExp += (player.xp.CurrentVal + mobExp) - player.xp.MaxVal;
        overExp += mobExp;
        RunLevel();
    }

    void RunLevel()
    {
        if (player.xp.CurrentVal + overExp >= player.xp.MaxVal)
        {
            LevelGain();
            RunLevel();
        }
        else if (player.xp.CurrentVal + overExp < player.xp.MaxVal)
        {
            player.xp.CurrentVal += overExp;
            overExp = 0;
        }
    }

    private void ShowGain()
    {
        gainOrnament.fillAmount = 0f;
        colorShade.a = 0f;
        colorOrnament.a = 0f;
        colorText.a = 0f;
        gainShade.color = colorShade;
        gainOrnament.color = colorOrnament;
        gainText.color = colorText;
        flag = true;

        gainShade.gameObject.SetActive(true);
    }
}
