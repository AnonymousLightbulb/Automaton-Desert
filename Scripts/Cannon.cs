using Godot;

public partial class Cannon : Storage
{
    [Export] public bool Firing;
    [Export] public float Strength = 300;
    [Export] public float Cooldown = 0.5f;
    public float RemainingCooldown;
    [Export] public PackedScene LaunchedBlock;
    [Export] public string TargetItem;

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
                if (ItemsI.ContainsKey(TargetItem))
                {
                    int ItemLaunched = -1;
                    if (TargetItem == "sand" && ItemsI["sand"] >= 36)
                    {
                        ItemsI["sand"] -= 36;
                        ItemLaunched = 0;
                    }
                    else if (TargetItem == "metal" && ItemsI["metal"] >= 36)
                    {
                        ItemsI["metal"] -= 36;
                        ItemLaunched = 1;
                    }
                    else if (TargetItem == "sandstone" && ItemsI["sandstone"] >= 36)
                    {
                        ItemsI["sandstone"] -= 36;
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
            }
            base._PhysicsProcess(delta);
        }
    }
}
