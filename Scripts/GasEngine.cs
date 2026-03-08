using Godot;

public partial class GasEngine : Storage
{
    [Export] bool BurningGas;
    [Export] public float GasBurn = 1;
    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(double delta)
    {
        float TransferAmmount = Mathf.Clamp(ItemsF["gas"], 0, GasBurn * (float)delta);
        ItemsF["gas"] -= TransferAmmount;
        Electricity += TransferAmmount * 1000;
        TransferAmmount = Mathf.Clamp(ItemsF["ethanol"], 0, Mathf.Clamp(GasBurn * (float)delta - TransferAmmount, 0, GasBurn * (float)delta));
        ItemsF["ethanol"] -= TransferAmmount;
        Electricity += TransferAmmount * 100;
        base._PhysicsProcess(delta);
    }

}
