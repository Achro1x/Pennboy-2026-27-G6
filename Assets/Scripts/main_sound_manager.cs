using UnityEngine;

public class main_sound_manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] AudioSource AudioSource; 
    public AudioClip[] audioClip;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_sound(int index)
    {
        AudioSource.clip = audioClip[index];
        AudioSource.Play();
    }
}
