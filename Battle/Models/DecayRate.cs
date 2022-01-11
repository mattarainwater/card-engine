using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum DecayRate
{
    [Description("")]
    None,
    [Description("Lose one per turn.")]
    OnePerTurn,
    [Description("Lose half of your stacks per turn.")]
    Half,
    [Description("Lose all at the beginning of your turn.")]
    All
}
