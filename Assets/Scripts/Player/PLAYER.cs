using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER : MonoBehaviour {

    public Text levelText;

    public int level = 1;
    public bool victory;

    public Stat health;
    public Stat mana;
    public Stat xp;
    public Stat fury;

    public Stat[] cd = new Stat[10];
    public Stat[] reputation = new Stat[2];
    public Stat movementSpeed;
    public Stat mainProgress;

    private void Awake()
    {
        health.Initialize();
        mana.Initialize();
        xp.Initialize();
        fury.Initialize();
        movementSpeed.Initialize();
        mainProgress.Initialize();
        for (int i = 0; i < cd.Length; i++)
        {
            cd[i].Initialize();
        }
        for (int i = 0; i < reputation.Length; i++)
        {
            reputation[i].Initialize();
        }
    }

    // Use this for initialization
    void Start () {
        victory = false;
        levelText.text = level.ToString();
        for (int i = 0; i < cd.Length; i++)
        {
            cd[i].MaxVal = SkillPointManager.spellCooldowns[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < cd.Length; i++)
        {
            cd[i].CurrentVal = gameObject.GetComponent<Player_Move>().timeStamp[i] - Time.time;
        }
    }
}
