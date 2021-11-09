using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource source;

    bool isPalyed = true;

    public void Change()
    {
        if(isPalyed)
        {
            isPalyed = false;
            source.volume = 0;
        }
        else
        {
           isPalyed = true;
            source.volume = .5f; 
        }
    }
}
