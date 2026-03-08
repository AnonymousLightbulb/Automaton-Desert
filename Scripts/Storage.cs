using Godot;

public partial class Storage : StructuralPart
{
    [Export] public float MaxFullness = 1000;
    [Export] public bool DynamicItems = false;
    [Export] public Godot.Collections.Dictionary<string, int> ItemsI = [];
    [Export] public Godot.Collections.Dictionary<string, float> ItemsF = [];


    public float Fullness()
    {
        float Filledness = 0;
        foreach (var item in ItemsI)
        {
            Filledness += item.Value;
        }
        foreach (var item in ItemsF)
        {
            Filledness += item.Value;
        }
        return Filledness;
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    // Called when the node enters the scene tree for the first time.

    // Called every frame. 'delta' is the elapsed time since the previous frame.

}
