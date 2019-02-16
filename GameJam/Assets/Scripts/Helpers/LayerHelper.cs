using UnityEngine;


public class LayerHelper:NPS.LayerHelper
{
    public static LayerMask Walkable
    {
        get => EnsureLayerId("Walkable");
    }
    public static LayerMask NonWalkable
    {
        get => EnsureLayerId("Non Walkable");
    }
    public static LayerMask Obstacle
    {
        get => EnsureLayerId("Obstacle");
    }
    public static LayerMask Interactable
    {
        get => EnsureLayerId("Interactable");
    }
    public static LayerMask Player
    {
        get => EnsureLayerId("Player");
    }
}