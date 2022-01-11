using System;
using System.Collections.Generic;
using System.Linq;

public class DiscardCardAbility : Ability
{
    public CardTarget Target { get; set; }
    public int Amount { get; set; }

    public DiscardCardAbility(CardTarget target, int amount)
    {
        Target = target;
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        var cardsToDiscard = new List<Card>();
        var actor = Source as DeckbuilderActor;
        if(actor != null)
        {
            switch (Target)
            {
                case CardTarget.RandomHand:
                    cardsToDiscard.AddRange(actor.Hand.PickRandom(Amount));
                    break;
                case CardTarget.TopOfDeck:
                    cardsToDiscard.AddRange(actor.Deck.Draw(Amount));
                    break;
            }
        }
        return new DiscardCardsAction(Source)
        {
            CardsToDiscard = cardsToDiscard
        };
    }
}