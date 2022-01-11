using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PairerActor : Actor
{
    public const int MAX_FOCUS_TOTAL = 5;

    public PairerActor()
        : base()
    {
        Styles = new List<Move>();
        Bases = new List<Move>();
        StyleDiscardQueue = new Queue<Move>();
        BaseDiscardQueue = new Queue<Move>();
    }

    public Move CurrentStyle { get; set; }
    public Move CurrentBase { get; set; }
    public List<Move> Styles { get; set; }
    public List<int> StylesFromAtlas { get; set; }
    public List<Move> Bases { get; set; }
    public List<int> BasesFromAtlas { get; set; }
    public Queue<Move> StyleDiscardQueue { get; set; }
    public Queue<Move> BaseDiscardQueue { get; set; }
    public int Focus { get; set; }
    public int Fury { get; set; }
}
