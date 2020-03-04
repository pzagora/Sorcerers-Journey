using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour {
    
    public CanvasGroup questLog, contentPlayerOpen, contentNpcTalk;
    public Text questDescriptionText, questNameText, npcNameText;
    public Text rewardOneText, rewardTwoText;
    private bool flag;

    public GameObject parentGrid;
    public static List<Quests> quest;
    private List<Text> texts;
    public Text spawnText;
    private int counter;

    void Awake()
    {
        quest = new List<Quests>();
        texts = new List<Text>();
    }

    // Use this for initialization
    void Start ()
    {
        flag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton14))
        {
            if (quest.Count == 0)
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
                    FindObjectOfType<Bestiary>().ManagerHide();
                    ManagerShow();
                }
            }
        }
        if (contentNpcTalk.alpha == 1f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
           Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                ManagerHide();
            }
        }

    }

    public static int GetQuest(string questName, string questDescription, string npcName, string questType, bool questStatus, int currentAmount, int targetAmount, string targetName, int rewardGold, int rewardExp, bool reputationTo)
    {
        bool isAny = false;

        foreach (Quests x in quest)
        {
            if (x.questName == questName)
            {
                isAny = true;
            }
        }

        if (isAny == false)
        {
            quest.Add(new Quests(questName, questDescription, npcName, questType, questStatus, currentAmount, targetAmount, targetName, rewardGold, rewardExp, reputationTo));
            return quest.Count;
        }

        return 0;
    }

    public void ManagerShowByNpc()
    {
        contentPlayerOpen.alpha = 0f;
        contentPlayerOpen.blocksRaycasts = false;
        contentNpcTalk.alpha = 1f;
        contentNpcTalk.blocksRaycasts = true;
        questLog.alpha = 1f;
        questLog.blocksRaycasts = true;
        flag = true;
        Time.timeScale = 0f;
    }

    public void ManagerHide()
    {
        questLog.alpha = 0f;
        questLog.blocksRaycasts = false;
        flag = false;
        Time.timeScale = 1f;
    }

    public void ManagerShow()
    {
        contentPlayerOpen.alpha = 1f;
        contentPlayerOpen.blocksRaycasts = true;
        contentNpcTalk.alpha = 0f;
        contentNpcTalk.blocksRaycasts = false;
        questLog.alpha = 1f;
        questLog.blocksRaycasts = true;
        flag = true;
        int actualCounter = 0;

        for (int i = 0; i <= (Mathf.FloorToInt(quest.Count / 20) - counter); i++)
        {
            var newText = Instantiate(spawnText, transform.position, Quaternion.identity);
            newText.transform.SetParent(parentGrid.transform);
            newText.transform.localScale = new Vector3(1, 1, 1);
            texts.Add(newText);
            actualCounter++;
        }
        counter = Mathf.Max(actualCounter, counter);

        foreach (Text x in texts)
        {
            x.text = "";
        }

        int count = 1;
        int whichOne = 0;

        foreach (Quests x in quest)
        {
            if (count % 20 == 0) whichOne++;
            if (x.questFailed)
            {
                texts[whichOne].text += string.Format("<size=25>{0}.</size> {1}  [{3}]\n\t<i><size=20>{2}</size></i>\n", count, x.questName, "<color=brown>✘ failed</color>", x.questType);
            }
            else if (x.questStatus)
            {
                texts[whichOne].text += string.Format("<size=25>{0}.</size> {1}\n\t<i><size=20>{2}</size></i>\n", count, x.questName, "<color=green>✓ complete</color>");
            }
            else if (x.currentAmount >= x.targetAmount)
            {
                texts[whichOne].text += string.Format("<size=25>{0}.</size> {1}  [{4}]\n\t<i><size=20>{2}  <color=orange>{3}</color></size></i>\n", count, x.questName, "Return to ", x.npcName, x.questType);
            }
            else
            {
                texts[whichOne].text += string.Format("<size=25>{0}.</size> {1}  [{5}]\n\t<i><size=20>{2} Progress: <color=orange>{3} / {4}</color></size></i>\n", count, x.questName, x.questDescription, Mathf.Clamp(x.currentAmount, 0, x.targetAmount), x.targetAmount, x.questType);
            }
            count++;
            if (!(count % 20 == 0))
            {
                texts[whichOne].text += string.Format("\n");
            }
        }
    }

    public static bool GetMobName(string monsterName)
    {
        bool isAny = false;

        foreach (Quests x in quest)
        {
            if (x.targetName == monsterName)
            {
                if (!(x.questStatus) && !(x.questFailed))
                {
                    if (x.currentAmount < x.targetAmount)
                    {
                        isAny = true;
                    }
                }
            }
        }

        if (isAny) return true;
        else return false;
    }

    public static void GetMobNameOnKill(string monsterName)
    {
        foreach (Quests x in quest)
        {
            if (x.targetName == monsterName)
            {
                if (!(x.questStatus) && !(x.questFailed))
                {
                    if (x.currentAmount < x.targetAmount)
                    {
                        x.currentAmount++;
                    }
                }
            }
        }
    }

    public static void RefreshManagerIfOpen()
    {
        if (quest.Count > 0 && FindObjectOfType<QuestLog>().flag == true)
        {
            FindObjectOfType<QuestLog>().ManagerShow();
        }
    }
}

public class Quests
{
    public string questName, questDescription, questType, targetName, npcName;
    public bool questStatus, questFailed, reputationTo;
    public int currentAmount, targetAmount, rewardGold, rewardExp;

    public Quests(string questName, string questDescription, string npcName, string questType, bool questStatus, int currentAmount, int targetAmount, string targetName, int rewardGold, int rewardExp, bool reputationTo)
    {
        this.questName = questName;
        this.questDescription = questDescription;
        this.npcName = npcName;
        this.questType = questType;
        this.questStatus = questStatus;
        this.currentAmount = currentAmount;
        this.targetAmount = targetAmount;
        this.targetName = targetName;
        this.rewardGold = rewardGold;
        this.rewardExp = rewardExp;
        this.reputationTo = reputationTo;
        questFailed = false;
    }
}
