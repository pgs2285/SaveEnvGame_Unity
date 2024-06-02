using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class buttonScript : MonoBehaviour
{
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject?.GetComponent<Button>().onClick.AddListener(playSound);
    }

    public void playSound ()
    {
        audioSource.Play();
    }
}
