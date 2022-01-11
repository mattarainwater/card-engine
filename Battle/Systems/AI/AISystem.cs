using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AISystem : Aspect, IObservable
{
	public void Awake()
	{
		this.AddObserver(StartRound, Global.PerformNotification<StartRoundAction>());
		this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
		this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
		this.AddObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
	}

    public void Destroy()
	{
		this.RemoveObserver(StartRound, Global.PerformNotification<StartRoundAction>());
		this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
		this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
		this.RemoveObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
	}

    private void StartRound(object sender, object args)
	{
		var action = args as StartRoundAction;
		var aiActors = Container.GetBattle().Actors.Where(x => x is AIActor).Cast<AIActor>();
		foreach(var aiActor in aiActors)
		{
			var randomMove = aiActor.Moves.PickRandom();
			var moveAction = new MoveAction(aiActor)
			{
				Move = randomMove,
				Target = Container.GetRandomTarget(aiActor, randomMove)
			};
			var damageAbilities = randomMove.Abilities.Where(x => x is DamageAbility).Select(x => x as DamageAbility);
			foreach(var damageAbility in damageAbilities)
			{
				damageAbility.AssignedDamage = Container.DamageAssign(aiActor, moveAction.Target, damageAbility);
			}
			var tripleDamageAbilities = randomMove.Abilities.Where(x => x is TriplePowerDamageAbility).Select(x => x as TriplePowerDamageAbility);
			foreach (var damageAbility in tripleDamageAbilities)
			{
				damageAbility.AssignedDamage = Container.DamageAssign(aiActor, moveAction.Target, damageAbility);
			}
			aiActor.QueuedMoveAction = moveAction;
		}
	}

    private void StartTurn(object sender, object args)
	{
		var action = args as StartTurnAction;
		var actor = action.Actor as AIActor;
	}

	private void EndTurn(object sender, object args)
	{
		var action = args as EndTurnAction;
		var actor = action.Actor as AIActor;
		if(actor != null)
		{
			var actorsInFaction = Container.GetBattle().Actors.Where(x => x.Faction == Faction.Enemy);
			var nextActor = actorsInFaction.FirstOrDefault(x => x.HasActed == false);
			if (nextActor != null)
			{
				nextActor.HasActed = false;
				Container.Perform(new StartTurnAction(nextActor));
			}
		}
	}

	private void OnPerformAddActor(object sender, object args)
	{
		var action = args as AddActorAction;
		var actor = action.Actor as AIActor;
		if (actor != null)
		{
			var atlas = Container.GetBattleAtlas();
			if(atlas != null)
            {
				actor.Moves = new List<Move>();
				foreach(var moveId in actor.MoveFromAtlas)
                {
					var moveFromAtlas = atlas.GetMove(moveId);
					if(moveFromAtlas != null)
                    {
						actor.Moves.Add(moveFromAtlas);
                    }
                }
            }
		}
	}
}
