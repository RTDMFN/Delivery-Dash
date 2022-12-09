using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyManager : MonoBehaviour
{
    [SerializeField]
    private Company company;

    private PlayerConfiguration playerConfig;

    //Order Lists
    [SerializeField]
    private List<Order> ordersBeingPrepared = new List<Order>();
    [SerializeField]
    private List<Order> ordersAvailableForPickup = new List<Order>();

    private void Update(){
        PrepareOrders();
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    private void PrepareOrders(){
        foreach(Order o in ordersBeingPrepared.ToArray()){
            if(Time.timeSinceLevelLoad - o.timeOrderWasPlaced >= OrderManager.instance.orderPrepTime){
                o.state = OrderState.Prepared;
                o.timeOrderWasReady = Time.timeSinceLevelLoad;
                ordersAvailableForPickup.Add(o);
                ordersBeingPrepared.Remove(o);
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    public void PickupOrders(PlayerGameData collectingPlayer){
        foreach(Order o in ordersAvailableForPickup.ToArray()){
            if(collectingPlayer.GetPlayerConfig().PlayerCompany != o.deliveryCompany){
                o.state = OrderState.Stolen;
                o.timeOrderWasPickedUp = Time.timeSinceLevelLoad;
                collectingPlayer.AddOrderToInventory(o);
                ordersAvailableForPickup.Remove(o);
            }else{
                o.state = OrderState.PickedUp;
                o.timeOrderWasPickedUp = Time.timeSinceLevelLoad;
                collectingPlayer.AddOrderToInventory(o);
                ordersAvailableForPickup.Remove(o);
            }
        }
    }

    public void InitializeCompany(PlayerConfiguration pc){
        playerConfig = pc;
        company = pc.PlayerCompany;
    }

    public void AddOrder(Order order){
        ordersBeingPrepared.Add(order);
    }

    public Company GetCompany(){
        return company;
    }
    
}
