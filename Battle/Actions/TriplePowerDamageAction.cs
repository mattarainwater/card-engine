using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TriplePowerDamageAction : DamageAction
{
    public TriplePowerDamageAction(Actor source) : base (source)
    {
    }

    public override string GetLog()
    {
        return $"Deal {MinimumAmount}-{MaximumAmount} damage (with 3X power mult)";
    }
}
