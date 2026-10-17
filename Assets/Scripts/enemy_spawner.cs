using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class enemy_spawner : MonoBehaviour
{

    [SerializeField] all_game_variables data_storage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //enemies are stored in a list 
    List<enemy> all_enemy_list;
    // render all enemmy in list. update update their positions accoridng to their target.
    // target is calculated everyframe as the closest infected block

    [SerializeField] InfectClick infect_click_script;
    private int current_level_ref;
    private Vector3 enemy_spawn_location;
    private float enemy_move_speed;
    private float enemy_eat_speed;
    private float enemy_health;

    [SerializeField] mob_targetchooser targetchooser_script;

    void Start()
    {
        current_level_ref = infect_click_script.current_level;
        all_enemy_list = new List<enemy>();
    }

    // Update is called once per frame


    public void spawn_enemy()
    {
        //enemy stat based on current level:
        current_level_ref = infect_click_script.current_level;

        //random location in a ring x>current level & y>> currrent level. spawn radius = 2-5
        enemy_spawn_location = new Vector3(
            (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)),
            (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(2, 7)),
            0);

        //enemy move, eat and health is porportional to current level 
        enemy_move_speed = current_level_ref / 2;
        enemy_eat_speed = 1;
        enemy_health = current_level_ref / 2;


        //find target block based on the spawn location. This should be updated after every block consumed. 
        square target_block = targetchooser_script.FindNearestRedSquare();

        //spawn rate scales acoording to level -> 
        data_storage.mob_spawn_rate = current_level_ref * 2;
        print("mob sr" + data_storage.mob_spawn_rate + current_level_ref);

        //spawn enemy numbers depending on current level^2

        for (int i = 0; i < data_storage.mob_spawn_rate; i++)
        {
            enemy new_enemy = new enemy(0, enemy_spawn_location, enemy_move_speed, enemy_eat_speed, enemy_health, target_block, 0f);
            all_enemy_list.Add(new_enemy);
        }
    }



    //This is identical to the mecahnics used to geenrate squares. location & quaternion data used in entity (enemy) culling
    private List<Matrix4x4> visible_matrices_enemies = new List<Matrix4x4>();


    [SerializeField] private Mesh beetle_mesh;
    [SerializeField] private Material beetle_material;
    private RenderParams beetle_render_params;
    [SerializeField] Camera main_camera;
    void draw_enemy(List<Matrix4x4> visible_matrix_list, RenderParams render_params)
    {
        //sliced into 1023 element groups bc its the max unity can render at a time

        for (int i = 0; i < visible_matrix_list.Count; i += 1023)
        {
            int number_of_sprites_to_be_rendered = Mathf.Min(1023, visible_matrix_list.Count - i);
            List<Matrix4x4> matrix_list_to_be_rendered = visible_matrix_list.GetRange(i, number_of_sprites_to_be_rendered);
            Graphics.RenderMeshInstanced(render_params, beetle_mesh, 0, matrix_list_to_be_rendered);
        }
    }

    float enemy_counter = 0; 
    void Update()
    {

        if (enemy_counter < 3)
        {
            enemy_counter += Time.deltaTime; 
        }
        else
        {
            spawn_enemy();
            enemy_counter = 0;
        }
        if (all_enemy_list != null)
        {
            //for every enemy in enemy list:
            foreach (enemy spooky_hungry_enemy in all_enemy_list)
            {

                // are we on the target block yet?? 
                bool are_we_at_target_blcok = false;

                if (are_we_at_target_blcok)
                {
                    //hell yea. E A T 
                    //eating_progress is the current percentage progress.
                    float blocks_eaten_per_sec = 1 / spooky_hungry_enemy.eat_speeed;
                    spooky_hungry_enemy.eating_progress += blocks_eaten_per_sec * Time.deltaTime;

                    if (spooky_hungry_enemy.eating_progress >= 1)
                    {
                        //block is no longer infected:
                        spooky_hungry_enemy.target_block.is_infected = false;
                        print("not infected!!");
                    }

                }
                else
                {
                    //nay :(
                    //follow the target_block
                    Vector3 target_position = spooky_hungry_enemy.target_block.location;

                    spooky_hungry_enemy.location = Vector3.MoveTowards(
                        spooky_hungry_enemy.location,
                        target_position,
                        spooky_hungry_enemy.move_speed * Time.deltaTime);
                    //end of following function

                }



            }
        }


        /// BELOW IS RENDERING CODDE FOR ENEMIES
        MaterialPropertyBlock beetle_property_block = new MaterialPropertyBlock();
        beetle_property_block.SetColor(Shader.PropertyToID("_BaseColor"), Color.red);
        beetle_render_params = new RenderParams(beetle_material)
        {
            matProps = beetle_property_block,

        };

        visible_matrices_enemies.Clear();

        Plane[] camera_planes = GeometryUtility.CalculateFrustumPlanes(main_camera);
        Vector3 square_size = new Vector3(1f, 1f, 0.1f);

        for (int i = 0; i < all_enemy_list.Count; i++)
        {
            Vector3 sprite_location = all_enemy_list[i].location;
            Bounds sprite_bounds = new Bounds(sprite_location, square_size);
            if (GeometryUtility.TestPlanesAABB(camera_planes, sprite_bounds))
            {
                Matrix4x4 matrix_of_sprite_in_view = Matrix4x4.TRS(sprite_location, Quaternion.identity, Vector3.one);
                visible_matrices_enemies.Add(matrix_of_sprite_in_view);
            }
        }    
        
        draw_enemy(visible_matrices_enemies, beetle_render_params);
         
    }






    

    
}
