using NUnit.Framework;
using System.Collections;
using System.Globalization;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mob_targetchooser : MonoBehaviour
{
    private level_gen_script level_generator;
    private InfectClick clicker;
    [SerializeField] enemy_spawner enemy_spawner_script;
    [SerializeField] all_game_variables data_storage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level_generator = FindFirstObjectByType<level_gen_script>();
        clicker = FindFirstObjectByType<InfectClick>();
    }

    public square FindNearestRedSquare(Vector3 loc)
    {
        if (level_generator == null)
            return null;

        Vector3 mob_position = loc;

        float closest_distance = float.MaxValue;

         

        //current level list shoudl be adjusted based on the number of squares left on the list
        int refernce_level = clicker.current_level;
        int number_of_viable_sq = 0;

        int number_of_enemy = enemy_spawner_script.all_enemy_list.Count;

        if (number_of_enemy <= data_storage.number_of_infected_blocks)
        {
            while (number_of_viable_sq == 0)
            {
                //find number of viable sq in current level

                foreach (square square
                    in level_generator.list_of_square_in_level_index[refernce_level])
                {
                    if (square.is_infected && !square.is_targeted)
                    {
                        number_of_viable_sq += 1;
                    }
                }
                if (number_of_viable_sq == 0)
                {
                    refernce_level -= 1;
                    print(refernce_level);
                }
            }

            square closest_sq = level_generator.list_of_square_in_level_index[refernce_level][0];

            Vector3? closest_location = level_generator.list_of_square_in_level_index[clicker.current_level][0].location - mob_position;
            foreach (square square
                in level_generator.list_of_square_in_level_index[refernce_level])
            {
                // find red squares
                if (!square.is_infected)
                    continue;
                if (square.is_targeted)
                    continue;

                float distance =
                    (square.location - mob_position).sqrMagnitude;

                if (distance < closest_distance)
                {
                    closest_distance = distance;
                    closest_location = square.location;
                    closest_sq = square;
                }
            }
            closest_sq.is_targeted = true;
            return closest_sq;
        }


        else
        {
            SceneManager.LoadScene("TitleScreen");
            return null;
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
