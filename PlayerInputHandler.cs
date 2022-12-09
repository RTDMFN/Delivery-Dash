using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public CarController carController;

    [SerializeField]
    private List<MeshRenderer> playerMesh;

    private PlayerConfiguration playerConfig;

    private PlayerControls controls;


    public void Awake(){
        carController = GetComponent<CarController>();
        controls = new PlayerControls();
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    public void InitializePlayer(PlayerConfiguration pc){
        playerConfig = pc;
        foreach(MeshRenderer mesh in playerMesh){
            mesh.material.color = pc.PlayerColor;
        }
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------

    private void Input_onActionTriggered(CallbackContext obj){
        if(obj.action.name == controls.Game.Movement.name) OnMove(obj);
        if(obj.action.name == controls.Game.Throttle.name) OnAccelerate(obj);
        if(obj.action.name == controls.Game.Brake.name) OnBrake(obj);
        if(obj.action.name == controls.Game.Handbrake.name) OnHandBrake(obj);
        if(obj.action.name == controls.Game.Drift.name) OnDrift(obj);
    }

    private void OnMove(CallbackContext context){
        float steering = context.ReadValue<Vector2>().x;
        carController.SetSteerInput(steering);
    }

    private void OnAccelerate(CallbackContext context){
        float throttle = context.ReadValue<float>();
        carController.SetThrottleInput(throttle);
    }

    private void OnBrake(CallbackContext context){
        bool brake = context.action.IsPressed();
        carController.SetBrakeInput(brake);

        float reverse = context.ReadValue<float>();
        carController.SetReverseInput(reverse);
    }

    private void OnHandBrake(CallbackContext context){
        bool handbrake = context.action.IsPressed();
        carController.SetHandBrakeInput(handbrake);
    }

    private void OnDrift(CallbackContext context){
        if(context.started) carController.SetDriftInput(true);
        if(context.canceled) carController.SetDriftInput(drift: false);
    }
}
