using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddCardAction : AbilityAction
{
    public AddCardAction(DeckbuilderActor actor)
        : base(actor)
    {
        Actor = actor;
    }

    public DeckbuilderActor Actor { get; set; }
    public int CardId { get; set; }
    public Zone Desintation { get; set; }

    public override string GetLog()
    {
        return $"Adding card {CardId} to {Desintation}";
    }
}
