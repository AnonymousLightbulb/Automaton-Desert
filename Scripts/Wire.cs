using Godot;

public partial class Wire : Part
{
    [Export] public Part Target;
    [Export] public Line2D WireDisplay;
    [Export] public bool Reverse;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Target != null)
        {
            WireDisplay.ClearPoints();
            WireDisplay.AddPoint((WireDisplay.GetParent().GetChild(0) as Part).Display.GlobalPosition + (WireDisplay.GetParent().GetChild(0) as Part).WireOffset.Rotated((WireDisplay.GetParent().GetChild(0) as Part).Display.GlobalRotation));
            if (Target is Wire)
            {
                WireDisplay.AddPoint(((Target as Wire).WireDisplay.Points[0] + (Target as Wire).WireDisplay.Points[1]) / 2);
            }
            else
            {
                WireDisplay.AddPoint(Target.Display.GlobalPosition + Target.WireOffset.Rotated(Target.Display.GlobalRotation));
            }
            WireDisplay.AddPoint((WireDisplay.Points[0] + WireDisplay.Points[1]) / 2 + Vector2.Down, 1);
        }
        base._Process(delta);
    }
}
