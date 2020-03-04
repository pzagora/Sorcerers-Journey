using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Explosion : MonoBehaviour {

    private PLAYER player;

    public GameObject damageNumber;

    public void Awake()
    {
        player = FindObjectOfType<PLAYER>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damage = Random.Range(SkillPointManager.fireballExplosionMinDamage, SkillPointManager.fireballExplosionMaxDamage);
            collision.gameObject.GetComponent<AI_HealthManager>().HurtAI(damage);

            var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
            clone.GetComponent<FloatingNumbers>().damageNumber = damage.ToString();
            clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        }
        Instantiate(Resources.Load("FireballDamageBurst"), gameObject.transform.position, Quaternion.identity);
        player.fury.CurrentVal += 2;
    }
}
