using System;
using System.Collections.Generic;

public abstract class Ability
{
    public AbilityTarget AllowedTargets { get; set; }
    public Actor Source { get; set; }
    public abstract AbilityAction ToAction();
}