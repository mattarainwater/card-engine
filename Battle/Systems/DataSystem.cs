using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataSystem : Aspect, IObservable
{
	public Battle Battle = new Battle();
	public BattleAtlas BattleAtlas = new BattleAtlas();

	public void Awake()
	{
		this.AddObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
	}

	public void Destroy()
	{
		this.RemoveObserver(OnPerformAddActor, Global.PerformNotification<AddActorAction>());
	}

	private void OnPerformAddActor(object sender, object args)
	{
		AddActorAction action = args as AddActorAction;
		var actors = Battle.Actors;
		if(actors == null)
		{
			actors = new List<Actor>();
		}
		actors.Add(action.Actor);
	}
}

public static class DataSystemExtensions
{
	public static Battle GetBattle(this IContainer game)
	{
		var dataSystem = game.GetAspect<DataSystem>();
		return dataSystem.Battle;
	}

	public static string GetBattleAsJson(this IContainer game)
	{
		var dataSystem = game.GetAspect<DataSystem>();
		var battleAsJson = JsonConvert.SerializeObject(dataSystem.Battle);
		return battleAsJson;
	}

	public static void SetBattleAtlas(this IContainer game, BattleAtlas atlas)
	{
		var dataSystem = game.GetAspect<DataSystem>();
		dataSystem.BattleAtlas = atlas;
	}

	public static BattleAtlas GetBattleAtlas(this IContainer game)
	{
		var dataSystem = game.GetAspect<DataSystem>();
		return dataSystem.BattleAtlas;
	}
}
