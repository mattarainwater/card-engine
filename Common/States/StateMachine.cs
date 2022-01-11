using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StateMachine : Aspect
{
	public IState CurrentState { get; private set; }
	public IState PreviousState { get; private set; }
	public Node Parent { get; set; }
	public bool Deactive { get; set; }

	public StateMachine()
	{

	}

	public StateMachine(Node parent)
	{
		Parent = parent;
		Container = new Container();
		Container.AddAspect(this);
	}

	public void ChangeState(IState toState, object data = null)
	{
		if(!Deactive)
		{
			IState fromState = CurrentState;
			if (fromState != null)
			{
				if (fromState == toState || fromState.CanTransition(toState) == false)
				{
					return;
				}
				fromState.Exit();
			}
			CurrentState = toState;
			PreviousState = fromState;
			CurrentState.Container = Container;
			CurrentState.Parent = Parent;
			CurrentState.Data = data;
			CurrentState.Enter();
		}
	}

	public void Deactivate()
	{
		Deactive = true;
	}
}

public static class StateMachineExtensions
{
	public static void ChangeState(this IContainer container, IState toState, object data = null)
	{
		var stateMachine = container.GetAspect<StateMachine>();
		stateMachine.ChangeState(toState, data);
	}
}
