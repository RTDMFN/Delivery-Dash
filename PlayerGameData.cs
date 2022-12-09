using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameData : MonoBehaviour
{

    private PlayerConfiguration playerConfig;

    [SerializeField]
    private List<Order> ordersBeingDelivered = new List<Order>();
    [SerializeField]
    private List<Order> ordersDelivered = new List<Order>();

    private int score;

    public void InitializePlayer(PlayerConfiguration pc){
        playerConfig = pc;
    }

    public void AddOrderToInventory(Order order){
        ordersBeingDelivered.Add(order);
    }

    public void RemoveOrderFromInventory(Order order){
        ordersBeingDelivered.Remove(order);
    }

    public void UpdateScore(int deltaScore){
        score += deltaScore;
    }
    public void DeliverOrder(int buildingAddress){

    }

    public PlayerConfiguration GetPlayerConfig(){
        return playerConfig;
    }
}
