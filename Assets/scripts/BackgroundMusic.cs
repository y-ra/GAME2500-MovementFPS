using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static AudioSource bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.loop = true;
        bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
