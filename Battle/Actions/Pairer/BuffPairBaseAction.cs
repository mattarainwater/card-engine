using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuffPairBaseAction : AbilityAction
{
    public BuffPairBaseAction(Actor source)
        : base(source)
    {
    }

    public int Amount { get; set; }

    public override string GetLog()
    {
        return $"";
    }
}
