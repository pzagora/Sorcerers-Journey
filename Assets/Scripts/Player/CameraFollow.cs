using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private PLAYER player;

    public Transform target;
    Camera cam;

	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {

        cam.orthographicSize = (Screen.height / 100f) / 5.25f;

        if (player.health.CurrentVal > 0) {
            if (target)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, 0.08f) + new Vector3(0, 0, -10);
            }
        }
	}
}
