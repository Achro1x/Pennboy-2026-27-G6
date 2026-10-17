using System.Collections;
using UnityEngine;

public class mob_targetchooser : MonoBehaviour
{
    private level_gen_script level_generator;
    private InfectClick clicker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level_generator = FindFirstObjectByType<level_gen_script>();
        clicker = FindFirstObjectByType<InfectClick>();
    }

    public square FindNearestRedSquare()
    {
        if (level_generator == null)
            return null;

        Vector3 mob_position = transform.position;

        float closest_distance = float.MaxValue;
        square closest_sq = level_generator.list_of_square_in_level_index[clicker.current_level][0] ;
        Vector3? closest_location = null;
        foreach (square square
            in level_generator.list_of_square_in_level_index[clicker.current_level])
    {
        // find red squares
        if (!square.is_infected)
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
    return closest_sq;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
