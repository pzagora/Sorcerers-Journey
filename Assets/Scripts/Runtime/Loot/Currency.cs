using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour {

    private CurrencyManager cMan;
    private SFX_Manager sfxMan;

    public string currencyType;
    public int minAmount, maxAmount;
    public GameObject amountText;

    void Awake()
    {
        sfxMan = FindObjectOfType<SFX_Manager>();
        cMan = FindObjectOfType<CurrencyManager>();
    }

    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            sfxMan.currencyPickup.Play();

            int rand = Random.Range(minAmount, maxAmount);
            if (currencyType == "Gold")
            {
                cMan.gold += rand;

                var clone = Instantiate(amountText, gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "+" + rand;

                Destroy(gameObject);
            }
            else if (currencyType == "Sapphire")
            {
                cMan.sapphire += rand;

                var clone = Instantiate(amountText, gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "+" + rand;

                Destroy(gameObject);
            }
            else if (currencyType == "Ruby")
            {

                cMan.ruby += rand;

                var clone = Instantiate(amountText, gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "+" + rand;

                Destroy(gameObject);
            }
            else if (currencyType == "Shard")
            {

                cMan.shard += rand;

                var clone = Instantiate(amountText, gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "+" + rand;

                Destroy(gameObject);
            }
        }
    }
}
