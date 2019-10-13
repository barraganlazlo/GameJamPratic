using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleLevelCochon : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private float valueCharge = 0.25f;
    //[SerializeField] private float valueCharge = 0.01f;
    [SerializeField] private ButtonSprite buttonScript;
    private string buttonToPress;
    [SerializeField] private Image jauge;

    [Header("HandleProgression")]
    [SerializeField] private int totalNumberOfLevels = 5;
    private int currentLevel = 1;
    private int totalNumberOfLevelSteps = 1;
    [SerializeField] private int stepsAdder = 2;
    private int currentLevelStep = 0;

    [Header("HandelDecrease")]
    private float valueDecharge;
    [SerializeField] [Range(0.0f, 1.0f)] private float decreaseRatio = 0.5f;

    [Header("Handle FeedBacks")]
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] srs;

    public GameObject winUi;


    [Header("handleMultiplayer")]
    private int playerFeeding = 0;

    private bool playerIsClose = false;
    private PlayerHandleWeapon playerScript;

    private Animator animator;

    bool playingSound;

    private bool feeding = false;

    public GameObject envolCochon;

    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        buttonToPress = buttonScript.realInput;
        animator = GetComponentInChildren<Animator>();
        valueDecharge = decreaseRatio;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript != null && playerIsClose && playerScript.hasFoin)
        {
            if (Input.GetButton(buttonToPress))
            {
                if (!buttonScript.isActive)
                {
                    buttonScript.isActive = true;
                }
                IncreaseJauge();
            }
            else
            {
                DecreaseJauge();
            }
        }
        else
        {
            DecreaseJauge();
        }
    }

    void DecreaseJauge()  
    {
        if (jauge.fillAmount == 0)
        {
            ResetFillAmount();
        }
        else
        {
            Debug.Log("Decrease");
            feeding = false;
            jauge.fillAmount -= valueDecharge * Time.deltaTime;
            if (jauge.fillAmount <= 0)
            {
                jauge.fillAmount = 0;
            }
        }
        if (playingSound)
        {
            AudioManager.instance.StopLoopOnEntity(audiosource);
            playingSound = false;
        }
    }

    void ResetFillAmount()
    {
        if (jauge.fillAmount > 0)
        {
            jauge.fillAmount = 0;
            feeding = false;
        }
    }

    void IncreaseJauge()
    {
        feeding = true;
        if (!playingSound)
        {
            audiosource=AudioManager.instance.PlayNewOnEntity("Cochon_miam", gameObject);
            playingSound = true;
        }
        jauge.fillAmount += valueCharge * Time.deltaTime;
        Debug.Log("Increase");
        if (jauge.fillAmount >= 1)
        {
            if (playingSound)
            {
                AudioManager.instance.StopLoopOnEntity(audiosource);
                playingSound = false;
            }
            playerScript.DestroyFoin();
            PassStep();
        }
    }

    void PassStep()
    {
        currentLevelStep += 1;
        jauge.fillAmount = 0;
        feeding = false;
        if (currentLevelStep == totalNumberOfLevelSteps)
        {
            LevelUp();
        }
        else
        {
            animator.SetTrigger("pump");
        }
    }

    void LevelUp()
    {
        animator.SetTrigger("levelUp");
        currentLevel += 1;
        UpdateSr();
        if (currentLevel == totalNumberOfLevels)
        {
            Win();
        }
        else
        {
            totalNumberOfLevelSteps += stepsAdder;
            currentLevelStep = 0;
            Debug.Log(totalNumberOfLevelSteps);
        }
    }

    //void CheckIfPlayersFoin()
    //{
    //    foreach (PlayerHandleWeapon player in players)
    //    {
    //        if (player.hasFoin)
    //        {
    //            playersCanFeed = true;
    //            if (!buttonScript.isActive)
    //            {
    //                buttonScript.isActive = true;
    //            }
    //            return;
    //        }
    //    }
    //    if (buttonScript.isActive)
    //    {
    //        buttonScript.isActive = false;
    //    }
    //    playersCanFeed = false;
    //}

    void UpdateSr()
    {
        animator.SetTrigger("levelUp");
        sr.sprite = srs[currentLevel - 1];
    }

    void Win()
    {
        Instantiate<GameObject>(winUi);
        this.enabled = false;
        buttonScript.isActive = false;
        buttonScript.gameObject.SetActive(false);
        AudioManager.instance.PlayOnEntity("cochon_flight", gameObject);
        StartCoroutine(WinFlight());
        Debug.Log("win");
        AudioManager.instance.FadeOutOnEntity("musique", AudioManager.instance.gameObject);
    }

    IEnumerator WinFlight()
    {
        yield return new WaitForSeconds(1);
        Instantiate(envolCochon, transform.GetChild(0).position, Quaternion.identity, transform);
        sr.enabled = false;
        GameManager.instance.Win();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !feeding)
        {
            playerScript = collision.gameObject.transform.parent.parent.GetComponent<PlayerHandleWeapon>();
            if (playerScript.hasFoin)
            {
                buttonScript._PlayerID = collision.gameObject.transform.parent.parent.GetComponent<PlayerInputs>().idPlayer;
                //buttonScript.isActive = true;
                playerIsClose = true;
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (buttonScript._PlayerID== collision.gameObject.transform.parent.parent.GetComponent<PlayerInputs>().idPlayer)
            {
                playerIsClose = false;
            }
        }
        //playerScript = null;
        //if (jauge.fillAmount <= 0)
        //{
        //    buttonScript.isActive = false;
        //}
    }
}
