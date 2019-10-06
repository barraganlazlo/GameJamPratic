using UnityEngine.Audio;
using UnityEngine;

//Classe servant à retenir les paramètres d'un son

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = .8f;
    [Range(.1f, 3)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float spatialBlend = 0f;

    [HideInInspector]
    public AudioSource source;
}
