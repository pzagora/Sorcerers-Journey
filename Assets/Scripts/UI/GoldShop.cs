using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldShop : MonoBehaviour {

    private PLAYER player;
    private CurrencyManager cMan;
    private CanvasGroup goldShopCanvas;
    private bool flag;
    private int[] price = new int[4];

    public Image npcImage;
    public Slider[] slider = new Slider[4];
    public Text[] fromSliderText = new Text[4];
    public Text[] priceText = new Text[4];
    public Text npcGreet;

    private int townId;

    void Awake()
    {
        player = FindObjectOfType<PLAYER>();
        goldShopCanvas = gameObject.GetComponent<CanvasGroup>();
        cMan = FindObjectOfType<CurrencyManager>();
    }

    // Use this for initialization
    void Start () {
        flag = true;
        price[0] = 250;
        price[1] = 250;
        price[2] = 7500;
        price[3] = 5000;
        townId = 0;
        HideGoldShop();
	}
	
	// Update is called once per frame
	void Update () {

        if (flag == true)
        {
            for (int i = 0; i < fromSliderText.Length; i++)
            {
                fromSliderText[i].text = slider[i].value.ToString();
                priceText[i].text = ((int)Math.Ceiling(price[i] * slider[i].value - ((price[i] * slider[i].value) * (player.reputation[townId].CurrentVal / 100)))).ToString();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
           Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                HideGoldShop();
            }
        }
	}

    public void BuySapphire()
    {
        if ((int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.gold)
        {
            cMan.gold -= (int)Math.Ceiling(price[0] * slider[0].value - ((price[0] * slider[0].value) * (player.reputation[townId].CurrentVal / 100)));
            cMan.sapphire += (int)slider[0].value;
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyRuby()
    {
        if ((int)Math.Ceiling(price[1] * slider[1].value - ((price[1] * slider[1].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.gold)
        {
            cMan.gold -= (int)Math.Ceiling(price[1] * slider[1].value - ((price[1] * slider[1].value) * (player.reputation[townId].CurrentVal / 100)));
            cMan.ruby += (int)slider[1].value;
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuyShard()
    {
        if ((int)Math.Ceiling(price[2] * slider[2].value - ((price[2] * slider[2].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.gold)
        {
            cMan.gold -= (int)Math.Ceiling(price[2] * slider[2].value - ((price[2] * slider[2].value) * (player.reputation[townId].CurrentVal / 100)));
            cMan.shard += (int)slider[2].value;
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void BuySkillPoint()
    {
        if ((int)Math.Ceiling(price[3] * slider[3].value - ((price[3] * slider[3].value) * (player.reputation[townId].CurrentVal / 100))) < cMan.gold)
        {
            cMan.gold -= (int)Math.Ceiling(price[3] * slider[3].value - ((price[3] * slider[3].value) * (player.reputation[townId].CurrentVal / 100)));
            SkillPointManager.skillPoints += (int)slider[3].value; ;
            FindObjectOfType<SkillPointManager>().skillPointAmount.text = SkillPointManager.skillPoints.ToString();
            FindObjectOfType<SkillPointManager>().PlusHideShow();
            StartCoroutine("ThanksForPurchase");
        }
        else
        {
            StartCoroutine("NotEnoughMoney");
        }
    }

    public void FreeHeal()
    {
        player.mana.CurrentVal = player.mana.MaxVal;
        player.health.CurrentVal = player.health.MaxVal;
        StartCoroutine("EnjoyFreeHeal");
    }

    public void ShowGoldShop(int townId)
    {
        this.townId = townId;
        goldShopCanvas.alpha = 1f;
        goldShopCanvas.blocksRaycasts = true;
        flag = true;
        npcGreet.text = "Welcome Traveler !\n\n<size=17> I might have something\nyou would want\nTake a look at my goods</size>\n<size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    public void HideGoldShop()
    {
        goldShopCanvas.alpha = 0f;
        goldShopCanvas.blocksRaycasts = false;
        flag = false;
    }

    IEnumerator NotEnoughMoney()
    {
        FindObjectOfType<SFX_Manager>().click.Play();
        StopCoroutine("ThanksForPurchase");
        StopCoroutine("EnjoyFreeHeal");
        npcGreet.text = "<size=19>I'm sorry,\n but unfortunately you don't have enough \n money to buy this</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=17>You may pick\nsomething different \n that you could \n actually afford \n Take a look again</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    IEnumerator ThanksForPurchase()
    {
        FindObjectOfType<SFX_Manager>().shopBuy.Play();
        StopCoroutine("EnjoyFreeHeal");
        StopCoroutine("NotEnoughMoney");
        npcGreet.text = "<size=19>Thank you \n for the purchase !</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=20>Do you need \n something else?</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    IEnumerator EnjoyFreeHeal()
    {
        FindObjectOfType<SFX_Manager>().shopBuy.Play();
        StopCoroutine("ThanksForPurchase");
        StopCoroutine("NotEnoughMoney");
        npcGreet.text = "<size=20>Enjoy \n free recovery!</size>";
        yield return new WaitForSeconds(5);
        npcGreet.text = "<size=20>Maybe you would \n like to buy something \n for more than a smile?</size> \n <size=15>Your current discount: <color=cyan>" + player.reputation[townId].CurrentVal + "%</color></size>";
    }

    public static void GetNpcImage(Sprite npcSprite)
    {
        FindObjectOfType<GoldShop>().npcImage.sprite = npcSprite;
    }

    public static void GetTownId(int townId)
    {
        FindObjectOfType<GoldShop>().townId = townId;
    }

}
