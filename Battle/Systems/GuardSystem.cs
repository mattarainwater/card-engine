public class GuardSystem : Aspect, IObservable
{
    public void Awake()
    {
        this.AddObserver(OnPerformGuardAction, Global.PerformNotification<GuardAction>());
    }

    public void Destroy()
    {
        this.RemoveObserver(OnPerformGuardAction, Global.PerformNotification<GuardAction>());
    }

    void OnPerformGuardAction(object sender, object args)
    {
        var action = args as GuardAction;
        foreach (Actor target in action.Targets)
        {
            target.Guard += action.Amount;
        }
    }
}
