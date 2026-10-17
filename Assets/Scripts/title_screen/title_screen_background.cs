using UnityEngine;

public class title_screen_background : MonoBehaviour
{
    // Get the title screen and title screen background.
    [SerializeField] private float angleDegreesForAnimation = 10f;
    [SerializeField] private float animationTime = 30f;

    private Quaternion initialRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = angleDegreesForAnimation * Mathf.Sin(2f * Mathf.PI * Time.time / animationTime);
        transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
    }
}
