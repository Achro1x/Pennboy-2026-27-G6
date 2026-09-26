using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //enemies are stored in a list 
    List<enemy> all_enemy_list = new List<enemy>();
    // render all enemmy in list. update update their positions accoridng to their target.
    // target is calculated everyframe as the closest infected block

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
