using Godot;

public partial class User : Node
{
    [Export] public Core TheCore;
    [Export] public Node Parts;
    [Export] public Godot.Collections.Array<string> RobotErrors = [];
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
        ClaimParts(Parts);
        if (TheCore == null)
        {
            CoreDescend(Parts);
        }
        if (TheCore == null && World.TheWorld != null)
        {
            Node asdf = World.TheWorld.CoreTemplate.Instantiate();
            Parts.AddChild(asdf);
            CoreDescend(Parts);
        }
        TheCore.OldInputs = TheCore.Inputs;
        TheCore.Inputs = [];
        base._PhysicsProcess(delta);
    }

    public void CoreDescend(Node Target)
    {
        if (Target.GetChildCount() > 0)
        {
            foreach (var Target2 in Target.GetChildren())
            {
                if (Target2 is Core)
                {
                    TheCore = Target2 as Core;
                }
                else
                {
                    CoreDescend(Target2);
                }
            }
        }
    }

    public void ClaimParts(Node PotentialPart)
    {
        if (PotentialPart is Part)
        {
            (PotentialPart as Part).MyUser = this;
        }
        foreach (Node item in PotentialPart.GetChildren())
        {
            ClaimParts(item);
        }
    }
}
