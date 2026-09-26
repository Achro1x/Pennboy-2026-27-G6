using UnityEngine;
using TMPro;

public class spores_calculator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] all_game_variables all_game_var_script;
    [SerializeField] TMP_Text spore_text;
    void Start()
    {
        all_game_var_script.spore_owned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //total spore is the is spore_per_sec * numebr of infected blocks
        print(all_game_var_script.number_of_infected_blocks);
         
        float delta_spore = all_game_var_script.spore_per_sec * all_game_var_script.number_of_infected_blocks * Time.deltaTime / 5;
        float new_total_spore_float = all_game_var_script.spore_owned + delta_spore;
        all_game_var_script.spore_owned =  new_total_spore_float;
        spore_text.text = "Current Spores: " + Mathf.Floor(all_game_var_script.spore_owned);
    }
}
