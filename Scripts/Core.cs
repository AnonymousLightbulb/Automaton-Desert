using Godot;

public partial class Core : ThinkingObject
{
    [Export] public int TargetCam;

    public Godot.Collections.Array<string> Input = [];
    [Export] public Godot.Collections.Array<string> Inputs = [];
    [Export] public Godot.Collections.Array<string> OldInputs = [];
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
