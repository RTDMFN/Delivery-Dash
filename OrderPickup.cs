using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider){
        CollectOrders(collider);
    }

    private void OnTriggerStay(Collider collider){
        CollectOrders(collider);
    }

    private void OnTriggerExit(Collider collider){
        CollectOrders(collider);
    }

    private void CollectOrders(Collider collider){
        if(collider.gameObject.tag == "Player"){
            PlayerGameData collectingPlayer = collider.GetComponentInParent<PlayerGameData>();
            GetComponentInParent<CompanyManager>().PickupOrders(collectingPlayer);
        }
    }
}
