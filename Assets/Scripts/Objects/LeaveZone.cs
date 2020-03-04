using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{

    public Transform monsterParent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var children = new List<GameObject>();
            foreach (Transform child in monsterParent)
            {
                children.Add(child.gameObject);
            }
            if (children.Count == 0)
            {
                FindObjectOfType<ScoreManager>().scoreCount += FindObjectOfType<PLAYER>().level * 750;
                FindObjectOfType<ScoreManager>().Scoring();
            }
            children.ForEach(child => Destroy(child));
        }
    }
}
