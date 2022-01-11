using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Status
{
    public Status()
    {

    }

    public Status(Status status, int stacks)
    {
        Stacks = stacks;
        Type = status.Type;
        DecayRate = status.DecayRate;
        Ability = status.Ability;
    }

    public int Stacks { get; set; }
    public StatusType Type { get; set; }
    public DecayRate DecayRate { get; set; }
    public Ability Ability { get; set; }
}
