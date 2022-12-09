using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CarController : MonoBehaviour
{
    [Header("Spring")]
    public float springRestLength;
    public float springStrength;
    public float springDamper;

    private float springOffset;
    private float springForce;

    [Header("Tire")]
    public List<Transform> tires;
    public float tireGripFactor;
    public float tireMass;

    [Header("Car")]
    public float motorTorque;
    public float reverseTorque;
    public float topSpeed;
    public float frictionCoefficient;
    public AnimationCurve powerCurve;

    private Rigidbody carRB;

    [Header("Steering")]
    public float rearTrack;
    public float tireBase;
    public float turnRadius;
    public float steerTime;

    private float wheelAngle;
    private float steerAngle;

    [Header("Brake")]
    public float brakeCoefficient;

    private bool engageBrake;
    private bool engageHandBrake;

    [Header("Drift")]
    public float driftEntranceTime;
    public float driftRecoveryTime;

    private bool engageDrift;

    [Header("Inputs")]
    private float throttleInput;
    private float reverseInput;
    private float steerInput;
    private float ackermannAngleLeft;
    private float ackermannAngleRight;

    private void Awake(){
        carRB = GetComponent<Rigidbody>();
    }

    private void Update(){
        TurnTires();
        Drift();
    }

    private void FixedUpdate(){
        CalculateSuspensionForces();
        CalculateSteeringForces();
        CalculateFrictionForces();
        AccelerateCar();
        ReverseCar();
        Brake();
    }

    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------

    public void SetSteerInput(float steering){
        steerInput = steering;
    }

    public void SetThrottleInput(float throttle){
        throttleInput = throttle;
    }

    public void SetBrakeInput(bool brake){
        engageBrake = brake;
    }

    public void SetReverseInput(float reverse){
        reverseInput = reverse;
    }

    public void SetHandBrakeInput(bool handbrake){
        engageHandBrake = handbrake;
    }

    public void SetDriftInput(bool drift){
        engageDrift = drift;
    }

    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------

    private void CalculateSuspensionForces(){
        foreach(Transform t in tires){
            if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                Vector3 springDirection = t.up;
                Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                springOffset = springRestLength - hit.distance;
                float velocity = Vector3.Dot(springDirection,tireWorldVelocity);
                float force = (springOffset * springStrength) - (velocity * springDamper);
                carRB.AddForceAtPosition(force * springDirection,t.position);
                Debug.DrawRay(t.position,springDirection * force,Color.green);
            }
        }
    }

    private void CalculateSteeringForces(){
        foreach(Transform t in tires){
            if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                Vector3 steeringDirection = t.right;
                Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                float steeringVelocity = Vector3.Dot(steeringDirection,tireWorldVelocity);
                float desiredVelocityChange = -steeringVelocity * tireGripFactor;
                float desiredAcceleration = desiredVelocityChange/Time.fixedDeltaTime;
                carRB.AddForceAtPosition(steeringDirection*tireMass*desiredAcceleration,t.position);
                Debug.DrawRay(t.position,steeringDirection*tireMass*desiredAcceleration,Color.red);
            }
        }
    }

    private void CalculateFrictionForces(){
        foreach(Transform t in tires){
            if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                Vector3 accelerationDirection = t.forward;
                Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                float throttleVelocity = Vector3.Dot(accelerationDirection,tireWorldVelocity);
                float desiredVelocityChange = -throttleVelocity * frictionCoefficient;
                float desiredAcceleration = desiredVelocityChange/Time.fixedDeltaTime;
                carRB.AddForceAtPosition(accelerationDirection * tireMass * desiredAcceleration,t.position);
                Debug.DrawRay(t.position,accelerationDirection * tireMass * desiredAcceleration,Color.blue);
            }
        }
    }

    private void CalculateAckermannAngles(){
        if(steerInput > 0){
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(tireBase/(turnRadius + (rearTrack/2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(tireBase/(turnRadius - (rearTrack/2))) * steerInput;   
        }else if(steerInput < 0){
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(tireBase/(turnRadius - (rearTrack/2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(tireBase/(turnRadius + (rearTrack/2))) * steerInput;   
        }else{
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }
    }

    private void TurnTires(){
        CalculateAckermannAngles();
        foreach(Transform t in tires){
            if(t.GetComponent<Tire>().frontLeft){
                steerAngle = ackermannAngleLeft;
                wheelAngle = Mathf.Lerp(wheelAngle,steerAngle,steerTime * Time.deltaTime);
                t.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
            }else if(t.GetComponent<Tire>().frontRight){
                steerAngle = ackermannAngleRight;
                wheelAngle = Mathf.Lerp(wheelAngle,steerAngle,steerTime * Time.deltaTime);
                t.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
            }
        }
    }

    private void AccelerateCar(){
        if(!engageBrake && !engageHandBrake){
            foreach(Transform t in tires){
                if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                    Vector3 accelerationDirection = t.forward;
                    Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                    float tireVelocityMagnitude = Vector3.Dot(accelerationDirection,tireWorldVelocity);
                    float desiredTorque = Mathf.Clamp01(tireVelocityMagnitude/topSpeed);
                    float availableTorque = powerCurve.Evaluate(desiredTorque) * motorTorque;
                    carRB.AddForceAtPosition(accelerationDirection * availableTorque * throttleInput,t.position);
                }
            }
        }
    }

    private void Brake(){
        if(engageBrake || engageHandBrake){
            foreach(Transform t in tires){
                if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                    Vector3 accelerationDirection = t.forward;
                    Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                    float brakeVelocity = Vector3.Dot(accelerationDirection,tireWorldVelocity);
                    float desiredVelocityChange = -brakeVelocity * brakeCoefficient;
                    float desiredAcceleration = desiredVelocityChange/Time.fixedDeltaTime;
                    carRB.AddForceAtPosition(accelerationDirection * tireMass * desiredAcceleration,t.position);
                    Debug.DrawRay(t.position,accelerationDirection * tireMass * desiredAcceleration,Color.magenta);
                }
            }
        }
    }

    private void ReverseCar(){
        if(engageBrake && throttleInput == 0){
            foreach(Transform t in tires){
                if(Physics.Raycast(t.position,-t.up,out RaycastHit hit)){
                    Vector3 accelerationDirection = t.forward;
                    Vector3 tireWorldVelocity = carRB.GetPointVelocity(t.position);
                    float tireVelocityMagnitude = Vector3.Dot(accelerationDirection,tireWorldVelocity);
                    if(tireVelocityMagnitude <= 5f) carRB.AddForceAtPosition(-accelerationDirection * reverseTorque * reverseInput,t.position);
                }
            }
        }
    }

    private void Drift(){
        if(engageDrift){
            tireGripFactor = Mathf.Lerp(tireGripFactor,0.1f,driftEntranceTime * Time.deltaTime);
        }
        if(!engageDrift){
            tireGripFactor = Mathf.Lerp(tireGripFactor,0.8f,driftRecoveryTime * Time.deltaTime);
        }
    }

}
