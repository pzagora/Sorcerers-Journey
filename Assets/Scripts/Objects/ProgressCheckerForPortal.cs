using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCheckerForPortal : MonoBehaviour {

    public GameObject closedPortal;

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
            if(FindObjectOfType<PLAYER>().mainProgress.CurrentVal == FindObjectOfType<PLAYER>().mainProgress.MaxVal)
            {
                Destroy(closedPortal);
            }
        }
    }
}
