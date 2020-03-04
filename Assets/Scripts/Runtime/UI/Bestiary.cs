using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour {

    private bool flag;
    public GameObject navigation;
    public CanvasGroup bestiary;
    public Text mobName, mobTier, mobHealth, mobExp, mobScore, currentPage;
    public Image mobPortrait, mobType;
    public int counter;

    private static List<Monsters> monster;

    void Awake()
    {
        monster = new List<Monsters>();
    }

    // Use this for initialization
    void Start () {
        flag = false;
        counter = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (monster.Count == 0)
            {

            }
            else
            {
                if (flag != false)
                {
                    ManagerHide();
                }
                else
                {
                    FindObjectOfType<QuestLog>().ManagerHide();
                    ManagerShow();
                    if (monster.Count != 0)
                    {
                        Show();
                    }
                }
            }
        }

        currentPage.text = (counter + 1) + " / " + monster.Count;
    }

    public void Show()
    {
        mobName.text = monster[counter].sName;
        mobTier.text = monster[counter].sTier;
        mobHealth.text = monster[counter].sHealth;
        mobExp.text = monster[counter].sExp;
        mobScore.text = monster[counter].sScore;
        mobPortrait.GetComponent<Image>().sprite = monster[counter].sPortrait;
        mobType.GetComponent<Image>().sprite = monster[counter].sType;

        if (monster[counter].sName.Contains("Shard"))
        {
            mobPortrait.gameObject.transform.localScale = new Vector3(0.8f, 1.4f, 1);
        }
        else
        {
            mobPortrait.gameObject.transform.localScale = new Vector3(1f, 1f, 1);
        }
    }

    public void ShowNext()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (counter == monster.Count-1)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        Show();
    }

    public void ShowPrev()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        if (counter == 0)
        {
            counter = monster.Count-1;
        }
        else
        {
            counter--;
        }
        Show();
    }

    public static void GetMonster(string sName, int sTier, int sHealth, int sExp, int sScore, Sprite sPortrait, Sprite sType)
    {
        bool isAny = false;

        foreach(Monsters x in monster)
        {
            if(x.sName == sName)
            {
                isAny = true;
            }
        }

        if(sName != "SHARDLING")
        {
            if (isAny == false)
            {
                FindObjectOfType<ScoreManager>().scoreCount += 100;
                FindObjectOfType<ScoreManager>().Scoring();
                monster.Add(new Monsters(sName, sTier, sHealth, sExp, sScore, sPortrait, sType));
                FindObjectOfType<Bestiary>().counter = monster.Count - 1;
                if (FindObjectOfType<Bestiary>().flag == true)
                {
                    FindObjectOfType<Bestiary>().Show();
                }
            }
        }
    }

    public void ManagerHide()
    {
        bestiary.alpha = 0f;
        bestiary.blocksRaycasts = false;
        flag = false;
    }

    public void ManagerShow()
    {
        bestiary.alpha = 1f;
        bestiary.blocksRaycasts = true;
        flag = true;
    }
}


public class Monsters
{
    public string sName, sTier, sHealth, sExp, sScore;
    public Sprite sPortrait, sType;

    public Monsters(string sName, int sTier, int sHealth, int sExp, int sScore, Sprite sPortrait, Sprite sType)
    {
        this.sName = sName;
        this.sTier = sTier.ToString();
        this.sHealth = sHealth.ToString();
        this.sExp = sExp.ToString();
        this.sScore = sScore.ToString();
        this.sPortrait = sPortrait;
        this.sType = sType;
    }
}