using UnityEngine;

public class audio_source_debug : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
