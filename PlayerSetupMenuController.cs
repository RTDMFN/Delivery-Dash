using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    
    [SerializeField]
    private TextMeshProUGUI playerNumberText;

    [Header("Color")]
    [SerializeField]
    private Image playerColorImage;
    [SerializeField]
    private Image selectedColorImage;
    [SerializeField]
    private Color playerColor;

    [Header("Company")]
    [SerializeField]
    private Image playerCompanyImage;
    [SerializeField]
    private TextMeshProUGUI playerCompanyText;
    [SerializeField]
    private Company playerCompany;

    [Header("Ready")]
    [SerializeField]
    private TextMeshProUGUI readyText;
    [SerializeField]
    private bool readyState;

    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------

    public void SetPlayerIndex(int pi){
        playerIndex = pi;
        playerNumberText.SetText("Player " + (pi + 1).ToString());
    }

    public void SetPlayerReady(){
        if(!readyState){
            readyText.SetText("Not Ready");
            readyText.color = Color.red;
        }else if(readyState){
            readyText.SetText("Ready");
            readyText.color = Color.green;
        }
    }

    public void FindNextAvailableColor(){
        CycleColorRight();
    }

    public void FindNextAvailableCompany(){
        CycleCompanyRight();
    }

    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------

    private void CycleColorRight(){
        playerColor = PlayerConfigurationManager.instance.GetNextColor(playerIndex);
        playerColorImage.color = playerColor;
        selectedColorImage.color = playerColor;
    }

    private void CycleColorLeft(){
        playerColor = PlayerConfigurationManager.instance.GetPreviousColor(playerIndex);
        playerColorImage.color = playerColor;
        selectedColorImage.color = playerColor;
    }

    private void CycleCompanyRight(){
        playerCompany = PlayerConfigurationManager.instance.GetNextCompany(playerIndex);
        playerCompanyText.SetText(playerCompany.CompanyName);        
        playerCompanyImage.sprite = playerCompany.CompanyLogo;
    }

    private void CycleCompanyLeft(){
        playerCompany = PlayerConfigurationManager.instance.GetPreviousCompany(playerIndex);
        playerCompanyText.SetText(playerCompany.CompanyName);       
        playerCompanyImage.sprite = playerCompany.CompanyLogo;
    }

    private void ReadyUp(){
        if(!readyState){
            PlayerConfigurationManager.instance.SetReadyState(playerIndex);
            readyState = true;
            readyText.SetText("Ready");
            readyText.color = Color.green;
        }else if(readyState){
            PlayerConfigurationManager.instance.SetReadyState(playerIndex);
            readyState = false;
            readyText.SetText("Not Ready");
            readyText.color = Color.red;
        }
    }

    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    
    public void OnMove(AxisEventData eventData, bool colorSelection, bool companySelection){
        if(colorSelection){
            if(eventData.moveDir == MoveDirection.Right) CycleColorRight();
            else if(eventData.moveDir == MoveDirection.Left) CycleColorLeft();
        }else if(companySelection){
            if(eventData.moveDir == MoveDirection.Right) CycleCompanyRight();
            else if(eventData.moveDir == MoveDirection.Left) CycleCompanyLeft();
        }
    }

    public void ReadyButton(){
        ReadyUp();
    }

    public void ColorButton(){
        CycleColorRight();
    }

    public void CompanyButton(){
        CycleCompanyRight();
    }
}
