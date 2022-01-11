using System;
using System.Collections.Generic;

public class HealingAbility : Ability
{
    public int Amount { get; set; }

    public HealingAbility(int amount)
    {
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        return new HealingAction(Source)
        {
            Amount = Amount
        };
    }
}