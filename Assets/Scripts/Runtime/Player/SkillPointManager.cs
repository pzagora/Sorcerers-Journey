using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointManager : MonoBehaviour {

    public static int[] skillLevel;
    public Text skillPointAmount;
    public Text[] descriptions = new Text[13];
    public Text hpText, mpText;

    public bool flag;

    [SerializeField]
    private PLAYER player;
    public CanvasGroup wholeManager;
    public CanvasGroup plusButtons;

    public static int skillPoints;
    public static int icicleDamage, fireballMinDamage, fireballExplosionMinDamage, lightningMinDamage, fireNovaMinDamage, iceNovaMinDamage;
    public static int fireballMaxDamage, fireballExplosionMaxDamage, lightningMaxDamage, fireNovaMaxDamage, iceNovaMaxDamage;
    public static int sparkMinDamage, sparkMaxDamage, ultiMinDamage, ultiMaxDamage, auraMinDamage, auraMaxDamage;
    public static int healRecovery, healRecoveryLevel, healManaCost, healManaCostLevel;
    public static int manaRecoveryMultiplier, manaRecoveryHealthCostLevel;
    public static int lightningPass, sparkBounce, health, mana, healthRegen, manaRegen, manaRegenCurrent;
    public static float furyCritChance, furyCritMulti, furyCritChanceCurrent;
    public static int icicleManaCost, fireballManaCost, lightningManaCost, fireNovaManaCost, iceNovaManaCost, auraManaCost, sparkManaCost;
    public static float[] spellCooldowns;
    public float timeStampHealthRegen = 0, timeStampManaRegen = 0, timeStampFuryLose = 0;

    // Use this for initialization
    void Awake () {
        skillLevel = new int[6];
        spellCooldowns = new float[10];
        spellCooldowns[0] = 0.5f;
        spellCooldowns[1] = 5f;
        spellCooldowns[2] = 7.5f;
        spellCooldowns[3] = 10f;
        spellCooldowns[4] = 15f;
        spellCooldowns[5] = 20f;
        spellCooldowns[6] = 30f;
        spellCooldowns[7] = 5f;
        spellCooldowns[8] = 5f;
        spellCooldowns[9] = 10f;
        flag = false;
        skillPoints = 0;
        health = 100;
        mana = 50;
        healthRegen = 1;
        manaRegen = 1;
        icicleDamage = 5;
        fireballMinDamage = 12;
        fireballMaxDamage = 15;
        fireballExplosionMinDamage = 9;
        fireballExplosionMaxDamage = 11;
        lightningMinDamage = 1;
        lightningMaxDamage = 20;
        lightningPass = 1;
        sparkBounce = 0;
        fireNovaMinDamage = 25;
        fireNovaMaxDamage = 35;
        iceNovaMinDamage = 40;
        iceNovaMaxDamage = 60;
        sparkMinDamage = 1;
        sparkMaxDamage = 1;
        auraMinDamage = 5;
        auraMaxDamage = 5;
        ultiMinDamage = 10;
        ultiMaxDamage = 10;

        healRecovery = (int)(player.health.MaxVal / 50);
        healManaCost = (int)Math.Ceiling(player.mana.MaxVal / 100);
        healRecoveryLevel = 5;
        healManaCostLevel = 10;
        manaRecoveryMultiplier = 2;
        manaRecoveryHealthCostLevel = 1;

        furyCritChance = 0.5f;
        furyCritMulti = 2.0f;

        icicleManaCost = 1;
        fireballManaCost = 10;
        lightningManaCost = 15;
        fireNovaManaCost = 25;
        sparkManaCost = 25;
        iceNovaManaCost = 40;
        auraManaCost = 5;

        PlusHideShow();
        skillPointAmount.text = skillPoints.ToString();
        wholeManager.alpha = 0f;
        wholeManager.blocksRaycasts = false;

        for (int i = 0; i < skillLevel.Length; i++)
        {
            skillLevel[i] = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(player.health.CurrentVal > 0)
        {
            ManaRegen();
            if (!player.GetComponent<Player_Move>().manaBoost) HealthRegen();
            FuryLoss();
        }

        furyCritChanceCurrent = furyCritChance * player.fury.CurrentVal;

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            if (flag != false)
            {
                ManagerHide();
            }
            else
            {
                ManagerShow();
            }
        }

    }

    public void HealthUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            health += 5;
            player.health.MaxVal = health;
            hpText.text = player.health.CurrentVal + "  / " + player.health.MaxVal;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void HealthRegenUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            healthRegen += 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void ManaUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            mana += 5;
            player.mana.MaxVal = mana;
            mpText.text = player.mana.CurrentVal + "  / " + player.mana.MaxVal;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void ManaRegenUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            manaRegen += 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void CritChanceUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            furyCritChance += 0.05f;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void CritMultiUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            furyCritMulti += 0.1f;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void IcicleUp ()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            icicleDamage += 3;
            skillPoints -= 1;
            icicleManaCost += 3;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void FireballUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            fireballMinDamage += 2;
            fireballMaxDamage += 3;
            fireballManaCost += 4;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void FireballExplosionUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            fireballExplosionMinDamage += 2;
            fireballExplosionMaxDamage += 5;
            fireballManaCost += 6;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void LightningUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            lightningMinDamage += 2;
            lightningMaxDamage += 12;
            lightningManaCost += 5;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void LightningPassUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            lightningPass++;
            skillPoints -= 1;
            lightningManaCost += 15;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void FireNovaUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            fireNovaMinDamage += 7;
            fireNovaMaxDamage += 9;
            fireNovaManaCost += 20;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void SparkUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            sparkMinDamage += 2;
            sparkMaxDamage += 12;
            sparkManaCost += 5;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void SparkBounceUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            sparkBounce++;
            skillPoints -= 1;
            sparkManaCost += 15;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void IceNovaUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            iceNovaMinDamage += 11;
            iceNovaMaxDamage += 14;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void UltiUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            ultiMinDamage += 15;
            ultiMaxDamage += 20;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void ManaRecoveryMultiplierUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            manaRecoveryMultiplier += 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void ManaHealthCostDecreaseUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            manaRecoveryHealthCostLevel += 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void HealRecoveryLevelUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            healRecoveryLevel += 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void HealRecoveryManaLevelUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1 && healManaCostLevel > 1)
        {
            healManaCostLevel -= 1;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void AuraUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1)
        {
            auraMinDamage += 4;
            auraMaxDamage += 8;
            auraManaCost += 5;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void AuraManaDecreaseUp()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (skillPoints >= 1 && auraManaCost >= 3)
        {
            auraManaCost -= 3;
            skillPoints -= 1;
            skillPointAmount.text = skillPoints.ToString();
            PlusHideShow();
        }
    }

    public void PlusHideShow ()
    {
        if (skillPoints == 0)
        {
            plusButtons.alpha = 0f;
            descriptions[0].text = "HEALTH\n\nMaximum health: " + health + "\nRegeneration: " + healthRegen + "/s";
            descriptions[1].text = "MANA\n\nMaximum mana: " + mana + "\nRegeneration: " + manaRegen + "/s";
            descriptions[2].text = "Critical chance: " + Math.Round(furyCritChanceCurrent, 2) + "%\n(" + Math.Round(furyCritChance, 2) + "% / 1 fury)\n\nCritical damage: " + Math.Round((furyCritMulti * 100), 0) + "%";
            descriptions[3].text = "ICICLE [" + skillLevel[0] + "] (cost " + icicleManaCost + " MP)\n\nDamage: " + icicleDamage;
            descriptions[4].text = "FIREBALL [" + skillLevel[1] + "] (cost " + fireballManaCost + " MP)\n\nDamage: " + fireballMinDamage + " - " + fireballMaxDamage + "\nArea damage: " + fireballExplosionMinDamage + " - " + fireballExplosionMaxDamage + "\nArea damage cannot crit";
            descriptions[5].text = "LIGHTNING [" + skillLevel[2] + "] (cost " + lightningManaCost + " MP)\n\nDamage: " + lightningMinDamage + " - " + lightningMaxDamage + "\nPierce " + (lightningPass-1) + " targets";
            descriptions[6].text = "FIRE NOVA [" + skillLevel[3] + "] (cost " + fireNovaManaCost + " MP)\n\nArea damage: " + fireNovaMinDamage + " - " + fireNovaMaxDamage;
            descriptions[7].text = "SPARK [" + skillLevel[4] + "] (cost " + sparkManaCost + " MP)\n\nDamage: " + sparkMinDamage + " - " + sparkMaxDamage + "\nBounce " + sparkBounce + " times"; ;
            descriptions[8].text = "ICE NOVA [" + skillLevel[5] + "] (cost " + player.mana.MaxVal + " MP)\n\nArea damage: " + iceNovaMinDamage + " - " + iceNovaMaxDamage;
            descriptions[9].text = "DARK MATTER (cost " + player.mana.MaxVal + " MP)\n\nArea damage: " + ultiMinDamage + " - " + ultiMaxDamage;
            descriptions[10].text = "HEAL (cost " + (healManaCost * healManaCostLevel) + " MP)\n\nHeal amount: " + (int)((healRecovery * healRecoveryLevel) / 2) + " - " + (int)((healRecovery * healRecoveryLevel)  * 1.8f);
            descriptions[11].text = "MANA REGENERATION BOOST (cost " + ((manaRegen * manaRecoveryMultiplier) * 2 - manaRecoveryHealthCostLevel) + " HP/s)\n\nMana recovery * " + manaRecoveryMultiplier;
            descriptions[12].text = "AURA (cost " + auraManaCost + " MP/s)\n\nArea damage: " + auraMinDamage + " - " + auraMaxDamage;
        }
        else
        {
            plusButtons.alpha = 1f;
            descriptions[0].text = "Maximum health: " + health + "(+5)\n\nHealth Regeneration: " + healthRegen + "(+1)/s";
            descriptions[1].text = "Maximum mana: " + mana + "(+5)\n\nMana Regeneration: " + manaRegen + "(+1)/s";
            descriptions[2].text = "Critical chance: " + Math.Round(furyCritChance, 2) + "(+0.05)% / 1 fury)\n\nCritical damage: " + Math.Round((furyCritMulti * 100), 0) + "(+10)%";
            descriptions[3].text = "Damage: " + icicleDamage + "(+3)";
            descriptions[4].text = "Damage: " + fireballMinDamage + "(+2) - " + fireballMaxDamage + "(+3)\n\nArea dmg: " + fireballExplosionMinDamage + "(+2) - " + fireballExplosionMaxDamage + "(+5)";
            descriptions[5].text = "Damage: " + lightningMinDamage + "(+2) - " + lightningMaxDamage + "(+12)\n\n" + (lightningPass - 1) + "(+1) pierce";
            descriptions[6].text = "Area dmg: " + fireNovaMinDamage + "(+7) - " + fireNovaMaxDamage + "(+9)";
            descriptions[7].text = "Damage: " + sparkMinDamage + "(+2) - " + sparkMaxDamage + "(+12)\n\n" + sparkBounce + "(+1) bounce"; //
            descriptions[8].text = "Area dmg: " + iceNovaMinDamage + "(+11) - " + iceNovaMaxDamage + "(+14)";
            descriptions[9].text = "Damage: " + ultiMinDamage + "(+15) - " + ultiMaxDamage + "(+20)"; //
            descriptions[10].text = "Heal: " + (int)((healRecovery * (healRecoveryLevel + 1)) / 2) + " - " + (int)((healRecovery * (healRecoveryLevel + 1)) * 1.8f) + "\n\n Mana Cost: " + (healManaCost * healManaCostLevel) + "(-" + ((healManaCost * healManaCostLevel) - (healManaCost * (healManaCostLevel - 1)) + ")"); //
            descriptions[11].text = "Mana Recovery * " + manaRecoveryMultiplier + "(+1)\n\nHealth cost decrease: " + manaRecoveryHealthCostLevel + "(+1)/s"; //
            descriptions[12].text = "Damage: " + auraMinDamage + "(+4) - " + auraMaxDamage + "(+8)\n\nMana cost: " + auraManaCost + "(-3)/s"; //
        }
    }

    public void ManagerHide()
    {
        ChangeColor.activeChanger = false;
        wholeManager.alpha = 0f;
        wholeManager.blocksRaycasts = false;
        flag = false;
    }

    public void ManagerShow()
    {
        ChangeColor.activeChanger = true;
        wholeManager.alpha = 1f;
        wholeManager.blocksRaycasts = true;
        flag = true;
    }

    public void HealthRegen()
    {
        if (timeStampHealthRegen <= Time.time && player.health.CurrentVal < player.health.MaxVal)
        {
            player.health.CurrentVal += healthRegen;
            timeStampHealthRegen = Time.time + 1f;
        }
        else if (timeStampHealthRegen >= Time.time && player.health.CurrentVal >= player.health.MaxVal)
        {
            timeStampHealthRegen = Time.time + 1f;
        }
    }

    public void ManaRegen()
    {
        if (player.GetComponent<Player_Move>().manaBoost)
        {
            if (timeStampManaRegen <= Time.time && player.mana.CurrentVal < player.mana.MaxVal)
            {
                manaRegenCurrent = (manaRegen * manaRecoveryMultiplier);
                if (player.health.CurrentVal > manaRegenCurrent * 2 - manaRecoveryHealthCostLevel)
                {
                    player.health.CurrentVal -= manaRegenCurrent * 2 - manaRecoveryHealthCostLevel;
                    player.mana.CurrentVal += manaRegenCurrent;
                    var clone2 = Instantiate(Resources.Load("Mana"), player.transform.position, Quaternion.identity) as GameObject;
                    clone2.transform.SetParent(player.transform);
                }
                else
                {
                    player.GetComponent<Player_Move>().manaBoost = false;
                }
                timeStampManaRegen = Time.time + 1f;
            }
            else if (timeStampManaRegen >= Time.time && player.mana.CurrentVal >= player.mana.MaxVal)
            {
                timeStampManaRegen = Time.time + 1f;
            }
            if (player.mana.CurrentVal == player.mana.MaxVal) player.GetComponent<Player_Move>().manaBoost = false;
        }
        else
        {
            if (timeStampManaRegen <= Time.time && player.mana.CurrentVal < player.mana.MaxVal)
            {
                player.mana.CurrentVal += manaRegen;
                timeStampManaRegen = Time.time + 1f;
            }
            else if (timeStampManaRegen >= Time.time && player.mana.CurrentVal >= player.mana.MaxVal)
            {
                timeStampManaRegen = Time.time + 1f;
            }
        }
    }

    public void FuryLoss()
    {
        if (timeStampFuryLose <= Time.time && player.fury.CurrentVal > 0)
        {
            player.fury.CurrentVal -= 0.5f;
            timeStampFuryLose = Time.time + 0.5f;
        }
        else if (timeStampFuryLose >= Time.time && player.fury.CurrentVal <= 0)
        {
            timeStampFuryLose = Time.time + 0.5f;
        }
        PlusHideShow();
    }
}
