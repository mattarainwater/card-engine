using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageAction : AbilityAction
{
    public int MinimumAmount { get; set; }
    public int MaximumAmount { get; set; }
    public DamageType Type { get; set; } = DamageType.Normal;
    public int? AssignedDamage { get; set; }
    public int DamageDealt { get; set; }

    public DamageAction(Actor source) : base (source)
    {
    }

    public override string GetLog()
    {
        if(AssignedDamage.HasValue)
        {
            return $"Deal {AssignedDamage.Value} damage";
        }
        return $"Deal {MinimumAmount}-{MaximumAmount} damage";
    }
}
