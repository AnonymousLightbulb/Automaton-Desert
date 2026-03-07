using Godot;

public partial class LaserGun : StructuralPart
{
    [Export] public RayCast2D LaserBeam;
    [Export] public Line2D LineDisplay;
    [Export] public bool Firing;
    [Export] public float Strength = 40;
    public override void _Process(double delta)
    {
        if (LaserBeam.IsColliding())
        {
            LineDisplay.Points = [GetParent<RigidBody2D>().GlobalPosition, LaserBeam.GetCollisionPoint()];
        }
        else
        {
            LineDisplay.Points = [GetParent<RigidBody2D>().GlobalPosition, GetParent<RigidBody2D>().GlobalPosition + new Vector2(2000, 0).Rotated(GetParent<RigidBody2D>().Rotation)];
        }
        base._Process(delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Firing == true)
        {
            LineDisplay.Visible = true;
        }
        else
        {
            LineDisplay.Visible = false;
        }
        if (LaserBeam.GetCollider() is RigidBody2D && (LaserBeam.GetCollider() as RigidBody2D).GetChild(0) is StructuralPart && ((LaserBeam.GetCollider() as RigidBody2D).GetChild(0) as StructuralPart).MyUser == MyUser)
        {
            LaserBeam.AddException(LaserBeam.GetCollider() as RigidBody2D);
            LaserBeam.ForceRaycastUpdate();
        }
        if (Firing == true && Electricity >= 2)
        {
            float TransferAmmount = Mathf.Clamp(Electricity, 0, Strength * (float)delta);
            Electricity -= TransferAmmount;
            if (LaserBeam.GetCollider() == World.TheWorld.Map)
            {
                World.TheWorld.DamageTile(World.TheWorld.Map.LocalToMap(World.TheWorld.Map.ToLocal(LaserBeam.GetCollisionPoint() + new Vector2(0.1f, 0).Rotated(GetParent<RigidBody2D>().Rotation))), TransferAmmount);
            }
            else if (LaserBeam.GetCollider() is FallingBlock)
            {
                (LaserBeam.GetCollider() as FallingBlock).Damaged = true;
                (LaserBeam.GetCollider() as FallingBlock).Damage += TransferAmmount;
            }
        }
        base._PhysicsProcess(delta);
    }
}
