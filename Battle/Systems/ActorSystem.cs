using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActorSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.AddObserver(OnDie, Global.PerformNotification<DieAction>());
        this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.RemoveObserver(OnDie, Global.PerformNotification<DieAction>());
        this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        action.Actor.Guard = 0;
    }

    private void OnDie(object sender, object args)
    {
        var action = args as DieAction;
    }

    private void EndTurn(object sender, object args)
    {
        var action = args as EndTurnAction;
    }
}
