using Godot;

public partial class DataLine : Wire
{
    [Export] public int PassId;
    [Export] public int PullId;
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
        Reverse = false;
        // if (Reverse == false)
        // { 
        // }
        // else
        // {
        // 	foreach (string item in CommandQueue)
        // 	{
        // 		(GetParent().GetChild(0) as Part).CommandQueue.Add(item);
        // 	}
        // Reverse = false;
        // }
        base._PhysicsProcess(delta);
    }

}
