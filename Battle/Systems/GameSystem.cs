using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GameSystem : IAspect
{
	public IContainer Container
	{
		get
		{
			if (_container == null)
			{
				_container = GameFactory.Create();
				_container.AddAspect(this);
			}
			return _container;
		}
		set
		{
			_container = value;
		}
	}

	private IContainer _container;
	private ActionSystem _actionSystem;

	public void Initialize(BattleAtlas atlas)
	{
		Container.Awake();
		_actionSystem = Container.GetAspect<ActionSystem>();
		Container.SetBattleAtlas(atlas);
    }

	public void Destroy()
    {
		Container.Destroy();
    }

    public void Perform(GameAction action)
    {
        _actionSystem.Perform(action);
	}

	public void LoadActors(List<int> actors)
	{
		var atlas = Container.GetBattleAtlas();
		if(atlas != null)
		{
			foreach (var actor in actors)
			{
				var actorFromAtlas = atlas.GetActor(actor);
				if(actorFromAtlas != null)
				{
					Perform(new AddActorAction
					{
						Actor = actorFromAtlas
					});
				}
			}
		}
		var battle = GetBattle();
	}

	public T GetAspect<T>() where T : IAspect
    {
        return Container.GetAspect<T>();
    }

	public Battle GetBattle()
    {
		var dataSystem = Container.GetAspect<DataSystem>();
		return dataSystem.Battle;
	}
}
