using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfectClick : MonoBehaviour
{
    public TMP_Text IBtext;
    public all_game_variables data;

    private int IBCount;
    private int IBPClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IBCount = data.number_of_infected_blocks;
        IBPClick = data.infected_block_per_click;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Infect_Increase(){
        IBCount += IBPClick;
        IBtext.text = "Current Infected Blocks: " + IBCount + " MWAHAHAHAH";
        data.number_of_infected_blocks = IBCount;
    }
}
