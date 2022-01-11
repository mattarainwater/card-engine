using System;
using System.Collections.Generic;

public class AddCardsAbility : Ability
{
    public int CardId { get; set; }
    public Zone Desintation { get; set; }

    public AddCardsAbility()
    {
    }

    public override AbilityAction ToAction()
    {
        return new AddCardAction(Source as DeckbuilderActor)
        {
            CardId = CardId,
            Desintation = Desintation
        };
    }
}