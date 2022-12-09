using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomSetupMenuButton : Button
{
    [SerializeField]
    private PlayerSetupMenuController menuController;

    public bool colorButton;
    public bool companyButton;

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);

        if(eventData.moveDir == MoveDirection.Right){
            menuController = GetComponentInParent<PlayerSetupMenuController>();
            menuController.OnMove(eventData,colorButton,companyButton);
        }
        else if(eventData.moveDir == MoveDirection.Left){
            menuController = GetComponentInParent<PlayerSetupMenuController>();
            menuController.OnMove(eventData,colorButton,companyButton);
        }
    }
}
