using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AI_Movement : MonoBehaviour {

    private AlertEye eye;
    private Animator anim;
    private GameObject player;
    private float range;
    public float speed;
    public bool alwaysWalking;
    public Sprite sprite;

    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;

    private bool alerted, backToPosition, begin;
    public float alertTime;
    private Vector3 currentPosition;

    void Awake()
    {
        eye = GetComponentInChildren<AlertEye>(true);
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        anim.SetFloat("velocity_y", -1);
        sprite = GetComponent<SpriteRenderer>().sprite;

        if (CheckIfObjectHaveAlertEyeAttached())
        {
            eye = GetComponentInChildren<AlertEye>(true);
        }
        else
        {
            var clone = Instantiate(Resources.Load("Eye"), gameObject.transform.position, Quaternion.identity) as GameObject;
            clone.gameObject.transform.SetParent(gameObject.transform.GetChild(0).transform);
            clone.GetComponent<RectTransform>().localScale = new Vector3(0.2554217f, 0.2554217f, 0);
            clone.GetComponent<RectTransform>().localPosition = new Vector3(0, 0.317f, 1);
            eye = clone.GetComponent<AlertEye>();
        }
    }

    // Use this for initialization
    void Start () {
        timeBetweenMoveCounter = Time.time + Random.Range(3.5f, 15f);
        backToPosition = true;
        begin = false;
    }
	
	// Update is called once per frame
	void Update () {

        range = Vector2.Distance(transform.position, player.transform.position);

        if (!alerted)
        {
            if (range <= 1.8f && !begin)
            {
                eye.gameObject.SetActive(true);
                StartCoroutine("AlertCoroutine");
            }
            else if (range > 1.8f)
            {
                if (!backToPosition && gameObject.GetComponent<AI_HealthManager>().mobName == "SHARDLING")
                {
                    Vector2 goingBack = new Vector2((transform.position.x - currentPosition.x) * speed, (transform.position.y - currentPosition.y) * speed);
                    anim.SetBool("iswalking", true);
                    anim.SetFloat("velocity_x", -goingBack.x);
                    anim.SetFloat("velocity_y", -goingBack.y);
                    GetComponent<Rigidbody2D>().velocity = -goingBack.normalized * speed * 2;
                    if ((Vector2.Distance(transform.position, currentPosition) < 0.05f))
                    {
                        backToPosition = true;
                    }
                }
                else
                {
                    eye.gameObject.SetActive(false);
                    begin = false;
                    StopCoroutine("HideEye");
                    StopCoroutine("AlertCoroutine");

                    if (timeToMoveCounter < Time.time)
                    {
                        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        if (!alwaysWalking) anim.SetBool("iswalking", false);
                    }

                    if (timeBetweenMoveCounter < Time.time && range < 5f)
                    {
                        timeBetweenMoveCounter = Time.time + Random.Range(3.5f, 15f);
                        timeToMoveCounter = Time.time + Random.Range(0.8f, 1.2f);

                        Vector3 moveUnaletred = new Vector3(Random.Range(-0.5f, 0.5f) * speed, Random.Range(-0.5f, 0.5f) * speed, 0f);
                        anim.SetBool("iswalking", true);
                        anim.SetFloat("velocity_x", moveUnaletred.x);
                        anim.SetFloat("velocity_y", moveUnaletred.y);
                        GetComponent<Rigidbody2D>().velocity = moveUnaletred;
                    }
                }
            }
        }
        else
        {
            if (range <= 1.8f)
            {
                Vector2 moveAlerted = new Vector2((transform.position.x - player.transform.position.x) * speed, (transform.position.y - player.transform.position.y) * speed);
                anim.SetBool("iswalking", true);
                anim.SetFloat("velocity_x", -moveAlerted.x);
                anim.SetFloat("velocity_y", -moveAlerted.y);
                GetComponent<Rigidbody2D>().velocity = -moveAlerted.normalized * speed;
            }
            else
            {
                alerted = false;
            }
        }
    }
    
    IEnumerator AlertCoroutine()
    {
        begin = true;
        Vector2 moveAlerted = new Vector2((transform.position.x - player.transform.position.x) * speed, (transform.position.y - player.transform.position.y) * speed);
        anim.SetBool("iswalking", false);
        anim.SetFloat("velocity_x", -moveAlerted.x);
        anim.SetFloat("velocity_y", -moveAlerted.y);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        eye.GetComponent<Image>().color = Color.yellow;

        if (gameObject.GetComponent<AI_HealthManager>().mobName != "SHARDLING")
        {
            Instantiate(Resources.Load("AlertEye"), eye.gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(alertTime);
        }
        else
        {
            backToPosition = false;
            currentPosition = gameObject.transform.position;
        }
        
        StartCoroutine("HideEye");

        eye.GetComponent<Image>().color = Color.red;
        Instantiate(Resources.Load("AlertEyeRed"), eye.gameObject.transform.position, Quaternion.identity);
        alerted = true;
        begin = false;
    }

    IEnumerator HideEye()
    {
        yield return new WaitForSeconds(2.5f);
        eye.gameObject.SetActive(false);
        Instantiate(Resources.Load("AlertEyeRed"), eye.gameObject.transform.position, Quaternion.identity);
    }

    private bool CheckIfObjectHaveAlertEyeAttached()
    {
        if (GetComponentInChildren<AlertEye>(true) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
