using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActionSystem : Aspect
{
    private Queue<GameAction> _queuedActions;
    public GameAction CurrentAction { get; set; }
    public const string ACTION_COMPLETE = ".actionComplete";

    public ActionSystem()
    {
        _queuedActions = new Queue<GameAction>();
    }

    public override void _Process(float delta)
    {
        if (CurrentAction == null)
        {
            return;
        }
        else
        {
            var actionType = CurrentAction.GetType();
            if (CurrentAction.IsValid)
            {
                this.PostNotification(Global.PrepareNotification(actionType), CurrentAction);
                if (!CurrentAction.IsReady.HasValue || CurrentAction.IsReady.Value)
                {
                    GD.Print(CurrentAction.GetLog());
                    this.PostNotification(Global.PerformNotification(actionType), CurrentAction);
                    this.PostNotification(Global.PostPerformNotification(actionType), CurrentAction);
                    this.PostNotification(ACTION_COMPLETE);
                    var isCheckVictoryAction = CurrentAction is CheckVictoryAction;
                    CurrentAction = null;
                    if (_queuedActions.Any())
                    {
                        var action = _queuedActions.Dequeue();
                        Perform(action);
                    }
                    else if(!isCheckVictoryAction)
                    {
                        var victoryAction = new CheckVictoryAction();
                        Perform(victoryAction);
                    }
                }
                else if(CurrentAction is MoveAction)
                {
                    var asMove = CurrentAction as MoveAction;
                    if(!asMove.Source.Alive)
                    {
                        CurrentAction = null;
                        Perform(new EndTurnAction(asMove.Source));
                    }    
                }
            }
            else
            {
                CurrentAction = null;
                if (_queuedActions.Any())
                {
                    Perform(_queuedActions.Dequeue());
                }
            }
        }
    }

    public void Perform(GameAction action)
    {
        if(CurrentAction == null)
        {
            var actionType = action.GetType();
            this.SyncPostNotification(Global.ValidateNotification(actionType), action);
            if(action.IsValid)
            {
                CurrentAction = action;
            }
        }
        else
        {
            _queuedActions.Enqueue(action);
        }
    }

    public void Clear()
    {
        CurrentAction = null;
        _queuedActions.Clear();
    }
}

public static class ActionSystemExtensions
{
    public static void Perform(this IContainer game, GameAction action)
    {
        var actionSystem = game.GetAspect<ActionSystem>();
        actionSystem.Perform(action);
    }

    public static void Clear(this IContainer game)
    {
        var actionSystem = game.GetAspect<ActionSystem>();
        actionSystem.Clear();
    }

    public static GameAction GetCurrentAction(this IContainer game)
    {
        var actionSystem = game.GetAspect<ActionSystem>();
        return actionSystem.CurrentAction;
    }
}
