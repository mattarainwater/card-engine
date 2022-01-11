using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BleedSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        var actor = action.Actor;
        if (actor.Statuses.Any(x => x.Type == StatusType.Bleed))
        {
            var bleed = actor.Statuses.First(x => x.Type == StatusType.Bleed);
            var damageAction = new DamageAction(actor)
            {
                MinimumAmount = bleed.Stacks,
                MaximumAmount = bleed.Stacks,
                Targets = new List<Actor> { actor }
            };
            Container.Perform(damageAction);
            this.PostNotification(".onTargetedAction", damageAction);
        }
    }
}
