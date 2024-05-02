using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels Colliders")]
    public WheelCollider frontRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider backRightCollider;
    public WheelCollider backLeftCollider;

    [Header("Wheels Transform")]
    public Transform FrontLeftTransform;
    public Transform FrontRightTransform;
    public Transform BackLeftTransform;
    public Transform BackRightTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float acceleration = 100f;
    public float breakingforce = 200f;
    private float presentBreakForce = 0f;
    private float presentAcceleration= 0f;

    [Header("Vehicle steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Security")]
    public PlayerScript PlayerScript;
    private float radius = 5f;
    private bool isOpened = false;

    [Header("Disable Things")]
    public GameObject AimCam;
    public GameObject aimCanvas;
    public GameObject thirdPersonCam;
    public GameObject thirdPersonCanvas;
    public GameObject playerCharacter;

    [Header("Vehicle Hit")]
    public float giveDamageOf = 100f;
    public float HitRange = 2f;
    public GameObject goreEffect;
    public Camera cam;




    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerScript.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;

                Objectivescomplete.occurance.GeTObjectivesDone(true, true, true, false);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                PlayerScript.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;

            }
        }


        if (isOpened)
        {
            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            aimCanvas.SetActive(false);
            playerCharacter.SetActive(false);

            MoveVehicle();
            vehicleSteering();
            ApplyBreaks();
            HitZombie();
        }

        else if(isOpened == false)
        {
            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);
            aimCanvas.SetActive(true );
            playerCharacter.SetActive(true );
            AimCam.SetActive(true ) ;

        }
        

        
    }

    void MoveVehicle()
    {
        frontRightCollider.motorTorque = presentAcceleration;
        frontLeftCollider.motorTorque = presentAcceleration;
        backLeftCollider.motorTorque = presentAcceleration;
        backRightCollider.motorTorque = presentAcceleration;

        presentAcceleration = acceleration * -Input.GetAxis("Vertical");
    }

    void vehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightCollider.steerAngle =presentTurnAngle;
        frontLeftCollider.steerAngle = presentTurnAngle;

        SteerWheels(frontRightCollider, FrontRightTransform);
        SteerWheels(frontLeftCollider, FrontLeftTransform);
        SteerWheels(backLeftCollider,BackLeftTransform);
        SteerWheels(backRightCollider, BackRightTransform);

    }

    void SteerWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);
        WT.position = position;
        WT.rotation = rotation;
    }

    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreakForce = breakingforce;
        }
        else
        {
            presentBreakForce = 0f;
        }
        frontRightCollider.brakeTorque = presentBreakForce;
        frontLeftCollider.brakeTorque= presentBreakForce;
        backLeftCollider.brakeTorque = presentBreakForce;
        backRightCollider.brakeTorque = presentBreakForce;

    }
     void HitZombie()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, HitRange))
        {
            Debug.Log(hit.transform.name);
            
            Zombie1 zombie1 = hit.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hit.transform.GetComponent<Zombie2>();


            if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(goreEffectGo, 1f);

            }
        }
    }
}
