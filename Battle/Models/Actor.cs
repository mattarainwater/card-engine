using Godot;
using System.Collections.Generic;

public abstract class Actor : Object
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxHP { get; set; }
    public int CurrentHP { get; set; }
    public int Morale { get; set; }
    public Faction Faction { get; set; }
    public bool HasActed { get; set; }
    public string Sprite { get; set; }
    public bool Alive { get { return CurrentHP > 0; } }
    public int Guard { get; set; }
    public List<Status> Statuses { get; set; }
    public List<int> MoveFromAtlas { get; set; }

    public Actor()
    {
        Statuses = new List<Status>();
    }
}