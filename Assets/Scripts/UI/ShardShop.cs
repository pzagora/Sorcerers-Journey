using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShardShop : MonoBehaviour
{
    public Image npcImage;
    public Slider[] slider = new Slider[2];
    public Text[] fromSliderText = new Text[2];
    public Text[] priceText = new Text[8];
    public Text npcGreet;
    public GameObject[] buttonOfChoice = new GameObject[6];
    public GameObject[] imageOfChoice = new GameObject[6];

    private PLAYER player;
    private CurrencyManager cMan;
    private CanvasGroup shardShopCanvas;
    private bool flag;
    private int[] price = new int[8];
    private int townId;

    void Awake()
    {
        player = FindObjectOfType<PLAYER>();
        shardShopCanvas = gameObject.GetComponent<CanvasGroup>();
        cMan = FindObjectOfType<CurrencyManager>();
    }

    // Use this for initialization
    void Start()
    {
        flag = true;
        price[0] = 15;
        price[1] = 3;
        price[2] = 30;
        price[3] = 30;
        price[4] = 40;
        price[5] = 40;
        price[6] = 50;
        price[7] = 50;
        townId = 0;
        StartCoroutine("RollRandomShopSlots");
        HideShardShop();
    }

    // Update is called once per frame
    void Update()
    {

        if (flag == true)
        {
            for (int i = 0; i < fromSliderText.Length; i++)
            {
                fromSliderText[i].text = slider[i].value.ToString();
                if (i == 1)
                {
                    priceText[i].text = ((int)Math.Ceiling(price[i] * slider[i].value - ((price[i] * slider[i].value) * (player.reputation[townId].CurrentVal / 100)))).ToString();
                }
                else
                {
                    priceText[i].text = "<color=red>" + ((int)Math.Ceiling(price[i] * slider[i].value - ((price[i] * slider[i].value) * (player.reputation[townId].CurrentVal / 100)))) + "</color> + <color=blue>" + ((int)Math.Ceiling(price[i] * slider[i].value - ((price[i] * slider[i].value) * (player.reputation[townId].CurrentVal / 100)))) + "</color>";
                }
            }
            for (int i = 2; i < priceText.Length; i++)
            {
                priceText[i].text = ((int)Math.Ceiling(price[i] - (price[i] * (player.reputation[townId].CurrentVal / 100)))).ToString();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
           Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                HideShardShop();
            }
        }
    }

    public void BuyIcicle()
    {
        if ((int)Math.Ceiling(price[2] - (price[2] * (player.reputation[townId].CurrentVal / 100))) < cMan.sapphire && SkillPointManager.skillLevel[0] < 3)
        {
            cMan.sapphire -= (int)Math.Ceiling(price[2] - (price[2] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[0] += 1;
            price[2] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[0] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyFireball()
    {
        if ((int)Math.Ceiling(price[3] - (price[3] * (player.reputation[townId].CurrentVal / 100))) < cMan.ruby && SkillPointManager.skillLevel[1] < 3)
        {
            cMan.ruby -= (int)Math.Ceiling(price[3] - (price[3] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[1] += 1;
            price[3] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[1] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyLightning()
    {
        if ((int)Math.Ceiling(price[4] - (price[4] * (player.reputation[townId].CurrentVal / 100))) < cMan.sapphire && SkillPointManager.skillLevel[2] < 3)
        {
            cMan.sapphire -= (int)Math.Ceiling(price[4] - (price[4] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[2] += 1;
            price[4] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[2] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyFireNova()
    {
        if ((int)Math.Ceiling(price[5] - (price[5] * (player.reputation[townId].CurrentVal / 100))) < cMan.ruby && SkillPointManager.skillLevel[3] < 3)
        {
            cMan.ruby -= (int)Math.Ceiling(price[5] - (price[5] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[3] += 1;
            price[5] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[3] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuySpark()
    {
        if ((int)Math.Ceiling(price[6] - (price[6] * (player.reputation[townId].CurrentVal / 100))) < cMan.ruby && SkillPointManager.skillLevel[4] < 3)
        {
            cMan.ruby -= (int)Math.Ceiling(price[6] - (price[6] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[4] += 1;
            price[6] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[4] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyIceNova()
    {
        if ((int)Math.Ceiling(price[7] - (price[7] * (player.reputation[townId].CurrentVal / 100))) < cMan.sapphire && SkillPointManager.skillLevel[5] < 3)
        {
            cMan.sapphire -= (int)Math.Ceiling(price[7] - (price[7] * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillLevel[5] += 1;
            price[7] *= 2;
            StartCoroutine("ThanksForPurchase");
        }
        else if (SkillPointManager.skillLevel[5] >= 3)
        {
            StartCoroutine("YouHaveItMaxedOut");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyShard()
    {
        if ((int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.sapphire &&
            (int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.ruby)
        {
            cMan.sapphire -= (int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100)));
            cMan.ruby -= (int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100)));
            cMan.shard += (int)slider[0].value;
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyMovementSpeed()
    {
        if ((int)Math.Ceiling(price[1] * slider[1].value - ((price[1] * slider[1].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.shard)
        {
            cMan.shard -= (int)Math.Ceiling(price[1] * slider[1].value - ((price[1] * slider[1].value) * (player.reputation[townId].CurrentVal / 100)));
            player.movementSpeed.CurrentVal += (int)slider[1].value;
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void ShowShardShop(int townId)
    {
        this.townId = townId;
        shardShopCanvas.alpha = 1f;
        shardShopCanvas.blocksRaycasts = true;
        flag = true;
        npcGreet.text = "Welcome Traveler !\n\n<size=17> I might have something\nyou would want\nTake a look at my goods</size>\n<size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    public void HideShardShop()
    {
        shardShopCanvas.alpha = 0f;
        shardShopCanvas.blocksRaycasts = false;
        flag = false;
    }

    IEnumerator NotEnoughMoney()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        StopCoroutine("ThanksForPurchase");
        StopCoroutine("EnjoyFreeHeal");
        StopCoroutine("YouHaveItMaxedOut");
        npcGreet.text = "<size=19>I'm sorry,\n but unfortunately you don't have enough \n money to buy this</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=17>You may pick\nsomething different \n that you could \n actually afford \n Take a look again</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    IEnumerator ThanksForPurchase()
    {
        FindObjectOfType<SFX_Manager>().shopBuy.Play();
        StopCoroutine("EnjoyFreeHeal");
        StopCoroutine("NotEnoughMoney");
        StopCoroutine("YouHaveItMaxedOut");
        npcGreet.text = "<size=19>Thank you \n for the purchase !</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=20>Do you need \n something else?</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    IEnumerator YouHaveItMaxedOut()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        StopCoroutine("EnjoyFreeHeal");
        StopCoroutine("NotEnoughMoney");
        StopCoroutine("ThanksForPurchase");
        npcGreet.text = "<size=19>You have \n maxed out this skill !</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=20>Maybe you want \n something else?</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    IEnumerator EnjoyFreeHeal()
    {
        StopCoroutine("ThanksForPurchase");
        StopCoroutine("NotEnoughMoney");
        StopCoroutine("YouHaveItMaxedOut");
        npcGreet.text = "<size=20>Enjoy \n free recovery!</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=20>Maybe you would \n like to buy something \n for more than a smile?</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    public static void GetNpcImage(Sprite npcSprite)
    {
        FindObjectOfType<ShardShop>().npcImage.sprite = npcSprite;
    }

    IEnumerator RollRandomShopSlots()
    {
        int choose = UnityEngine.Random.Range(0, 2);
        if (choose == 0)
        {
            imageOfChoice[0].SetActive(true);
            buttonOfChoice[0].SetActive(true);
            imageOfChoice[1].SetActive(false);
            buttonOfChoice[1].SetActive(false);
        }
        else
        {

            imageOfChoice[1].SetActive(true);
            buttonOfChoice[1].SetActive(true);
            imageOfChoice[0].SetActive(false);
            buttonOfChoice[0].SetActive(false);
        }

        choose = UnityEngine.Random.Range(0, 2);
        if (choose == 0)
        {
            imageOfChoice[2].SetActive(true);
            buttonOfChoice[2].SetActive(true);
            imageOfChoice[3].SetActive(false);
            buttonOfChoice[3].SetActive(false);
        }
        else
        {

            imageOfChoice[3].SetActive(true);
            buttonOfChoice[3].SetActive(true);
            imageOfChoice[2].SetActive(false);
            buttonOfChoice[2].SetActive(false);
        }

        choose = UnityEngine.Random.Range(0, 2);
        if (choose == 0)
        {
            imageOfChoice[4].SetActive(true);
            buttonOfChoice[4].SetActive(true);
            imageOfChoice[5].SetActive(false);
            buttonOfChoice[5].SetActive(false);
        }
        else
        {

            imageOfChoice[5].SetActive(true);
            buttonOfChoice[5].SetActive(true);
            imageOfChoice[4].SetActive(false);
            buttonOfChoice[4].SetActive(false);
        }

        yield return new WaitForSecondsRealtime(300f);
        StartCoroutine("RollRandomShopSlots");
    }
}
