using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class all_game_variables: MonoBehaviour
{
    //level settings
    public int map_size = 5;

    //number of generators
    public int number_of_infected_blocks = 1;
    public int number_of_uninfected_blocks;

    //rates
    public float spore_per_sec = 5;
    public int infected_block_per_sec = 0;
    public int infected_block_per_click = 1;
    public int mob_spawn_rate = 0;


    //player inventory
    public float spore_owned = 0;




    void Start()
    {
        number_of_uninfected_blocks = map_size * map_size * 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
