using System;
using System.Collections.Generic;

public class GainEnergyAbility : Ability
{
    public int Amount { get; set; }

    public GainEnergyAbility()
    {
    }

    public override AbilityAction ToAction()
    {
        return new GainEnergyAction(Source as DeckbuilderActor)
        {
            Amount = Amount
        };
    }
}