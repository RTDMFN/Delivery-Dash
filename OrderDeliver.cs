using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDeliver : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider){
        
    }

    private void OnTriggerStay(Collider collider){
        
    }

    private void OnTriggerExit(Collider collider){

    }

    private void DropOffOrder(Collider collider){
        if(collider.gameObject.tag == "Player"){
            int address = this.GetComponentInParent<Address>().address;
            collider.GetComponentInParent<PlayerGameData>().DeliverOrder(address);
        }
    }
}
