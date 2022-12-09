using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessManager : MonoBehaviour
{
    

    // [Header("Order Settings")]
    // public AnimationCurve orderCooldownCurve;
    // public float orderPrepTime;

    // private float timeOfLastOrder;

    // public List<Order> ordersBeingPrepared;
    // public List<Order> ordersAvailableForPickup;
    // public List<Order> ordersBeingDelivered;
    // public List<Order> ordersDelivered;

    // [SerializeField]
    // private List<Address> listOfAllAddresses;

    // private void Start(){
    //     IndexAllAddresses();
    // }

    // private void Update(){
    //     PlaceOrder();
    //     PrepareOrder();
    // }

    // public void PlaceOrder(){
    //     if(Time.timeSinceLevelLoad - timeOfLastOrder >= CalculateOrderCooldown()){
    //         GenerateAnOrder();
    //         timeOfLastOrder = Time.timeSinceLevelLoad;
    //     }
    // }

    // private void PrepareOrder(){
    //     foreach(Order o in ordersBeingPrepared.ToArray()){
    //         if(Time.timeSinceLevelLoad - o.timeOrderWasPlaced >= orderPrepTime){
    //             o.state = OrderState.Prepared;
    //             o.timeOrderWasReady = Time.timeSinceLevelLoad;
    //             ordersAvailableForPickup.Add(o);
    //             ordersBeingPrepared.Remove(o);
    //         }
    //     }
    // }

    // public void PickupOrder(){
    //     foreach(Order o in ordersAvailableForPickup.ToArray()){
    //         o.state = OrderState.PickedUp;
    //         o.timeOrderWasPickedUp = Time.timeSinceLevelLoad;
    //         listOfAllAddresses[o.deliveryAddress].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    //         ordersBeingDelivered.Add(o);
    //         ordersAvailableForPickup.Remove(o);
    //     }
    // }

    // public void DeliverOrder(int houseAddress){
    //     foreach(Order o in ordersBeingDelivered.ToArray()){
    //         if(o.deliveryAddress == houseAddress){
    //             o.state = OrderState.Delivered;
    //             o.timeOrderWasDelivered = Time.timeSinceLevelLoad;
    //             listOfAllAddresses[o.deliveryAddress].gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    //             GameManager.instance.AddScore(o);
    //             ordersDelivered.Add(o);
    //             ordersBeingDelivered.Remove(o);
    //         }
    //     }
    // }

    // private float CalculateOrderCooldown(){
    //     float remainingTime = GameManager.instance.timeRemainingInMatch;
    //     float maxTimeInMatch = GameManager.instance.maximumTimeInMatch;
    //     float x = 1-(remainingTime/maxTimeInMatch);
    //     float orderCooldown = orderCooldownCurve.Evaluate(x);
    //     return orderCooldown;
    // }

    // private void GenerateAnOrder(){
    //     Order newOrder = new Order();
    //     newOrder.state = OrderState.Placed;
    //     newOrder.timeOrderWasPlaced = Time.timeSinceLevelLoad;
    //     newOrder.deliveryAddress = GenerateAnAddress();
    //     ordersBeingPrepared.Add(newOrder);
    // }

    // private int GenerateAnAddress(){
    //     int address = Random.Range(0,listOfAllAddresses.Count);
    //     return address;
    // }

    // private void IndexAllAddresses(){
    //     var Addresses = FindObjectsOfType<Address>();
    //     int addressIndex = 0;
    //     foreach(Address a in Addresses){
    //         listOfAllAddresses.Add(a);
    //         a.address = addressIndex;
    //         addressIndex++;
    //     }
    // }
}
