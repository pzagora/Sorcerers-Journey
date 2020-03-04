using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    public Button[] teleportButton = new Button[4];
    public GameObject[] tpLocked = new GameObject[4];
    public Transform[] destination = new Transform[4];
    public CanvasGroup teleportManager;

    public bool[] lockStatus = new bool[4];

	// Use this for initialization
	void Start () {
        for (int i = 0; i < lockStatus.Length; i++)
        {
            lockStatus[i] = false;
        }
        HideTeleportManager();
	}
	
	// Update is called once per frame
	void Update () {
        if (teleportManager.alpha == 1f)
        {
            for (int i = 0; i < lockStatus.Length; i++)
            {
                if (lockStatus[i] == true && tpLocked[i].activeSelf)
                {
                    tpLocked[i].gameObject.SetActive(false);
                    teleportButton[i].interactable = true;
                }
            }
        }
	}

    public void ShowTeleportManager()
    {
        teleportManager.alpha = 1f;
        teleportManager.blocksRaycasts = true;
    }

    public void HideTeleportManager()
    {
        teleportManager.alpha = 0f;
        teleportManager.blocksRaycasts = false;
    }

    public void TeleportToVillage()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        FindObjectOfType<PLAYER>().gameObject.transform.position = destination[0].position;
        FindObjectOfType<CameraFollow>().gameObject.transform.position = destination[0].position;
        Instantiate(Resources.Load("Poof"), FindObjectOfType<PLAYER>().gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<SFX_Manager>().poof.Play();
    }

    public void TeleportToWickedForest()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        FindObjectOfType<PLAYER>().gameObject.transform.position = destination[1].position;
        FindObjectOfType<CameraFollow>().gameObject.transform.position = destination[1].position;
        Instantiate(Resources.Load("Poof"), FindObjectOfType<PLAYER>().gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<SFX_Manager>().poof.Play();
    }

    public void TeleportToOasis()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        FindObjectOfType<PLAYER>().gameObject.transform.position = destination[2].position;
        FindObjectOfType<CameraFollow>().gameObject.transform.position = destination[2].position;
        Instantiate(Resources.Load("Poof"), FindObjectOfType<PLAYER>().gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<SFX_Manager>().poof.Play();
    }

    public void TeleportToPeninsula()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        FindObjectOfType<PLAYER>().gameObject.transform.position = destination[3].position;
        FindObjectOfType<CameraFollow>().gameObject.transform.position = destination[3].position;
        Instantiate(Resources.Load("Poof"), FindObjectOfType<PLAYER>().gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<SFX_Manager>().poof.Play();
    }
}
