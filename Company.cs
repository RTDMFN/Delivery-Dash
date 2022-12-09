using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Company", order = 1)]
public class Company : ScriptableObject
{
    public Company(){
        
    }

    public string CompanyName;
    public Sprite CompanyLogo;
    public GameObject CompanyBuildingPrefab;
}
