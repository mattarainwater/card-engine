using Godot;
using System;
using System.Collections;

public abstract class GameAction : Godot.Object
{
    private bool _isValid = true;
    public readonly int Id;

    public bool IsValid 
    {
        get 
        {
            return _isValid;
        }
    }

    public bool? IsReady { get; set; } = null;

    public GameAction()
    {
        Id = Global.GenerateID(this.GetType());
    }

    public abstract string GetLog();

    public void SetInvalid()
    {
        _isValid = false;
    }
}
