using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class BlackCarAgent : Agent{
    //Rigidbody rb;
    //init goal localPosition
    [SerializeField] private Transform targetTransformRed;
    [SerializeField] private Transform targetTransformYellow;

    public override void OnEpisodeBegin(){
        //transform.localPosition = Vector3.one;
        transform.localPosition = new Vector3(-6f,-0.4f,-8f);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
        //red car localPosition
        sensor.AddObservation(targetTransformRed.localPosition);
        //black car localPosition
        sensor.AddObservation(targetTransformYellow.localPosition);
    }

    //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 8f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private float firstTime = -1f;

    //if an objects gets touched
    private void OnTriggerEnter(Collider other){
      //  rb = GetComponent<Rigidbody>();
        //ich nehme mir die zeit wann jedes collider objekt eintrifft
        //wenn die zeitabstaende extrem klein sind (wie klein?), dann end episode
        float currentTime = Time.time;
        if(firstTime == currentTime){
            Debug.Log("YES");
           // EndEpisode();
        }
        firstTime = currentTime;
        if(other.material.name == "CarBox (Instance)"){
           // other.attachedRigidbody.AddForce(new Vector3(-10,0,0));
           other.attachedRigidbody.AddForce(-10,0,0,ForceMode.Impulse);
                  //  Rigidbody rb;

            Debug.Log("CARBOX");
           // other.transform.parent.position = other.transform.position + new Vector3(0f,0f,1f);
          //  other.attachedRigidbody.AddForce(-transform.forward);
         // rb.AddForce(1,1,1);
         //   other.rb.AddForce(-transform.forward);
           // other.GetComponent<Rigidbody>().AddForce(new Vector3(10,10,10));
         //  other.transform = rb.AddForce(transform.up * 40);
         //   other.attachedRigidbody.AddForce(new Vector3(10,10,10));
           // other.Rigidbody.velosity


        }

        if(other.material.name == "Front (Instance)"){
            Debug.Log("No reward Front!");
          //   EndEpisode();
        }

        if(other.material.name == "Back (Instance)"){
            SetReward(+1f);
            Debug.Log("Reward Back!");
           // EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-5f);
         //   EndEpisode();
        }
        /*if (other.TryGetComponent<RedGoal>(out RedGoal goalRed)){
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<YellowGoal>(out YellowGoal goalYellow)){
            
            SetReward(+1f);
            //EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-5f);
            EndEpisode();
        }*/
    }
}

    



