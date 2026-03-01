using Godot;
using System.Collections.Generic;

public partial class World : Node2D
{
    public static World TheWorld;
    [Export] public Node Entities;
    [Export] public PackedScene CoreTemplate;
    [Export] public PackedScene FallingBlockTemplate;
    [Export] public Godot.Collections.Dictionary<Vector2I, float> TileDamages = [];
    [Export] public TileMapLayer Map;
    [Export] public TileMapLayer DamagedTiles;
    public RandomNumberGenerator Bob;

    public static List<float> TileDurabilities =
    [
        10,
        100,
        20,
    ];
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

    public void DamageTile(Vector2I Target, float Damage)
    {
        if (TileDamages.ContainsKey(Target))
        {
            TileDamages[Target] += Damage;
        }
        else
        {
            TileDamages.Add(Target, Damage);
        }
        if (Map.GetCellSourceId(Target) != -1)
        {
            if (TileDamages[Target] < TileDurabilities[Map.GetCellSourceId(Target)] / 4)
            {
                DamagedTiles.SetCell(Target, 0, Vector2I.Zero, 0);
            }
            else if (TileDamages[Target] < TileDurabilities[Map.GetCellSourceId(Target)] / 2)
            {
                DamagedTiles.SetCell(Target, 1, Vector2I.Zero, 0);
            }
            else if (TileDamages[Target] < TileDurabilities[Map.GetCellSourceId(Target)] * 3 / 4)
            {
                DamagedTiles.SetCell(Target, 2, Vector2I.Zero, 0);
            }
            else if (TileDamages[Target] < TileDurabilities[Map.GetCellSourceId(Target)])
            {
                DamagedTiles.SetCell(Target, 3, Vector2I.Zero, 0);
            }
            else if (TileDamages[Target] >= TileDurabilities[Map.GetCellSourceId(Target)])
            {
                Map.EraseCell(Target);
                TileDamages.Remove(Target);
                UpdateTile(Target + Vector2I.Up);
            }
        }
    }
    public void UpdateTile(Vector2I Target)
    {
        if (Map.GetUsedCells().Contains(Target))
        {
            if (Map.GetCellSourceId(Target) == 0)
            {
                if (!Map.GetUsedCells().Contains(Target + Vector2I.Down))
                {
                    var asdf = FallingBlockTemplate.Instantiate<FallingBlock>();
                    asdf.Position = Map.ToGlobal(Map.MapToLocal(Target));
                    if (TileDamages.ContainsKey(Target))
                    {
                        asdf.Damaged = true;
                        asdf.Damage = TileDamages[Target];
                    }
                    TileDamages.Remove(Target);
                    DamagedTiles.EraseCell(Target);
                    Map.EraseCell(Target);

                    Entities.AddChild(asdf);
                    UpdateTile(Target + Vector2I.Up);
                }
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
                DamageTile(MoveTo, TileDamages[ToMove]);
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
