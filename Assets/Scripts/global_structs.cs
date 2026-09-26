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