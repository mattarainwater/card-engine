using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProgrammedActor : Actor
{
    public const int MAX_QUEUE_SIZE = 3;

    public ProgrammedActor()
        : base()
    {
        Moves = new List<Move>();
        MoveQueue = new Queue<QueuedMove>();
    }

    public List<Move> Moves { get; set; }
    public List<int> QueueableMovesFromAtlas { get; set; }
    public Queue<QueuedMove> MoveQueue { get; set; }
    public int StoredDamage { get; set; }
}
