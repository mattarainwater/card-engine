using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HealingAction : AbilityAction
{
    public int Amount { get; set; }

    public HealingAction(Actor source) : base (source)
    {
    }

    public override string GetLog()
    {
        return $"Heal {Amount} damage";
    }
}
