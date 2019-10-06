using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSprite : MonoBehaviour
{
    /// <summary>
    /// Change l'image du boutton quand la touche voulut est enfoncée.
    /// Incrémente la jauge quand celle-ci est utilisée.
    /// </summary>
    public bool isActive = false;
    public string _Myinput = "InteractButton";
    public int _PlayerID = 1;
    public string Type = "A"; // A ou B

    public Sprite[] buttonImg;
    public float ValueCharge = 0; //reste a 0 si la jauge est innutile.
    private Image mySpr;

    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<Image>();
        if (Type == "A")
        {
            mySpr.sprite = buttonImg[0];
        }
        else
        {
            mySpr.sprite = buttonImg[2];
        }
        mySpr.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(isActive);
        if (isActive)
        {
            if (!mySpr.enabled)
            {
                mySpr.enabled = true;
            }
            mySpr.enabled = true;
            if (Input.GetButton(_Myinput + _PlayerID))
            {
                if (Type == "A")
                {
                    mySpr.sprite = buttonImg[1];
                }
                else
                {
                    mySpr.sprite = buttonImg[3];
                }
            }
            else
            {
                if (Type == "A")
                {
                    mySpr.sprite = buttonImg[0];
                }
                else
                {
                    mySpr.sprite = buttonImg[2];
                }
            }
        }
        else
        {
            if (mySpr.enabled)
            {
                mySpr.enabled = false;
            }
        }
    }
}
