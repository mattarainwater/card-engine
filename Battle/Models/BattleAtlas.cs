using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleAtlas
{
    public BattleAtlas()
    {
    }

    public List<Actor> Units { private get; set; }
    public List<Card> Cards { private get; set; }
    public List<Move> Moves { private get; set; }

    public Actor GetActor(int id)
    {
        var actor = Units.FirstOrDefault(x => x.Id == id);
        if (actor != null)
        {
            return actor.Copy();
        }
        return null;
    }

    public Card GetCard(int id)
    {
        var card = Cards.FirstOrDefault(x => x.Id == id);
        if(card != null)
        {
            return card.Copy();
        }
        return null;
    }

    public Move GetMove(int id)
    {
        var move = Moves.FirstOrDefault(x => x.Id == id);
        if (move != null)
        {
            return move.Copy();
        }
        return null;
    }
}
