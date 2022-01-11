using System;
using System.Linq;

public class HealingSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformHealing, Global.PerformNotification<HealingAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformHealing, Global.PerformNotification<HealingAction>());
    }

    void OnPerformHealing(object sender, object args)
    {
        var action = args as HealingAction;
        foreach (Actor target in action.Targets)
        {
            target.CurrentHP += action.Amount;
            target.CurrentHP = Math.Min(target.CurrentHP, target.MaxHP);
        }
    }
}
