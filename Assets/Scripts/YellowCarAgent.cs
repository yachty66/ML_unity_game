using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class YellowCarAgent : Agent{
    //init goal position
    [SerializeField] private Transform targetTransformRed;
    [SerializeField] private Transform targetTransformBlack;

    public override void OnEpisodeBegin(){
        //transform.position = Vector3.one;
        transform.position = new Vector3(10f,0.5f,-3f);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent position
        sensor.AddObservation(transform.position);
        //red car position
        sensor.AddObservation(targetTransformRed.position);
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
        
         // Declare and initialize a new List of GameObjects called currentCollisions.
     List <GameObject> currentCollisions = new List <GameObject> ();
     
     void OnCollisionEnter (Collision col) {
 
         // Add the GameObject collided with to the list.
         currentCollisions.Add (col.gameObject);
 
         // Print the entire list to the console.
         foreach (GameObject gObject in currentCollisions) {
             print (gObject.name);
         }
     }
 
     void OnCollisionExit (Collision col) {
 
         // Remove the GameObject collided with from the list.
         currentCollisions.Remove (col.gameObject);
 
         // Print the entire list to the console.
         foreach (GameObject gObject in currentCollisions) {
             print (gObject.name);
         }
     }

        Debug.Log(other.GetComponent<Collider>().sharedMaterial.name);
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
    }
}

    



