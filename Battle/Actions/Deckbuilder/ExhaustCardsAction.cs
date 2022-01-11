using System;
using System.Collections.Generic;
using System.Linq;

public class ExhaustCardsAction : AbilityAction
{
    public List<Card> CardsToExhaust { get; set; }

    public ExhaustCardsAction(Actor source) : base(source)
    {
    }

    public override string GetLog()
    {
        return $"{Source.Name} exhausted {CardsToExhaust.Count()} cards";
    }
}
