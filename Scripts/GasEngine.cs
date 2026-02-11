using Godot;

public partial class GasEngine : Storage
{
    [Export] bool BurningGas;
    [Export] public float GasBurn = 1;
    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(double delta)
    {
        float TransferAmmount = Mathf.Clamp(Items[0].ItemCount, 0, GasBurn * (float)delta);
        Items[0].ItemCount -= TransferAmmount;
        Electricity += TransferAmmount * 1000;
        TransferAmmount = Mathf.Clamp(Items[1].ItemCount, 0, Mathf.Clamp(GasBurn * (float)delta - TransferAmmount, 0, GasBurn * (float)delta));
        Items[1].ItemCount -= TransferAmmount;
        Electricity += TransferAmmount * 100;
        base._PhysicsProcess(delta);
    }

}
