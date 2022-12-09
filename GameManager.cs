using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Gamne Information")]
    public int score;
    public float maximumTimeInMatch;
    public float timeRemainingInMatch;

    [Header("Score Modifiers")]
    [Range(0f,1f)]
    public float deliveryScoreWeight;
    public float maxDeliveryTime;
    public float maxPickupTime;
    public AnimationCurve deliveryTimeCurve;
    public AnimationCurve pickupTimeCurve;

    [Header("Game State")]
    public GameState state;

    private void Awake(){
        instance = this;
        timeRemainingInMatch = maximumTimeInMatch;
    }

    private void Update(){
        timeRemainingInMatch -= Time.deltaTime;
    }

    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------

    private void ChangeState(GameState newState){
        switch(newState){
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Paused:
                HandlePauseState();
                break;
            case GameState.Default:
                HandleDefaultState();
                break;
        }
    }

    private void HandlePlayingState(){
        state = GameState.Playing;
    }

    private void HandlePauseState(){
        if(state == GameState.Paused) state = GameState.Playing;
        else if(state == GameState.Playing) state = GameState.Paused;
    }

    private void HandleDefaultState(){
        state = GameState.Default;
    }

    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------

    public void AddScore(Order o){
        float timeTakenToDeliver = o.timeOrderWasDelivered - o.timeOrderWasPickedUp;
        float deliveryTimeValue = deliveryTimeCurve.Evaluate(Mathf.Clamp01(timeTakenToDeliver/maxDeliveryTime));
        float timeSatInWindow = o.timeOrderWasPickedUp - o.timeOrderWasReady;
        float pickupTimeValue = pickupTimeCurve.Evaluate(Mathf.Clamp01(timeTakenToDeliver/maxPickupTime));
        float scoreValue = ((deliveryTimeValue * deliveryScoreWeight) + (pickupTimeValue * (1-deliveryScoreWeight))) * 100;
        score += (int)scoreValue;
    }

    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------------

}

public enum GameState{
    Playing,
    Paused,
    Default    

}
