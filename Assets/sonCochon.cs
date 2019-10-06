using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySon : MonoBehaviour
{
    public string son;

    public void Play()
    {
        AudioManager.instance.PlayOnEntity(son, gameObject);
    }
}
