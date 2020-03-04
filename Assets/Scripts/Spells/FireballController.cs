using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

    public Transform explosionSpawnPoint, particlePoint;

    public Vector2 speed;

    public GameObject fEx;
    public GameObject damageNumber;

    private SFX_Manager sfxManager;

    Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFX_Manager>();
        rbody.velocity = speed;
	}
	
	// Update is called once per frame
	void Update () {
        rbody.velocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damage = Random.Range(SkillPointManager.fireballMinDamage, SkillPointManager.fireballMaxDamage);
            Instantiate(fEx, explosionSpawnPoint.position, Quaternion.Euler(0, 0, Random.Range(1, 360)));
            if (Random.Range(0.1f, 75.0f) <= SkillPointManager.furyCritChanceCurrent)
            {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(damage * SkillPointManager.furyCritMulti);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "Critical " + (int)(damage * SkillPointManager.furyCritMulti);
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
            else
            {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(damage);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = damage.ToString();
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(Resources.Load("FireballDamageBurstNoCollision"), explosionSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load("FireballDamageBurst"), particlePoint.position, Quaternion.identity);
        }
        if (collision.gameObject.tag != "Spell")
        {
            sfxManager.fireballHit.Play();
            Destroy(gameObject);
        }
    }
}
