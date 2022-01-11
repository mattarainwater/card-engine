using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CheckVictoryAction : GameAction
{
    public CheckVictoryAction()
    {
    }

    public bool IsVictory { get; set; }
    public bool IsDefeat { get; set; }

    public override string GetLog()
    {
        return "Check victory";
    }
}
