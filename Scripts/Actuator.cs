using Godot;

public partial class Actuator : Weld
{
    [Export] public float MaxSpeed;
    [Export] public float Speed;
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
        if (Joiner != null && Target != null)
        {
            Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed);

            float TransferAmmount = Mathf.Clamp(Mathf.Clamp(Electricity, 0, Mathf.Abs(Speed)), -Mathf.Abs(Speed) / 20, Mathf.Abs(Speed) / 20);
            Electricity -= Mathf.Abs(TransferAmmount) * (float)delta;
            Joiner.AngularLimitEnabled = false;
            if (Speed > 0)
            {
                Joiner.MotorTargetVelocity = TransferAmmount * 60;
            }
            else if (Speed < 0)
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
            base._PhysicsProcess(delta);
        }
    }

}
