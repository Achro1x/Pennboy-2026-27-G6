using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class enemy_spawner : MonoBehaviour
{
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


    void Start()
    {
        current_level_ref = infect_click_script.current_level;
        all_enemy_list = new List<enemy>();
    }

    // Update is called once per frame


    void Update()
    {
        //enemy stat based on current level:
        current_level_ref = infect_click_script.current_level;

        //random location in a ring x>current level & y>> currrent level. spawn radius = 2-5
         enemy_spawn_location = new Vector3(
             (Random.Range(0,2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)),
             (Random.Range(0,2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)), 
             0);

        //enemy move, eat and health is porportional to current level 
        enemy_move_speed = current_level_ref / 5;
        enemy_eat_speed = current_level_ref / 3;
        enemy_health = current_level_ref / 2;


        //find target block based on the spawn location. This should be updated after every block consumed. 

        //spawn enemy numbers depending on current level^2



        //enemy new_enemy = new enemy(0, enemy_spawn_location, enemy_move_speed, enemy_eat_speed, enemy_health, target_block);
        //all_enemy_list.Add(new_enemy);

    }
}
