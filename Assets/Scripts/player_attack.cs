using System.Data.Common;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] all_game_variables data_storage;
    [SerializeField] enemy_spawner enemy_spawner_script;
    [SerializeField] Camera main_camera;
    [SerializeField] TrailRenderer trail;
    [SerializeField] SFX_player sfx_player_ref;
    private AudioSource sfx_player;


    private float timer = 0;
    private int kills = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx_player = sfx_player_ref.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool left_mouse_down = Input.GetKey(KeyCode.Mouse0);
        Vector3 cursor_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
 
        for (int i = enemy_spawner_script.all_enemy_list.Count - 1; i >= 0; i--)
        {
            enemy spooky_enemy = enemy_spawner_script.all_enemy_list[i];
            //updtate hitbox
            spooky_enemy.hitbox = new Rect(spooky_enemy.location.x - 1, spooky_enemy.location.y - 1, 2, 2);

            if (left_mouse_down)
            {
                //check hitbox 
                if (spooky_enemy.hitbox.Contains(cursor_pos) && !spooky_enemy.last_hit)
                {
                    spooky_enemy.last_hit = true;
                    enemy_spawner_script.all_enemy_list[i].health -= data_storage.player_attack;
 

                 
                    if (enemy_spawner_script.all_enemy_list[i].health <= 0)
                    {
                        data_storage.spore_owned++;
                        kills++; 
                        square enemy_target_block = enemy_spawner_script.all_enemy_list[i].target_block;
                        if (enemy_target_block != null)
                        {
                            enemy_spawner_script.all_enemy_list[i].target_block.is_targeted = false;
                        }

                        enemy_spawner_script.all_enemy_list.Remove(enemy_spawner_script.all_enemy_list[i]);

                        sfx_player.clip = sfx_player_ref.audio_clips[3];
                        sfx_player.Play();



                    }
                }
                else if (!spooky_enemy.hitbox.Contains(cursor_pos) && spooky_enemy.last_hit)
                {
                    spooky_enemy.last_hit = false;
                }
            }

        }

        cursor_pos.z = -1f;
        trail.transform.position = cursor_pos;
        trail.emitting = left_mouse_down;


        timer += Time.deltaTime; 
        if (timer >= 3)
        {
            data_storage.multiplier = 1+ kills / 5; 
            kills = 0;
            timer = 0;
            //print(data_storage.multiplier);
        }



    }
}
