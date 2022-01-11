using Godot;

public interface IAspect
{
    IContainer Container { get; set; }
}

public class Aspect : Node, IAspect
{
    public IContainer Container { get; set; }
}
