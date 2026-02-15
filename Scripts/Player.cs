using Godot;
using System.Collections.Generic;

public partial class Player : User
{
    // bool MoveRobot;
    [Export] Godot.Collections.Dictionary<string, string> KeyInput = []; // Creates an empty dictionary.
    bool QueuedPause;
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
        if (Input.IsActionJustPressed("Pause"))
        {
            GetTree().Paused = !GetTree().Paused;
        }
        foreach (KeyValuePair<string, string> item in KeyInput)
        {
            if (Input.IsKeyPressed(OS.FindKeycodeFromString(item.Key)))
            {
                TheCore.Inputs.Add(item.Value);
            }
        }
    }
}
