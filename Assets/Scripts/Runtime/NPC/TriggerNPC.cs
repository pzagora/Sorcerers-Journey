using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerNPC : MonoBehaviour {

    public Text npcText;
    public GameObject npc;
    public Sprite npcSprite;
    public int townId;
    public string npcType;
    
    private Color alpha;
    private float currentLerp;
    private bool flag, talked;

    // Use this for initialization
    void Start () {
        npcSprite = npc.GetComponent<SpriteRenderer>().sprite;
        talked = false;
        flag = true;
        alpha = npcText.color;
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = npc.gameObject.transform.position;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currentLerp = alpha.a;
            npcText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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

            if (Input.GetKeyDown(KeyCode.F) && talked==false)
            {
                if (npcType == "GoldShop")
                {
                    npcText.text = "Trading";
                    GoldShop.GetNpcImage(npcSprite);
                    FindObjectOfType<GoldShop>().ShowGoldShop(townId);
                }
                else if (npcType == "ShardShop")
                {
                    npcText.text = "Trading";
                    ShardShop.GetNpcImage(npcSprite);
                    FindObjectOfType<ShardShop>().ShowShardShop(townId);
                }
                else if (npcType == "Quest")
                {
                    npcText.text = "Talking";
                    GetComponent<QuestingNpc>().TalkWIthQuestingNpc(townId);
                }
                else
                {
                    npcText.text = "Sorry, I'm too busy";
                }
            }

            npcText.color = alpha;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            npcText.gameObject.SetActive(false);
            talked = false;
            npcText.text = "Press F to talk";
        }
    }
}
