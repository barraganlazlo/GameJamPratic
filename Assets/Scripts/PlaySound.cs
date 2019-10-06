using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public string songName;
    public void Play()
    {
        AudioManager.instance.PlayOnEntity(songName, gameObject);
    }
}
