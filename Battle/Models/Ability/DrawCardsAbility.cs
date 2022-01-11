using System;
using System.Collections.Generic;

public class DrawCardsAbility : Ability
{
    public int Amount { get; set; }

    public DrawCardsAbility(int amount)
    {
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        return new DrawCardsAction(Source)
        {
            Amount = Amount
        };
    }
}