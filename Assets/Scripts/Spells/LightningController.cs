using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour {

    private PLAYER player;
    public GameObject particles;
    public GameObject damageNumber;
    public Transform particleSpawnPoint;

    private SFX_Manager sfxManager;

    public Vector2 speed;

    private int counter = 1;

    Rigidbody2D rbody;

    public void Awake()
    {
        player = FindObjectOfType<PLAYER>();

        sfxManager = FindObjectOfType<SFX_Manager>();
    }

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
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
            var damage = Random.Range(SkillPointManager.lightningMinDamage, SkillPointManager.lightningMaxDamage);
            if (Random.Range(0.1f, 50.0f) <= SkillPointManager.furyCritChanceCurrent)
            {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(damage * SkillPointManager.furyCritMulti);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "Critical " + (int)(damage * SkillPointManager.furyCritMulti);
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            } else {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(damage);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = damage.ToString();
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
            if (counter >= SkillPointManager.lightningPass) Destroy(gameObject);
            counter++;

            Instantiate(particles, particleSpawnPoint.position, particleSpawnPoint.rotation);

            player.fury.CurrentVal += 3;
        }
        Instantiate(Resources.Load("LightningDamageBurst"), particleSpawnPoint.position, particleSpawnPoint.rotation);
        sfxManager.lightningHit.Play();
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Spell") Destroy(gameObject);
    }
}
