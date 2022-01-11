using System;
using System.Collections.Generic;

public class GuardAbility : Ability
{
    public int Amount { get; set; }

    public GuardAbility(int amount)
    {
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        return new GuardAction(Source)
        {
            Amount = Amount
        };
    }
}