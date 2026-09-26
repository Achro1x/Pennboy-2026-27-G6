using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class level_gen_script : MonoBehaviour
{
    // Start is called before the first frame update
    private all_game_variables game_variable_script;
    [SerializeField] GameObject square_uninfected;
    public List<square> all_squares_list = new List<square>();
    [SerializeField] Camera main_camera;
    [SerializeField] private Material unlit_material;

    private RenderParams infected_render_params;
    private RenderParams uninfected_render_params;

    [SerializeField] private Mesh quad_mesh;


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

        MaterialPropertyBlock infected_property_block = new MaterialPropertyBlock();
        infected_property_block.SetColor(Shader.PropertyToID("_BaseColor"), Color.red);
        infected_render_params = new RenderParams(unlit_material)
        {
            matProps = infected_property_block,
           //worldBounds = new Bounds(Vector3.zero, Vector3.one * 10000f)
        };


        MaterialPropertyBlock uninfected_property_block = new MaterialPropertyBlock();
        uninfected_property_block.SetColor(Shader.PropertyToID("_BaseColor"), Color.white);
        uninfected_render_params = new RenderParams(unlit_material)
        {
            matProps = uninfected_property_block,
            //worldBounds = new Bounds(Vector3.zero, Vector3.one * 10000f)
        };
    }

    //location & quaternion data used in entity (square) culling
    private List<Matrix4x4> visible_matrices_infected = new List<Matrix4x4>();
    private List<Matrix4x4> visible_matrices_uninfected = new List<Matrix4x4>();



    void draw_square(List<Matrix4x4> visible_matrix_list, RenderParams render_params)
    {
        //sliced into 1023 element groups bc its the max unity can render at a time
  
        for (int i = 0; i < visible_matrix_list.Count; i += 1023)
        {
            int number_of_sprites_to_be_rendered = Mathf.Min(1023, visible_matrix_list.Count - i);
            List<Matrix4x4> matrix_list_to_be_rendered = visible_matrix_list.GetRange(i, number_of_sprites_to_be_rendered);
            Graphics.RenderMeshInstanced(render_params, quad_mesh, 0, matrix_list_to_be_rendered);
        }
    }


    // Update is called once per frame
    void Update()
    {
        visible_matrices_infected.Clear();
        visible_matrices_uninfected.Clear();

        Plane[] camera_planes = GeometryUtility.CalculateFrustumPlanes(main_camera);
        Vector3 square_size = new Vector3(1f, 1f, 0.1f);

        for(int i = 0; i < Mathf.Pow(game_variable_script.map_size, 2); i++)
        {
            Vector3 sprite_location = all_squares_list[i].location;
            Bounds sprite_bounds = new Bounds(sprite_location, square_size);
            if (GeometryUtility.TestPlanesAABB(camera_planes, sprite_bounds))
            {
                Matrix4x4 matrix_of_sprite_in_view = Matrix4x4.TRS(sprite_location, Quaternion.identity, Vector3.one);
                if (all_squares_list[i].is_infected)
                {
                    visible_matrices_infected.Add(matrix_of_sprite_in_view);
                }
                else
                {
                    visible_matrices_uninfected.Add(matrix_of_sprite_in_view);
                }
            }
        }

        draw_square(visible_matrices_uninfected, uninfected_render_params);
        draw_square(visible_matrices_infected, infected_render_params);
 
        
    }
}
