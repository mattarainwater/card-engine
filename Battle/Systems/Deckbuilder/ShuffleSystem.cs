using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShuffleSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformShuffle, Global.PerformNotification<ShuffleAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformShuffle, Global.PerformNotification<ShuffleAction>());
    }

    private void OnPerformShuffle(object sender, object args)
    {
        var shuffleAction = args as ShuffleAction;
        var actor = shuffleAction.Actor;
        var deck = actor.Deck;
        var discard = actor.DiscardPile;
        deck.AddRange(discard);
        actor.Deck = deck.Shuffle().ToList();
        discard.Clear();
    }
}
