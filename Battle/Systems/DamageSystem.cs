using System;
using System.Linq;

public class DamageSystem : Aspect, IObservable
{
	public void Awake()
	{
		this.AddObserver(OnPerformDamageAction, Global.PerformNotification<DamageAction>());
		this.AddObserver(OnPerformTripleDamageAction, Global.PerformNotification<TriplePowerDamageAction>());
	}

	public void Destroy()
	{
		this.RemoveObserver(OnPerformDamageAction, Global.PerformNotification<DamageAction>());
		this.RemoveObserver(OnPerformTripleDamageAction, Global.PerformNotification<TriplePowerDamageAction>());
	}

	private void OnPerformDamageAction(object sender, object args)
	{
		var action = args as DamageAction;
		foreach (Actor target in action.Targets)
		{
			var damage = TotalDamage(action.MinimumAmount, action.MaximumAmount, action.Source, target, 1, action.Type);
			if (action.AssignedDamage.HasValue)
			{
				damage = action.AssignedDamage.Value;
			}
			action.DamageDealt = damage;
			var damageToGuard = Math.Min(damage, target.Guard);
			var damageToHealth = damage - damageToGuard;
			if (!(target is ProgrammedActor))
			{
				target.Guard -= damageToGuard;
				target.CurrentHP -= damageToHealth;
				target.Morale -= damageToHealth;
				if (target.CurrentHP <= 0)
				{
					var dieAction = new DieAction
					{
						Actor = target
					};
					Container.Perform(dieAction);
				}
			}
			else
			{
				var asProgrammed = target as ProgrammedActor;
				if (asProgrammed.MoveQueue.Any())
				{
					asProgrammed.StoredDamage += (damageToGuard + damageToHealth);
				}
				else
				{
					target.Guard -= damageToGuard;
					target.CurrentHP -= damageToHealth;
					target.Morale -= damageToHealth;
					if (target.CurrentHP <= 0)
					{
						var dieAction = new DieAction
						{
							Actor = target
						};
						Container.Perform(dieAction);
					}
				}
			}
		}
	}

	private void OnPerformTripleDamageAction(object sender, object args)
	{
		var action = args as TriplePowerDamageAction;
		foreach (Actor target in action.Targets)
		{
			var damage = TotalDamage(action.MinimumAmount, action.MaximumAmount, action.Source, target, 3, action.Type);
			if (action.AssignedDamage.HasValue)
			{
				damage = action.AssignedDamage.Value;
			}
			action.DamageDealt = damage;
			var damageToGuard = Math.Min(damage, target.Guard);
			var damageToHealth = damage - damageToGuard;
			if(!(target is ProgrammedActor))
			{
				target.Guard -= damageToGuard;
				target.CurrentHP -= damageToHealth;
				target.Morale -= damageToHealth;
				if (target.CurrentHP <= 0)
				{
					var dieAction = new DieAction
					{
						Actor = target
					};
					Container.Perform(dieAction);
				}
			}
			else
			{
				var asProgrammed = target as ProgrammedActor;
				if (asProgrammed.MoveQueue.Any())
				{
					asProgrammed.StoredDamage += (damageToGuard + damageToHealth);
				}
				else
				{
					target.Guard -= damageToGuard;
					target.CurrentHP -= damageToHealth;
					target.Morale -= damageToHealth;
					if (target.CurrentHP <= 0)
					{
						var dieAction = new DieAction
						{
							Actor = target
						};
						Container.Perform(dieAction);
					}
				}
			}
		}
	}

	public int TotalDamage(int min, int max, Actor source, Actor target, int powMult, DamageType type)
	{
		if(target == null)
		{
			return 0;
		}
		var targetVulnerableStatus = target.Statuses.Any(x => x.Type == StatusType.Vulnerable) && source != target;
		var sourceWeakStatus = source.Statuses.Any(x => x.Type == StatusType.Weakness) && source != target;
		var powerStatus = source.Statuses.FirstOrDefault(x => x.Type == StatusType.Power);
		var powerBonus = 0;
		if (powerStatus != null)
		{
			powerBonus += (powerStatus.Stacks * powMult);
		}
		var woundStatus = target.Statuses.FirstOrDefault(x => x.Type == StatusType.Wound);
		var woundBonus = 0;
		if(woundStatus != null && source != target)
		{
			woundBonus += woundStatus.Stacks;
		}
		var targetFireVulnerableStatus = target.Statuses.Any(x => x.Type == StatusType.FireVulnerable) && source != target && type == DamageType.Fire;

		var rand = new Random();
		var amount = rand.Next(min, max + 1);

		var totalDamage = (amount + powerBonus + woundBonus) *
			(targetVulnerableStatus ? 1.5 : 1) *
			(targetFireVulnerableStatus ? 1.5 : 1) *
			(sourceWeakStatus ? .5 : 1);
		var damage = (int)Math.Ceiling(totalDamage);
		return damage;
	}
}

public static class DamageSystemExtensions
{
	public static int DamageAssign(this IContainer game, Actor source, Actor target, Ability ability)
	{
		var damageSystem = game.GetAspect<DamageSystem>();
		if(ability is DamageAbility)
		{
			var asDamage = ability as DamageAbility;
			return damageSystem.TotalDamage(asDamage.MinimumAmount, asDamage.MaximumAmount, source, target, 1, asDamage.Type);
		}
		if (ability is TriplePowerDamageAbility)
		{
			var asDamage = ability as TriplePowerDamageAbility;
			return damageSystem.TotalDamage(asDamage.MinimumAmount, asDamage.MaximumAmount, source, target, 3, asDamage.Type);
		}
		return 0;
	}
}
