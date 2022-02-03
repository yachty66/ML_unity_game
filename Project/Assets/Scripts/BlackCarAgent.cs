using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BlackCarAgent : Agent{
    //init goal position
    [SerializeField] private Transform targetTransformRed;
    [SerializeField] private Transform targetTransformYellow;

    public override void OnEpisodeBegin(){
        //transform.position = Vector3.one;
        transform.position = new Vector3(-6f,0.5f,-8f);
    }

    //how the agent receives the environment
    public override void CollectObservations(VectorSensor sensor){
        //agent position
        sensor.AddObservation(transform.position);
        //red car position
        sensor.AddObservation(targetTransformRed.position);
        //black car position
        sensor.AddObservation(targetTransformYellow.position);
    }

    //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
            //    Debug.Log(actions.ContinuousActions[0]);

        //Debug.Log(actions.ContinuousActions[1]);

        float moveSpeed = 4f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
//  Debug.Log(other.material.name);
/*
     void OnCollisionEnter(Collision collision)
 {
         if (collision.gameObject.tag == "YellowCarAgent")
         {
             Debug.Log("front!!!");
         }
       }
     
*/
    //if an objects gets touched
    private void OnTriggerEnter(Collider other){
        GameObject car1 = this.gameObject;
        GameObject car2 = other.gameObject;

        GameObject test = this.transform.GetChild(2).gameObject;
        GameObject test1 = this.transform.GetChild(3).gameObject;

        //GameObject car1 = this.Collider;

     
        Debug.Log("Triggered Obj1: :" + car1.name);
        Debug.Log("Triggered obj2: :" + car2.name);
        Debug.Log("Child: :" + test.name);
        Debug.Log("Child222: :" + test1.name);

     //   Debug.Log("Triggered Obj1: :" + this.material.name);
        Debug.Log("Material obj2: :" + other.material.name);
        /*
        if(car1.TryGetComponent<(car2.name == "frontbox")>(out RedCarAgent goalRed)){
            Debug.Log("111111111111111111111111111");
     

        }

*/
        if (car2.name == "frontbox" && car1.name == "backbox"){
            Debug.Log("YES!!!!");
        }



        if (other.TryGetComponent<RedCarAgent>(out RedCarAgent goalRed)){
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<YellowCarAgent>(out YellowCarAgent goalYellow)){
            //Debug.Log(other.material.name);
         

            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-5f);
            EndEpisode();
        }
    }

}

     



