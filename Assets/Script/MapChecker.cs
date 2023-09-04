using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapChecker : MonoBehaviour
{
    public event EventHandler<OnChangePlaceCheckerTownEventArgs> OnChangePlaceCheckerTown;
    public class OnChangePlaceCheckerTownEventArgs : EventArgs{
        public MapTown place;
    }
    public event EventHandler<OnChangePlaceCheckerBridgeEventArgs> OnChangePlaceCheckerBridge;
    
    public class OnChangePlaceCheckerBridgeEventArgs : EventArgs{
        public MapMagicalBridge place;
    }
    public enum MapTown
    {
        kiriAtas,kananAtas,kiriBawah,kananBawah,Fox,Vii
    }
    
    public enum MapMagicalBridge
    {
        kiri, kanan
    }
    [SerializeField]private MapTown mapTownPlace;
    [SerializeField]private MapMagicalBridge mapMagicalBridgePlace;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(WitchGameManager.Instance.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
            {
                OnChangePlaceCheckerBridge?.Invoke(this, new OnChangePlaceCheckerBridgeEventArgs{
                    place = mapMagicalBridgePlace
                });
            }
            if(WitchGameManager.Instance.GetOutDoorType() == WitchGameManager.OutDoorType.town)
            {
                OnChangePlaceCheckerTown?.Invoke(this, new OnChangePlaceCheckerTownEventArgs{
                    place = mapTownPlace
                });
            }
        }
        
    }
}
