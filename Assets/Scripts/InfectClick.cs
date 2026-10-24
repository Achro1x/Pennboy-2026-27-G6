using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Linq;  

public class InfectClick : MonoBehaviour
{
    public TMP_Text IBtext;
    public all_game_variables data;

    private int IBCount;
    private int IBPClick;
    public bool level_upgrade_unlocked = true;

    public bool level_filled = false;
    public List<int> square_index;


    [SerializeField] level_gen_script level_gen_script_reference;
    public List<square> eaten_block_list = new List<square>();

    [SerializeField] SFX_player sfx_player_ref; 
    private AudioSource sfx_player;

    public int current_level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IBCount = data.number_of_infected_blocks;
        IBPClick = data.infected_block_per_click;
        current_level = 0;
        
        square_index = new List<int> { 0 };
        sfx_player = sfx_player_ref.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IBtext.text = ": " + data.number_of_infected_blocks;
        IBPClick = data.infected_block_per_click;
        
    }

    public List<int> generate_list(){
        List<square> list_of_square_at_current_level = level_gen_script_reference.list_of_square_in_level_index[current_level];
        int n = list_of_square_at_current_level.Count;

        return Enumerable.Range(0, n).ToList();

    }



    public void Infect_Increase(){

        
        List<square> list_of_square_at_current_level = level_gen_script_reference.list_of_square_in_level_index[current_level];
        int num_of_square_at_current_level = list_of_square_at_current_level.Count;

        //if eaten_block_list is not empty, fill those first
        //pick random square in current_level 
        for (int i = 0; i < IBPClick; i++){
            if (eaten_block_list.Count != 0)
            {
                eaten_block_list[0].is_infected = true;
                eaten_block_list.Remove(eaten_block_list[0]);
                
                IBCount++;
                data.number_of_infected_blocks = IBCount;
            }
            else
            {
                if (square_index.Count == 0)
                {
                    level_filled = true;
                    break;

                }

                int randomIndex = Random.Range(0, square_index.Count);
                list_of_square_at_current_level[square_index[randomIndex]].is_infected = true;

                IBCount++;
                data.number_of_infected_blocks = IBCount;

                square_index.RemoveAt(randomIndex);

                sfx_player.clip = sfx_player_ref.audio_clips[1];
                sfx_player.Play();
            }


        }

        //infect block at current level
        /*for (int i = 0; i < num_of_square_at_current_level; i++)
        {
            if (!(list_of_square_at_current_level[i].is_infected))
            {
                //infect the next block
                list_of_square_at_current_level[i].is_infected = true;
                IBCount += 1;
 
                break;
            }
        }*/

                
        level_filled = true;
        //check level filled
        for (int i = 0; i < num_of_square_at_current_level; i++)
        {
            if (!(list_of_square_at_current_level[i].is_infected))
            {
                level_filled = false;
            }
        }

        //check if level is filled
        /*if (level_filled)
        {
            IBCount += 0;
            //print("true");
        }
        else
        {
            IBCount += IBPClick;
            data.number_of_infected_blocks = IBCount;
        }*/


            //check current level

        /*bool should_inc_level = true;
        foreach (square sq in list_of_square_at_current_level)
        {
            if (!(sq.is_infected))
            {
                should_inc_level = false;
                break;
            }
        }*/
        /*if (should_inc_level && level_upgrade_unlocked)
        {
            current_level++;
            level_filled = false;
            level_upgrade_unlocked = false;
        }*/
        //print(current_level);



        data.number_of_infected_blocks = IBCount;
    }
}
