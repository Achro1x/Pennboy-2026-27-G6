using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class level_gen_script : MonoBehaviour
{
    // Start is called before the first frame update
    private all_game_variables game_variable_script;
    [SerializeField] GameObject square_uninfected;
    public List<square> all_squares_list; 

    public struct square
    {
        public int id;
        public Vector3 location;
        public bool is_infected; 

        // Constructor to easily create them in bulk
        public square (int id, Vector3 location, bool is_infected)
        {
            this.id = id;
            this.location = location;
            this.is_infected = is_infected;
        }
    }

    void Start()
    {
        game_variable_script = GetComponent<all_game_variables>();
        
        
        //layout the map
        int size = game_variable_script.map_size;
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                
                square new_square = new square(index, new Vector3(i, j, 0), false);
                all_squares_list.Add(new_square);
                index++; 

            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
