using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Variables : MonoBehaviour
{
 
    //level settings
    private int map_size;

    //number of generators
    private int number_of_infected_blocks;

    //rates
    private int spore_per_sec;
    private int infected_block_per_sec;
    private int mob_spawn_rate;

    //player inventory
    private int spore_owned;
    
    void Start()
    {
        map_size = 10000;

        number_of_infected_blocks = 0;

        spore_per_sec = 1;
        infected_block_per_sec = 0;
        mob_spawn_rate = 0;

        spore_owned = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
