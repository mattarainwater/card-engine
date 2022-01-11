using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DieAction : GameAction
{
    public Actor Actor { get; set; }

    public override string GetLog()
    {
        return $"{Actor.Name} dies";
    }
}
