using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSoundManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> sounds;
    public List<AudioClip> sfx;
    public List<string> names;
    public AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        source = GetComponent<AudioSource>();
        sounds = new Dictionary<string, AudioClip>();
        for (int i = 0; i < sfx.Count; i++)
            sounds.Add(names[i], sfx[i]);
    }
    public void play(string soundName)
    {
        source.clip = sounds[soundName];
        source.Play();
    }
}
