using Godot;

public partial class World : Node2D
{
    public static World TheWorld;
    [Export] public PackedScene CoreTemplate;

    public RandomNumberGenerator Bob;

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
    }
}
