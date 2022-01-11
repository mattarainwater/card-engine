using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PairAction : MoveAction
{
    public PairAction(PairerActor actor)
        : base(actor)
    {
        Actor = actor;
    }

    public PairerActor Actor { get; set; }
    public Move Base { get; set; }
    public Move Style { get; set; }

    public override string GetLog()
    {
        return $"";
    }
}
