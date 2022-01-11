using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum StatusType
{
    [Description("Negate the next attack.")]
    Evasion = 60,
    [Description("Dummy.")]
    SwordForms = 52,
    [Description("Lose life equal to bleed at the beginning of your turn.")]
    Bleed = 42,
    [Description("Take 50% more damage.")]
    Vulnerable = 2,
    [Description("Deal 50% less damage.")]
    Weakness = 6,
    [Description("Take extra damage on all attacks.")]
    Wound = 58,
    [Description("Deal extra damage on all attacks.")]
    Power = 4,
    [Description("Perform an action at the beginning of your turn.")]
    AbilityOnStartOfTurn = 56,
    [Description("Take 50% more damage from fire attacks.")]
    FireVulnerable = 20
}
