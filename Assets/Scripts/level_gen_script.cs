using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_gen_script : MonoBehaviour
{
    // Start is called before the first frame update
    private all_game_variables game_variable_script;
    public GameObject square_uninfected;
    void Start()
    {
        game_variable_script = GetComponent<all_game_variables>();
        
        
        //layout the map
        int size = game_variable_script.map_size; 
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject new_square = Instantiate(square_uninfected, new Vector3(i, j, 0), Quaternion.identity);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
