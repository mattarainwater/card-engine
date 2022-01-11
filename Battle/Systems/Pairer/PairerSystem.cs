using System;
using System.Collections.Generic;
using System.Linq;

public class PairerSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.AddObserver(OnFocus, Global.PerformNotification<PairerFocusAction>());
        this.AddObserver(OnPairAction, Global.PerformNotification<PairAction>());
        this.AddObserver(ValidatePairAction, Global.ValidateNotification<PairAction>());
        this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.AddObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.RemoveObserver(OnFocus, Global.PerformNotification<PairerFocusAction>());
        this.RemoveObserver(OnPairAction, Global.PerformNotification<PairAction>());
        this.RemoveObserver(ValidatePairAction, Global.ValidateNotification<PairAction>());
        this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.RemoveObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
    }

    private void ValidatePairAction(object sender, object args)
    {
        var action = args as PairAction;
        var actor = action.Actor;
        if (!Container.ValidateTargets(actor, action.Base, action.Target))
        {
            action.SetInvalid();
        }
    }

    private void OnPerformAddActor(object sender, object args)
    {
        var action = args as AddActorAction;
        var actor = action.Actor as PairerActor;
        if (actor != null)
        {
            var atlas = Container.GetBattleAtlas();
            if (atlas != null)
            {
                actor.Bases = new List<Move>();
                foreach (var baseId in actor.BasesFromAtlas)
                {
                    var baseFromAtlas = atlas.GetMove(baseId);
                    if (baseFromAtlas != null)
                    {
                        actor.Bases.Add(baseFromAtlas);
                    }
                }
                actor.Styles = new List<Move>();
                foreach (var styleId in actor.StylesFromAtlas)
                {
                    var styleFromAtlas = atlas.GetMove(styleId);
                    if (styleFromAtlas != null)
                    {
                        actor.Styles.Add(styleFromAtlas);
                    }
                }
            }
            actor.Focus = (int)Math.Ceiling(PairerActor.MAX_FOCUS_TOTAL / 2f);
            actor.Fury = (int)Math.Floor(PairerActor.MAX_FOCUS_TOTAL / 2f);
        }
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        var actor = action.Actor as PairerActor;
        if (actor != null)
        {
            if(actor.BaseDiscardQueue.Any())
            {
                actor.Bases.Add(actor.BaseDiscardQueue.Dequeue());
            }
            if(actor.StyleDiscardQueue.Any())
            {
                actor.Styles.Add(actor.StyleDiscardQueue.Dequeue());
            }
        }
    }

    private void OnFocus(object sender, object args)
    {
        var action = args as PairerFocusAction;
        var actor = action.Source as PairerActor;
        if (actor != null)
        {
            actor.Focus += action.Amount;
            actor.Focus = actor.Focus.Clamp(0, PairerActor.MAX_FOCUS_TOTAL);
            actor.Fury -= action.Amount;
            actor.Fury = actor.Fury.Clamp(0, PairerActor.MAX_FOCUS_TOTAL);
        }
    }

    private void OnPairAction(object sender, object args)
    {
        var action = args as PairAction;
        var actor = action.Actor;
        if (actor != null)
        {
            var styleAsMove = new MoveAction(action)
            {
                Move = action.Style,
                Target = null
            };
            Container.Perform(styleAsMove);
            var baseAsMove = new MoveAction(action)
            {
                Move = action.Base
            };
            Container.Perform(baseAsMove);
            actor.CurrentStyle = action.Style;
            actor.CurrentBase = action.Base;
            Container.Perform(new EndTurnAction(actor));
        }
    }

    private void EndTurn(object sender, object args)
    {
        var action = args as EndTurnAction;
        var actor = action.Actor as PairerActor;
        if (actor != null)
        {
            actor.Styles.Remove(actor.CurrentStyle);
            actor.StyleDiscardQueue.Enqueue(actor.CurrentStyle);
            actor.CurrentStyle = null;

            actor.Bases.Remove(actor.CurrentBase);
            actor.BaseDiscardQueue.Enqueue(actor.CurrentBase);
            actor.CurrentBase = null;
        }
    }
}
