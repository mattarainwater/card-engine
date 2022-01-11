using System.Collections.Generic;

public interface ITargets
{
    Actor Source { get; set; }
    List<Actor> Targets { get; set; }
}