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
    public List<enemy> all_enemy_list;
    // render all enemmy in list. update update their positions accoridng to their target.
    // target is calculated everyframe as the closest infected block

    [SerializeField] InfectClick infect_click_script;
    private int current_level_ref;
    private Vector3 enemy_spawn_location;
    private float enemy_move_speed;
    private float enemy_eat_speed;
    private float enemy_health;
    private float seconds_per_enemy;

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


        //enemy move, eat and health is porportional to current level 
        enemy_move_speed = Mathf.Max(0.5f, current_level_ref / 2f);
        enemy_eat_speed = 2;
        enemy_health = current_level_ref +2 ;


        //find target block based on the spawn location. This should be updated after every block consumed. 
        square target_block = targetchooser_script.FindNearestRedSquare(enemy_spawn_location);

        //spawn rate scales acoording to level -> 
        data_storage.mob_spawn_rate = current_level_ref*2;


        //spawn 1 enemy numbers depending


            enemy_spawn_location = new Vector3(
                (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(5, 10)),
                (Random.Range(0, 2) == 0 ? -1 : 1) * (current_level_ref + Random.Range(5, 10)),
                0);

            Rect hitbox = new Rect(enemy_spawn_location.x - 1, enemy_spawn_location.y - 1, 2, 2);
            enemy new_enemy = new enemy(0, enemy_spawn_location, enemy_move_speed, enemy_eat_speed, enemy_health, target_block, 0f, hitbox, false);
            all_enemy_list.Add(new_enemy);
        
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


        if (all_enemy_list != null)
        {
            //for every enemy in enemy list:
            foreach (enemy spooky_hungry_enemy in all_enemy_list)
            {
                if (spooky_hungry_enemy.target_block != null)
                {

                    // are we on the target block yet?? 
                    bool are_we_at_target_blcok = false;

                    if (spooky_hungry_enemy.location == spooky_hungry_enemy.target_block.location)
                    {
                        are_we_at_target_blcok = true;
                    }

                    if (are_we_at_target_blcok)
                    {
                        //hell yea. E A T 
                        //eating_progress is the current percentage progress.
                        float blocks_eaten_per_sec = 1 / spooky_hungry_enemy.eat_speeed;
                        spooky_hungry_enemy.eating_progress += blocks_eaten_per_sec * Time.deltaTime;

                        if (spooky_hungry_enemy.eating_progress >= 1)
                        {
                            //block is no longer infected:

                            spooky_hungry_enemy.eating_progress = 0;
                            spooky_hungry_enemy.target_block.is_infected = false;
                            spooky_hungry_enemy.target_block.is_targeted = false;
                            infect_click_script.eaten_block_list.Add(spooky_hungry_enemy.target_block);
                            data_storage.number_of_infected_blocks -= 1;
                            print(data_storage.number_of_infected_blocks);

                            spooky_hungry_enemy.target_block = targetchooser_script.FindNearestRedSquare(spooky_hungry_enemy.location);


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

                else
                {
                    // enemyposition is null. Check if there is a new targetless block
                    square targetable_block = targetchooser_script.FindTargetableBlock();
                    if (targetable_block != null)
                    {
                        spooky_hungry_enemy.target_block = targetable_block;
                        targetable_block.is_targeted = true;

                    }
                    else
                    {
                        //do nothing
                    }

                }
            }

            enemy_counter += Time.deltaTime;
            print("counter" + enemy_counter +"goal" + seconds_per_enemy);


            //seconds_per_enemy = Mathf.Max(0.1f, (10f - 0.5f*current_level_ref));

            seconds_per_enemy = 0.2f;
            if (enemy_counter >= seconds_per_enemy && infect_click_script.current_level >= 0 && targetchooser_script.FindTargetableBlock() != null)
            {
                print("spawned");
                spawn_enemy();
                enemy_counter = 0;
            }


            /// BELOW IS RENDERING CODDE FOR ENEMIES
            MaterialPropertyBlock beetle_property_block = new MaterialPropertyBlock();
            beetle_property_block.SetColor(Shader.PropertyToID("_BaseColor"), Color.white);
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
}
