using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatusSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.AddObserver(Apply, Global.PerformNotification<AddStatusAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.RemoveObserver(Apply, Global.PerformNotification<AddStatusAction>());
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        var actor = action.Actor;
        var abilityStatuses = actor.Statuses.Where(x => x.Type == StatusType.AbilityOnStartOfTurn);
        foreach(var abilityStatus in abilityStatuses)
        {
            abilityStatus.Ability.Source = actor;
            var actionFromAbility = abilityStatus.Ability.ToAction();
            actionFromAbility.Targets = Container.GetTargetsForAbility(actor, null, abilityStatus.Ability);
            this.PostNotification(".onStartTurnAbility", actionFromAbility);
        }
        foreach(var status in actor.Statuses)
        {
            switch(status.DecayRate)
            {
                case DecayRate.All:
                    status.Stacks = 0;
                    break;
                case DecayRate.Half:
                    status.Stacks = (int)Math.Floor(status.Stacks / 2f);
                    break;
                case DecayRate.OnePerTurn:
                    status.Stacks -= 1;
                    break;
                case DecayRate.None:
                    break;
            }
        }
        actor.Statuses = actor.Statuses.Where(x => x.Stacks != 0).ToList();
    }

    private void Apply(object sender, object args)
    {
        var action = args as AddStatusAction;
        foreach(var target in action.Targets)
        {
            if (action.Status.Type != StatusType.AbilityOnStartOfTurn && target.Statuses.Any(x => x.Type == action.Status.Type && x.DecayRate == action.Status.DecayRate))
            {
                var existingStatus = target.Statuses.FirstOrDefault(x => x.Type == action.Status.Type);
                existingStatus.Stacks += action.Stacks;
            }
            else
            {
                var status = new Status(action.Status, action.Stacks);
                target.Statuses.Add(status);
            }
        }
    }
}
