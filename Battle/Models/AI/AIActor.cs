using System.Collections.Generic;

public class AIActor : Actor
{
    public AIActor()
        : base()
    {

    }

    public List<Move> Moves { get; set; }
    public MoveAction QueuedMoveAction { get; set; }
}
