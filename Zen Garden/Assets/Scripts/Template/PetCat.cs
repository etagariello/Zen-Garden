using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCat : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the cat
        if (collision.gameObject.CompareTag("Cat"))
        {
            // Play pet sound
            audioSource.Play();
        }
    }
}
