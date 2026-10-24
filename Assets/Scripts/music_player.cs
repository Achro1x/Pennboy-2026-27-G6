using NUnit.Framework;
using UnityEngine;

public class music_player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    [SerializeField] AudioClip[] audio_clips; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audio_clips[0];
        audioSource.Play();

    }

    private int current_index = 0;
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (current_index < audio_clips.Length-1)
            {
                current_index++;
            }
            else
            {
                current_index = 0;
            }
            audioSource.clip = audio_clips[current_index];
            audioSource.Play();
            
        }
    }
}
