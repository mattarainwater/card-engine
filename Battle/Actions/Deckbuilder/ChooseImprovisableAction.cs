using System;
using System.Collections.Generic;
using System.Linq;

public class ChooseImprovisableAction : GameAction
{
    public Card ChosenCard { get; set; }
    public DeckbuilderActor Actor { get; set; }

    public ChooseImprovisableAction(DeckbuilderActor actor)
    {
        Actor = actor;
    }

    public override string GetLog()
    {
        return $"{Actor.Name} chooses {ChosenCard.Name}";
    }
}
