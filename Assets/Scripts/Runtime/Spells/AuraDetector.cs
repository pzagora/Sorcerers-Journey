using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraDetector : MonoBehaviour {

    public GameObject auraSpell;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var clone = Instantiate(auraSpell, collision.gameObject.transform.position, Quaternion.identity);
            clone.transform.SetParent(collision.gameObject.transform);
        }
    }
}
