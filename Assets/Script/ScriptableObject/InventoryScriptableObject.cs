using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class InventoryScriptableObject : ScriptableObject
{
    public int size = 20;
    public List<InventorySlot> inventSlot;
    public event EventHandler<OnItemAddEventArgs> OnItemAdd; 
    public class OnItemAddEventArgs : EventArgs{
        public int position;
    }
    public void CreateInventory(){
        inventSlot = new List<InventorySlot>();
        for(int i=0;i<size;i++){
            InventorySlot newInvent = new InventorySlot();
            inventSlot.Add(newInvent.EmptySlot());
        }
    }
    public void AddItemToSlot(ItemScriptableObject newItem, int newQuantity){
        bool hasItem = true;//ngecek ada itemnya ga

        bool firstTimeEmptySlot = true;//ngecek ketemu empty slot ga, kalo ga ktmu pasti tetep true
        int emptySpace = 0;//simpan empty slot trakhir biar gausa ngulang lg for nya

        int remainderQuantity = 0;//kalo ada item tp quantitynya berlebihan
        bool hasRemainder = false;//kshtau kalo ada remaindernya
        if(hasItem){
            for(int i=0;i<size;i++){
                if(firstTimeEmptySlot){
                    if(inventSlot[i].isEmpty){
                        firstTimeEmptySlot = false;
                        emptySpace = i;
                        if(!newItem.isStackable){
                            break;
                        }
                    }
                }
                if(!inventSlot[i].isEmpty && newItem.isStackable){
                    if(inventSlot[i].itemSO == newItem){
                        if(inventSlot[i].quantity < newItem.maxStack){
                            InventorySlot newInvent = new InventorySlot();
                            if(inventSlot[i].quantity + newQuantity > newItem.maxStack){
                                remainderQuantity = inventSlot[i].quantity + newQuantity - newItem.maxStack;
                                inventSlot[i] = newInvent.ChangeQuantity(newItem,newItem.maxStack);
                                hasRemainder = true;
                                OnItemAdd?.Invoke(this, new OnItemAddEventArgs{
                                    position = i
                                });
                                //ini updet d slot it saja
                                // OnItemAdd?.Invoke(this,EventArgs.Empty);
                                continue;
                            }
                            else{
                                inventSlot[i] = newInvent.ChangeQuantity(newItem,inventSlot[i].quantity + newQuantity);
                                OnItemAdd?.Invoke(this, new OnItemAddEventArgs{
                                    position = i
                                });
                                // OnItemAdd?.Invoke(this,EventArgs.Empty);
                                break;
                            }
                            
                        }
                        else{
                            continue;
                        }
                    }
                    
                }
                if(i == size - 1){
                    hasItem = false;
                }
            }
        }
        if(!hasItem){
            InventorySlot newInvent = new InventorySlot();
            if(!firstTimeEmptySlot){
                if(hasRemainder){
                    inventSlot[emptySpace] = newInvent.ChangeQuantity(newItem,remainderQuantity);
                    OnItemAdd?.Invoke(this, new OnItemAddEventArgs{
                        position = emptySpace
                    });
                }
                else{
                    inventSlot[emptySpace] = newInvent.ChangeQuantity(newItem,newQuantity);
                    OnItemAdd?.Invoke(this, new OnItemAddEventArgs{
                        position = emptySpace
                    });
                }
            }

            //kalo slot kosong ga mungkin brg yg didapet lebi besar dr max stack.
        }

    }
    public void TakeItemFromSlot(int position){
        InventorySlot newInvent = new InventorySlot();
        inventSlot[position].EmptySlot();
        OnItemAdd?.Invoke(this, new OnItemAddEventArgs{
            position = position
        });
        //mungkin di sini ntr kek dikasih data slotnya ke siapapun yg minta, atau ada lg yg lain kali
    }
    public InventorySlot TakeDataFromSlot(int position){
        return inventSlot[position].GetSlotData();
    }
}

[Serializable]
public class InventorySlot{
    public ItemScriptableObject itemSO;
    public int quantity;
    public bool isEmpty=> itemSO == null;
    public InventorySlot EmptySlot(){
        return new InventorySlot{
            itemSO = null,
            quantity = 0,
        };
    }
    public InventorySlot ChangeQuantity(ItemScriptableObject changeItem, int changeQuantity){
        return new InventorySlot{
            itemSO = changeItem,
            quantity = changeQuantity,
        };
    }
    public InventorySlot GetSlotData(){
        return new InventorySlot{
            itemSO = this.itemSO,
            quantity = this.quantity,
        };
    }

}
