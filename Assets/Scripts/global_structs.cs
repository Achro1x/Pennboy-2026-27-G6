using UnityEngine;

[System.Serializable]

//square struct for easy access from all files
public struct square
{
    public int id;
    public Vector3 location;
    public bool is_infected;
 
    public square(int id, Vector3 location, bool is_infected)
    {
        this.id = id;
        this.location = location;
        this.is_infected = is_infected;

    }
}

public class enemy
{
    public int id;
    public Vector3 location;
    public int move_speed;
    public int eat_speeed; //NOTE: how many seconds it takes for a block to be eaten
    public int health;
    public string name;
    public square target_block;
    public float eating_progress; //1 --> finished consuming. progression fraction

    public enemy(int id, Vector3 location, int move_speed, int eat_speeed, int health, string name, square target_block, float eating_progress)
    {
        this.id = id;
        this.location = location;
        this.move_speed = move_speed;
        this.eat_speeed = eat_speeed;
        this.health = health;
        this.name = name;
        this.target_block = target_block;
        this.eating_progress = eating_progress; 


}
}