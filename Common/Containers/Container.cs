using Godot;
using System;
using System.Collections.Generic;

public interface IContainer
{
    T AddAspect<T>(string key = null) where T : IAspect, new();
    T AddAspect<T>(T aspect, string key = null) where T : IAspect;
    T GetAspect<T>(string key = null) where T : IAspect;
    ICollection<IAspect> Aspects();
}

public class Container : IContainer
{
    Dictionary<string, IAspect> aspects = new Dictionary<string, IAspect>();

    public T AddAspect<T>(string key = null) where T : IAspect, new()
    {
        return AddAspect<T>(new T(), key);
    }

    public T AddAspect<T>(T aspect, string key = null) where T : IAspect
    {
        key = key ?? GetName(typeof(T));
        aspects.Add(key, aspect);
        aspect.Container = this;
        return aspect;
    }

    public T AddOrUpdate<T>(T aspect, string key = null) where T : IAspect
    {
        key = key ?? GetName(typeof(T));
        if (aspects.ContainsKey(key))
        {
            aspects.Remove(key);
        }
        aspects.Add(key, aspect);
        aspect.Container = this;
        return aspect;
    }

    public T GetAspect<T>(string key = null) where T : IAspect
    {
        key = key ?? GetName(typeof(T));
        T aspect = aspects.ContainsKey(key) ? (T)aspects[key] : default(T);
        return aspect;
    }

    public ICollection<IAspect> Aspects()
    {
        return aspects.Values;
    }

    private string GetName(Type t)
    {
        var name = t.Name;
        if(t.GenericTypeArguments != null && t.GenericTypeArguments.Length > 0)
        {
            foreach(var gen in t.GenericTypeArguments)
            {
                name += gen.Name;
            }
        }
        return name;
    }
}