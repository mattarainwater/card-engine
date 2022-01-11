using System;
using System.Collections.Generic;

public class DamageAbility : Ability
{
    public int MinimumAmount { get; set; }
    public int MaximumAmount { get; set; }
    public int? AssignedDamage { get; set; }
    public DamageType Type { get; set; } = DamageType.Normal;
 
    public DamageAbility(int min, int max, int? assignedDamage)
    {
        MinimumAmount = min;
        MaximumAmount = max;
        AssignedDamage = assignedDamage;
    }

    public override AbilityAction ToAction()
    {
        return new DamageAction(Source)
        {
            MinimumAmount = MinimumAmount,
            MaximumAmount = MaximumAmount,
            Type = Type,
            AssignedDamage = AssignedDamage
        };
    }
}