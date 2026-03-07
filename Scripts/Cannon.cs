using Godot;

public partial class Cannon : Storage
{
    [Export] public bool Firing;
    [Export] public float Strength = 300;
    [Export] public float Cooldown = 0.5f;
    public float RemainingCooldown;
    [Export] public PackedScene LaunchedBlock;

    public override void _PhysicsProcess(double delta)
    {
        if (RemainingCooldown > 0)
        {
            RemainingCooldown -= (float)delta;
        }
        else
        {
            if (Firing == true)
            {
                int ItemLaunched = -1;
                if (Items[0].ItemType == "sand" && Items[0].ItemCountI >= 36)
                {
                    Items[0].ItemCountI -= 36;
                    ItemLaunched = 0;
                }
                else if (Items[0].ItemType == "metal" && Items[0].ItemCountI >= 36)
                {
                    Items[0].ItemCountI -= 36;
                    ItemLaunched = 1;
                }
                else if (Items[0].ItemType == "sandstone" && Items[0].ItemCountI >= 36)
                {
                    Items[0].ItemCountI -= 36;
                    ItemLaunched = 2;
                }
                if (ItemLaunched >= 0)
                {
                    FallingBlock FiredBlock = LaunchedBlock.Instantiate<FallingBlock>();
                    World.TheWorld.Map.GetParent().AddChild(FiredBlock);
                    FiredBlock.GlobalPosition = Body.GlobalPosition;
                    FiredBlock.ApplyCentralImpulse(new Vector2(-Strength, 0).Rotated(Body.GlobalRotation));
                    FiredBlock.ID = ItemLaunched;
                    RemainingCooldown = Cooldown;
                }
            }
            base._PhysicsProcess(delta);
        }
    }
}
