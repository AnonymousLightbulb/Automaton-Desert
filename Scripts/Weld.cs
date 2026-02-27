using Godot;

public partial class Weld : Part
{
    [Export] public PinJoint2D Joiner;
    [Export] public RigidBody2D Target;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Joiner != null && Target != null)
        {
            Joiner.NodeA = Joiner.GetParent().GetPath();
            Joiner.NodeB = Target.GetPath();
        }
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Joiner != null && Target != null)
        {
            Joiner.NodeA = Joiner.GetParent().GetPath();
            Joiner.NodeB = Target.GetPath();
        }
        base._Process(delta);

    }
}
