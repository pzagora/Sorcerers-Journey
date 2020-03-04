using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {
    
    public Text chestText;
    public int chestTier;
    
    private PLAYER player;
    private QuestMark qm;
    private Color alpha;
    private float currentLerp;
    private bool flag, used;

    void Awake()
    {
        player = FindObjectOfType<PLAYER>();
        gameObject.GetComponent<Animator>().speed = 0f;
    }

    // Use this for initialization
    void Start ()
    {
        qm = GetComponentInChildren<QuestMark>(true);
        used = false;
        flag = true;
        alpha = chestText.color;
        currentLerp = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (QuestLog.GetMobName("Chest"))
        {
            if (qm != null)
            {
                qm.gameObject.SetActive(true);
            }
        }
        else
        {
            if (qm != null)
            {
                qm.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currentLerp = alpha.a;
            chestText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (flag)
            {
                alpha.a = Mathf.Lerp(currentLerp, 0.4f, 0.05f);
                currentLerp = alpha.a;
                if (currentLerp <= 0.5f) flag = false;
            }
            else
            {
                alpha.a = Mathf.Lerp(currentLerp, 1f, 0.05f);
                currentLerp = alpha.a;
                if (currentLerp >= 0.9f) flag = true;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                used = true;
                alpha.a = 1f;
                gameObject.GetComponent<Animator>().speed = 0.5f;
                chestText.text = "Opened..";
                Destroy(gameObject.GetComponent<CircleCollider2D>());
                Destroy(gameObject, 1.5f);
            }

            chestText.color = alpha;
        }
    }

    private void OnDestroy()
    {
        if (used==true) ChestDrop();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chestText.gameObject.SetActive(false);
        }
    }

    private void ChestDrop()
    {
        int dropTimes = Random.Range(5, Mathf.Max(5, player.level * 2));

        for (int i = 0; i < dropTimes; i++)
        {
            int randomDrop = Random.Range(0, 1000);
            if (randomDrop >= 990)
            {
                var clone = Instantiate(Resources.Load("Shard"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, chestTier);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(5, 10));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 990 && randomDrop >= 940)
            {
                var clone = Instantiate(Resources.Load("Ruby"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, chestTier * player.level);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(5, 10));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 940 && randomDrop >= 890)
            {
                var clone = Instantiate(Resources.Load("Sapphire"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, chestTier * player.level);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(5, 10));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 890)
            {
                var clone = Instantiate(Resources.Load("Gold"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, Mathf.Max(chestTier * player.level, chestTier + player.level)) * 2;
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(5, 10));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
        }

        if (QuestLog.GetMobName("Chest"))
        {
            QuestLog.GetMobNameOnKill("Chest");
            QuestLog.RefreshManagerIfOpen();
        }

        Instantiate(Resources.Load("Poof"), gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<SFX_Manager>().poof.Play();
    }
}
