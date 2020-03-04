using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEye : MonoBehaviour {

    private bool direction;

	// Use this for initialization
	void Start () {
        direction = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!direction)
        {
            gameObject.transform.Rotate(0, Time.deltaTime * 60f, 0);
        }
        else
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
    }

    void OnEnable()
    {
        StartCoroutine("ChangeDirection");
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerable ChangeDirection()
    {
        direction = true;
        yield return new WaitForSeconds(1);
        direction = false;
    }
}
