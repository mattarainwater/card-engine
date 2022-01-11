using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TargetSystem : Aspect
{
    public List<Actor> GetAllowedTargets(Actor source, MoveTarget moveTarget, Battle battle)
    {
        var targetGroups = new List<Actor>();
        switch(moveTarget)
        {
            case MoveTarget.None:
                break;
            case MoveTarget.Ally:
                foreach (var ally in battle.Actors.Where(x => x.Faction == source.Faction))
                {
                    targetGroups.Add(ally);
                }
                break;
            case MoveTarget.Enemy:
                foreach (var enemy in battle.Actors.Where(x => x.Faction != source.Faction))
                {
                    targetGroups.Add(enemy);
                }
                break;
            default:
                break;
        }
        return targetGroups.Where(x => x.Alive).ToList();
    }

    public List<Actor> GetTargetsForAbility(Actor source, MoveAction moveAction, Battle battle, Ability ability)
    {
        var targetableSide = ability.AllowedTargets;
        switch (targetableSide)
        {
            case AbilityTarget.All:
                return battle.Actors.Where(x => x.Alive).ToList();
            case AbilityTarget.AllAllies:
                return battle.Actors.Where(x => x.Faction == source.Faction && x.Alive).ToList();
            case AbilityTarget.AllEnemies:
                return battle.Actors.Where(x => x.Faction != source.Faction && x.Alive).ToList();
            case AbilityTarget.Move:
                return new List<Actor> { moveAction.Target }.Where(x => x != null && x.Alive).ToList();
            case AbilityTarget.Self:
                return new List<Actor> { source }.Where(x => x != null && x.Alive).ToList();
            case AbilityTarget.RandomEnemy:
                return new List<Actor> { battle.Actors.Where(x => x.Faction != source.Faction && x.Alive).PickRandom() };
        }
        return new List<Actor>();
    }

    public Actor GetRandomTarget(Actor source, MoveTarget moveTarget, Battle battle)
    {
        var allowedTargets = GetAllowedTargets(source, moveTarget, battle);
        return allowedTargets.PickRandom();
    }

    public bool ValidateTargets(Actor source, MoveTarget moveTarget, Battle battle, Actor selectedTarget)
    {
        var allowedTargets = GetAllowedTargets(source, moveTarget, battle);
        return allowedTargets.Contains(selectedTarget) || (selectedTarget == null && !allowedTargets.Any());
    }
}

public static class TargetSystemExtensions
{
    public static List<Actor> GetAllowedTargets(this IContainer game, Actor source, MoveTarget moveTarget)
    {
        var targetSystem = game.GetAspect<TargetSystem>();
        return targetSystem.GetAllowedTargets(source, moveTarget, game.GetBattle());
    }

    public static Actor GetRandomTarget(this IContainer game, Actor source, Move move)
    {
        var targetSystem = game.GetAspect<TargetSystem>();
        return targetSystem.GetRandomTarget(source, move.AllowedTargets, game.GetBattle());
    }

    public static bool ValidateTargets(this IContainer game, Actor source, Move move, Actor selectedTarget)
    {
        var targetSystem = game.GetAspect<TargetSystem>();
        return targetSystem.ValidateTargets(source, move.AllowedTargets, game.GetBattle(), selectedTarget);
    }
    public static List<Actor> GetTargetsForAbility(this IContainer game, Actor source, MoveAction moveAction, Ability ability)
    {
        var targetSystem = game.GetAspect<TargetSystem>();
        return targetSystem.GetTargetsForAbility(source, moveAction, game.GetBattle(), ability);
    }
}