using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>());
        this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.AddObserver(EndAllTurn, Global.PerformNotification<EndAllFactionTurnAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformChangeTurn, Global.PerformNotification<ChangeTurnAction>());
        this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.RemoveObserver(EndAllTurn, Global.PerformNotification<EndAllFactionTurnAction>());
    }

    private void OnPerformChangeTurn(object sender, object args)
    {
        var action = args as ChangeTurnAction;
        if(action.Faction == Faction.PC)
        {
            Container.Perform(new StartRoundAction());
        }
        var battle = Container.GetBattle();
        battle.CurrentFaction = action.Faction;
        var actorsInFaction = battle.Actors.Where(x => x.Faction == action.Faction && x.Alive);
        if(action.Faction == Faction.PC)
        {
            foreach (var actor in actorsInFaction)
            {
                actor.HasActed = false;
                Container.Perform(new StartTurnAction(actor));
            }
        }
        else
        {
            foreach (var actor in actorsInFaction)
            {
                actor.HasActed = false;
            }
            var firstAIActor = actorsInFaction.FirstOrDefault();
            if(firstAIActor != null)
            {
                Container.Perform(new StartTurnAction(firstAIActor));
            }
        }
    }

    private void EndAllTurn(object sender, object args)
    {
        var action = args as EndAllFactionTurnAction;
        var battle = Container.GetBattle();
        var currentActors = battle.CurrentActors;
        if (currentActors.Any())
        {
            var actorsToEnd = currentActors.Where(x => !x.HasActed);
            foreach(var actor in actorsToEnd)
            {
                var endTurnAction = new EndTurnAction(actor);
                Container.Perform(endTurnAction);
            }
        }
    }

    private void EndTurn(object sender, object args)
    {
        var action = args as EndTurnAction;
        var actor = action.Actor;
        actor.HasActed = true;
        var battle = Container.GetBattle();
        var currentActors = battle.CurrentActors;
        if (currentActors.All(x => x.HasActed))
        {
            var changeTurnAction = new ChangeTurnAction(battle.CurrentFaction.Toggle());
            Container.Perform(changeTurnAction);
        }
    }
}
