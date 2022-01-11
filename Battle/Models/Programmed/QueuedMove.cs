using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QueuedMove : Move
{
    public int Position { get; set; }

    public QueuedMove(Move move, int position)
        : base(move)
    {
        Position = position;
    }
}
