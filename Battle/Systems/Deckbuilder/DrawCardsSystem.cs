using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DrawCardsSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformDrawCards, Global.PerformNotification<DrawCardsAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformDrawCards, Global.PerformNotification<DrawCardsAction>());
    }

    private void OnPerformDrawCards(object sender, object args)
    {
        var drawCardsAction = args as DrawCardsAction;
        var actor = drawCardsAction.Source as DeckbuilderActor;
        var deck = actor.Deck;
        if(deck.Count() >= drawCardsAction.Amount)
        {
            var cardsToDraw = deck.Draw(drawCardsAction.Amount);
            actor.Hand.AddRange(cardsToDraw);
            drawCardsAction.CardsDrawn = cardsToDraw;
        }
        else
        {
            var deckCount = deck.Count();
            var cardsToDraw = deck.Draw(deckCount);
            actor.Hand.AddRange(cardsToDraw);
            drawCardsAction.CardsDrawn = cardsToDraw;
            if (actor.DiscardPile.Any())
            {
                Container.Perform(new ShuffleAction()
                {
                    Actor = actor
                });
                Container.Perform(new DrawCardsAction(actor)
                {
                    Amount = drawCardsAction.Amount - deckCount
                });
            }
        }
    }
}
