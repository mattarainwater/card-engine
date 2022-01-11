using System;
using System.Collections.Generic;

public class TriplePowerDamageAbility : DamageAbility
{
    public TriplePowerDamageAbility(int min, int max, int? assignedDamage)
        : base (min, max, assignedDamage)
    {
    }

    public override AbilityAction ToAction()
    {
        return new TriplePowerDamageAction(Source)
        {
            MinimumAmount = MinimumAmount,
            MaximumAmount = MaximumAmount,
            AssignedDamage = AssignedDamage
        };
    }
}