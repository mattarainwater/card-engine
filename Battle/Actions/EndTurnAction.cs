using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndTurnAction : GameAction
{
    public Actor Actor { get; set; }

    public EndTurnAction(Actor actor)
    {
        Actor = actor;
    }

    public override string GetLog()
    {
        return $"Ending turn for {Actor.Id} | {Actor.Name}";
    }
}