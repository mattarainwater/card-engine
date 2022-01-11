using Handler = System.Action<System.Object, System.Object>;
using Godot;

public static class NotificationExtensions
{
	public static void SyncPostNotification(this object obj, string notificationName)
	{
		NotificationCenter.instance.CallDeferred("PostNotification", notificationName, obj);
	}

	public static void SyncPostNotification(this object obj, string notificationName, object e)
	{
		NotificationCenter.instance.Call("PostNotification", notificationName, obj, e);
	}

	public static void PostNotification (this object obj, string notificationName)
    {
        NotificationCenter.instance.CallDeferred("PostNotification", notificationName, obj);
	}
		
	public static void PostNotification (this object obj, string notificationName, object e)
    {
		NotificationCenter.instance.CallDeferred("PostNotification", notificationName, obj, e);
	}
		
	public static void AddObserver (this object obj, Handler handler, string notificationName)
	{
		NotificationCenter.instance.AddObserver(handler, notificationName);
	}
		
	public static void AddObserver (this object obj, Handler handler, string notificationName, object sender)
	{
		NotificationCenter.instance.AddObserver(handler, notificationName, sender);
	}
		
	public static void RemoveObserver (this object obj, Handler handler, string notificationName)
	{
		NotificationCenter.instance.RemoveObserver(handler, notificationName);
	}
		
	public static void RemoveObserver (this object obj, Handler handler, string notificationName, System.Object sender)
	{
		NotificationCenter.instance.RemoveObserver(handler, notificationName, sender);
	}
}