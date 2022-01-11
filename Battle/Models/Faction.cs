using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public enum Faction
{
    PC,
    Enemy
}

public static class FactionExtensions
{
    public static Faction Toggle(this Faction faction)
    {
        return faction == Faction.PC ? Faction.Enemy : Faction.PC;
    }
}