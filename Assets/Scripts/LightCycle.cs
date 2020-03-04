using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour {

    public GameObject target;
    public GameObject playerLight;

	// Use this for initialization
	void Start () {
        FindObjectOfType<SFX_Manager>().daySound.Play();
        FindObjectOfType<SFX_Manager>().daySound.volume = 0.01f;
        FindObjectOfType<SFX_Manager>().nightSound.Play();
        FindObjectOfType<SFX_Manager>().nightSound.volume = 0;
        FindObjectOfType<SFX_Manager>().mobsAlerted.Play();
        FindObjectOfType<SFX_Manager>().mobsAlerted.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (target.transform.rotation.x < 0.75f && target.transform.rotation.x > - 0.75f)
        {
            FindObjectOfType<SFX_Manager>().daySound.volume = Mathf.Lerp(FindObjectOfType<SFX_Manager>().daySound.volume, 0.5f, 0.005f);
            FindObjectOfType<SFX_Manager>().nightSound.volume = Mathf.Lerp(FindObjectOfType<SFX_Manager>().nightSound.volume, 0, 0.005f);
        }
        else
        {
            FindObjectOfType<SFX_Manager>().daySound.volume = Mathf.Lerp(FindObjectOfType<SFX_Manager>().daySound.volume, 0, 0.005f);
            FindObjectOfType<SFX_Manager>().nightSound.volume = Mathf.Lerp(FindObjectOfType<SFX_Manager>().nightSound.volume, 0.5f, 0.005f);
        }

        target.transform.Rotate(Vector3.right * Time.deltaTime * 0.05f);
        playerLight.GetComponent<Light>().intensity = Map(Mathf.Abs(target.transform.rotation.x), 0, 180, 0, 200);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
