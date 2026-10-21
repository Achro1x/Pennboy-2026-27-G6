using UnityEngine;

public class music_player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource music_source;
    public AudioClip[] AudioClip;
    private int current_song; 
    void Start()
    {
        music_source=GetComponent<AudioSource>();

        //randomly pick first song
        current_song = Random.Range(0, 2);
        music_source.clip = AudioClip[current_song];
        music_source.Play();

        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!music_source.isPlaying)
        {
            current_song ++;
            

            if (current_song > 1)
            {
                current_song = 0;
                 
            }

            music_source.clip = AudioClip[current_song];
            music_source.Play();
        }
    }
}
