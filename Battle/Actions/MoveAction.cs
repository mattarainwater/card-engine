using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MoveAction : GameAction, ITargets
{
    public MoveAction(Actor source)
    {
        Source = source;
    }

    public MoveAction(MoveAction action)
    {
        Source = action.Source;
        Move = action.Move;
        Target = action.Target;
    }

    public virtual Move Move { get; set; }

    public Actor Source { get; set; }
    public Actor Target { get; set; }
    public List<Actor> Targets { get { return new List<Actor> { Target }; } set { Target = value != null && value.Any() ? value.First() : null; } }

    public override string GetLog()
    {
        return "Perform Move";
    }
}
