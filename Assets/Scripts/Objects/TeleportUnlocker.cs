using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportUnlocker : MonoBehaviour {

    public string teleportZoneName;

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
            if (teleportZoneName == "Village")
            {
                FindObjectOfType<Teleport>().lockStatus[0] = true;
            }
            else if (teleportZoneName == "WickedForest")
            {
                FindObjectOfType<Teleport>().lockStatus[1] = true;
            }
            else if (teleportZoneName == "Oasis")
            {
                FindObjectOfType<Teleport>().lockStatus[2] = true;
            }
            else if (teleportZoneName == "Peninsula")
            {
                FindObjectOfType<Teleport>().lockStatus[3] = true;
            }
            FindObjectOfType<Teleport>().ShowTeleportManager();
        }
    }
}
