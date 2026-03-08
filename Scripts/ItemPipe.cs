using Godot;

public partial class ItemPipe : Wire
{
    [Export] public int InputSlot;
    [Export] public int OutputSlot;
    [Export] public bool Open;
    [Export] public float MaxTransferRate = 50;
    [Export] public float TransferRate = 50;
    [Export] public Godot.Collections.Dictionary<string, float> TransferItemITime = [];

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
        //TransferItemITime += (float)delta;
        TransferRate = Mathf.Clamp(TransferRate, 0, MaxTransferRate);
        Node par = WireDisplay.GetParent().GetChild(0);
        if (Target is Storage && par is Storage)
        {
            Godot.Collections.Array<string> TransfersF = [];
            Godot.Collections.Array<string> TransfersI = [];

            if (Reverse == false)
            {
                foreach (var item in (par as Storage).ItemsF)
                {
                    if ((Target as Storage).DynamicItems == true || (Target as Storage).ItemsF.ContainsKey(item.Key))
                    {
                        TransfersF.Add(item.Key);
                    }
                }
                foreach (var item in (par as Storage).ItemsI)
                {
                    if ((Target as Storage).DynamicItems == true || (Target as Storage).ItemsI.ContainsKey(item.Key))
                    {
                        TransfersI.Add(item.Key);
                    }
                }

                foreach (var item in TransfersF)
                {
                    TransferItemF((float)delta, item, WireDisplay.GetParent().GetChild(0) as Storage, Target as Storage);
                }
                foreach (var item in TransfersI)
                {
                    if (TransferItemITime.ContainsKey(item))
                    {
                        TransferItemITime[item] += TransferRate * (float)delta;
                    }
                    else
                    {
                        TransferItemITime.Add(item, TransferRate * (float)delta);
                    }
                }
                foreach (var item in TransferItemITime)
                {
                    if (item.Value > 0)
                    {
                        TransferItemI(Mathf.FloorToInt(item.Value), item.Key, WireDisplay.GetParent().GetChild(0) as Storage, Target as Storage);
                        TransferItemITime[item.Key] -= Mathf.FloorToInt(item.Value);
                    }
                }

            }
            else
            {
                foreach (var item in (Target as Storage).ItemsF)
                {
                    if ((par as Storage).DynamicItems == true || (par as Storage).ItemsF.ContainsKey(item.Key))
                    {
                        TransfersF.Add(item.Key);
                    }
                }
                foreach (var item in (Target as Storage).ItemsI)
                {
                    if ((par as Storage).DynamicItems == true || (par as Storage).ItemsI.ContainsKey(item.Key))
                    {
                        TransfersI.Add(item.Key);
                    }
                }

                foreach (var item in TransfersF)
                {
                    TransferItemF((float)delta, item, Target as Storage, WireDisplay.GetParent().GetChild(0) as Storage);
                }
                foreach (var item in TransfersI)
                {
                    if (TransferItemITime.ContainsKey(item))
                    {
                        TransferItemITime[item] += TransferRate * (float)delta;
                    }
                    else
                    {
                        TransferItemITime.Add(item, TransferRate * (float)delta);
                    }
                }
                foreach (var item in TransferItemITime)
                {
                    if (item.Value > 0)
                    {
                        TransferItemI(Mathf.FloorToInt(item.Value), item.Key, Target as Storage, WireDisplay.GetParent().GetChild(0) as Storage);
                        TransferItemITime[item.Key] -= Mathf.FloorToInt(item.Value);
                    }
                }
            }
        }
        base._PhysicsProcess(delta);
    }
    // TakeIn is the inventory that goes into the pipe and SendOut is the inventory that the pipe empties into
    public void TransferItemF(float delta, string ToSend, Storage From, Storage To)
    {

        float TransferAmmount = Mathf.Clamp(TransferRate * delta, 0, Mathf.Clamp(From.ItemsF[ToSend], 0, Mathf.Inf));
        TransferAmmount = Mathf.Clamp(TransferAmmount, 0, Mathf.Clamp(To.MaxFullness - To.Fullness(), 0, Mathf.Inf));
        From.ItemsF[ToSend] -= TransferAmmount;
        if (To.ItemsF.ContainsKey(ToSend))
        {
            To.ItemsF[ToSend] += TransferAmmount;
        }
        else
        {
            To.ItemsF.Add(ToSend, TransferAmmount);
        }
    }
    public void TransferItemI(int Amount, string ToSend, Storage From, Storage To)
    {
        int TransferAmmount = Mathf.Clamp(Amount, 0, Mathf.RoundToInt(Mathf.Clamp(From.ItemsI[ToSend], 0, Mathf.Inf)));
        TransferAmmount = Mathf.Clamp(TransferAmmount, 0, Mathf.FloorToInt(Mathf.Clamp(To.MaxFullness - To.Fullness(), 0, Mathf.Inf)));
        From.ItemsI[ToSend] -= TransferAmmount;
        if (To.ItemsI.ContainsKey(ToSend))
        {
            To.ItemsI[ToSend] += TransferAmmount;
        }
        else
        {
            To.ItemsI.Add(ToSend, TransferAmmount);
        }
    }
}
