using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleLevelCochon : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private float valueCharge = 0.0025f;
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

    // Start is called before the first frame update
    void Start()
    {
        buttonToPress = buttonScript._Myinput;
        animator = GetComponentInChildren<Animator>();
        valueDecharge = valueCharge * 0.7f;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript != null)
        {
            Debug.Log(playerScript.hasFoin);
        }
        if (playerScript != null && playerIsClose && playerScript.hasFoin)
        {
            if (Input.GetButton(buttonToPress+buttonScript._PlayerID))
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
                jauge.fillAmount -= valueDecharge;
                if (jauge.fillAmount <= 0 && !playerIsClose)
                {
                    buttonScript.isActive = false;
                }
            }
    }

    void ResetFillAmount()
    {
        if (jauge.fillAmount > 0)
        {
            jauge.fillAmount = 0;
        }
    }

    void IncreaseJauge()
    {
        if (jauge.fillAmount == 0)
        {
            AudioManager.instance.PlayOnEntity("Cochon_miam", gameObject);
        }
        jauge.fillAmount += valueCharge;
        Debug.Log("Increase");
        if (jauge.fillAmount >= 1)
        {
            AudioManager.instance.StopOnEntity("Cochon_miam", gameObject);
            //playerScript.hasFoin = false;
            playerScript.DestroyFoin();
            PassStep();
        }
    }

    void PassStep()
    {
        currentLevelStep += 1;
        jauge.fillAmount = 0;
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
        if (currentLevel == totalNumberOfLevels)
        {
            Win();
        }
        else
        {
            totalNumberOfLevelSteps += stepsAdder;
            currentLevelStep = 0;
            UpdateSr();
            Debug.Log(totalNumberOfLevelSteps);
        }
    }

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player2Entered");
            buttonScript._PlayerID = collision.gameObject.transform.parent.transform.parent.gameObject.GetComponent<WhichPlayer>().idPlayer;
            buttonScript.isActive = true;
            playerIsClose = true;
            playerScript = collision.gameObject.transform.parent.transform.parent.gameObject.GetComponent<PlayerHandleWeapon>();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsClose = false;
        playerScript = null;
        if (jauge.fillAmount <= 0)
        {
            buttonScript.isActive = false;
        }
    }
}
