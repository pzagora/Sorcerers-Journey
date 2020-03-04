using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    public static bool activeChanger;

    private bool flag;
    private float currentLerp, currentLerp2, currentLerp3, rRnd, gRnd, bRnd;

    public Image[] reputation = new Image[3];
    public Image img;
    private Color colorActual;

	// Use this for initialization
	void Start () {
        activeChanger = false;
        colorActual = img.color;
        currentLerp = 1f;
        currentLerp2 = 1f;
        currentLerp3 = 1f;
    }
	
	// Update is called once per frame
	void Update () {

        if (activeChanger)
        {
            for (int i = 0; i < reputation.Length; i++)
            {
                if (reputation[i].fillAmount <= 0.33)
                {
                    reputation[i].color = new Color32(126, 30, 30, 255);
                }
                else if (reputation[i].fillAmount > 0.33 && reputation[i].fillAmount <= 0.66)
                {
                    reputation[i].color = new Color32(139, 139, 139, 255);
                }
                else
                {
                    reputation[i].color = new Color32(44, 44, 131, 255);
                }
            }

            if (flag)
            {
                colorActual.r = Mathf.Lerp(currentLerp, 0.2f, rRnd);
                currentLerp = colorActual.r;
                colorActual.g = Mathf.Lerp(currentLerp2, 0.2f, gRnd);
                currentLerp2 = colorActual.g;
                colorActual.b = Mathf.Lerp(currentLerp3, 0.2f, bRnd);
                currentLerp3 = colorActual.b;
                if (currentLerp <= 0.3f && currentLerp2 <= 0.3f && currentLerp3 <= 0.3f)
                {
                    rRnd = Random.Range(0.01f, 0.2f);
                    gRnd = Random.Range(0.01f, 0.1f);
                    bRnd = Random.Range(0.01f, 0.3f);
                    flag = false;
                }
            }
            else
            {
                colorActual.r = Mathf.Lerp(currentLerp, 1f, Random.Range(0.007f, 0.013f));
                currentLerp = colorActual.r;
                colorActual.g = Mathf.Lerp(currentLerp2, 1f, Random.Range(0.007f, 0.013f));
                currentLerp2 = colorActual.g;
                colorActual.b = Mathf.Lerp(currentLerp3, 1f, Random.Range(0.007f, 0.013f));
                currentLerp3 = colorActual.b;
                if (currentLerp >= 0.7f && currentLerp2 >= 0.7f && currentLerp3 >= 0.7f)
                {
                    rRnd = Random.Range(0.01f, 0.3f);
                    gRnd = Random.Range(0.01f, 0.2f);
                    bRnd = Random.Range(0.01f, 0.1f);
                    flag = true;
                }
            }
            img.color = colorActual;
        }
    }
}
