using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_HealthManager : MonoBehaviour {

    private ScoreManager scoreManager;
    private PLAYER player;
    private LevelUp levelUp;
    public int monsterTier;

    private AI_Shard_Spawner[] shard;

    public GameObject poof;
    private QuestMark qm;

    public string mobName;
    public float aiMaxHealth;
    public float aiCurrentHealth;
    public int scorePerKill, expPerKill;

    public Image aiCurrentHealthBar;
    private float fillAmount;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        player = FindObjectOfType<PLAYER>();
        levelUp = FindObjectOfType<LevelUp>();
        shard = FindObjectsOfType<AI_Shard_Spawner>();
    }

    // Use this for initialization
    void Start () {
        qm = GetComponentInChildren<QuestMark>(true);
        aiCurrentHealth = aiMaxHealth;

        if (mobName != "SHARDLING")
        {
            if (aiMaxHealth < 500)
            {
                expPerKill = (int)(aiMaxHealth / 3);
            }
            else
            {
                expPerKill = (int)(aiMaxHealth / 1.2);
            }

            if (mobName == "Giant Spider" || mobName == "Deadly Cobra" || mobName == "Cerberus" || mobName == "Behemoth" || mobName == "Medusa" || mobName == "Chimera" || mobName == "Sapphire Shard" || mobName == "Ruby Shard" || mobName == "Evil Shard")
            {
                scorePerKill = (int)((aiMaxHealth / 2) * 1.25);
            }
            else
            {
                scorePerKill = (int)(aiMaxHealth / 7);
            }
        }

        if (mobName.Length == 0)
        {
            mobName = "???";
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (QuestLog.GetMobName(mobName))
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

        fillAmount = Map(aiCurrentHealth, 0, aiMaxHealth, 0, 1);

        aiCurrentHealthBar.fillAmount = Mathf.Lerp(aiCurrentHealthBar.fillAmount, fillAmount, Time.deltaTime * 3);

        if (aiCurrentHealth <= 0)
        {
            scoreManager.scoreCount += scorePerKill;
            scoreManager.Scoring();

            if (mobName == "Giant Spider" || mobName == "Deadly Cobra" || mobName == "Cerberus" || mobName == "Behemoth" || mobName == "Medusa" || mobName == "Chimera")
            {
                FindObjectOfType<CurrencyManager>().shard += 2;
                FindObjectOfType<PLAYER>().mainProgress.CurrentVal++;
            }

            if (expPerKill != 0) levelUp.GetComponent<LevelUp>().ExpPass(expPerKill);

            Instantiate(poof, GetComponent<Transform>().position, Quaternion.identity);
            FindObjectOfType<SFX_Manager>().poof.Play();

            if (GetComponent<AI_Movement>() != null)
            {
                Bestiary.GetMonster(mobName, monsterTier, (int)aiMaxHealth, expPerKill, scorePerKill, GetComponent<AI_Movement>().sprite, transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite);
            }
            else if (GetComponent<AI_RangedMovement>() != null)
            {
                Bestiary.GetMonster(mobName, monsterTier, (int)aiMaxHealth, expPerKill, scorePerKill, GetComponent<AI_RangedMovement>().sprite, transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite);
            }
            else
            {
                Bestiary.GetMonster(mobName, monsterTier, (int)aiMaxHealth, expPerKill, scorePerKill, GetComponent<SpriteRenderer>().sprite, transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite);
            }

            Drop();

            if (QuestLog.GetMobName(mobName))
            {
                QuestLog.GetMobNameOnKill(mobName);
                QuestLog.RefreshManagerIfOpen();
            }

            Destroy(gameObject);
        }
	}

    public void HurtAI(float damageToGive)
    {
        aiCurrentHealth -= damageToGive;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    private void Drop()
    {
        int dropTimes = Random.Range(0, monsterTier);

        for(int i = 0; i < dropTimes; i++)
        {
            int randomDrop = Random.Range(0, 1000);
            if (randomDrop >= 995)
            {
                var clone = Instantiate(Resources.Load("Shard"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, monsterTier);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(0, 50));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 995 && randomDrop >= 945)
            {
                var clone = Instantiate(Resources.Load("Ruby"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, monsterTier * player.level);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(0, 50));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 945 && randomDrop >= 895)
            {
                var clone = Instantiate(Resources.Load("Sapphire"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, monsterTier * player.level);
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(0, 50));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
            else if (randomDrop < 895)
            {
                var clone = Instantiate(Resources.Load("Gold"), gameObject.transform.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Currency>().maxAmount = Random.Range(1, monsterTier * player.level) * 2;
                clone.GetComponent<Rigidbody2D>().AddForce((transform.position - new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), 0)).normalized * Random.Range(0, 50));
                clone.transform.parent = FindObjectOfType<CurrencyManager>().currencyParrent.transform;
            }
        }
    }

    void OnDestroy()
    {
        foreach (var item in shard)
        {
            item.EnemyKilled(this);
        }
    }
}
