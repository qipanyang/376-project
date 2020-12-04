using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPage : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip castle_explore;
    void Start()
    {
        audioSource.PlayOneShot(castle_explore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
