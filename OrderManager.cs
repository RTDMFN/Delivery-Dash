using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [Header("Order Settings")]
    public AnimationCurve orderCooldownCurve;
    public float orderPrepTime;
    private float timeOfLastOrder;

    private List<CompanyManager> listOfCompanyManagers = new List<CompanyManager>();

    private void Awake(){
        instance = this;
    }

    private void Update(){
        PlaceOrders();
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    public void IndexAllCompanies(){
        var companyManagers = FindObjectsOfType<CompanyManager>();
        foreach(CompanyManager cm in companyManagers){
            listOfCompanyManagers.Add(cm);
        }
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    private void PlaceOrders(){
        if(Time.timeSinceLevelLoad - timeOfLastOrder >= CalculateOrderCooldown()){
            foreach(CompanyManager cm in listOfCompanyManagers){
                GenerateAnOrder(cm);
            }
            timeOfLastOrder = Time.timeSinceLevelLoad;
        }
    }

    private void GenerateAnOrder(CompanyManager companyManager){
        int address = AddressManager.instance.GenerateAnAddress();
        Debug.Log(companyManager.GetCompany());
        Company company = companyManager.GetCompany();
        Order newOrder = new Order(company,address);
        companyManager.AddOrder(newOrder);
    }

    private float CalculateOrderCooldown(){
        float remainingTime = GameManager.instance.timeRemainingInMatch;
        float maxTimeInMatch = GameManager.instance.maximumTimeInMatch;
        float x = 1-(remainingTime/maxTimeInMatch);
        float orderCooldown = orderCooldownCurve.Evaluate(x);
        return orderCooldown;
    }

    
}
