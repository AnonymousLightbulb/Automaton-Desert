using Godot;

public partial class ItemPipe : Wire
{
    [Export] public int InputSlot;
    [Export] public int OutputSlot;
    [Export] public bool Open;
    [Export] public bool SendAll;
    [Export] public float MaxTransferRate = 25;
    [Export] public float TransferRate = 25;

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
        TransferRate = Mathf.Clamp(TransferRate, 0, MaxTransferRate);
        Node par = WireDisplay.GetParent().GetChild(0);
        if (Target is Storage && par is Storage)
        {
            if (SendAll == false)
            {
                InputSlot = Mathf.Clamp(InputSlot, 0, (par as Storage).Items.Count - 1);
                OutputSlot = Mathf.Clamp(OutputSlot, 0, (Target as Storage).Items.Count - 1);
                if (Open == true)
                {
                    if (Reverse == false)
                    {
                        TransferItem((float)delta, (par as Storage).Items[InputSlot], (Target as Storage).Items[OutputSlot], TransferRate);
                    }
                    else
                    {
                        TransferItem((float)delta, (Target as Storage).Items[OutputSlot], (par as Storage).Items[InputSlot], TransferRate);
                    }
                }
            }
            else
            {
                Godot.Collections.Array<Vector2I> Transfers = [];
                for (int i = 0; i < (par as Storage).Items.Count; i++)
                {
                    Inventory item = (par as Storage).Items[i];
                    for (int i1 = 0; i1 < (Target as Storage).Items.Count; i1++)
                    {
                        Inventory item2 = (Target as Storage).Items[i1];
                        if (item.ItemType == item2.ItemType)
                        {
                            if (Reverse == false)
                            {
                                Transfers.Add(new Vector2I(i, i1));
                            }
                            else
                            {
                                Transfers.Add(new Vector2I(i1, i));
                            }
                        }
                    }
                }
                foreach (Vector2I item in Transfers)
                {
                    TransferItem((float)delta, (par as Storage).Items[item.X], (Target as Storage).Items[item.Y], TransferRate / Transfers.Count);
                }
            }
        }
        base._PhysicsProcess(delta);
    }
    public void TransferItem(float delta, Inventory TakeIn, Inventory SendOut, float TransferVolume)
    {
        if (TakeIn.IsItemTypeDynamic == true && Mathf.IsEqualApprox(SendOut.ItemCount, 0))
        {
            SendOut.ItemType = TakeIn.ItemType;
        }
        if (TakeIn.ItemType == SendOut.ItemType || TakeIn.IsItemTypeDynamic == true && Mathf.IsEqualApprox(SendOut.ItemCount, 0))
        {
            float TransferAmmount = Mathf.Clamp(TakeIn.ItemCount, 0, Mathf.Clamp((Target as Storage).MaxFullness - (Target as Storage).Fullness(), 0, TransferVolume * delta));
            TakeIn.ItemCount -= TransferAmmount;
            SendOut.ItemCount += TransferAmmount;
        }
    }
}
