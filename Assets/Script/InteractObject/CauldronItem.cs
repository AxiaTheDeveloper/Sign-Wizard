using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronItem : InventorySlot
{
    public int position_InInventory;
    public CauldronItem EmptyItem()
    {
        return new CauldronItem
        {
            itemSO = null,
            quantity = 0,
            position_InInventory = 0
        };
    }
    public CauldronItem AddItem(ItemScriptableObject addItem, int addQuantity, int addPosition)
    {
        return new CauldronItem
        {
            itemSO = addItem,
            quantity = addQuantity,
            position_InInventory = addPosition
        };
    }
    public CauldronItem GetItemData()
    {
        return new CauldronItem
        {
            itemSO = this.itemSO,
            quantity = this.quantity,
            position_InInventory = this.position_InInventory
        };
    }
}
