using Godot;
using System.Collections.Generic;

public partial class Inventory : Node
{
    public static Dictionary<string, bool> ItemTypeDictionary = new(){
      {"gas", true},
      {"ethanol", true},
      {"metal", false},
      {"sand", false},
    };
    public bool IsItemFloat()
    {
        if (ItemTypeDictionary.ContainsKey(ItemType))
        {
            return ItemTypeDictionary[ItemType];
        }
        else
        {
            return true;
        }
    }
    [Export] public bool IsItemTypeDynamic;
    [Export] public string ItemType = "";
    [Export] public int ItemCountI;
    [Export] public float ItemCountF;
    public override void _PhysicsProcess(double delta)
    {
        // if (ItemType != "")
        // {
        // 	ItemType = ItemType.ToLower();
        // }
        if (IsItemFloat() == true)
        {
            ItemCountI = 0;
        }
        else
        {
            ItemCountF = 0;
        }
        base._PhysicsProcess(delta);
    }

}
