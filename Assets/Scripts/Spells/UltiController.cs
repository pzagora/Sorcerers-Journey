﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiController : MonoBehaviour {

    private PLAYER player;

    public GameObject damageNumber;

    public void Awake()
    {
        player = FindObjectOfType<PLAYER>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damage = Random.Range(SkillPointManager.ultiMinDamage, SkillPointManager.ultiMaxDamage);
            if (Random.Range(0.1f, 60.0f) <= SkillPointManager.furyCritChanceCurrent)
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

            player.fury.CurrentVal += 2.5f;
        }
        Instantiate(Resources.Load("DarkMatterDamageBurst"), gameObject.transform.position, Quaternion.identity);
    }
}
