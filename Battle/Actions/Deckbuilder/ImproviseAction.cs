using System;
using System.Collections.Generic;
using System.Linq;

public class ImproviseAction : AbilityAction
{
    public List<Card> ImprovisableCards { get; set; }
    public List<int> MarketIds { get; set; }
    public ImproviseType Type { get; set; }

    public ImproviseAction(Actor source) : base(source)
    {
    }

    public override string GetLog()
    {
        return $"{Source.Name} improvises";
    }
}
