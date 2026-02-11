using Godot;

public partial class Storage : StructuralPart
{
    [Export] public float MaxFullness = 1000;
    [Export] public Godot.Collections.Array<Inventory> Items = [];

    public float Fullness()
    {
        float Filledness = 0;
        foreach (Inventory item in Items)
        {
            Filledness += item.ItemCount;
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
