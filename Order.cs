using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public Order(Company company, int address){
        state = OrderState.Placed;

        deliveryCompany = company;
        deliveryAddress = address;
        
        timeOrderWasPlaced = Time.timeSinceLevelLoad;
    }

    //Order Details
    public OrderState state;
    public Company deliveryCompany;
    public int deliveryAddress;

    //Score Details
    public float timeOrderWasPlaced;
    public float timeOrderWasReady;
    public float timeOrderWasPickedUp;
    public float timeOrderWasDelivered;
}

public enum OrderState{
    Placed,
    Prepared,
    PickedUp,
    Stolen,
    Delivered
}
