using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class YellowCarAgent : Agent{
    //init goal localPosition
    [SerializeField] private Transform targetTransformRed;
    [SerializeField] private Transform targetTransformBlack;

    public override void OnEpisodeBegin(){
        //transform.position = Vector3.one;
        transform.localPosition = new Vector3(8f,-0.4f,3f);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent localPosition
        sensor.AddObservation(transform.localPosition);
        //red car localPosition
        sensor.AddObservation(targetTransformRed.localPosition);
        //black car localPosition
        sensor.AddObservation(targetTransformBlack.localPosition);
    }

    //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 2f;
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
        //wenn die zeitabstände extrem klein sind (wie klein?), dann end episode
        float currentTime = Time.time;
        if(firstTime == currentTime){
            Debug.Log("YES");
            EndEpisode();
        }
        firstTime = currentTime;

        if(other.material.name == "Front (Instance)"){
            Debug.Log("No reward Front!");
             EndEpisode();
        }

        if(other.material.name == "Back (Instance)"){
            SetReward(+1f);
            Debug.Log("Reward Back!");
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-5f);
            EndEpisode();
        }
    }

    /*//if an objects gets touched
    private void OnTriggerEnter(Collider other){
        
        
        if (other.TryGetComponent<RedGoal>(out RedGoal goalRed)){
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

    



