using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game_Over_2_AudioManager : MonoBehaviour
{
    [SerializeField] private Game_Over_2_Sound[] _sounds;

    [SerializeField] private AudioSource externalAudioSource;

    public static Game_Over_2_AudioManager audioManInstance;
    string sceneName = "";

    public float musicMoment = 0;

    private void Awake()
    {
        if (audioManInstance == null)
        {
            audioManInstance = this;
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    void Init()
    {
        foreach (Game_Over_2_Sound s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play_Sfx("PieOrDie");
        StartCoroutine(StartMusicAfterWhile());
    }

    IEnumerator StartMusicAfterWhile()
    {
        yield return new WaitForSeconds(.7f);
        Play_Music("Music");
    }

    public AudioClip Get_Music_Clip()
    {
        foreach (Game_Over_2_Sound s in _sounds)
        {
            if (s.name == "Music")
            {
                if (s.source != null)
                {
                    musicMoment = s.source.time;
                }

                return s.clip;
            }
        }
        Debug.Log("No Music Found");

        return null;
    }

    public void Play_Sfx(string name)
    {
        Game_Over_2_Sound s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not Found!");
        }
        s.source.Play();
    }

    public void Stop_Sfx(string name)
    {
        Game_Over_2_Sound s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not Found!");
        }
        s.source.Stop();
    }

    public void Mute_Sfx(bool m)
    {
        foreach (Game_Over_2_Sound s in _sounds)
        {
            if (s.name != "Music")
            {
                s.source.mute = m;
            }
        }
    }

    public void Play_Music(string name)
    {
        Game_Over_2_Sound s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not Found!");
        }

        if (musicMoment > 0)
            s.source.time = musicMoment;

        s.source.Play();
    }

    public void Mute_Music(bool m)
    {
        foreach (Game_Over_2_Sound s in _sounds)
        {
            if (s.name == "Music")
            {
                s.source.mute = m;
                break;
            }
        }
    }

}

[Serializable]
public class Game_Over_2_Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

[Serializable]
public struct Toggles
{
    public GameObject onSwitch;
    public GameObject offSwitch;
}