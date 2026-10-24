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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelUP_Text.text = "Lv UP: " + 1; 
        ibUP_Text.text = "Click UP: " + 1; 
        sporeUP_Text.text = "Spore Gen UP: " + 5;


        levelPrice_text.text = "Price: " + level_price(infect_script.current_level);
        ibPrice_text.text = "Price: " + click_price(all_game_var_script.infected_block_per_click);
        sporePrice_text.text = "Price: " + spore_price(all_game_var_script.spore_per_sec);
    }

    // Update is called once per frame
    void Update()
    {

    }
    int level_price(int current_level){
        return (int)Mathf.Pow((current_level+0.5f)*20, 2.5f);
    }

    int click_price(int infected_block_per_click){
        return (int)Mathf.Pow(infected_block_per_click * 20, 1.5f);
    }

    int spore_price(int spore_per_sec){
        return (int)Mathf.Pow(spore_per_sec * 2, 1.45f);
    }
    public void Upgrade_level_up()
    {
        //enable player to unlock the next level (moreclicking)
        int purchase_price = level_price(infect_script.current_level);
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!


        if ((all_game_var_script.spore_owned >= purchase_price) & infect_script.level_filled)
        {
            all_game_var_script.spore_owned -= purchase_price;

            infect_script.current_level++;
            infect_script.level_filled = false;
            infect_script.square_index = infect_script.generate_list();

            purchase_price = level_price(infect_script.current_level);

            levelUP_Text.text = "Lv UP: " + infect_script.current_level; 
            levelPrice_text.text = "Price: " + purchase_price;


        }

    }

    public void Upgrade_click_up()
    {
        //enable player to unlock the next level (moreclicking)
        int purchase_price = click_price(all_game_var_script.infected_block_per_click);
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        //print(infect_script.current_level);

        if (all_game_var_script.spore_owned >= purchase_price)
        {
            all_game_var_script.spore_owned -= purchase_price;

            all_game_var_script.infected_block_per_click += 1;

            purchase_price = click_price(all_game_var_script.infected_block_per_click);

            ibUP_Text.text = "Click UP: " + all_game_var_script.infected_block_per_click; 
            ibPrice_text.text = "Price: " + purchase_price;

        }


    }

    public void Upgrade_wait_up()
    {
        //enable player to unlock the next level (moreclicking)
        int purchase_price = spore_price(all_game_var_script.spore_per_sec);
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        //print(infect_script.current_level);

        if (all_game_var_script.spore_owned >= purchase_price)
        {
            all_game_var_script.spore_owned -= purchase_price;

            all_game_var_script.spore_per_sec += 1;

            purchase_price = spore_price(all_game_var_script.spore_per_sec);

            sporeUP_Text.text = "Spore Gen UP: " + all_game_var_script.spore_per_sec;
            sporePrice_text.text = "Price: " + purchase_price;

        }



    }

    //balance functions for upgrades

}
