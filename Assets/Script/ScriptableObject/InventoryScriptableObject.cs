using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//inventoryyy
[CreateAssetMenu]
public class InventoryScriptableObject : ScriptableObject
{
    public int size = 20;
    public enum InventoryType{
        ZeroGone, ZeroNotGone // kek misal di chest gitu ya gambar quantity tetep ada, ga diapus;
    }
    public InventoryType type;
    public bool isFull = false;
    public int isFullyEmpty = 0; // ini di cek pas mau clear inventory player biar kalo 0 ga jalan clear inventnya
    public List<InventorySlot> inventSlot;
    public event EventHandler<OnItemUpdateEventArgs> OnItemUpdate; 
    public class OnItemUpdateEventArgs : EventArgs{
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
                // Debug.Log(i);
                if(firstTimeEmptySlot){
                    if(inventSlot[i].isEmpty){
                        firstTimeEmptySlot = false;
                        emptySpace = i;
                        // if(!newItem.isStackable){
                        //     hasItem = false;
                        //     break;
                        // }
                    }
                }
                if(!inventSlot[i].isEmpty){
                    // Debug.Log("masuk sini");
                    
                    if(inventSlot[i].itemSO == newItem){
                        // Debug.Log("Sama");
                        if(inventSlot[i].quantity < newItem.maxStack){
                            // Debug.Log("Size lebi kecil");
                            InventorySlot newInvent = new InventorySlot();
                            if(inventSlot[i].quantity + newQuantity > newItem.maxStack){
                                // Debug.Log("Size berlebih");
                                remainderQuantity = inventSlot[i].quantity + newQuantity - newItem.maxStack;
                                inventSlot[i] = newInvent.ChangeQuantity(newItem,newItem.maxStack);
                                hasRemainder = true;
                                OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                                    position = i
                                });
                                //ini updet d slot it saja
                                // OnItemAdd?.Invoke(this,EventArgs.Empty);
                                continue;
                            }
                            else{
                                // Debug.Log("Size tidak berlebi");
                                inventSlot[i] = newInvent.ChangeQuantity(newItem,inventSlot[i].quantity + newQuantity);
                                OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                                    position = i
                                });
                                // OnItemAdd?.Invoke(this,EventArgs.Empty);
                                break;
                            }
                            
                        }
                        else{
                            // Debug.Log("Uda penu");
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
            // Debug.Log("Ga ada trnyata");
            InventorySlot newInvent = new InventorySlot();
            if(!firstTimeEmptySlot){
                if(hasRemainder){
                    inventSlot[emptySpace] = newInvent.ChangeQuantity(newItem,remainderQuantity);
                    OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                        position = emptySpace
                    });
                }
                else{
                    // Debug.Log(newItem.itemName);
                    inventSlot[emptySpace] = newInvent.ChangeQuantity(newItem,newQuantity);
                    OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                        position = emptySpace
                    });
                }
                isFullyEmpty++;
            }
            else if(firstTimeEmptySlot){
                isFull = true;
            }

            //kalo slot kosong ga mungkin brg yg didapet lebi besar dr max stack.
        }

    }
    public void TakeItemFromSlot(int position, int quantityTake){
        if(!inventSlot[position].isEmpty){
            int remainderQuantity = inventSlot[position].quantity - quantityTake;
            //quantitytake dipastikan ga lebih dr quantity di inventslot
            if(remainderQuantity == 0){
                if(type == InventoryType.ZeroGone){
                    inventSlot[position] = inventSlot[position].EmptySlot();
                    // Debug.Log("empty");
                    // Debug.Log(inventSlot[position].quantity + " " + inventSlot[position].itemSO.itemName);
                    if(isFull){
                        isFull = false;
                    }
                    isFullyEmpty--;
                }
                else if(type == InventoryType.ZeroNotGone){
                    inventSlot[position].quantity = remainderQuantity;
                }
                
            }
            else if(remainderQuantity > 0){
                inventSlot[position].quantity = remainderQuantity;
            }
            
            OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                position = position
            });
            
        }
    }
    

    public void RemoveAllItem(){
        for(int i=0;i<size;i++){
            inventSlot[i] = new InventorySlot().EmptySlot();
            OnItemUpdate?.Invoke(this, new OnItemUpdateEventArgs{
                position = i
            });
        }
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
