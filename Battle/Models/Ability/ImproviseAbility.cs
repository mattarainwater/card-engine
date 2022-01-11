using System;
using System.Collections.Generic;

public class ImproviseAbility : Ability
{
    public List<int> MarketIds { get; set; }
    public ImproviseType Type { get; set; }

    public ImproviseAbility()
    {
    }

    public override AbilityAction ToAction()
    {
        return new ImproviseAction(Source)
        {
            Type = Type,
            MarketIds = MarketIds
        };
    }
}