using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QueueMovesAction : AbilityAction
{
    public QueueMovesAction(ProgrammedActor actor)
        : base(actor)
    {
        Actor = actor;
    }

    public ProgrammedActor Actor { get; set; }
    public List<Move> MovesToQueue { get; set; }

    public override string GetLog()
    {
        return $"";
    }
}
