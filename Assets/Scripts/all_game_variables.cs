using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class all_game_variables: MonoBehaviour
{
    //level settings
    public int map_size;

    //number of generators
    public int number_of_infected_blocks;

    //rates
    public float spore_per_sec;
    public int infected_block_per_sec;
    public int infected_block_per_click;
    public int mob_spawn_rate;


    //player inventory
    public float spore_owned;

    void Start()
    {
        map_size = 500;
        number_of_infected_blocks = 0;
        spore_per_sec = 1;
        infected_block_per_sec = 0;
        infected_block_per_click = 1;
        mob_spawn_rate = 0;
        spore_owned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
