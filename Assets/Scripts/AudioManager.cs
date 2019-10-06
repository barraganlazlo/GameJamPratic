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
            AudioSource a = InitializeAudioSource(s, gameObject);
            print(a);
        }
        
    }

    private AudioSource InitializeAudioSource(Sound s, GameObject go)
    {
        AudioSource[] sources = go.GetComponents<AudioSource>();
        AudioSource source = Array.Find(sources, sourceComp => sourceComp.clip == s.clip);
        if (source == null)
        {
            Sound newSound = new Sound();

            newSound.source = go.AddComponent<AudioSource>();
            newSound.source.clip = s.clip;

            newSound.source.volume = s.volume;
            newSound.source.pitch = s.pitch;
            newSound.source.loop = s.loop;
            newSound.source.spatialBlend = s.spatialBlend;

            return newSound.source;
        }
        else
            return source;
    }

    //Utiliser cette méthode pour jouer un son non spatial (interface, etc)
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
            return;
        }
        print(s.name);
        print(s.source);
        s.source.Play();
    }

    //Utiliser cette méthode pour jouer un son sur une entité
    public void PlayOnEntity (string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
            return;
        }

        InitializeAudioSource(s, go).Play();
    }

}
