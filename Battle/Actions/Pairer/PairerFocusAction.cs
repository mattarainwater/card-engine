using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PairerFocusAction : AbilityAction
{
    public PairerFocusAction(Actor source) : base(source)
    {
    }

    public int Amount { get; set; }

    public override string GetLog()
    {
        return $"Actor {Source.Name} increasing focus {Amount}";
    }
}
