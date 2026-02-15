using Godot;

public partial class PowerLine : Wire
{
    [Export] public bool IntakeOpen;
    [Export] public bool OutputOpen;
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
        if (Target != null)
        {
            if (Reverse == false)
            {
                if (IntakeOpen == true)
                {
                    float IntakeAmount = Mathf.Clamp(Mathf.Clamp(Mathf.Clamp(Intake * (float)delta, 0, (WireDisplay.GetParent().GetChild(0) as Part).Output * (float)delta), 0, (WireDisplay.GetParent().GetChild(0) as Part).Electricity), 0, MaxElectricity - Electricity);
                    (WireDisplay.GetParent().GetChild(0) as Part).Electricity -= IntakeAmount;
                    Electricity += IntakeAmount;
                }
                if (OutputOpen == true)
                {
                    float OutputAmount = Mathf.Clamp(Mathf.Clamp(Mathf.Clamp(Electricity, 0, Output * (float)delta), 0, Target.Intake), 0, Target.MaxElectricity - Target.Electricity);
                    Electricity -= OutputAmount;
                    Target.Electricity += OutputAmount;
                }
            }
            else
            {
                if (IntakeOpen == true)
                {
                    float IntakeAmount = Mathf.Clamp(Mathf.Clamp(Mathf.Clamp(Intake * (float)delta, 0, (WireDisplay.GetParent().GetChild(0) as Part).Output * (float)delta), 0, (WireDisplay.GetParent().GetChild(0) as Part).Electricity), 0, MaxElectricity - Electricity);
                    Target.Electricity -= IntakeAmount;
                    Electricity += IntakeAmount;
                }
                if (OutputOpen == true)
                {
                    float OutputAmount = Mathf.Clamp(Mathf.Clamp(Mathf.Clamp(Electricity, 0, Output * (float)delta), 0, Target.Intake), 0, Target.MaxElectricity - Target.Electricity);
                    Electricity -= OutputAmount;
                    (WireDisplay.GetParent().GetChild(0) as Part).Electricity += OutputAmount;
                }
            }
        }
        base._PhysicsProcess(delta);
    }

}
