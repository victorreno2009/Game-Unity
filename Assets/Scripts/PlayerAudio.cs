using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource; // Referência ao AudioSource no Player

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Pega o AudioSource se estiver no Player
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Toca o som diretamente
            }
            else
            {
                Debug.LogWarning("AudioSource não está atribuído ao Player");
            }

            Destroy(col.gameObject); // Remove a moeda
        }
    }
}
