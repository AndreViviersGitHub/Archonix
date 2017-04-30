using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int hexX; // uint x
    public int hexY; // unit y
    public GameObject hexOccupied;
    public HexMap map;  // hexmap to get the map ingame
    private bool moveBusy = false;  // boolean to check if unit is bussy moving
    public List<Node> currentPath = null;  //list of all nodes from start to end
    public float moveSpeed =2f; // How far a unit can move
    private float actionsLeft = 1; // Units only get one action either move or attack
    private GameObject getHexInPath; // get hexes in the path to the end 
    private GameObject lastHex; // verry last hex in the path (the one that is clicked on)
    private List<Vector3> clickedNode = null; // stores 2 hexes first click and then second click 
    private List<GameObject> gObjList ; // stores all the objects between start and end nodes
    private bool done = false; // check if loop is done
    private bool done2 = false; // check if loop is done
    private int currNode = 0; //used for indexing 
    private TurnBasedStateManager TS;

    private GameObject targetedUnit;
    //unit attributes
    public int attackPower;
    private int dameg;
    public int totalHealth;
    public int defense;
    public string unitName;
    public int playerNumber;


    private void Start()
    {
        clickedNode = new List<Vector3>(); // initialise the clickedNode
        gObjList = new List<GameObject>(); // initialise the gObjList
        TS = Camera.main.GetComponent<TurnBasedStateManager>();

    }
 
    private void Update()
    {
        if (TS.getTurnState() == "USER_MOVE")
        {
            if (currentPath != null)
            {
                int currentPatLength = currentPath.Count - 1;
                Vector3 endHexNode = map.HexCoordToWorldCoord(currentPath[currentPatLength].x, currentPath[currentPatLength].y) + new Vector3(0, 0, -1f); // gets end node

                if (Input.GetMouseButtonUp(0))
                {
                    clickedNode.Add(endHexNode);
                }

                if (clickedNode.Count == 1 && done == false)
                {
                    if (gObjList.Count > 2)
                    {
                        RemoveHexColor(); // remove the hexColors
                        done = false;
                    }
                    CreateHexColorPath(currNode, true, false); // Colors the hexes in the path to the end      
                }
                currNode = 0;
                if (clickedNode.Count == 2 && done2 == false)
                {
                    Debug.Log(gObjList.Count);
                    RemoveHexColor(); // remove the hexColors    
                    clickedNode = new List<Vector3>();
                    CreateHexColorPath(currNode, false, true); // Colors the hexes in the path to the end               
                    clickedNode = new List<Vector3>(); //reset clickedNode list
                }

            }
        }
        
    }


   //All Code for Coloring hexes on the movement path
    #region Pathfinding Color

    #region remove hex material color 
    private void RemoveHexColor()
    {                    
            while (currNode < gObjList.Count)
            {
                gObjList[currNode].GetComponent<Renderer>().material.color = new Color32(0, 255, 33, 0);
                currNode++;
            }
        gObjList = new List<GameObject>();
        currNode = 0;
    }
    #endregion

    #region Find At (gets the hex)
    /// <summary>
    /// Finds the hex at the co-ordinates given in HexPos
    /// </summary>
    /// <param name="HexPos">the co-ordinates of the object that is to be returned</param>
    /// <returns></returns>
    public GameObject FindAt(Vector3 HexPos)
    {
        HexPos.z += 1; // shift the position 1 unit backwards       
        RaycastHit hitInfo; // stores the info about the object that was hit by the raycast
        if (Physics.Raycast(HexPos, Vector3.forward, out hitInfo))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            gObjList.Add(hitInfo.collider.gameObject); // adds the object that was caught by the raycast to the list
            return hitInfo.collider.gameObject; // return the object             
        }
        else
        {
            return null; // return nothing if there was no object hit
        }
    }
    #endregion 

    #region Change hex material color
    /// <summary>
    /// Change hex material color to blue from start to where the user clicked
    /// </summary>
    /// <param name="CurrentNode">the node that is currently being changed color of</param>
    /// <param name="Done1">used for the loop parameters Done1 = true if the first loop needs to complete</param>
    /// <param name="Done2">used for the loop parameters Done1 = true if the second loop needs to complete</param>
    private void CreateHexColorPath(int CurrentNode, bool Done1, bool Done2)
    {
        ColorFirstHex();
        while (CurrentNode < currentPath.Count - 1)
        {
            Vector3 nexHexNode = map.HexCoordToWorldCoord(currentPath[CurrentNode + 1].x, currentPath[CurrentNode + 1].y) + new Vector3(0, 0, -1f); // gets next node
            getHexInPath = FindAt(nexHexNode);
            getHexInPath.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            CurrentNode++; // increase hex  
        }
        done = Done1;
        done2 = Done2;
    }
    #endregion

    #region color first hex
    private void ColorFirstHex()
    {
        Vector3 firsthexNode = map.HexCoordToWorldCoord(currentPath[0].x, currentPath[0].y) + new Vector3(0, 0, -1f); // gets start node
        getHexInPath = FindAt(firsthexNode);
        getHexInPath.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
    }
    #endregion


    #endregion

    //All Code for movement / transition / movement ques

    #region Move next / move over time / move que/ transition
    // Move to next Hex Code
    #region Move To Next Hex
    public void MoveNextTile()
    {
        
        if (actionsLeft <= 0)
        {
            return; // no actions left
        }
        else
        {
            for (int i = 0; i < moveSpeed; i++) // loops to the max movement speed of the unit Will change later to loop to the Amount of Hexes to move over (less unneccesary looping)
            {

                if (currentPath == null)
                    return; // return if theres no path available

                currentPath.RemoveAt(0); // we start on 0 so remove it to get next tile as 0 

                MoveNextTransition(map.HexCoordToWorldCoord(currentPath[0].x, currentPath[0].y)); // calls the method to start the movement que
                actionsLeft -= 1;  //removes a user action (only one turn per unit ATTACK OR MOVE)

                RemoveHexColor(); // remove the hexColors

                if (currentPath.Count == 1)
                {
                    currentPath = null;//set current pat to null since we are on last hex
                }
            }
            
        }
        

        Debug.Log("Actions Left: " + actionsLeft);
    }
    #endregion
    // move to end position over time AVOIDS TELEPORTING
    #region Move to Position over time
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPos">New pos unit should move to</param>
    /// <param name="time"> ow long it should take before it reaches end</param>
    /// <returns></returns>
    IEnumerator MoveToPosOverTime(Vector3 newPos, float time)
    {

        Vector3 startPoint = transform.position;
        float elaspedTime = 0;
        while (transform.position != newPos)
        {
            transform.position = Vector3.Lerp(startPoint, newPos, (elaspedTime / time));
            elaspedTime += Time.deltaTime;
            //Debug.Log("position x: " + transform.position.x + " position y: " + transform.position.y);
            yield return new WaitForEndOfFrame();
        }
        moveBusy = false;

        //Debug.Log("Reached destination");

    }

    #endregion
    // Create movement que so only one unit can move at a time
    #region MoveQue
    /// <summary>
    ///
    /// </summary>
    /// <param name="newPos"> New pos unit should move to</param>
    /// <param name="time">how long it should take before it reaches end</param>
    /// <returns></returns>

    IEnumerator MoveQue(Vector3 newPos, float time)
    {
        bool done = false;
        while (!done)
        {
            if (moveBusy == false)
            {
                moveBusy = true;
                StartCoroutine(MoveToPosOverTime(newPos, time));
                done = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    #region Movement Transition
    private void MoveNextTransition(Vector3 movingTo)
    {
        
        StartCoroutine(MoveQue(movingTo, 1f));

    }

    #endregion

    #endregion
 
    //All code for Action Reset and Action Check

    #region Action Reset and Action Check


    //Reset Actions so each unit has refreshed amount of actions at start of each new turn

    #region End Turn resets
    public void ResetActionsLeft()
    {
        Debug.Log("Actions Left: " + actionsLeft);
        actionsLeft = 1;
    }

    public void ResetCurrentPath()
    {
        currentPath = null;
    }

    #endregion


    //Check howmany actions the unit has left

    #region Check Actions
    public bool ActionsLeft()
    {
        if (actionsLeft <= 0)        
            return false;        
        else
            return true;
    }
    #endregion
    #endregion
}

