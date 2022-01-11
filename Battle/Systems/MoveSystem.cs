using System;
using System.Linq;

public class MoveSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformMoveAction, Global.PerformNotification<MoveAction>());
        this.AddObserver(ValidateMove, Global.ValidateNotification<MoveAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformMoveAction, Global.PerformNotification<MoveAction>());
        this.RemoveObserver(ValidateMove, Global.ValidateNotification<MoveAction>());
    }

    private void ValidateMove(object sender, object args)
    {
        var action = args as MoveAction;
        var actor = action.Source;
        if (!Container.ValidateTargets(actor, action.Move, action.Target))
        {
            action.SetInvalid();
        }
    }

    private void OnPerformMoveAction(object sender, object args)
    {
        var moveAction = args as MoveAction;
        var move = moveAction.Move;
        if(move != null)
        {
            foreach (var ability in move.Abilities)
            {
                ability.Source = moveAction.Source;
                var actionFromAbility = ability.ToAction();
                actionFromAbility.Targets = Container.GetTargetsForAbility(moveAction.Source, moveAction, ability);

                Container.Perform(actionFromAbility);
            }
        }
    }
}
