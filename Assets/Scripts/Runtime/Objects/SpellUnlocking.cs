using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUnlocking : MonoBehaviour {

    public string spellName;
    public Text gainText, info, altarText;
    public Image gainShade, gainOrnament;
    
    private bool flagUnlock;
    private Color colorShade, colorOrnament, colorText;
    private Color alpha;
    private float currentLerp;
    private bool flag, skillPointsTaken;

    // Use this for initialization
    void Start () {
        flagUnlock = true;
        flag = true;
        alpha = altarText.color;
        skillPointsTaken = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gainShade.gameObject.activeSelf)
        {
            if (flagUnlock)
            {
                colorShade.a = Mathf.Lerp(colorShade.a, 0.7f, 0.03f);
                gainShade.color = colorShade;
                colorText.a = Mathf.Lerp(colorText.a, 0.8f, 0.05f);
                gainText.color = colorText;
                colorOrnament.a = Mathf.Lerp(colorOrnament.a, 0.8f, 0.05f);
                gainOrnament.color = colorOrnament;
                gainOrnament.fillAmount = Mathf.Lerp(gainOrnament.fillAmount, 1f, 0.005f);
                if (colorShade.a >= 0.699f) flagUnlock = false;
            }
            else if (!flagUnlock)
            {
                colorShade.a = Mathf.Lerp(colorShade.a, 0f, 0.01f);
                gainShade.color = colorShade;
                colorText.a = Mathf.Lerp(colorText.a, 0f, 0.02f);
                gainText.color = colorText;
                colorOrnament.a = Mathf.Lerp(colorOrnament.a, 0f, 0.02f);
                gainOrnament.color = colorOrnament;
                gainOrnament.fillAmount = Mathf.Lerp(gainOrnament.fillAmount, 0f, 0.005f);
                if (colorShade.a <= 0.05f) gainShade.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currentLerp = alpha.a;
            altarText.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (flag)
            {
                alpha.a = Mathf.Lerp(currentLerp, 0.4f, 0.05f);
                currentLerp = alpha.a;
                if (currentLerp <= 0.5f) flag = false;
            }
            else
            {
                alpha.a = Mathf.Lerp(currentLerp, 1f, 0.05f);
                currentLerp = alpha.a;
                if (currentLerp >= 0.9f) flag = true;
            }

            altarText.color = alpha;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (spellName == "Fireball")
                {
                    if (GetLockValue(1))
                    {
                        UnlockSpell(1);
                        ShowUnlockedSpell("\nFireball");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "Lightning")
                {
                    if (GetLockValue(2))
                    {
                        UnlockSpell(2);
                        ShowUnlockedSpell("\nLightning");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "FireNova")
                {
                    if (GetLockValue(3))
                    {
                        UnlockSpell(3);
                        ShowUnlockedSpell("\nFire Nova");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "Spark")
                {
                    if (GetLockValue(4))
                    {
                        UnlockSpell(4);
                        ShowUnlockedSpell("\nSpark");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "IceNova")
                {
                    if (GetLockValue(5))
                    {
                        UnlockSpell(5);
                        ShowUnlockedSpell("\nIce Nova");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "DarkMatter")
                {
                    if (GetLockValue(6))
                    {
                        UnlockSpell(6);
                        ShowUnlockedSpell("Dark Matter");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "Heal")
                {
                    if (GetLockValue(7))
                    {
                        UnlockSpell(7);
                        ShowUnlockedSpell("\nHeal");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "ManaHeal")
                {
                    if (GetLockValue(8))
                    {
                        UnlockSpell(8);
                        ShowUnlockedSpell("Mana Recovery");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "Aura")
                {
                    if (GetLockValue(9))
                    {
                        UnlockSpell(9);
                        ShowUnlockedSpell("\nAura");
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
                else if (spellName == "Skill")
                {
                    if (!skillPointsTaken)
                    {
                        ShowUnlockedSpell("5 skill\npoints");
                        SkillPointManager.skillPoints += 5;
                        FindObjectOfType<SkillPointManager>().skillPointAmount.text = SkillPointManager.skillPoints.ToString();
                        FindObjectOfType<SkillPointManager>().PlusHideShow();
                        skillPointsTaken = true;
                        altarText.text = "This altar has been used";
                    }
                    else
                    {
                        StopCoroutine("ShowInfo");
                        StartCoroutine("ShowInfo");
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            altarText.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowInfo()
    {
        info.text = "This altar has been used before";
        info.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        info.gameObject.SetActive(false);
    }

    private bool GetLockValue(int spellNumber)
    {
        if (FindObjectOfType<Player_Move>().LockValue(spellNumber))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UnlockSpell(int spellNumber)
    {
        FindObjectOfType<Player_Move>().Unlock(spellNumber);
        altarText.text = "This altar has been used";
    }

    private void ShowUnlockedSpell(string text)
    {
        FindObjectOfType<SFX_Manager>().levelUp.Play();
        FindObjectOfType<ScoreManager>().scoreCount += 500;
        FindObjectOfType<ScoreManager>().Scoring();
        FindObjectOfType<PLAYER>().movementSpeed.CurrentVal += 2;
        gainText.text = "Unlocked\n" + text;
        gainOrnament.fillAmount = 0f;
        colorShade.a = 0f;
        colorOrnament.a = 0f;
        colorText.a = 0f;
        gainShade.color = colorShade;
        gainOrnament.color = colorOrnament;
        gainText.color = colorText;
        flagUnlock = true;

        gainShade.gameObject.SetActive(true);
    }
}
