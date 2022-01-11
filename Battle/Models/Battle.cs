using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Battle
{
    public Battle()
    {
        Actors = new List<Actor>();
    }

    public List<Actor> Actors { get; set; }
    public Faction CurrentFaction { get; set; }

    public List<Actor> CurrentActors { 
        get 
        {
            return Actors.Where(x => x.Faction == CurrentFaction).ToList();
        } 
    }
}
