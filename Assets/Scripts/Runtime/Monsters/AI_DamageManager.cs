using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DamageManager : MonoBehaviour {

    public int aiCritChance;
    public float boltSpeed;
    public float minCastSpeed, maxCastSpeed;

    public GameObject spell;

    public GameObject damageNumber;
    private GameObject player;

    private int aiMinDamage, aiMaxDamage;
    private Rigidbody2D rbody;
    private Vector2 bolt;

    public bool isRanged;
    private float range;
    private float timeBetweenCasts;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        aiMinDamage = (GetComponent<AI_HealthManager>().monsterTier * 6);
        aiMaxDamage = ((GetComponent<AI_HealthManager>().monsterTier * 9) + (FindObjectOfType<PLAYER>().level * 2));
        if (isRanged)
        {
            aiMinDamage /= 2;
            aiMaxDamage /= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRanged && gameObject.GetComponent<AI_RangedMovement>() != null)
        {
            range = Vector2.Distance(transform.position, player.transform.position);

            if (range <= 1.5f && range > 0.3f && gameObject.GetComponent<AI_RangedMovement>().ReturnAlertedStatus())
            {
                if (timeBetweenCasts < Time.time)
                {
                    timeBetweenCasts = Time.time + Random.Range(minCastSpeed, maxCastSpeed);
                    
                    bolt = new Vector2((transform.position.x - player.transform.position.x) * boltSpeed, (transform.position.y - player.transform.position.y) * boltSpeed);

                    Vector3 v = transform.position - player.transform.position;

                    ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), 0, 0).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);

                    if (player.GetComponent<PLAYER>().level >= 10)
                    {
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(1, 3), 0).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(1, 3), 0).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                    }

                    if (player.GetComponent<PLAYER>().level >= 15)
                    {
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), 0, Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), 0, Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);

                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(0, 3), Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(0, 3), Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                    }

                    if (player.GetComponent<PLAYER>().level >= 25)
                    {
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(1, 3), Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                        ChangeBolt(Instantiate(spell, rbody.position, Quaternion.identity), Random.Range(1, 3), Random.Range(1, 3)).transform.rotation = Quaternion.FromToRotation(Vector3.right, -v);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var damage = Random.Range(aiMinDamage, aiMaxDamage);

            rbody.AddForce(-(transform.position + collision.transform.position).normalized * 150);

            if (Random.Range(0, 100) <= aiCritChance)
            {
                collision.gameObject.GetComponent<Player_Move>().HurtPlayer(damage * 2);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = "Critical " + (damage * 2);
            }
            else
            {
                collision.gameObject.GetComponent<Player_Move>().HurtPlayer(damage);

                var clone = Instantiate(damageNumber, collision.gameObject.transform.position, Quaternion.identity);
                clone.GetComponent<FloatingNumbers>().damageNumber = damage.ToString();
            }
        }
    }

    private GameObject ChangeBolt(GameObject clone, int i, int j)
    {
        clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        clone.GetComponent<AI_SpellController>().speed.x = -(bolt.normalized * boltSpeed).x;
        clone.GetComponent<AI_SpellController>().speed.y = -(bolt.normalized * boltSpeed).y;

        if (i == 1)
        {
            clone.GetComponent<AI_SpellController>().speed.x -= 0.4f;
            clone.GetComponent<AI_SpellController>().speed.y += 0.4f;
        }
        else if (i == 2)
        {
            clone.GetComponent<AI_SpellController>().speed.x += 0.4f;
            clone.GetComponent<AI_SpellController>().speed.y -= 0.4f;
        }

        if (j == 1)
        {
            clone.GetComponent<AI_SpellController>().speed /= 1.2f;
        }
        else if (j == 2)
        {
            clone.GetComponent<AI_SpellController>().speed *= 1.2f;
        }

        clone.GetComponent<AI_SpellController>().critChance = aiCritChance;
        clone.GetComponent<AI_SpellController>().minDamage = aiMinDamage;
        clone.GetComponent<AI_SpellController>().maxDamage = aiMaxDamage;

        return clone;
    }
}
