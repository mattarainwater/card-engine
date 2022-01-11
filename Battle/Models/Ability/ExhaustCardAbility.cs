using System;
using System.Collections.Generic;

public class ExhaustCardAbility : Ability
{
    public CardTarget Target { get; set; }
    public int Amount { get; set; }

    public ExhaustCardAbility(CardTarget target, int amount)
    {
        Target = target;
        Amount = amount;
    }

    public override AbilityAction ToAction()
    {
        var cardsToExhaust = new List<Card>();
        var actor = Source as DeckbuilderActor;
        if (actor != null)
        {
            switch (Target)
            {
                case CardTarget.RandomHand:
                    cardsToExhaust.AddRange(actor.Hand.PickRandom(Amount));
                    break;
                case CardTarget.TopOfDeck:
                    cardsToExhaust.AddRange(actor.Deck.Draw(Amount));
                    break;
            }
        }
        return new ExhaustCardsAction(Source)
        {
            CardsToExhaust = cardsToExhaust
        };
    }
}