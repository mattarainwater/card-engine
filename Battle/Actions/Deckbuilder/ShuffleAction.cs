using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShuffleAction : GameAction
{
    public DeckbuilderActor Actor { get; set; }

    public override string GetLog()
    {
        return $"Shuffle deck";
    }
}
