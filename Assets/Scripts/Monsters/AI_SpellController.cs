using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SpellController : MonoBehaviour {

    public GameObject damageNumber;
    public int minDamage, maxDamage, critChance;

    public Vector2 speed;
    private Rigidbody2D rbody;
    public GameObject spellHitEffect;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var damage = Random.Range(minDamage, maxDamage);
            if (Random.Range(0, 100) <= critChance)
            {
                collision.gameObject.GetComponent<Player_Move>().HurtPlayer(damage * 2);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "Critical " + (damage * 2);
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
            else
            {
                collision.gameObject.GetComponent<Player_Move>().HurtPlayer(damage);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = damage.ToString();
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
        }
        var clone2 = Instantiate(spellHitEffect, gameObject.transform.Find("HitPoint").transform.position, Quaternion.identity);
        clone2.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        Destroy(gameObject);
    }
}
