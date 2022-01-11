using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChangeTurnAction : GameAction
{
    public Faction Faction { get; set; }

    public ChangeTurnAction(Faction faction)
    {
        Faction = faction;
    }

    public override string GetLog()
    {
        return $"Change turn, now {Faction} turn";
    }
}
