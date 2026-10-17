using UnityEngine;

public class mob_spawner : MonoBehaviour
{
    [SerializeField] private GameObject mob_prefab;
    [SerializeField] private float spawn_interval = 5f;
    [SerializeField] private int max_mobs = 10;

    private float spawn_timer = 0f;
    private int mobs_spawned = 0;

    void Update()
    {
        spawn_timer += Time.deltaTime;

        if (spawn_timer >= spawn_interval && mobs_spawned < max_mobs)
        {
            SpawnMob();
            spawn_timer = 0f;
        }
    }

    void SpawnMob()
    {
        int map_size = FindFirstObjectByType<all_game_variables>().map_size;

        float random_x = Random.Range(0, map_size) - (map_size/2);
        float random_y = Random.Range(0, map_size) - (map_size/2);

        Vector3 spawn_position = new Vector3(
            random_x,
            random_y,
            0f
        );

        Instantiate(
            mob_prefab,
            spawn_position,
            Quaternion.identity
        );

        mobs_spawned++;
    }
}
