using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeckbuilderActor : Actor
{
    public const int MAX_HAND = 10;

    public DeckbuilderActor()
        : base()
    {
        Energy = new Energy();
        Deck = new List<Card>();
        DiscardPile = new List<Card>();
        Hand = new List<Card>();
        Trash = new List<Card>();
        ImprovisableCards = new List<Card>();
    }

    public Energy Energy { get; set; }
    public int DrawValue { get; set; } = 5;
    public List<Card> Deck { get; set; }
    public List<Card> DiscardPile { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> Trash { get; set; }
    public List<Card> ImprovisableCards { get; set; }
    public List<int> CardsFromAtlas { get; set; }
}
