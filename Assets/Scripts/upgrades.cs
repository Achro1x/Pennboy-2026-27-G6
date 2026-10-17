using UnityEngine;

public class upgrades : MonoBehaviour
{
    [SerializeField] InfectClick infect_script;
    [SerializeField] all_game_variables all_game_var_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade_level_up()
    {
        //enable player to unlock the next level (moreclicking)
        int next_level_purchase_price = infect_script.current_level ;
        //LOOOK HERE TO INSERT BALANCE FUNCTION!!!!

        print(infect_script.current_level);

        if (all_game_var_script.spore_owned >= next_level_purchase_price && !infect_script.level_upgrade_unlocked)
        {
            infect_script.level_upgrade_unlocked = true;
            all_game_var_script.spore_owned -= next_level_purchase_price;
            //increase next levle prive 
            next_level_purchase_price += 1;
        }
 


    }
}
