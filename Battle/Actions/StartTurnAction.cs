using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartTurnAction : GameAction
{
    public Actor Actor { get; set; }

    public StartTurnAction(Actor actor)
    {
        Actor = actor;
    }

    public override string GetLog()
    {
        return $"Start turn for {Actor.Id} | {Actor.Name}";
    }
}