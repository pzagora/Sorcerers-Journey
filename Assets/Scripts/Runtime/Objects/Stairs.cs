using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stairs : MonoBehaviour {

    public GameObject monsterParent, chestParent;
    public Text completedText, beginText;
    public Transform[] enterance;
    public Transform[] chestLocationsFirstDungeon, chestLocationsSecondDungeon, chestLocationsCorruptionCenter;
    public Transform[] monsterSpawnPointsFirstDungeon, monsterSpawnPointsSecondDungeon, monsterSpawnPointsCorruptionCenter;
    public bool corruptionCenter;
    
    private Transform returnPoint;
    private bool completed, timeCanStart;

    private void Awake()
    {
        returnPoint = gameObject.transform.GetChild(0).gameObject.transform;
    }

    // Use this for initialization
    void Start () {
        completed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0f)
        {
            if (Input.anyKey && timeCanStart)
            {
                Time.timeScale = 1f;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!completed)
            {
                completed = true;

                SaveLocation();

                StartCoroutine("WarningOnEnterDungeon");
                StartCoroutine("TimeStatus");
                Time.timeScale = 0f;

                int choice = Random.Range(0, 2);

                if (corruptionCenter)
                {
                    collision.gameObject.transform.position = enterance[2].position;
                    FindObjectOfType<CameraFollow>().gameObject.transform.position = enterance[2].position;
                    PopulateDungeon(2);
                    SpawnChestsInDungeon(2);
                    gameObject.GetComponent<Collider2D>().isTrigger = false;
                }
                else
                {
                    if (choice == 0)
                    {
                        collision.gameObject.transform.position = enterance[0].position;
                        FindObjectOfType<CameraFollow>().gameObject.transform.position = enterance[0].position;
                        PopulateDungeon(0);
                        SpawnChestsInDungeon(0);
                    }
                    else
                    {
                        collision.gameObject.transform.position = enterance[1].position;
                        FindObjectOfType<CameraFollow>().gameObject.transform.position = enterance[1].position;
                        PopulateDungeon(1);
                        SpawnChestsInDungeon(1);
                    }
                }
            }
            else
            {
                StopCoroutine("ThisIsCompletedAlready");
                StartCoroutine("ThisIsCompletedAlready");
            }
        }
    }

    IEnumerator ThisIsCompletedAlready()
    {
        completedText.text = "You have been there before";
        completedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        completedText.gameObject.SetActive(false);
    }

    private void PopulateDungeon(int number)
    {
        if (corruptionCenter)
        {
            foreach (var item in monsterSpawnPointsCorruptionCenter)
            {
                MonsterInstantiator(item, 0).transform.SetParent(monsterParent.transform);
            }
        }
        else
        {
            if (number == 0)
            {
                foreach (var item in monsterSpawnPointsFirstDungeon)
                {
                    MonsterInstantiator(item, 0).transform.SetParent(monsterParent.transform);
                }
            }
            else
            {
                foreach (var item in monsterSpawnPointsSecondDungeon)
                {
                    MonsterInstantiator(item, 0).transform.SetParent(monsterParent.transform);
                }
            }
        }
    }

    public GameObject MonsterInstantiator(Transform item, int tier)
    {
        int difficulty;
        if (tier == 0)
        {
            difficulty = FindObjectOfType<PLAYER>().level;
            if (corruptionCenter)
            {
                difficulty++;
            }
        }
        else
        {
            difficulty = tier;
        }

        switch (difficulty)
        {
            case 1:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier1.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier1[random], item.position, Quaternion.identity);
                }
            case 2:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier2.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier2[random], item.position, Quaternion.identity);
                }
            case 3:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier3.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier3[random], item.position, Quaternion.identity);
                }
            case 4:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier4.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier4[random], item.position, Quaternion.identity);
                }
            case 5:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier5.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier5[random], item.position, Quaternion.identity);
                }
            case 6:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier6.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier6[random], item.position, Quaternion.identity);
                }
            case 7:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier7.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier7[random], item.position, Quaternion.identity);
                }
            case 8:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier8.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier8[random], item.position, Quaternion.identity);
                }
            case 9:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier9.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier9[random], item.position, Quaternion.identity);
                }
            case 10:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier10.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier10[random], item.position, Quaternion.identity);
                }
            case 11:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier11.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier11[random], item.position, Quaternion.identity);
                }
            case 12:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier12.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier12[random], item.position, Quaternion.identity);
                }
            case 13:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier13.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier13[random], item.position, Quaternion.identity);
                }
            case 14:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier14.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier14[random], item.position, Quaternion.identity);
                }
            case 15:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier15.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier15[random], item.position, Quaternion.identity);
                }
            case 16:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier16.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier16[random], item.position, Quaternion.identity);
                }
            case 17:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier17.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier17[random], item.position, Quaternion.identity);
                }
            case 18:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier18.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier18[random], item.position, Quaternion.identity);
                }
            case 19:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier19.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier19[random], item.position, Quaternion.identity);
                }
            case 20:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier20.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier20[random], item.position, Quaternion.identity);
                }
            default:
                {
                    int random = Random.Range(0, FindObjectOfType<TierAssigner>().tier21.Length);
                    return Instantiate(FindObjectOfType<TierAssigner>().tier21[random], item.position, Quaternion.identity);
                }
        }
    }

    private void SpawnChestsInDungeon(int number)
    {
        if (corruptionCenter)
        {
            foreach (var item in chestLocationsCorruptionCenter)
            {
                var clone = Instantiate(Resources.Load("Chest"), item.position, Quaternion.identity) as GameObject;
                clone.GetComponent<Chest>().chestTier = (FindObjectOfType<PLAYER>().level + 4);
                clone.transform.SetParent(chestParent.transform);
            }
        }
        else
        {
            if (number == 0)
            {
                foreach (var item in chestLocationsFirstDungeon)
                {
                    var clone = Instantiate(Resources.Load("Chest"), item.position, Quaternion.identity) as GameObject;
                    clone.GetComponent<Chest>().chestTier = (FindObjectOfType<PLAYER>().level + 2);
                    clone.transform.SetParent(chestParent.transform);
                }
            }
            else
            {
                foreach (var item in chestLocationsSecondDungeon)
                {
                    var clone = Instantiate(Resources.Load("Chest"), item.position, Quaternion.identity) as GameObject;
                    clone.GetComponent<Chest>().chestTier = (FindObjectOfType<PLAYER>().level + 2);
                    clone.transform.SetParent(chestParent.transform);
                }
            }
        }
    }

    IEnumerator WarningOnEnterDungeon()
    {
        beginText.text = "<size=60>You can't reenter this dungeon once you leave it</size>";
        beginText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4.5f);
        beginText.text = "READY";
        yield return new WaitForSecondsRealtime(1.25f);
        beginText.text = "SET";
        yield return new WaitForSecondsRealtime(1.25f);
        beginText.text = "GO";
        yield return new WaitForSecondsRealtime(2f);
        beginText.gameObject.SetActive(false);
    }

    IEnumerator TimeStatus()
    {
        timeCanStart = false;
        yield return new WaitForSecondsRealtime(7f);
        timeCanStart = true;
    }

    private void SaveLocation()
    {
        PlayerPrefs.SetFloat("return_x", returnPoint.position.x);
        PlayerPrefs.SetFloat("return_y", returnPoint.position.y);
        PlayerPrefs.SetFloat("return_z", returnPoint.position.z);
    }
}
