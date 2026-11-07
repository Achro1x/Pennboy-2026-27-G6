using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class all_game_variables: MonoBehaviour
{
    //level settings
    public int map_size = 5;

    //number of generators
    public int number_of_infected_blocks = 1;
    public int number_of_uninfected_blocks;


    //rates
    public int spore_per_sec;
    public int infected_block_per_sec;
    public int infected_block_per_click;
    public int mob_spawn_rate;
    public float multiplier; 


    //player inventory
    public float spore_owned = 0;
    public float player_attack = 10; 


    void Start()
    {
        map_size = 11;
        number_of_infected_blocks = 0;
        number_of_uninfected_blocks = map_size*map_size*4;
        spore_per_sec = 5;
        infected_block_per_sec = 0;
        infected_block_per_click = 1;
        mob_spawn_rate = 0;

        multiplier = 1;

        //tetsing param
        player_attack = 1;
        spore_owned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(number_of_uninfected_blocks);
        if (number_of_infected_blocks == 0 && spore_owned != 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        else if (number_of_uninfected_blocks <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
