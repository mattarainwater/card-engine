using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AddStatusAction : AbilityAction
{
    public AddStatusAction(Actor source) : base(source)
    {
    }

    public Status Status { get; set; }
    public int Stacks { get; set; }

    public override string GetLog()
    {
        return $"Adding {Stacks} stacks of status {Status.Type} " +
            $"to {string.Join(",", Targets.Select(x => x.Name))}";
    }
}
