using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
        { 
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //Génération de AudioSource sur le AudioManager en parcourant l'intégralité la liste des sons présents dans le tableau sounds
        foreach (Sound s in sounds)
        {
            InitializeAudioSource(s, gameObject);
        }
        
    }

    private Sound InitializeAudioSource(Sound s, GameObject go)
    {
        Sound newSound = new Sound();

        newSound.source = go.AddComponent<AudioSource>();
        newSound.source.clip = s.clip;

        newSound.source.volume = s.volume;
        newSound.source.pitch = s.pitch;
        newSound.source.loop = s.loop;
        newSound.source.spatialBlend = s.spatialBlend;

        return newSound;
    }

    //Utiliser cette méthode pour jouer un son non spatial (interface, etc)
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
        }
        s.source.Play();
    }

    //Utiliser cette méthode pour jouer un son sur une entité
    public void PlayOnEntity (string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
        }

        Sound newSound = initializeAudioSource(s, go);

        newSound.source.Play();
    }

}
