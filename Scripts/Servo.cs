using Godot;

public partial class Servo : Weld
{
    [Export] public float MaxSpeed;
    [Export] public float Speed;
    [Export] public float Angle;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    public float PartsRelativeAngle()
    {
        var asdf = Target.GlobalRotationDegrees - Joiner.GetParent<RigidBody2D>().GlobalRotationDegrees;
        while (asdf > 180)
        {
            asdf -= 360;
        }
        while (asdf < -180)
        {
            asdf += 360;
        }
        return asdf;
    }

    // Called every frame. 'delta' is the elapsed time sin:ce the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Joiner != null && Target != null)
        {
            Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed);
            if (PartsRelativeAngle() > Angle - 5 && PartsRelativeAngle() < Angle + 5)
            {
                Joiner.MotorTargetVelocity = 0;
            }
            else
            {
                float TransferAmmount = Mathf.Clamp(Mathf.Clamp(Electricity, 0, Mathf.Abs(Speed)), -Mathf.Abs(Speed) / 20, Mathf.Abs(Speed) / 20);
                Electricity -= Mathf.Abs(TransferAmmount) * (float)delta;
                Joiner.AngularLimitEnabled = false;
                if (Angle - PartsRelativeAngle() > 0)
                {
                    Joiner.MotorTargetVelocity = TransferAmmount * 60;
                }
                else if (Angle - PartsRelativeAngle() < 0)
                {
                    Joiner.MotorTargetVelocity = TransferAmmount * -60;
                }
                else
                {
                    Joiner.MotorTargetVelocity = 0;
                    // Joiner.AngularLimitEnabled = true;
                    // Joiner.AngularLimitLower = (GetNode(Joiner.NodeA) as Node2D).Rotation - (GetNode(Joiner.NodeB) as Node2D).Rotation;
                    // Joiner.AngularLimitUpper = (GetNode(Joiner.NodeA) as Node2D).Rotation - (GetNode(Joiner.NodeB) as Node2D).Rotation;
                }
            }
            base._PhysicsProcess(delta);
        }
    }
}
