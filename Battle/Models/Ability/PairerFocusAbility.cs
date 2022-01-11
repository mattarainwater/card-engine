using System;
using System.Collections.Generic;

public class PairerFocusAbility : Ability
{
    public int Amount { get; set; }

    public PairerFocusAbility(int amount)
    {
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        return new PairerFocusAction(Source)
        {
            Amount = Amount
        };
    }
}