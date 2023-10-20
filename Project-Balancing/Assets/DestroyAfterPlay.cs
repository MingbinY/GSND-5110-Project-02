using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (!source)
            source = gameObject.AddComponent<AudioSource>();

        source.PlayOneShot(clip);
        Destroy(gameObject, clip.length);
    }
}
