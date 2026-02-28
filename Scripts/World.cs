using Godot;
using System.Collections.Generic;

public partial class World : Node2D
{
    public static World TheWorld;
    [Export] public PackedScene CoreTemplate;
    [Export] public Godot.Collections.Dictionary<Vector2I, float> TileDamages = [];
    [Export] public TileMapLayer Map;
    [Export] public TileMapLayer DamagedTiles;
    public RandomNumberGenerator Bob;

    public static Dictionary<int, float> DamageMultipliers = new()
    {
        {0, 1},
        {1, 0.125f},
    };
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        TheWorld = this;
        Bob = new();
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        foreach (var item in DamagedTiles.GetUsedCells())
        {
            if (!Map.GetUsedCells().Contains(item))
            {
                DamagedTiles.EraseCell(item);
            }
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public void DamageTile(Vector2I Target, float Damage, bool IgnoreMultiplier = false)
    {
        if (IgnoreMultiplier == false && Map.GetCellSourceId(Target) != -1)
        {
            Damage *= DamageMultipliers[Map.GetCellSourceId(Target)];
        }
        if (TileDamages.ContainsKey(Target))
        {
            TileDamages[Target] += Damage;
        }
        else
        {
            TileDamages.Add(Target, Damage);
        }
        if (TileDamages[Target] < 2.5f)
        {
            DamagedTiles.SetCell(Target, 0, Vector2I.Zero, 0);
        }
        else if (TileDamages[Target] < 5)
        {
            DamagedTiles.SetCell(Target, 1, Vector2I.Zero, 0);
        }
        else if (TileDamages[Target] < 7.5f)
        {
            DamagedTiles.SetCell(Target, 2, Vector2I.Zero, 0);
        }
        else if (TileDamages[Target] < 10)
        {
            DamagedTiles.SetCell(Target, 3, Vector2I.Zero, 0);
        }
        else if (TileDamages[Target] >= 10)
        {
            Map.EraseCell(Target);
            TileDamages.Remove(Target);
            UpdateTile(Target + Vector2I.Up);
        }
    }
    public void UpdateTile(Vector2I Target)
    {
        if (Map.GetUsedCells().Contains(Target))
        {
            if (Map.GetCellSourceId(Target) == 0)
            {
                MoveTile(Target, Target + Vector2I.Down);
            }
        }
    }
    public void MoveTile(Vector2I ToMove, Vector2I MoveTo)
    {
        if (!Map.GetUsedCells().Contains(MoveTo))
        {
            TileDamages.Remove(MoveTo);
            DamagedTiles.EraseCell((MoveTo));
            if (TileDamages.ContainsKey(ToMove))
            {
                DamageTile(MoveTo, TileDamages[ToMove], true);
            }
            TileDamages.Remove(ToMove);
            DamagedTiles.EraseCell((ToMove));
            Map.SetCell(MoveTo, Map.GetCellSourceId(ToMove), Vector2I.Zero, 0);
            Map.EraseCell(ToMove);
            UpdateTile(MoveTo);
        }
        UpdateTile(ToMove + Vector2I.Up);
    }
}
