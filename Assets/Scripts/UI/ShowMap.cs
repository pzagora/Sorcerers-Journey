using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour {

    private CanvasGroup map;
    private bool flag, characterhaveMap;

    void Awake()
    {
        map = FindObjectOfType<ShowMap>().GetComponent<CanvasGroup>();    
    }

    // Use this for initialization
    void Start () {
        characterhaveMap = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (characterhaveMap)
            {
                if (flag != false)
                {
                    Time.timeScale = 1f;
                    HideMap();
                }
                else
                {
                    Time.timeScale = 0f;
                    ShowMapCanvas();
                }
            }
        }
    }

    public void ShowMapCanvas()
    {
        map.alpha = 1f;
        map.blocksRaycasts = true;
        flag = true;
    }

    public void HideMap()
    {
        map.alpha = 0f;
        map.blocksRaycasts = false;
        flag = false;
    }
}
