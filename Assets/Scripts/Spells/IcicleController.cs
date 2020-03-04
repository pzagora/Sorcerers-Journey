using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleController : MonoBehaviour {

    private PLAYER player;

    public GameObject damageNumber;
    public Transform hitPoint;

    public Vector2 speed;
    Rigidbody2D rbody;

    private SFX_Manager sfxManager;

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

            if (Random.Range(0.1f, 85.0f) <= SkillPointManager.furyCritChanceCurrent)
            {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(SkillPointManager.icicleDamage * SkillPointManager.furyCritMulti);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "Critical " + (int)(SkillPointManager.icicleDamage * SkillPointManager.furyCritMulti);
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
            else
            {
                collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(SkillPointManager.icicleDamage);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = SkillPointManager.icicleDamage.ToString();
                clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            }
            player.fury.CurrentVal += 1;
        }
        Instantiate(Resources.Load("IcicleDamageBurst"), hitPoint.position, Quaternion.identity);
        if (collision.gameObject.tag != "Spell")
        {
            sfxManager.icicleHit.Play();
            Destroy(gameObject);
        }
    }
}
