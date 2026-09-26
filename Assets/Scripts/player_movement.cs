using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]int player_speed;
    [SerializeField] int scroll_speed;
    [SerializeField] Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move_x = Input.GetAxisRaw("Horizontal");
        float move_y = Input.GetAxisRaw("Vertical");

        Vector3 movement_direction = new Vector3 (move_x, move_y, 0);

        cam.transform.position += movement_direction * player_speed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cam.orthographicSize += scroll * scroll_speed * Time.deltaTime;

    }
}
