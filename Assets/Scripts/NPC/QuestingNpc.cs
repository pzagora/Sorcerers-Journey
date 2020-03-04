using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestingNpc : MonoBehaviour
{
    public string npcName;
    public string[] questName;
    public string[] questDescription;
    public string[] questType;
    public int[] amountToKill;
    public string[] monsterTokill;
    public int[] goldReward;
    public int[] expReward;
    public bool[] mainQuest;
    [TextArea] public string[] dialogue;

    private bool town;
    private int questAmount;
    private int questNumber;
    private bool[] locked;
    private bool[] questTaken;
    private QuestLog questLog;
    private int iterator;

    // Use this for initialization
    void Start()
    {
        questAmount = Mathf.Min(questName.Length, Mathf.Min(questDescription.Length, Mathf.Min(questType.Length, Mathf.Min(amountToKill.Length, Mathf.Min(monsterTokill.Length, Mathf.Min(goldReward.Length, Mathf.Min(expReward.Length, Mathf.Min(mainQuest.Length, dialogue.Length))))))));
        locked = new bool[questAmount];
        questTaken = new bool[questAmount];
        iterator = 0;
        for (int i = 0; i < locked.Length; i++)
        {
            locked[i] = true;
        }
        for (int i = 0; i < questTaken.Length; i++)
        {
            questTaken[i] = false;
        }
        questLog = FindObjectOfType<QuestLog>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TalkWIthQuestingNpc(int townId)
    {
        if (QuestLog.GetMobName(npcName))
        {
            QuestLog.GetMobNameOnKill(npcName);
            QuestLog.RefreshManagerIfOpen();
        }

        if (iterator < questAmount)
        {
            if (townId == 0)
            {
                town = true;
            }
            else
            {
                town = false;
            }
            if (!questTaken[iterator]) questNumber = QuestLog.GetQuest(questName[iterator], questDescription[iterator], npcName, questType[iterator], false, 0, amountToKill[iterator], monsterTokill[iterator], goldReward[iterator], expReward[iterator], town) - 1;
            questTaken[iterator] = true;
            questLog.npcNameText.text = npcName;
            questLog.questNameText.text = QuestLog.quest[questNumber].questName;
            questLog.questDescriptionText.text = dialogue[iterator];
            questLog.rewardOneText.text = goldReward[iterator].ToString();
            questLog.rewardTwoText.text = expReward[iterator].ToString();
            questLog.ManagerShowByNpc();
            if (QuestLog.quest[questNumber].currentAmount >= QuestLog.quest[questNumber].targetAmount && QuestLog.quest[questNumber].questStatus != true)
            {
                locked[iterator] = false;
                QuestLog.quest[questNumber].questStatus = true;
                FindObjectOfType<CurrencyManager>().gold += QuestLog.quest[questNumber].rewardGold;
                FindObjectOfType<LevelUp>().ExpPass(QuestLog.quest[questNumber].rewardExp);
                FindObjectOfType<PLAYER>().reputation[townId].CurrentVal += 2;
                if (mainQuest[iterator])
                {
                    FindObjectOfType<PLAYER>().movementSpeed.CurrentVal++;
                    FindObjectOfType<PLAYER>().mainProgress.CurrentVal++;
                }
            }
            if (!locked[iterator])
            {
                iterator++;
                TalkWIthQuestingNpc(townId);
            }
        }
        else
        {
            questLog.npcNameText.text = npcName;
            questLog.questNameText.text = "---";
            questLog.questDescriptionText.text = "Currently I don't have more missions for you traveller..";
            questLog.rewardOneText.text = "---";
            questLog.rewardTwoText.text = "---";
            questLog.ManagerShowByNpc();
        }
    }
}
