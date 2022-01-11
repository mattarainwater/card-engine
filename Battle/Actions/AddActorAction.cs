using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddActorAction : GameAction
{
    public Actor Actor { get; set; }

    public override string GetLog()
    {
        return $"Adding actor {Actor.Name} for faction {Actor.Faction}";
    }
}
