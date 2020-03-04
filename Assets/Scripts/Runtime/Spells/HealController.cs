using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealController : MonoBehaviour {

    private PLAYER player;
    public Text cannot;

    public void Awake()
    {
        player = FindObjectOfType<PLAYER>();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Heal()
    {
        SkillPointManager.healRecovery = (int)(player.health.MaxVal / 50);
        SkillPointManager.healManaCost = (int)System.Math.Ceiling(player.mana.MaxVal / 100);
        if (player.mana.CurrentVal > (SkillPointManager.healManaCost * SkillPointManager.healManaCostLevel))
        {
            if(player.health.CurrentVal != player.health.MaxVal)
            {
                int actualHeal = Random.Range((SkillPointManager.healRecovery * SkillPointManager.healRecoveryLevel) / 2, (SkillPointManager.healRecovery * SkillPointManager.healRecoveryLevel) * 2);
                player.health.CurrentVal += actualHeal;
                player.mana.CurrentVal -= SkillPointManager.healManaCost * SkillPointManager.healManaCostLevel;
                var clone2 = Instantiate(Resources.Load("Heal"), player.transform.position, Quaternion.identity) as GameObject;
                var clone = Instantiate(Resources.Load("HealNumbers"), player.transform.position, Quaternion.identity) as GameObject;
                clone.transform.SetParent(player.transform);
                clone2.transform.SetParent(player.transform);
                if (actualHeal <= (SkillPointManager.healRecovery * SkillPointManager.healRecoveryLevel) * 1.8f)
                {
                    clone.GetComponent<FloatingNumbers>().damageNumber = "HEAL " + actualHeal;
                }
                else
                {
                    clone.GetComponent<FloatingNumbers>().damageNumber = "CRITICAL\nHEAL " + actualHeal;
                }
                FindObjectOfType<SFX_Manager>().heal.Play();
            }
            else
            {
                cannot.text = "You have full health";
                StartCoroutine(WaitAndPrint());
            }
        }
        else
        {
            cannot.text = "Not enough mana";
            StartCoroutine(WaitAndPrint());
        }
    }

    private IEnumerator WaitAndPrint()
    {
        cannot.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        cannot.gameObject.SetActive(false);
    }
}
