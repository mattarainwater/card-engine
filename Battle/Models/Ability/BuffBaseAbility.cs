using System;
using System.Collections.Generic;

public class BuffBaseAbility : Ability
{
    public int Amount { get; set; }

    public BuffBaseAbility(int amount)
    {
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        return new BuffPairBaseAction(Source)
        {
            Amount = Amount
        };
    }
}