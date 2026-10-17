using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class enemy_spawner : MonoBehaviour
{

    [SerializeField] all_game_variables data_storage; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //enemies are stored in a list 
    List<enemy> all_enemy_list;
    // render all enemmy in list. update update their positions accoridng to their target.
    // target is calculated everyframe as the closest infected block

    [SerializeField] InfectClick infect_click_script;
    private int current_level_ref;
    private Vector3 enemy_spawn_location;
    private float enemy_move_speed;
    private float enemy_eat_speed;
    private float enemy_health;

    [SerializeField] mob_targetchooser targetchooser_script;

    void Start()
    {
        current_level_ref = infect_click_script.current_level;
        all_enemy_list = new List<enemy>();
    }

    // Update is called once per frame


    public void spawn_enemy()
    {
        //enemy stat based on current level:
        current_level_ref = infect_click_script.current_level;

        //random location in a ring x>current level & y>> currrent level. spawn radius = 2-5
        enemy_spawn_location = new Vector3(
            (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)),
            (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)),
            0);

        //enemy move, eat and health is porportional to current level 
        enemy_move_speed = current_level_ref / 5;
        enemy_eat_speed = current_level_ref / 3;
        enemy_health = current_level_ref / 2;


        //find target block based on the spawn location. This should be updated after every block consumed. 
        ///////////////////////square target_block = targetchooser_script.FindNearestRedSquare();

        //spawn rate scales acoording to level -> 
        data_storage.mob_spawn_rate = current_level_ref * 2;

        //spawn enemy numbers depending on current level^2
        //////////////////////////////////////
        /*for (int i = 0; i < data_storage.mob_spawn_rate; i++)
        {
            enemy new_enemy = new enemy(0, enemy_spawn_location, enemy_move_speed, enemy_eat_speed, enemy_health, target_block);
            all_enemy_list.Add(new_enemy);
        }*/
    }



    void Update()
    {
        //for every enemy in enemy list:
        foreach (enemy spooky_hungry_enemy in all_enemy_list)
        {

            // are we on the target block yet?? 
            bool are_we_at_target_blcok = false; 

            if (are_we_at_target_blcok)
            {
                //hell yea. E A T 
                //eating_progress is the current percentage progress.
                float blocks_eaten_per_sec = 1 / spooky_hungry_enemy.eat_speeed;
                spooky_hungry_enemy.eating_progress += blocks_eaten_per_sec * Time.deltaTime;

                if (spooky_hungry_enemy.eating_progress >= 1)
                {
                    //block is no longer infected:
                    spooky_hungry_enemy.target_block.is_infected = false; 
                }

            }
            else
            {
                //nay :(
                //follow the target_block
                Vector3 target_position = spooky_hungry_enemy.target_block.location;

                spooky_hungry_enemy.location = Vector3.MoveTowards(
                    spooky_hungry_enemy.location,
                    target_position,
                    spooky_hungry_enemy.move_speed * Time.deltaTime);
                //end of following function
 
            }


         
        }






    }
}
