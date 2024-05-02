using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class SquareStacking : MonoBehaviour
{
    public GameObject Squares;
    private GameObject GhostSquares;

    private Animator TumbleAnimator;

    // Square game objects
    private GameObject FirstSquare;
    private GameObject SecondSquare;
    private GameObject ThirdSquare;
    private GameObject LastSquare;

    // ghost Square game objects
    private GameObject FirstSquareGhost;
    private GameObject SecondSquareGhost;
    private GameObject ThirdSquareGhost;
    private GameObject LastSquareGhost;

    // Square positions
    private Transform FirstSquarePosition;
    private Transform SecondSquarePosition;
    private Transform ThirdSquarePosition;
    private Transform LastSquarePosition;

    // ghost Square positions
    private Transform FirstSquareGhostPosition;
    private Transform SecondSquareGhostPosition;
    private Transform ThirdSquareGhostPosition;
    private Transform LastSquareGhostPosition;

    // Square rigidbodies
    private Rigidbody FirstSquareRigidBody;
    private Rigidbody SecondSquareRigidBody;
    private Rigidbody ThirdSquareRigidBody;
    private Rigidbody LastSquareRigidBody;

    // Square grabbable scripts
    private OVRGrabbable FirstSquareGrabbable;
    private OVRGrabbable SecondSquareGrabbable;
    private OVRGrabbable ThirdSquareGrabbable;
    private OVRGrabbable LastSquareGrabbable;

    // ghost square mesh renderer
    private MeshRenderer FirstSquareGhostMeshRenderer;
    private MeshRenderer SecondSquareGhostMeshRenderer;
    private MeshRenderer ThirdSquareGhostMeshRenderer;
    private MeshRenderer LastSquareGhostMeshRenderer;

    private int SquareCount = 0;

    private void Start()
    {

        // assign values

        GhostSquares = gameObject;

        TumbleAnimator = gameObject.GetComponent<Animator>();

        // Square game objects
        FirstSquare = Squares.transform.GetChild(0).gameObject;
        SecondSquare = Squares.transform.GetChild(1).gameObject;
        ThirdSquare = Squares.transform.GetChild(2).gameObject;
        LastSquare = Squares.transform.transform.GetChild(3).gameObject;

        // ghost Square game objects
        FirstSquareGhost = GhostSquares.transform.GetChild(0).gameObject;
        SecondSquareGhost = GhostSquares.transform.GetChild(1).gameObject;
        ThirdSquareGhost = GhostSquares.transform.GetChild(2).gameObject;
        LastSquareGhost = GhostSquares.transform.GetChild(3).gameObject;

        // Square positions
        FirstSquarePosition = FirstSquare.transform;
        SecondSquarePosition = SecondSquare.transform;
        ThirdSquarePosition = ThirdSquare.transform;
        LastSquarePosition = LastSquare.transform;

        // ghost Square positions
        FirstSquareGhostPosition = FirstSquareGhost.transform;
        SecondSquareGhostPosition = SecondSquareGhost.transform;
        ThirdSquareGhostPosition = ThirdSquareGhost.transform;
        LastSquareGhostPosition = LastSquareGhost.transform;

        // Square rigidbodies
        FirstSquareRigidBody = FirstSquare.GetComponent<Rigidbody>();
        SecondSquareRigidBody = SecondSquare.GetComponent<Rigidbody>();
        ThirdSquareRigidBody = ThirdSquare.GetComponent<Rigidbody>();
        LastSquareRigidBody = LastSquare.GetComponent<Rigidbody>();

        // Square grabbable scripts
        FirstSquareGrabbable = FirstSquare.GetComponent<OVRGrabbable>();
        SecondSquareGrabbable = SecondSquare.GetComponent<OVRGrabbable>();
        ThirdSquareGrabbable = ThirdSquare.GetComponent<OVRGrabbable>();
        LastSquareGrabbable = LastSquare.GetComponent<OVRGrabbable>();

        // ghost square mesh renderer
        FirstSquareGhostMeshRenderer = FirstSquareGhost.GetComponent<MeshRenderer>();
        SecondSquareGhostMeshRenderer = SecondSquareGhost.GetComponent<MeshRenderer>();
        ThirdSquareGhostMeshRenderer = ThirdSquareGhost.GetComponent<MeshRenderer>();
        LastSquareGhostMeshRenderer = LastSquareGhost.GetComponent<MeshRenderer>();

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject[] squares = { FirstSquare, SecondSquare, ThirdSquare, LastSquare };
        GameObject[] SquareGhosts = { FirstSquareGhost, SecondSquareGhost, ThirdSquareGhost, LastSquareGhost };
        Transform[] SquarePositions = { FirstSquarePosition, SecondSquarePosition, ThirdSquarePosition, LastSquarePosition };
        Transform[] SquareGhostPositions = { FirstSquareGhostPosition, SecondSquareGhostPosition, ThirdSquareGhostPosition, LastSquareGhostPosition };
        OVRGrabbable[] SquareGrabbables = { FirstSquareGrabbable, SecondSquareGrabbable, ThirdSquareGrabbable, LastSquareGrabbable };
        Rigidbody[] SquareRigidBodies = { FirstSquareRigidBody, SecondSquareRigidBody, ThirdSquareRigidBody, LastSquareRigidBody };


        // when collision occurs, this loop will begin and try to find the Square that collided in the array, and see if its allowed to be placed in that spot (depending on Square count)
        for (int i = 0; i < squares.Length; i++)
        {
            if (SquareCount == i && collision.gameObject == squares[i])
            {
                SquarePlacement(squares[i], SquareGhosts[i], SquarePositions[i], SquareGhostPositions[i], SquareGrabbables[i], SquareRigidBodies[i]);
                SquareCount++;

                // if this was the last Square needed, we will make it tumble with an animation
                if (SquareCount == 4)
                {
                    //destroy squares so that the ghost ones are visible
                    Destroy(Squares);
                    
                   //make the ghost squares visible
                   FirstSquareGhost.SetActive(true); 
                   SecondSquareGhost.SetActive(true);
                   ThirdSquareGhost.SetActive(true);
                   LastSquareGhost.SetActive(true);
                   FirstSquareGhostMeshRenderer.enabled = true;
                   SecondSquareGhostMeshRenderer.enabled= true;
                   ThirdSquareGhostMeshRenderer.enabled= true;
                   LastSquareGhostMeshRenderer.enabled= true;

                   //trigger animation
                   TumbleAnimator.SetTrigger("Play");
                }

                break; // Exit the loop once the correct rock is found and placed
            }
        }
    }

    // this method is to move the rock where the ghost one was when called, and destroy what causes the rock to be moveable
    void SquarePlacement(GameObject Square, GameObject GhostSquare, Transform SquarePosition, Transform GhostSquarePosition, OVRGrabbable SquareGrabbable, Rigidbody SquareRigidbody)
    {
        // deactivates ghost rock so that rock is visible
        GhostSquare.SetActive(false);

        // destroy the rigidbody so it no longer can move
        Destroy(SquareRigidbody);

        // destroy the grabbable script so that it can no longer be grabbed once placed
        Destroy(SquareGrabbable);

        // moves the rock that collided to the location of the ghost rock
        SquarePosition.position = GhostSquarePosition.position;

        // makes the square face the correct direction
        SquarePosition.rotation = GhostSquarePosition.rotation;
    }
}
