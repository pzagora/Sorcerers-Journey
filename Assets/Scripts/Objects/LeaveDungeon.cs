using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveDungeon : MonoBehaviour {

    public Transform monsterParent, chestParent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var children = new List<GameObject>();
            foreach (Transform child in monsterParent) children.Add(child.gameObject);
            if (children.Count == 0)
            {
                if (QuestLog.GetMobName("Dungeon"))
                {
                    QuestLog.GetMobNameOnKill("Dungeon");
                    QuestLog.RefreshManagerIfOpen();
                }

                FindObjectOfType<CurrencyManager>().ruby += 5;
                FindObjectOfType<CurrencyManager>().sapphire += 5;
                FindObjectOfType<CurrencyManager>().gold += 100;
                FindObjectOfType<ScoreManager>().scoreCount += 1000;
                FindObjectOfType<ScoreManager>().Scoring();
            }
            foreach (Transform child in chestParent) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));

            FindObjectOfType<PLAYER>().transform.position = new Vector3(PlayerPrefs.GetFloat("return_x"), PlayerPrefs.GetFloat("return_y"), PlayerPrefs.GetFloat("return_z"));
            FindObjectOfType<CameraFollow>().gameObject.transform.position = FindObjectOfType<PLAYER>().transform.position;
        }
    }
}
