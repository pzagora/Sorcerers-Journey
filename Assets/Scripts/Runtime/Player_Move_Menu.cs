using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Menu : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetFloat("input_y", -1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Time.timeScale = 1f;

        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("iswalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);

            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            anim.SetBool("iswalking", false);
            GetComponent<AudioSource>().Pause();
        }

        rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);

    }
}
