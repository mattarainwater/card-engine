using System;
using System.Collections.Generic;
using System.Linq;

public class DiscardCardsAction : AbilityAction
{
    public List<Card> CardsToDiscard { get; set; }

    public DiscardCardsAction(Actor source) : base(source)
    {
    }

    public override string GetLog()
    {
        return $"{Source.Name} discarded {CardsToDiscard.Count()} cards";
    }
}
