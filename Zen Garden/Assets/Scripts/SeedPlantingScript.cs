using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlantingScript : MonoBehaviour
{
    public GameObject Seed;
    public GameObject Carrot;

    //when colliding
    private void OnCollisionEnter(Collision collision)
    {

        //delete the seed
        Destroy(Seed);

        //after the length of 1 day (180 seconds), call the method that grows the carrot
        Invoke("CarrotGrew", 180);
    }

    void CarrotGrew()
    {
        //make the carrot appear
        Carrot.SetActive(true);
    }
}
