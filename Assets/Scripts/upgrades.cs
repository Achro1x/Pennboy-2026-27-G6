using UnityEngine;
using TMPro;
using NUnit.Framework;

public class upgrades : MonoBehaviour
{
    [SerializeField] InfectClick infect_script;
    [SerializeField] all_game_variables all_game_var_script;

    public TMP_Text levelUP_Text;
    public TMP_Text ibUP_Text;
    public TMP_Text sporeUP_Text;
    public TMP_Text levelPrice_text;
    public TMP_Text ibPrice_text;
    public TMP_Text sporePrice_text;

    [SerializeField] main_sound_manager main_sound_manager; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelUP_Text.text = "Lv UP: " + infect_script.current_level; 
        ibUP_Text.text = "Click UP: " + all_game_var_script.infected_block_per_click; 
        sporeUP_Text.text = "Spore Gen UP: " + all_game_var_script.spore_per_sec;
    }

    public void Upgrade_level_up()
    {
        //enable player to unlock the next level (moreclicking)
        int next_level_purchase_price = (infect_script.current_level+1)*20 ;
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        print(next_level_purchase_price);
        print(infect_script.level_filled);

        if ((all_game_var_script.spore_owned >= next_level_purchase_price) & infect_script.level_filled)
        {
            all_game_var_script.spore_owned -= next_level_purchase_price;

            infect_script.current_level++;
            infect_script.level_filled = false;

            levelPrice_text.text = "Price: " + next_level_purchase_price;

            main_sound_manager.play_sound(2);
        }
        else
        {
            main_sound_manager.play_sound(4);
        }


            levelPrice_text.text = "Price: " + next_level_purchase_price;



        
    }

    public void Upgrade_click_up()
    {
        //enable player to unlock the next level (moreclicking)
        int next_level_purchase_price = all_game_var_script.infected_block_per_click * 10;
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        //print(infect_script.current_level);

        if (all_game_var_script.spore_owned >= next_level_purchase_price)
        {
            all_game_var_script.spore_owned -= next_level_purchase_price;

            all_game_var_script.infected_block_per_click += 1;

            main_sound_manager.play_sound(2);
        }
        else
        {
            main_sound_manager.play_sound(4);
        }


            ibPrice_text.text = "Price: " + next_level_purchase_price;



        
    }

    public void Upgrade_wait_up()
    {
        //enable player to unlock the next level (moreclicking)
        int next_level_purchase_price = all_game_var_script.spore_per_sec * 5;
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        //print(infect_script.current_level);

        if (all_game_var_script.spore_owned >= next_level_purchase_price)
        {
            all_game_var_script.spore_owned -= next_level_purchase_price;

            all_game_var_script.spore_per_sec += 1;

            main_sound_manager.play_sound(2);

        }
        else
        {
            main_sound_manager.play_sound(4);
        }


            sporePrice_text.text = "Price: " + next_level_purchase_price;



        
    }
}
