using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour {

    public int gold, sapphire, ruby, shard;
    public Text goldT, sapphireT, rubyT, shardT;
    public GameObject currencyParrent;

	// Use this for initialization
	void Start () {
        gold = 0;
        sapphire = 0;
        ruby = 0;
        shard = 0;
	}
	
	// Update is called once per frame
	void Update () {
        goldT.text = gold.ToString();
        sapphireT.text = sapphire.ToString();
        rubyT.text = ruby.ToString();
        shardT.text = shard.ToString();
    }
}
