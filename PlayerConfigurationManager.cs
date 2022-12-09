using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerConfiguration> playerConfigs;

    private List<Color> listOfAllColors = new List<Color>(){
        Color.blue,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow
    };

    private List<Color> listOfAvailableColors = new List<Color>();

    [SerializeField]
    private List<Company> listOfAllCompanies;

    private List <Company> listOfAvailableCompanies = new List<Company>();
    
    public static PlayerConfigurationManager instance;

    private void Awake(){
        if(instance != null){
            Debug.Log("Trying to create another instance!");
        }else{
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Update(){
        UpdateListOfAvailableColors();
        UpdateListOfAvailableCompanies();
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    public void OnPlayerJoin(PlayerInput pi){
        Debug.Log("Player " + pi.playerIndex + " Joined");
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex)){
            pi.transform.SetParent(this.transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public void OnPlayerLeave(PlayerInput pi){
        Debug.Log("Player " + pi.playerIndex + " Left");
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    public void SetReadyState(int playerIndex){
        playerConfigs[playerIndex].PlayerIsReady = !playerConfigs[playerIndex].PlayerIsReady;
        if(playerConfigs.Count() > 0 && playerConfigs.All(pi => pi.PlayerIsReady)){
            GetComponent<PlayerInputManager>().DisableJoining();
            Debug.Log("All players are ready");
            SceneManager.LoadScene("TestCoop");
        }
    }

    public Color GetPreviousColor(int playerIndex){
        Color previousColor = new Color();
        bool colorIsNotAvailable;
        int currentColorIndex = listOfAllColors.IndexOf(playerConfigs[playerIndex].PlayerColor);
        int nextColorIndex = currentColorIndex;

        do{
            nextColorIndex--;

            if(nextColorIndex <= -1) nextColorIndex = listOfAllColors.Count() - 1;

            previousColor = listOfAllColors[nextColorIndex];

            if(listOfAvailableColors.Contains(previousColor)){
                colorIsNotAvailable = false;
            }else{
                colorIsNotAvailable = true;
            }

        }while(colorIsNotAvailable);

        playerConfigs[playerIndex].PlayerColor = previousColor;
        return previousColor;
    }

    public Color GetNextColor(int playerIndex){
        Color nextColor = new Color();
        bool colorIsNotAvailable;
        int currentColorIndex = listOfAllColors.IndexOf(playerConfigs[playerIndex].PlayerColor);
        int nextColorIndex = currentColorIndex;

        do{
            nextColorIndex++;

            if(nextColorIndex >= listOfAllColors.Count()) nextColorIndex = 0;

            nextColor = listOfAllColors[nextColorIndex];

            if(listOfAvailableColors.Contains(nextColor)){
                colorIsNotAvailable = false;
            }else{
                colorIsNotAvailable = true;
            }

        }while(colorIsNotAvailable);

        playerConfigs[playerIndex].PlayerColor = nextColor;
        return nextColor;
    }

    public Company GetPreviousCompany(int playerIndex){
        Company previousCompany = new Company();
        bool companyIsNotAvailable;
        int currentCompanyIndex = listOfAllCompanies.IndexOf(playerConfigs[playerIndex].PlayerCompany);
        int nextCompanyIndex = currentCompanyIndex;

        do{
            nextCompanyIndex--;

            if(nextCompanyIndex <= -1) nextCompanyIndex = listOfAllCompanies.Count() - 1;

            previousCompany = listOfAllCompanies[nextCompanyIndex];

            if(listOfAvailableCompanies.Contains(previousCompany)){
                companyIsNotAvailable = false;
            }else{
                companyIsNotAvailable = true;
            }
        }while(companyIsNotAvailable);

        playerConfigs[playerIndex].PlayerCompany = previousCompany;

        return previousCompany;
    }

    public Company GetNextCompany(int playerIndex){
        Company nextCompany = new Company();
        bool companyIsNotAvailable;
        int currentCompanyIndex = listOfAllCompanies.IndexOf(playerConfigs[playerIndex].PlayerCompany);
        int nextCompanyIndex = currentCompanyIndex;

        do{
            nextCompanyIndex++;

            if(nextCompanyIndex >= listOfAllCompanies.Count()) nextCompanyIndex = 0;

            nextCompany = listOfAllCompanies[nextCompanyIndex];

            if(listOfAvailableCompanies.Contains(nextCompany)){
                companyIsNotAvailable = false;
            }else{
                companyIsNotAvailable = true;
            }
        }while(companyIsNotAvailable);

        playerConfigs[playerIndex].PlayerCompany = nextCompany;
        return nextCompany;
    }

    public List<PlayerConfiguration> GetPlayerConfigurations(){
        return playerConfigs;
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    private void UpdateListOfAvailableColors(){
        foreach(Color c in listOfAllColors.ToArray()){
            if(!listOfAvailableColors.Any(availableColor => availableColor == c)){
                listOfAvailableColors.Add(c);
            }
        }
        foreach(PlayerConfiguration p in playerConfigs){
            if(listOfAvailableColors.Any(availableColor => availableColor == p.PlayerColor)){
                listOfAvailableColors.Remove(p.PlayerColor);
            }
        }
    }

    private void UpdateListOfAvailableCompanies(){
        foreach(Company c in listOfAllCompanies.ToArray()){
            if(!listOfAvailableCompanies.Any(availableCompany => availableCompany == c)){
                listOfAvailableCompanies.Add(c);
            }
        }
        foreach(PlayerConfiguration p in playerConfigs){
            if(listOfAvailableCompanies.Any(availableCompany => availableCompany == p.PlayerCompany)){
                listOfAvailableCompanies.Remove(p.PlayerCompany);
            }
        }
    }
}

[System.Serializable]
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi){
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input;

    public int PlayerIndex;

    public int PlayerNumber = 1;

    public Color PlayerColor;

    public Company PlayerCompany;

    public bool PlayerIsReady = false;

}
