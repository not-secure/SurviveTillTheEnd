using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : Common.SingletonMonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;

        audioSource.Play();
    }
}
