using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NUnit.Framework;

//This could upgrade into text manager for ui
public class uninfect_block_count : MonoBehaviour
{
    public TMP_Text uninfectText;
    public all_game_variables data;

    private int full_map_size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        full_map_size = data.map_size * data.map_size;
    }

    // Update is called once per frame
    void Update()
    {
        data.number_of_uninfected_blocks = full_map_size - data.number_of_infected_blocks;
        uninfectText.text = "Domination Progress: " + data.number_of_uninfected_blocks; 
    }
}
