using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class AbilityAction : GameAction, ITargets
{
    public AbilityAction(Actor source)
    {
        Source = source;
    }

    public Actor Source { get; set; }
    public List<Actor> Targets { get; set; }

    public override string GetLog()
    {
        return "Perform Ability";
    }
}
