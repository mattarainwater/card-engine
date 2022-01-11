using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeckbuilderSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.AddObserver(GainEnergy, Global.PerformNotification<GainEnergyAction>());
        this.AddObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.AddObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(StartTurn, Global.PerformNotification<StartTurnAction>());
        this.RemoveObserver(GainEnergy, Global.PerformNotification<GainEnergyAction>());
        this.RemoveObserver(EndTurn, Global.PerformNotification<EndTurnAction>());
        this.RemoveObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
    }

    private void StartTurn(object sender, object args)
    {
        var action = args as StartTurnAction;
        var actor = action.Actor as DeckbuilderActor;
        if (actor != null)
        {
            var drawCardsAction = new DrawCardsAction(actor)
            {
                Amount = actor.DrawValue
            };
            Container.Perform(drawCardsAction);

            actor.Energy = new Energy
            {
                CurrentEnergy = actor.Energy.MaxEnergy
            };
        }
    }

    private void GainEnergy(object sender, object args)
    {
        var action = args as GainEnergyAction;
        var actor = action.Actor;
        actor.Energy.CurrentEnergy += action.Amount;
    }

    private void EndTurn(object sender, object args)
    {
        var action = args as EndTurnAction;
        var actor = action.Actor as DeckbuilderActor;
        if (actor != null)
        {
            actor.DiscardPile.AddRange(actor.Hand);
            actor.Hand.Clear();
        }
    }

    private void OnPerformAddActor(object sender, object args)
    {
        var action = args as AddActorAction;
        var actor = action.Actor as DeckbuilderActor;
        if(actor != null)
        {
            var atlas = Container.GetBattleAtlas();
            if (atlas != null)
            {
                actor.Deck = new List<Card>();
                foreach (var cardId in actor.CardsFromAtlas)
                {
                    var cardFromAtlas = atlas.GetCard(cardId);
                    if (cardFromAtlas != null)
                    {
                        actor.Deck.Add(cardFromAtlas);
                    }
                }
            }
            var shuffleAction = new ShuffleAction()
            {
                Actor = actor
            };
            Container.Perform(shuffleAction);
        }
    }
}
