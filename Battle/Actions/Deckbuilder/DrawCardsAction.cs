using System;
using System.Collections.Generic;
using System.Linq;

public class DrawCardsAction : AbilityAction
{
    public int Amount { get; set; }
    public List<Card> CardsDrawn { get; set; }

    public DrawCardsAction(Actor source) : base(source)
    {
    }

    public override string GetLog()
    {
        return $"{Source.Name} drew {Amount} cards";
    }
}
