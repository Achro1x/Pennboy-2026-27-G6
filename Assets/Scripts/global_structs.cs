using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]

//square struct for easy access from all files
public class square
{
    public int id;
    public Vector3 location;
    public bool is_infected;
    public bool is_targeted;
    


    public square(int id, Vector3 location, bool is_infected, bool is_targeted)
    {
        this.id = id;
        this.location = location;
        this.is_infected = is_infected;
        this.is_targeted = is_targeted;

    }
}

public class enemy
{
    public int id;
    public Vector3 location;
    public float move_speed;
    public float eat_speeed; //NOTE: how many seconds it takes for a block to be eaten
    public float health;
    public square target_block;
    public float eating_progress; //1 --> finished consuming. progression fraction
    public Rect hitbox;
    public bool last_hit;

    public enemy(int id, Vector3 location, float move_speed, float eat_speeed, float health, square target_block, float eating_progress, Rect hitbox, bool last_hit)
    {
        this.id = id;
        this.location = location;
        this.move_speed = move_speed;
        this.eat_speeed = eat_speeed;
        this.health = health;
        this.target_block = target_block;
        this.eating_progress = eating_progress;
        this.hitbox = hitbox;
        this.last_hit = last_hit;


    }
}