using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySound : MonoBehaviour
{
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (!GameManager.isAudioOn)
        {
            GameManager.isAudioOn = true;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            _audio.Stop();
        }
    }
}
