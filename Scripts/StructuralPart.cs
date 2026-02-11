using Godot;

public partial class StructuralPart : Part
{
    [Export] public RigidBody2D Body;
    [Export] public CollisionShape2D BodyShape;
    [Export] public bool Round;
    [Export] Vector2I Size;
    [Export] public float Density = .1f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Round == false)
        {
            BodyShape.Shape = new RectangleShape2D();
        }
        else
        {
            BodyShape.Shape = new CircleShape2D();
        }
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // if (Round == false)
        // {
        base._Process(delta);
        // }
        // else
        // {
        // 	Body.Mass = Density * Size.X * Mathf.Pi;
        // 	(BodyShape.Shape as RectangleShape2D).Size = Size;
        // 	Display.RegionRect = new(0, 0, new(Size.X, Size.X));
        // }
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Round == false)
        {
            Body.Mass = Density * Size.X * Size.Y;
            (BodyShape.Shape as RectangleShape2D).Size = Size;
            Display.RegionEnabled = true;
            Display.RegionRect = new(0, 0, Size);
        }
        else
        {
            Size.Y = Size.X;
            Body.Mass = Density * (Size.X / 2) * Mathf.Pi;
            (BodyShape.Shape as CircleShape2D).Radius = Size.X / 2;
            Display.RegionEnabled = false;
            Display.Scale = Size / Display.Texture.GetSize();
        }
        base._PhysicsProcess(delta);
    }
}
