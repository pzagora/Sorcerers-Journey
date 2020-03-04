using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiDetector : MonoBehaviour {

    public GameObject ultiSpell;

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
            var clone = Instantiate(ultiSpell, collision.gameObject.transform.position, Quaternion.identity);
            clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        }
    }
}
