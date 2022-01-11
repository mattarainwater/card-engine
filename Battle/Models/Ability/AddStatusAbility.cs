using System;
using System.Collections.Generic;

public class AddStatusAbility : Ability
{
    public int Stacks { get; set; }
    public Status Status { get; set; }

    public AddStatusAbility(int stacks, Status status)
    {
        Stacks = stacks;
        Status = status;
    }

    public override AbilityAction ToAction()
    {
        return new AddStatusAction(Source)
        {
            Stacks = Stacks,
            Status = Status
        };
    }
}