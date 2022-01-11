using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IState : IAspect
{
    Node Parent { get; set; }
    object Data { get; set; }
    void Enter();
    bool CanTransition(IState other);
    void Exit();
}

public abstract class BaseState : Aspect, IState
{
    public Node Parent { get; set; }
    public object Data { get; set; }
    public virtual void Enter() { }
    public virtual bool CanTransition(IState other) { return true; }
    public virtual void Exit() { }
}
