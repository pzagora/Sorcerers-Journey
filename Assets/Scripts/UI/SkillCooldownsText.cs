using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCooldownsText : MonoBehaviour {

    [SerializeField]
    private PLAYER player;

    public GameObject[] cd = new GameObject[10];

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < cd.Length; i++)
        {
            cd[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cd.Length; i++)
        {
            if (player.gameObject.GetComponent<Player_Move>().timeStamp[i] > Time.time) cd[i].SetActive(true); else cd[i].SetActive(false);
        }
    }
}
