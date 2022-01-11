using System;
using System.Linq;

public class VictorySystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnAction, Global.PerformNotification<CheckVictoryAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnAction, Global.PerformNotification<CheckVictoryAction>());
    }

    private void OnAction(object sender, object args)
    {
        var action = args as CheckVictoryAction;
        var battle = Container.GetBattle();

        action.IsDefeat = !battle.Actors.Any(x => x.Alive && x.Faction == Faction.PC) && 
            battle.Actors.Any(x => x.Faction == Faction.PC);

        action.IsVictory = !battle.Actors.Any(x => x.Alive && x.Faction == Faction.Enemy) && 
            battle.Actors.Any(x => x.Faction == Faction.Enemy);
    }
}

