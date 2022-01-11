using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayCardSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(PlayCard, Global.PerformNotification<PlayCardAction>());
        this.AddObserver(ValidateCard, Global.ValidateNotification<PlayCardAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(PlayCard, Global.PerformNotification<PlayCardAction>());
        this.RemoveObserver(ValidateCard, Global.ValidateNotification<PlayCardAction>());
    }

    private void ValidateCard(object sender, object args)
    {
        var action = args as PlayCardAction;
        var actor = action.Actor;
        var card = action.Card;
        if (actor.Energy.CurrentEnergy < action.Card.EnergyCost)
        {
            action.SetInvalid();
        }
        if(!actor.Hand.Contains(card))
        {
            action.SetInvalid();
        }
        if (!Container.ValidateTargets(actor, action.Move, action.Target))
        {
            action.SetInvalid();
        }
    }

    private void PlayCard(object sender, object args)
    {
        var action = args as PlayCardAction;
        var actor = action.Actor;
        var card = action.Card;
        if (actor != null)
        {
            var actionAsMove = new MoveAction(action);
            Container.Perform(actionAsMove);
            actor.Energy.CurrentEnergy -= action.Card.EnergyCost;
            actor.Hand.Remove(card);
            if (card.ExhaustsOnUse)
            {
                actor.Trash.Add(card);
            }
            else
            {
                actor.DiscardPile.Add(card);
            }
        }
    }
}
