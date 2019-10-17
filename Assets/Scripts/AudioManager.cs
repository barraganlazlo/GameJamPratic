using System;
using System.Collections;
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
            s.source = a;
        }
        Play("musique");
    }
    private AudioSource InitializeAudioSource(Sound s, GameObject go)
    {
        AudioSource[] sources = go.GetComponents<AudioSource>();
        AudioSource source = Array.Find(sources, sourceComp => sourceComp.clip == s.clip);
        if (source == null)
        {
            source = go.AddComponent<AudioSource>();
            source.clip = s.clip;

            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;
            source.spatialBlend = s.spatialBlend;
        }
        else { 
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;
            source.spatialBlend = s.spatialBlend;
        }
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
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = s.spatialBlend;
        print(s.name);
        print(s.source);
        s.source.Play();
    }
    public AudioSource PlayNewOnEntity(string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
            return null;
        }
        AudioSource src = InitializeAudioSource(s, go);
        src.Play();
        return src;
    }

    //Utiliser cette méthode pour jouer un son sur une entité
    public void PlayOnEntity(string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
            return;
        }
        AudioSource audioSource = InitializeAudioSource(s, go);
        //if(!audioSource.isPlaying)
            audioSource.Play();
    }
    public void StopLoopOnEntity(AudioSource source)
    {
        if (source==null)
        {
            return;
        }
        source.loop = false;
    }

    public void FadeOutOnEntity(string name, GameObject go)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son " + name + " n'a pas été trouvé.");
            return;
        }
        StartCoroutine(AudioManager.FadeOut(InitializeAudioSource(s, go), 1f)); 
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        while(audioSource.volume > 0 )
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();        
    }
}
