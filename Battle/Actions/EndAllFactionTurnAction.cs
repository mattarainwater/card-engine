using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EndAllFactionTurnAction : GameAction
{
    public EndAllFactionTurnAction()
    {
    }

    public override string GetLog()
    {
        return $"Ending turns";
    }
}