using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NUnit.Framework;

public class InfectClick : MonoBehaviour
{
    public TMP_Text IBtext;
    public all_game_variables data;

    private int IBCount;
    private int IBPClick;


    [SerializeField] level_gen_script level_gen_script_reference;
    public int current_level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IBCount = data.number_of_infected_blocks;
        IBPClick = data.infected_block_per_click;
        current_level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Infect_Increase(){
        IBCount += IBPClick;
        IBtext.text = "Current Infected Blocks: " + IBCount + " MWAHAHAHAH";
        data.number_of_infected_blocks = IBCount;
        
        List<square> list_of_square_at_current_level = level_gen_script_reference.list_of_square_in_level_index[current_level];
        int num_of_square_at_current_level = list_of_square_at_current_level.Count;

        //infect block at current level
        for (int i = 0; i < num_of_square_at_current_level; i++)
        {
            if (!(list_of_square_at_current_level[i].is_infected))
            {
                //infect the next block
                square copy = list_of_square_at_current_level[i];
                copy.is_infected = true;
                list_of_square_at_current_level[i] = copy;

                //also update this change to all_square_list
                int index = copy.id;
                level_gen_script_reference.all_squares_list[index] = copy;
                break;
            }
        }

        //check current level

        bool should_inc_level = true;
        foreach (square sq in list_of_square_at_current_level)
        {
            if (!(sq.is_infected))
            {
                should_inc_level = false;
                break;
            }
        }
        if (should_inc_level)
        {
            current_level++;
        }
        print(current_level);

    }
}
