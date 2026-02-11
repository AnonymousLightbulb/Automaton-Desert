using Godot;

public partial class Inventory : Node
{
    [Export] public bool IsItemTypeDynamic;
    [Export] public string ItemType;
    [Export] public float ItemCount;
    public override void _PhysicsProcess(double delta)
    {
        // if (ItemType != "")
        // {
        // 	ItemType = ItemType.ToLower();
        // }
        base._PhysicsProcess(delta);
    }

}
