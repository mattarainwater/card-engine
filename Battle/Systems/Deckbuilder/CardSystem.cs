using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CardSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(DiscardCard, Global.PerformNotification<DiscardCardsAction>());
        this.AddObserver(ValidateDiscardCard, Global.ValidateNotification<DiscardCardsAction>());

        this.AddObserver(ExhaustCard, Global.PerformNotification<ExhaustCardsAction>());
        this.AddObserver(ValidateExhaustCard, Global.ValidateNotification<ExhaustCardsAction>());

        this.AddObserver(AddCard, Global.PerformNotification<AddCardAction>());
        this.AddObserver(ValidateAddCard, Global.ValidateNotification<AddCardAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(DiscardCard, Global.PerformNotification<DiscardCardsAction>());
        this.RemoveObserver(ValidateDiscardCard, Global.ValidateNotification<DiscardCardsAction>());

        this.AddObserver(ExhaustCard, Global.PerformNotification<ExhaustCardsAction>());
        this.AddObserver(ValidateExhaustCard, Global.ValidateNotification<ExhaustCardsAction>());

        this.AddObserver(AddCard, Global.PerformNotification<AddCardAction>());
        this.AddObserver(ValidateAddCard, Global.ValidateNotification<AddCardAction>());
    }

    private void ValidateDiscardCard(object sender, object args)
    {
        var action = args as DiscardCardsAction;
    }

    private void DiscardCard(object sender, object args)
    {
        var action = args as DiscardCardsAction;
        var actor = action.Source as DeckbuilderActor;
        if (actor != null)
        {
            foreach(var card in action.CardsToDiscard)
            {
                actor.Hand.Remove(card);
                actor.Deck.Remove(card);
                actor.DiscardPile.Add(card);
            }
        }
    }

    private void ValidateExhaustCard(object sender, object args)
    {
        var action = args as ExhaustCardsAction;
    }

    private void ExhaustCard(object sender, object args)
    {
        var action = args as ExhaustCardsAction;
        var actor = action.Source as DeckbuilderActor;
        if (actor != null)
        {
            foreach (var card in action.CardsToExhaust)
            {
                actor.Hand.Remove(card);
                actor.Deck.Remove(card);
                actor.DiscardPile.Remove(card);
                actor.Trash.Add(card);
            }
        }
    }

    private void ValidateAddCard(object sender, object args)
    {
        var action = args as AddCardAction;
    }

    private void AddCard(object sender, object args)
    {
        var action = args as AddCardAction;
        var actor = action.Source as DeckbuilderActor;
        if (actor != null)
        {
            var atlas = Container.GetBattleAtlas();
            var card = atlas.GetCard(action.CardId);
            switch(action.Desintation)
            {
                case Zone.Deck:
                    actor.Deck.Add(card);
                    Container.Perform(new ShuffleAction() { 
                        Actor = actor
                    });
                    break;
                case Zone.DiscardPile:
                    actor.DiscardPile.Add(card);
                    break;
                case Zone.Hand:
                    actor.Hand.Add(card);
                    break;
                case Zone.Trash:
                    actor.Trash.Add(card);
                    break;
            }
        }
    }
}
