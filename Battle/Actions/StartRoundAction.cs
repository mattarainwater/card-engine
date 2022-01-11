using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartRoundAction : GameAction
{
    public StartRoundAction()
    {
    }

    public override string GetLog()
    {
        return $"Starting round";
    }
}