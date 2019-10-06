using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouttonSprite : MonoBehaviour
{
    /// <summary>
    /// Change l'image du boutton quand la touche voulut est enfoncée.
    /// Incrémente la jauge quand celle-ci est utilisée.
    /// </summary>

    public string _Myinput = "";
    public int _PlayerID = 0;
    public string Type = "A"; // A ou B

    public Sprite[] buttonImg;
    public float ValueCharge = 0; //reste a 0 si la jauge est innutile.
    public Image _Jauge;
    private Image mySpr;

    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<Image>();
        if(Type == "A")
        {
            mySpr.sprite = buttonImg[0];
        }
        else
        {
            mySpr.sprite = buttonImg[2];
        }
     
    }

    // Update is called once per frame
    void LateUpdate()
    {
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
            _Jauge.fillAmount = ValueCharge;
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
            _Jauge.fillAmount = 0;
        }
    }
}
