using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour {

    private PLAYER player;
    private int lightningCounter;
    private float movementSpeed;

    public Text cannot;
    public GameObject bottomFireball, topFireball, leftFireball, rightFireball;
    public GameObject bottomIcicle, topIcicle, leftIcicle, rightIcicle;
    public GameObject bottomLightning, topLightning, leftLightning, rightLightning;
    public GameObject fireNova, iceNova, spark, aura, ulti;
    public GameObject sparksOrange, sparksBlue;
    public GameObject[] padlock = new GameObject[9];
    public GameObject activeManaBoost, activeAura;
    public bool[] locked;
    private Transform castPosition;

    public bool manaBoost, auraOn;

    private SFX_Manager sfqManager;

    public float[] timeStamp;

    System.Random rndRot = new System.Random();

    private int facing=2;

    Rigidbody2D rbody;
    Animator anim;

    private void Awake()
    {
        player = FindObjectOfType<PLAYER>();

        lightningCounter = 0;
        locked = new bool[10];
        timeStamp = new float[10];
        locked[0] = false;
        for (int i = 1; i < 10; i++)
        {
            locked[i] = true;
        }
    }

    // Use this for initialization
    void Start () {

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sfqManager = FindObjectOfType<SFX_Manager>();

        anim.SetFloat("input_y", -1);

        castPosition = transform.FindChild("castPosition");

        manaBoost = false;
        auraOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        movementSpeed = (player.movementSpeed.CurrentVal / 100) + 0.6f;

        if (auraOn)
        {
            activeAura.SetActive(true);
        }
        else
        {
            activeAura.SetActive(false);
        }

        if (manaBoost)
        {
            activeManaBoost.SetActive(true);
        }
        else
        {
            activeManaBoost.SetActive(false);
        }

        if (player.fury.CurrentVal == player.fury.MaxVal && (timeStamp[3] > 5 + Time.time))
        {
            timeStamp[3] = 5 + Time.time;
        }

        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movementSpeed;

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("iswalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);

            if (movement_vector.y > 0) facing = 1; else if(movement_vector.y < 0) facing = 2; else if (movement_vector.x > 0) facing = 3; else if (movement_vector.x < 0) facing = 4;

            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            anim.SetBool("iswalking", false);
            GetComponent<AudioSource>().Pause();
        }

        rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (locked[0])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[0] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.icicleManaCost || (SkillPointManager.icicleManaCost >= player.mana.MaxVal && player.mana.CurrentVal == player.mana.MaxVal)))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastIcicle();
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (locked[1])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[1] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.fireballManaCost))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastFireball();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (locked[2])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[2] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.lightningManaCost))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastLightning();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (locked[3])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[3] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.fireNovaManaCost))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastFireNova();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (locked[4])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[4] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.sparkManaCost))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastSpark();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (locked[5])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[5] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= SkillPointManager.iceNovaManaCost))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastIceNova();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (locked[6])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[6] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (!(player.mana.CurrentVal >= player.mana.MaxVal))
                    {
                        StartCoroutine(WaitAndPrint("Not enough mana"));
                    }
                    else
                    {
                        CastUlti();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (locked[7])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[7] <= Time.time))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    player.GetComponent<HealController>().Heal();
                    timeStamp[7] = Time.time + SkillPointManager.spellCooldowns[7];
                    TimeStamper();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (locked[8])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[8] <= Time.time || manaBoost))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (manaBoost)
                    {
                        manaBoost = false;
                        timeStamp[8] = Time.time + SkillPointManager.spellCooldowns[8];
                        TimeStamper();
                    }
                    else
                    {
                        manaBoost = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (locked[9])
            {
                StartCoroutine(WaitAndPrint("Skill is locked"));
            }
            else
            {
                if (!(timeStamp[9] <= Time.time || auraOn))
                {
                    StartCoroutine(WaitAndPrint("Skill is on cooldown"));
                }
                else
                {
                    if (auraOn)
                    {
                        StopCoroutine(CastAura());
                        Destroy(GameObject.FindWithTag("AuraRes"));
                        timeStamp[9] = Time.time + SkillPointManager.spellCooldowns[9];
                        TimeStamper();
                        auraOn = false;
                    }
                    else
                    {
                        StartCoroutine(CastAura());
                        auraOn = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<CurrencyManager>().gold += 50;
            FindObjectOfType<CurrencyManager>().sapphire += 50;
            FindObjectOfType<CurrencyManager>().ruby += 50;

            FindObjectOfType<CurrencyManager>().shard += 50;

            player.level += 5;
        } 

    }

    void CastIcicle()
    {
        if (facing == 1)
        {
            Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 90));
            if(SkillPointManager.skillLevel[0] > 1)
            {
                var clone = Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 105));
                clone.GetComponent<IcicleController>().speed.x -= 0.3f;
                var clone2 = Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 75));
                clone2.GetComponent<IcicleController>().speed.x += 0.3f;
                if (SkillPointManager.skillLevel[0] > 2)
                {
                    var clone3 = Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 105));
                    clone3.GetComponent<IcicleController>().speed.x -= 0.3f;
                    clone3.GetComponent<IcicleController>().speed *= 2;
                    var clone4 = Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 75));
                    clone4.GetComponent<IcicleController>().speed.x += 0.3f;
                    clone4.GetComponent<IcicleController>().speed *= 2;
                    var clone5 = Instantiate(topIcicle, castPosition.position, Quaternion.Euler(0, 0, 90));
                    clone5.GetComponent<IcicleController>().speed *= 2;
                }
            }
        }
        else if (facing == 2)
        {
            Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 270));
            if (SkillPointManager.skillLevel[0] > 1)
            {
                var clone = Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 255));
                clone.GetComponent<IcicleController>().speed.x -= 0.3f;
                var clone2 = Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 285));
                clone2.GetComponent<IcicleController>().speed.x += 0.3f;
                if (SkillPointManager.skillLevel[0] > 2)
                {
                    var clone3 = Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 255));
                    clone3.GetComponent<IcicleController>().speed.x -= 0.3f;
                    clone3.GetComponent<IcicleController>().speed *= 2;
                    var clone4 = Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 285));
                    clone4.GetComponent<IcicleController>().speed.x += 0.3f;
                    clone4.GetComponent<IcicleController>().speed *= 2;
                    var clone5 = Instantiate(bottomIcicle, castPosition.position, Quaternion.Euler(0, 0, 270));
                    clone5.GetComponent<IcicleController>().speed *= 2;
                }
            }
        }
        else if (facing == 3)
        {
            Instantiate(rightIcicle, castPosition.position, Quaternion.identity);
            if (SkillPointManager.skillLevel[0] > 1)
            {
                var clone = Instantiate(rightIcicle, castPosition.position, Quaternion.Euler(0, 0, 345));
                clone.GetComponent<IcicleController>().speed.y -= 0.3f;
                var clone2 = Instantiate(rightIcicle, castPosition.position, Quaternion.Euler(0, 0, 15));
                clone2.GetComponent<IcicleController>().speed.y += 0.3f;
                if (SkillPointManager.skillLevel[0] > 2)
                {
                    var clone3 = Instantiate(rightIcicle, castPosition.position, Quaternion.Euler(0, 0, 345));
                    clone3.GetComponent<IcicleController>().speed.y -= 0.3f;
                    clone3.GetComponent<IcicleController>().speed *= 2;
                    var clone4 = Instantiate(rightIcicle, castPosition.position, Quaternion.Euler(0, 0, 15));
                    clone4.GetComponent<IcicleController>().speed.y += 0.3f;
                    clone4.GetComponent<IcicleController>().speed *= 2;
                    var clone5 = Instantiate(rightIcicle, castPosition.position, Quaternion.identity);
                    clone5.GetComponent<IcicleController>().speed *= 2;
                }
            }
        }
        else if (facing == 4)
        {
            Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 180));
            if (SkillPointManager.skillLevel[0] > 1)
            {
                var clone = Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 195));
                clone.GetComponent<IcicleController>().speed.y -= 0.3f;
                var clone2 = Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 165));
                clone2.GetComponent<IcicleController>().speed.y += 0.3f;
                if (SkillPointManager.skillLevel[0] > 2)
                {
                    var clone3 = Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 195));
                    clone3.GetComponent<IcicleController>().speed.y -= 0.3f;
                    clone3.GetComponent<IcicleController>().speed *= 2;
                    var clone4 = Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 165));
                    clone4.GetComponent<IcicleController>().speed.y += 0.3f;
                    clone4.GetComponent<IcicleController>().speed *= 2;
                    var clone5 = Instantiate(leftIcicle, castPosition.position, Quaternion.Euler(0, 0, 180));
                    clone5.GetComponent<IcicleController>().speed *= 2;
                }
            }
        }
        TimeStamper();
        player.mana.CurrentVal -= SkillPointManager.icicleManaCost;
    }

    void CastFireball()
    {
        if (facing == 1)
        {
            Instantiate(topFireball, castPosition.position, Quaternion.Euler(0, 0, 90));
            Instantiate(sparksOrange, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[1] > 1)
            {
                var clone = Instantiate(topFireball, castPosition.position, Quaternion.Euler(0, 0, 105));
                clone.GetComponent<FireballController>().speed.x -= 0.4f;
                var clone2 = Instantiate(topFireball, castPosition.position, Quaternion.Euler(0, 0, 75));
                clone2.GetComponent<FireballController>().speed.x += 0.4f;
                if (SkillPointManager.skillLevel[1] > 2)
                {
                    var clone3 = Instantiate(topFireball, castPosition.position, Quaternion.Euler(0, 0, 120));
                    clone3.GetComponent<FireballController>().speed.x -= 0.8f;
                    var clone4 = Instantiate(topFireball, castPosition.position, Quaternion.Euler(0, 0, 60));
                    clone4.GetComponent<FireballController>().speed.x += 0.8f;
                }
            }
        }
        else if (facing == 2)
        {
            Instantiate(bottomFireball, castPosition.position, Quaternion.Euler(0, 0, 270));
            Instantiate(sparksOrange, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[1] > 1)
            {
                var clone = Instantiate(bottomFireball, castPosition.position, Quaternion.Euler(0, 0, 255));
                clone.GetComponent<FireballController>().speed.x -= 0.4f;
                var clone2 = Instantiate(bottomFireball, castPosition.position, Quaternion.Euler(0, 0, 285));
                clone2.GetComponent<FireballController>().speed.x += 0.4f;
                if (SkillPointManager.skillLevel[1] > 2)
                {
                    var clone3 = Instantiate(bottomFireball, castPosition.position, Quaternion.Euler(0, 0, 240));
                    clone3.GetComponent<FireballController>().speed.x -= 0.8f;
                    var clone4 = Instantiate(bottomFireball, castPosition.position, Quaternion.Euler(0, 0, 300));
                    clone4.GetComponent<FireballController>().speed.x += 0.8f;
                }
            }
        }
        else if (facing == 3)
        {
            Instantiate(rightFireball, castPosition.position, Quaternion.identity);
            Instantiate(sparksOrange, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[1] > 1)
            {
                var clone = Instantiate(rightFireball, castPosition.position, Quaternion.Euler(0, 0, 345));
                clone.GetComponent<FireballController>().speed.y -= 0.4f;
                var clone2 = Instantiate(rightFireball, castPosition.position, Quaternion.Euler(0, 0, 15));
                clone2.GetComponent<FireballController>().speed.y += 0.4f;
                if (SkillPointManager.skillLevel[1] > 2)
                {
                    var clone3 = Instantiate(rightFireball, castPosition.position, Quaternion.Euler(0, 0, 330));
                    clone3.GetComponent<FireballController>().speed.y -= 0.8f;
                    var clone4 = Instantiate(rightFireball, castPosition.position, Quaternion.Euler(0, 0, 30));
                    clone4.GetComponent<FireballController>().speed.y += 0.8f;
                }
            }
        }
        else if (facing == 4)
        {
            Instantiate(leftFireball, castPosition.position, Quaternion.Euler(0, 0, 180));
            Instantiate(sparksOrange, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[1] > 1)
            {
                var clone = Instantiate(leftFireball, castPosition.position, Quaternion.Euler(0, 0, 195));
                clone.GetComponent<FireballController>().speed.y -= 0.4f;
                var clone2 = Instantiate(leftFireball, castPosition.position, Quaternion.Euler(0, 0, 165));
                clone2.GetComponent<FireballController>().speed.y += 0.4f;
                if (SkillPointManager.skillLevel[1] > 1)
                {
                    var clone3 = Instantiate(leftFireball, castPosition.position, Quaternion.Euler(0, 0, 210));
                    clone3.GetComponent<FireballController>().speed.y -= 0.8f;
                    var clone4 = Instantiate(leftFireball, castPosition.position, Quaternion.Euler(0, 0, 150));
                    clone4.GetComponent<FireballController>().speed.y += 0.8f;
                }
            }
        }
        timeStamp[1] = Time.time + SkillPointManager.spellCooldowns[1];
        TimeStamper();
        player.mana.CurrentVal -= SkillPointManager.fireballManaCost;
    }

    void CastLightning()
    {
        if (SkillPointManager.skillLevel[2] > 2) lightningCounter++;
        if (facing == 1 || lightningCounter >= 3)
        {
            Instantiate(topLightning, castPosition.position, Quaternion.Euler(0, 0, 90));
            Instantiate(sparksBlue, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[2] > 1)
            {
                var clone = Instantiate(topLightning, castPosition.position, Quaternion.Euler(0, 0, 105));
                clone.GetComponent<LightningController>().speed.x -= 0.5f;
                var clone2 = Instantiate(topLightning, castPosition.position, Quaternion.Euler(0, 0, 75));
                clone2.GetComponent<LightningController>().speed.x += 0.5f;
            }
        }
        if (facing == 2 || lightningCounter >= 3)
        {
            Instantiate(bottomLightning, castPosition.position, Quaternion.Euler(0, 0, 270));
            Instantiate(sparksBlue, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[2] > 1)
            {
                var clone = Instantiate(bottomLightning, castPosition.position, Quaternion.Euler(0, 0, 255));
                clone.GetComponent<LightningController>().speed.x -= 0.5f;
                var clone2 = Instantiate(bottomLightning, castPosition.position, Quaternion.Euler(0, 0, 285));
                clone2.GetComponent<LightningController>().speed.x += 0.5f;
            }
        }
        if (facing == 3 || lightningCounter >= 3)
        {
            Instantiate(rightLightning, castPosition.position, Quaternion.identity);
            Instantiate(sparksBlue, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[2] > 1)
            {
                var clone = Instantiate(rightLightning, castPosition.position, Quaternion.Euler(0, 0, 345));
                clone.GetComponent<LightningController>().speed.y -= 0.5f;
                var clone2 = Instantiate(rightLightning, castPosition.position, Quaternion.Euler(0, 0, 15));
                clone2.GetComponent<LightningController>().speed.y += 0.5f;
            }
        }
        if (facing == 4 || lightningCounter >= 3)
        {
            Instantiate(leftLightning, castPosition.position, Quaternion.Euler(0, 0, 180));
            Instantiate(sparksBlue, rbody.position, Quaternion.Euler(0, 0, rndRot.Next(1, 360)));
            if (SkillPointManager.skillLevel[2] > 1)
            {
                var clone = Instantiate(leftLightning, castPosition.position, Quaternion.Euler(0, 0, 195));
                clone.GetComponent<LightningController>().speed.y -= 0.5f;
                var clone2 = Instantiate(leftLightning, castPosition.position, Quaternion.Euler(0, 0, 165));
                clone2.GetComponent<LightningController>().speed.y += 0.5f;
            }
        }

        if (lightningCounter >= 3) lightningCounter = 0;

        timeStamp[2] = Time.time + SkillPointManager.spellCooldowns[2];
        TimeStamper();

        player.mana.CurrentVal -= SkillPointManager.lightningManaCost;
    }

    void CastFireNova()
    {
        var clone = Instantiate(fireNova, castPosition.position, Quaternion.identity);
        clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);

        if (SkillPointManager.skillLevel[3] == 1)
        {
            clone.transform.localScale += new Vector3(0.3f, 0.3f, 0);
            clone.GetComponentInChildren<Light>().range = 1;
        }
        else if (SkillPointManager.skillLevel[3] == 2)
        {
            clone.transform.localScale += new Vector3(0.6f, 0.6f, 0);
            clone.GetComponentInChildren<Light>().range = 3;
        }
        else if (SkillPointManager.skillLevel[3] == 3)
        {
            clone.transform.localScale += new Vector3(0.8f, 0.8f, 0);
            clone.GetComponentInChildren<Light>().range = 6;
        }
        else if (SkillPointManager.skillLevel[3] > 3)
        {
            clone.transform.localScale += new Vector3(1f, 1f, 0);
            clone.GetComponentInChildren<Light>().range = 9;
        }

        timeStamp[3] = Time.time + SkillPointManager.spellCooldowns[3];
        TimeStamper();
        player.mana.CurrentVal -= SkillPointManager.fireballManaCost;

        sfqManager.fireballCast.Play();
    }

    void CastSpark()
    {
        var clone0 = Instantiate(spark, player.transform.position, Quaternion.identity);
        var clone1 = Instantiate(spark, player.transform.position, Quaternion.identity);
        var clone2 = Instantiate(spark, player.transform.position, Quaternion.identity);
        var clone3 = Instantiate(spark, player.transform.position, Quaternion.identity);
        clone0.GetComponent<SparkController>().speed.y += 1.8f;
        clone1.GetComponent<SparkController>().delay = 0.55f;
        clone1.GetComponent<SparkController>().speed.x += 1.8f;
        clone2.GetComponent<SparkController>().speed.y -= 1.8f;
        clone3.GetComponent<SparkController>().delay = 0.55f;
        clone3.GetComponent<SparkController>().speed.x -= 1.8f;
        clone0.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        clone1.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        clone2.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        clone3.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        if (SkillPointManager.skillLevel[4] > 1)
        {
            var clone4 = Instantiate(spark, player.transform.position, Quaternion.identity);
            var clone5 = Instantiate(spark, player.transform.position, Quaternion.identity);
            var clone6 = Instantiate(spark, player.transform.position, Quaternion.identity);
            var clone7 = Instantiate(spark, player.transform.position, Quaternion.identity);
            clone4.GetComponent<SparkController>().speed.x -= 1.2f;
            clone4.GetComponent<SparkController>().speed.y -= 1.2f;
            clone5.GetComponent<SparkController>().speed.x += 1.2f;
            clone5.GetComponent<SparkController>().speed.y += 1.2f;
            clone6.GetComponent<SparkController>().speed.x += 1.2f;
            clone6.GetComponent<SparkController>().speed.y -= 1.2f;
            clone7.GetComponent<SparkController>().speed.x -= 1.2f;
            clone7.GetComponent<SparkController>().speed.y += 1.2f;
            clone4.GetComponent<SparkController>().delay = 0.45f;
            clone5.GetComponent<SparkController>().delay = 0.45f;
            clone6.GetComponent<SparkController>().delay = 0.45f;
            clone7.GetComponent<SparkController>().delay = 0.45f;
            clone4.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            clone5.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            clone6.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
            clone7.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        }
        timeStamp[4] = Time.time + SkillPointManager.spellCooldowns[4];
        TimeStamper();
        player.mana.CurrentVal -= SkillPointManager.sparkManaCost;
    }

    void CastIceNova()
    {
        var clone = Instantiate(iceNova, castPosition.position, Quaternion.identity);
        clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);

        if (SkillPointManager.skillLevel[5] == 1)
        {
            clone.transform.localScale += new Vector3(0.3f, 0.3f, 0);
            clone.GetComponentInChildren<Light>().range = 5;
        }
        else if (SkillPointManager.skillLevel[5] == 2)
        {
            clone.transform.localScale += new Vector3(0.6f, 0.6f, 0);
            clone.GetComponentInChildren<Light>().range = 10;
        }
        else if (SkillPointManager.skillLevel[5] == 3)
        {
            clone.transform.localScale += new Vector3(0.8f, 0.8f, 0);
            clone.GetComponentInChildren<Light>().range = 15;
        }
        else if (SkillPointManager.skillLevel[5] > 3)
        {
            clone.transform.localScale += new Vector3(1f, 1f, 0);
            clone.GetComponentInChildren<Light>().range = 20;
        }

        timeStamp[5] = Time.time + SkillPointManager.spellCooldowns[5];
        TimeStamper();
        player.mana.CurrentVal -= SkillPointManager.iceNovaManaCost;

        sfqManager.iceNovaCast.Play();
    }

    IEnumerator CastAura()
    {
        var auraClone = Instantiate(Resources.Load("AuraRes"), player.gameObject.transform.position, Quaternion.identity) as GameObject;
        auraClone.transform.SetParent(player.gameObject.transform);
        var auraCloneTransform = auraClone.transform.position;
        auraCloneTransform.x = player.transform.position.x - 0.01f;
        auraCloneTransform.y = player.transform.position.y - 0.05f;
        auraClone.transform.position = auraCloneTransform;
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (auraOn)
            {
                if(player.mana.CurrentVal < SkillPointManager.auraManaCost)
                {
                    StopCoroutine(CastAura());
                    Destroy(GameObject.FindWithTag("AuraRes"));
                    timeStamp[9] = Time.time + SkillPointManager.spellCooldowns[9];
                    TimeStamper();
                    auraOn = false;
                }
                else
                {
                    player.mana.CurrentVal -= SkillPointManager.auraManaCost;
                    var clone = Instantiate(aura, player.gameObject.transform.position, Quaternion.identity);
                    clone.transform.SetParent(player.gameObject.transform);
                    yield return new WaitForSeconds(5);
                }
            } else yield break;
        }
    }

    void CastUlti()
    {
        var clone = Instantiate(ulti, player.gameObject.transform.position, Quaternion.identity);
        clone.transform.SetParent(GameObject.FindWithTag("SpellParent").transform);
        timeStamp[6] = Time.time + SkillPointManager.spellCooldowns[6];
        TimeStamper();
        player.mana.CurrentVal = 0;
    }

    public void HurtPlayer(float damageToGive)
    {
        player.health.CurrentVal -= damageToGive;
        player.fury.CurrentVal += 0.5f;
    }

    private void TimeStamper()
    {
        for (int i = 0; i < timeStamp.Length; i++)
        {
            if (timeStamp[i] <= Time.time + SkillPointManager.spellCooldowns[0]) timeStamp[i] = Time.time + SkillPointManager.spellCooldowns[0];
        }
    }

    public void Unlock(int i)
    {
        locked[i] = false;
        for (int j = 1; j < 10; j++)
        {
            if (locked[j] == false) padlock[j - 1].SetActive(false);
        }
    }

    public bool LockValue(int i)
    {
        if (locked[i])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator WaitAndPrint(string text)
    {
        cannot.text = text;
        cannot.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        cannot.gameObject.SetActive(false);
    }
}