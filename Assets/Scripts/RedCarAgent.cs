using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RedCarAgent : Agent{
    //init goal localPosition
    [SerializeField] private Transform targetTransformYellow;
    [SerializeField] private Transform targetTransformBlack;

    //localPosition at the beginning
    public override void OnEpisodeBegin(){
      //  transform.localPosition = new Vector3(-10f,-0.4f,5f);
          transform.localPosition = new Vector3(Random.Range(-10.0f,10.0f),0,Random.Range(-10.0f,10.0f));

    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
        //yellow car localPosition
        sensor.AddObservation(targetTransformYellow.localPosition);
        //black car localPosition
        sensor.AddObservation(targetTransformBlack.localPosition);
    }

    //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float turn = actions.ContinuousActions[2];

//int turn = actions.DiscreteActions[0];
        // Rechtsdrehung
        if (turn > 0.9f){
            transform.Rotate(0.0f,90.0f,0.0f);

        }
        // Linksdrehung
           if (turn < -0.9){
            transform.Rotate(0.0f,-90.0f,0.0f);

        }
        float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    /*//just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }*/

    private float firstTime = -1f;

    //if an objects gets touched
    private void OnTriggerEnter(Collider other){
        //ich nehme mir die zeit wann jedes collider objekt eintrifft
        //wenn die zeitabstaende extrem klein sind (wie klein?), dann end episode
        float currentTime = Time.time;
        if(firstTime == currentTime){
       //     Debug.Log("YES");
         //   EndEpisode();
        }
        firstTime = currentTime;
        if(other.material.name == "CarBox (Instance)"){
        
           other.attachedRigidbody.AddForce(-5,0,0,ForceMode.Impulse);
       //    Debug.Log("CARBOX_RED");
        }
        if(other.material.name == "Front (Instance)"){
       //     Debug.Log("No reward Front!");
          //  EndEpisode();
        }

        if(other.material.name == "Back (Instance)"){
            SetReward(+20f);
       //     Debug.Log("Reward Back!");
         //   EndEpisode();
        }
        /*
        // Position des getroffenen Autos veraendern
        if(this.GetChild().material.name == "Back (Instance)"){
            EndEpisode();
        }
        */
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-5f);
          //  other.attachedRigidbody.AddForce(-5,0,0,ForceMode.Impulse);
      //     Debug.Log("CARBOX_BLACK");

            EndEpisode();
        }
    }

    /*//if an objects gets touched
    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent<YellowGoal>(out YellowGoal goalYellow)){
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<BlackGoal>(out BlackGoal goalBlack)){
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-1f);
            EndEpisode();
        }
    }*/

}

    



