using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ImproviseSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnChoose, Global.PerformNotification<ChooseImprovisableAction>());
        this.AddObserver(OnChooseValidate, Global.ValidateNotification<ChooseImprovisableAction>());
        this.AddObserver(OnImprovise, Global.PerformNotification<ImproviseAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnChoose, Global.PerformNotification<ChooseImprovisableAction>());
        this.AddObserver(OnChooseValidate, Global.ValidateNotification<ChooseImprovisableAction>());
        this.RemoveObserver(OnImprovise, Global.PerformNotification<ImproviseAction>());
    }
    private void OnChooseValidate(object sender, object args)
    {
        var action = args as ChooseImprovisableAction;
        var actor = action.Actor;
        if (action != null && actor != null)
        {
            if(!actor.ImprovisableCards.Contains(action.ChosenCard))
            {
                action.SetInvalid();
            }
        }
    }

    private void OnChoose(object sender, object args)
    {
        var action = args as ChooseImprovisableAction;
        var actor = action.Actor;
        if(action != null && actor != null)
        {
            actor.Hand.Add(action.ChosenCard);
            actor.ImprovisableCards.Clear();
        }
    }

    private void OnImprovise(object sender, object args)
    {
        var action = args as ImproviseAction;
        var actor = action.Source as DeckbuilderActor;
        if (action != null && actor != null)
        {
            var cardsToImproviseFrom = new List<Card>();

            switch(action.Type)
            {
                case ImproviseType.Deck:
                    var cardsToRevealFromDeck = Math.Min(actor.Deck.Count, 3);
                    cardsToImproviseFrom.AddRange(actor.Deck.PickRandom(cardsToRevealFromDeck));
                    break;
                case ImproviseType.Discard:
                    var cardsToRevealFromDiscard = Math.Min(actor.DiscardPile.Count, 3);
                    cardsToImproviseFrom.AddRange(actor.DiscardPile.PickRandom(cardsToRevealFromDiscard));
                    break;
                case ImproviseType.Market:
                    var atlas = Container.GetBattleAtlas();
                    var shuffled = action.MarketIds.Shuffle();
                    foreach(var id in shuffled.Take(3))
                    {
                        var cardFromAtlas = atlas.GetCard(id);
                        if(cardFromAtlas != null)
                        {
                            cardsToImproviseFrom.Add(cardFromAtlas);
                        }
                    }
                    break;
            }
            actor.ImprovisableCards = cardsToImproviseFrom;
        }
    }
}
