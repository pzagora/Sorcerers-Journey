using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZone : MonoBehaviour {

    public Transform monsterParent;
    public Transform spawnParent;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
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
                foreach (Transform item in spawnParent)
                {
                    FindObjectOfType<Stairs>().MonsterInstantiator(item, item.gameObject.GetComponent<SpawnPointTier>().thisSpawnPointTier).transform.SetParent(monsterParent);
                }
            }
        }

        if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(Resources.Load("Poof"), collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
