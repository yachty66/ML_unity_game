using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RedCarAgent : Agent{
    //init goal position
    [SerializeField] private Transform targetTransformYellow;
    [SerializeField] private Transform targetTransformBlack;

    //position at the beginning
    public override void OnEpisodeBegin(){
        transform.position = new Vector3(-10f,0.5f,5f);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent position
        sensor.AddObservation(transform.position);
        //yellow car position
        sensor.AddObservation(targetTransformYellow.position);
        //black car position
        sensor.AddObservation(targetTransformBlack.position);
    }

    //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 2f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    /*//just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }*/

    //if an objects gets touched
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
    }

}

    



