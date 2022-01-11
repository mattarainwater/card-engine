using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GainEnergyAction : AbilityAction
{
    public GainEnergyAction(DeckbuilderActor actor)
        : base(actor)
    {
        Actor = actor;
    }

    public DeckbuilderActor Actor { get; set; }
    public int Amount { get; set; }

    public override string GetLog()
    {
        return $"gaining {Amount} energy";
    }
}
