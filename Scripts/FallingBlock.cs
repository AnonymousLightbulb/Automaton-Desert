using Godot;

public partial class FallingBlock : RigidBody2D
{
    [Export] public int ID = 0;
    [Export] public bool Damaged = false;
    [Export] public float Damage = 0;
    [Export] public Sprite2D Image;
    [Export] public Sprite2D DamageImage;
    [Export] public Area2D Detector;
    [Export] public Area2D Safety;
    [Export] public short FailCount = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Image.Texture = World.TheWorld.BlockTextures[ID];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Damaged == true)
        {
            if (Damage < World.TileDurabilities[ID] / 4)
            {
                DamageImage.Texture = ResourceLoader.Load<Texture2D>("uid://dfkrukgf04ld0");
            }
            else if (Damage < World.TileDurabilities[ID] / 2)
            {
                DamageImage.Texture = ResourceLoader.Load<Texture2D>("uid://jio4bflg1sim");
            }
            else if (Damage < World.TileDurabilities[ID] * 3 / 4)
            {
                DamageImage.Texture = ResourceLoader.Load<Texture2D>("uid://guibdv7ukh32");
            }
            else if (Damage < World.TileDurabilities[ID])
            {
                DamageImage.Texture = ResourceLoader.Load<Texture2D>("uid://d3lxq0w881pu");
            }
            else if (Damage >= World.TileDurabilities[ID])
            {
                QueueFree();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Safety.GetOverlappingBodies().Count < 1 && Detector.GetOverlappingBodies().Contains(World.TheWorld.Map))
        {
            Vector2I Target = World.TheWorld.Map.LocalToMap(World.TheWorld.Map.ToLocal(GlobalPosition));
            if (!World.TheWorld.Map.GetUsedCells().Contains(Target))
            {
                World.TheWorld.Map.SetCell(Target, ID, Vector2I.Zero, 0);
                if (Damaged)
                {
                    if (World.TheWorld.TileDamages.ContainsKey(Target))
                    {
                        World.TheWorld.TileDamages[Target] = Damage;
                    }
                    else
                    {
                        World.TheWorld.TileDamages.Add(Target, Damage);
                    }
                    if (World.TheWorld.TileDamages[Target] < World.TileDurabilities[World.TheWorld.Map.GetCellSourceId(Target)] / 4)
                    {
                        World.TheWorld.DamagedTiles.SetCell(Target, 0, Vector2I.Zero, 0);
                    }
                    else if (World.TheWorld.TileDamages[Target] < World.TileDurabilities[World.TheWorld.Map.GetCellSourceId(Target)] / 2)
                    {
                        World.TheWorld.DamagedTiles.SetCell(Target, 1, Vector2I.Zero, 0);
                    }
                    else if (World.TheWorld.TileDamages[Target] < World.TileDurabilities[World.TheWorld.Map.GetCellSourceId(Target)] * 3 / 4)
                    {
                        World.TheWorld.DamagedTiles.SetCell(Target, 2, Vector2I.Zero, 0);
                    }
                    else if (World.TheWorld.TileDamages[Target] < World.TileDurabilities[World.TheWorld.Map.GetCellSourceId(Target)])
                    {
                        World.TheWorld.DamagedTiles.SetCell(Target, 3, Vector2I.Zero, 0);
                    }
                }
                World.TheWorld.UpdateTile(Target);
                QueueFree();
            }
            else
            {
                FailCount += 1;
                if (FailCount >= 1000)
                {
                    QueueFree();
                }
            }
        }
        base._PhysicsProcess(delta);
    }

    public void _on_area_2d_body_entered(Node2D EnteringBody)
    {
    }
}
