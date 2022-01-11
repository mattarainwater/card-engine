using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ProgrammedSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.AddObserver(QueueMove, Global.PerformNotification<QueueMovesAction>());
        this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.AddObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
        this.AddObserver(ValidateQueue, Global.ValidateNotification<QueueMovesAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.RemoveObserver(QueueMove, Global.PerformNotification<QueueMovesAction>());
        this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.RemoveObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
        this.RemoveObserver(ValidateQueue, Global.ValidateNotification<QueueMovesAction>());
    }

    private void ValidateQueue(object sender, object args)
    {
        var action = args as QueueMovesAction;
        var actor = action.Actor;
        if(actor.MoveQueue.Count() >= ProgrammedActor.MAX_QUEUE_SIZE)
        {
            action.SetInvalid();
        }
    }

    private void OnPerformAddActor(object sender, object args)
    {
        var action = args as AddActorAction;
        var actor = action.Actor as ProgrammedActor;
        if (actor != null)
        {
            var atlas = Container.GetBattleAtlas();
            if (atlas != null)
            {
                actor.Moves = new List<Move>();
                foreach (var baseId in actor.QueueableMovesFromAtlas)
                {
                    var baseFromAtlas = atlas.GetMove(baseId);
                    if (baseFromAtlas != null)
                    {
                        actor.Moves.Add(baseFromAtlas);
                    }
                }
            }
        }
    }

    private void QueueMove(object sender, object args)
    {
        var action = args as QueueMovesAction;
        var actor = action.Actor;
        if(actor != null)
        {
            for(var i = 0; i < action.MovesToQueue.Count; i++)
            {
                actor.MoveQueue.Enqueue(new QueuedMove(action.MovesToQueue[i], i + 1));
            }
        }
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        var actor = action.Actor as ProgrammedActor;
    }

    private void EndTurn(object sender, object args)
    {
        var action = args as EndTurnAction;
        var actor = action.Actor as ProgrammedActor;
        if (actor != null && actor.MoveQueue.Any())
        {
            var nextMove = actor.MoveQueue.Dequeue();
            var moveAction = new MoveAction(actor)
            {
                Move = nextMove,
                Target = null
            };
            Container.Perform(moveAction);
            if (!actor.MoveQueue.Any())
            {
                var damageAction = new DamageAction(actor)
                {
                    MinimumAmount = actor.StoredDamage,
                    MaximumAmount = actor.StoredDamage,
                    Targets = new List<Actor> { actor }
                };
                Container.Perform(damageAction);
                actor.StoredDamage = 0;
            }
        }
    }
}
