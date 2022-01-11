using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayCardAction : MoveAction
{
    public PlayCardAction(DeckbuilderActor actor)
        : base(actor)
    {
        Actor = actor;
    }

    public DeckbuilderActor Actor { get; set; }
    public Card Card { get; set; }
    public override Move Move { get { return Card; } }

    public override string GetLog()
    {
        return $"";
    }
}
