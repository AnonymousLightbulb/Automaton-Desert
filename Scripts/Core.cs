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
        CommandSet = [
        new([
              [new Part.BrainWord.Flag.ReadFlag(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "EngineSpeed"), Part.BrainWord.Flag.ReadFlag.Operation.FlagUnInintialized, new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true")), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "EngineSpeed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.Flag.ReadFlag(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "ArmLength"), Part.BrainWord.Flag.ReadFlag.Operation.FlagUnInintialized, new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true")), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "ArmLength"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), true), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Speed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), true), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Speed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.CheckInput("SpeedUp", Part.BrainWord.CheckInput.InputType.JustPressed), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "EngineSpeed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), Part.BrainWord.Flag.Math.Operation.Add)],
              [new Part.BrainWord.CheckInput("SpeedDown", Part.BrainWord.CheckInput.InputType.JustPressed), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "EngineSpeed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), Part.BrainWord.Flag.Math.Operation.Subtract)],

              [new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "Velocity"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.CheckInput("Right", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "Velocity"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("Left", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "Velocity"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "-1"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.Flag.Math(new(false, Part.BrainWord.Flag.FlagType.IntFlag, "Velocity"), new Part.BrainWord.Flag.Value.FlagReference(false, Part.BrainWord.Flag.FlagType.IntFlag, "EngineSpeed"), Part.BrainWord.Flag.Math.Operation.multiply)],

              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0"), true), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Speed"), new Part.BrainWord.Flag.Value.FlagReference(false, Part.BrainWord.Flag.FlagType.IntFlag, "Velocity"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.CheckInput("ArmLeft", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Speed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "-0.25"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("ArmRight", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "1"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Speed"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "0.25"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "2"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "false"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "3"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "false"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("Laser", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "2"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("MultiLaser", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "2"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("MultiLaser", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "3"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true"), Part.BrainWord.Flag.Math.Operation.Set)],

              [new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "4"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "false"), Part.BrainWord.Flag.Math.Operation.Set)],
              [new Part.BrainWord.CheckInput("Cannon", Part.BrainWord.CheckInput.InputType.Pressed), new Part.BrainWord.Action.Pass(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.IntFlag, "4"), false), new Part.BrainWord.Action.PartSettings(new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.StringFlag, "Firing"), new Part.BrainWord.Flag.Value.Constant(Part.BrainWord.Flag.FlagType.BoolFlag, "true"), Part.BrainWord.Flag.Math.Operation.Set)],
          ])
      ];
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
