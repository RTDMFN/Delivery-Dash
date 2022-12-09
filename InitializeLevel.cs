using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform playerHolder;
    [SerializeField]
    private Transform buildingHolder;

    [SerializeField]
    private List<Transform> playerSpawnPoints;
    [SerializeField]
    private List<Transform> buildingSpawnPoints;

    private List<int> randomizedSpawnPoints = new List<int>();

    [SerializeField]
    private GameObject playerPrefab;

    private void Start(){
        var playerConfigs = PlayerConfigurationManager.instance.GetPlayerConfigurations().ToArray();
        System.Random r = new System.Random();
        foreach(int i in Enumerable.Range(0,playerSpawnPoints.Count()).OrderBy(x => r.Next())){
            randomizedSpawnPoints.Add(i);
        }
        for(int i = 0; i < playerConfigs.Length; i++){
            var Player = Instantiate(playerPrefab,
                                                playerSpawnPoints[randomizedSpawnPoints[i]].position,
                                                playerSpawnPoints[randomizedSpawnPoints[i]].rotation,
                                                playerHolder);
            Player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            Player.GetComponent<PlayerGameData>().InitializePlayer(playerConfigs[i]);
            
            var Building = Instantiate(playerConfigs[i].PlayerCompany.CompanyBuildingPrefab,
                                                buildingSpawnPoints[randomizedSpawnPoints[i]].position,
                                                buildingSpawnPoints[randomizedSpawnPoints[i]].rotation,
                                                buildingHolder);
            Building.GetComponent<CompanyManager>().InitializeCompany(playerConfigs[i]);
        }

        OrderManager.instance.IndexAllCompanies();
    }
}
