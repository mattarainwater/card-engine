using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GuardAction : AbilityAction
{
    public GuardAction(Actor source) : base(source)
    {
    }

    public int Amount { get; set; }


    public override string GetLog()
    {
        return $"Add {Amount} guard to {string.Join(", ", Targets.Select(x => x.Name))}";
    }
}
