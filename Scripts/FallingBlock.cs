using Godot;

public partial class FallingBlock : RigidBody2D
{
    [Export] public int id;
    [Export] public Area2D Body;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_area_2d_body_entered(Node2D EnteringBody)
    {
        if (EnteringBody == World.TheWorld.Map)
        {
            World.TheWorld.Map.SetCell(World.TheWorld.Map.LocalToMap(World.TheWorld.Map.ToLocal(GlobalPosition)), id, Vector2I.Zero, 0);
            QueueFree();
        }
    }
}
