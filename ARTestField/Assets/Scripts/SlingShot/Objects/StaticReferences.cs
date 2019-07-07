using UnityEngine;
using UnityAD;

public static class StaticRefrences 
{
    public static EventSubject EventSubject = new EventSubject();
    public static float MinimumVerticalPoint { get; } = Screen.height * 0.3f;
    public static Vector2 SlingerOriginPoint { get; } = new Vector2(Screen.width * 0.5f, MinimumVerticalPoint);
}

