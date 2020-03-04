using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour {

    public Vector2 speed;
    private PLAYER player;
    public GameObject damageNumber, particles;

    public float delay;

    private SFX_Manager sfxManager;
    private int counter;

    bool flag;

    Rigidbody2D rbody;

    // Use this for initialization
    void Start()
    {
        delay = 1.1f;
        flag = true;
        counter = 0;
        rbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PLAYER>();
        sfxManager = FindObjectOfType<SFX_Manager>();
        rbody.velocity = speed;
        Invoke("DestroyMe", delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            rbody.velocity = speed;
        }
        else
        {
            rbody.velocity = -speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            sfxManager.lightningHit.Play();

            var damage = Random.Range(SkillPointManager.sparkMinDamage, SkillPointManager.sparkMaxDamage);
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
            counter++;

            Instantiate(particles, collision.transform.position, Quaternion.identity);

            player.fury.CurrentVal += 0.5f;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (SkillPointManager.skillLevel[4] > 2)
            {
                CancelInvoke("DestroyMe");
                Invoke("DestroyMe", delay);
                if (counter >= SkillPointManager.sparkBounce) Destroy(gameObject);
                counter++;
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Spell")
        {
            CancelInvoke("DestroyMe");
            Invoke("DestroyMe", delay);
            if (counter >= SkillPointManager.sparkBounce) Destroy(gameObject);
            counter++;
            if( flag)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
        }
        Instantiate(Resources.Load("LightningDamageBurst"), gameObject.transform.position, Quaternion.identity);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
