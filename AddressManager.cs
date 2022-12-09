using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressManager : MonoBehaviour
{
    public static AddressManager instance;

    private List<Address> listOfAllAddresses = new List<Address>();

    private void Awake(){
        instance = this;
    }

    private void Start()
    {
        IndexAllAddresses();
    }

    private void IndexAllAddresses(){
        var Addresses = FindObjectsOfType<Address>();
        int addressIndex = 0;
        foreach(Address a in Addresses){
            listOfAllAddresses.Add(a);
            a.address = addressIndex;
            addressIndex++;
        }
    }

    public int GenerateAnAddress(){
        int address = Random.Range(0,listOfAllAddresses.Count);
        return address;
    }
}
