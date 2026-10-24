using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] all_game_variables data_storage;
    [SerializeField] enemy_spawner enemy_spawner_script;
    [SerializeField] Camera main_camera;
    [SerializeField] ParticleSystem damage_particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool left_mouse_down = Input.GetKey(KeyCode.Mouse0);
        Vector3 cursor_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        /*        foreach (enemy spooky_enemy in enemy_spawner_script.all_enemy_list)
                {
                    if (spooky_enemy.hitbox.Contains(cursor_pos) && left_mouse_down)
                    {
                        spooky_enemy.health -= data_storage.player_attack * Time.deltaTime;
                        print("killling enemy");
                        if (spooky_enemy.health <= 0)
                        {
                            enemy_spawner_script.all_enemy_list.Remove(spooky_enemy);

                        }
                    }
                }*/

            for (int i = enemy_spawner_script.all_enemy_list.Count-1; i >= 0; i--)
            {
                if (enemy_spawner_script.all_enemy_list[i].hitbox.Contains(cursor_pos) && left_mouse_down)
                {
                    enemy_spawner_script.all_enemy_list[i].health -= data_storage.player_attack * Time.deltaTime;

                    damage_particle.transform.position = cursor_pos;
                print(damage_particle.transform.position);
                    damage_particle.Play();
                    print("killling enemy");
                    if (enemy_spawner_script.all_enemy_list[i].health <= 0)
                    {
                        enemy_spawner_script.all_enemy_list.Remove(enemy_spawner_script.all_enemy_list[i]);

                    }
                }
                else
                {
                    damage_particle.Stop();
                }
            }
        

    }
}
