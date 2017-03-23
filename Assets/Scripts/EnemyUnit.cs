using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour {

    public int hexX;
    public int hexY;
    public HexMap map;
    //public GameObject wMap = GameObject.FindGameObjectWithTag("Map");


    public List<Node> currentPath = null;
    //private void OnMouseDown()
    //{

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    GameObject wMap = GameObject.FindGameObjectWithTag("Map");
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(ray, out hitInfo))
    //    {
    //        GameObject ourHitObject = hitInfo.collider.transform.gameObject;
    //        Debug.Log("we hit " + ourHitObject.name);
    //        wMap.GetComponent<HexMap>().selectedUnit = ourHitObject;
    //    }


    //}
    private void Update()
    {
        if(currentPath != null)
        {
            int currNode = 0;

            while(currNode < currentPath.Count - 1)
            {
                Vector3 start = map.HexCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) + new Vector3(0, 0, -1f);
                Vector3 end = map.HexCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) + new Vector3(0, 0, -1f);
                Debug.DrawLine(start, end, Color.red);
                currNode++;
            }
        }

        
        if (Input.GetMouseButtonDown(0)) // if left clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                     // create a ray cast at mouse position
            RaycastHit hitInfo;                                                              // captures object that was hit
            GameObject wMap = GameObject.FindGameObjectWithTag("Map");                       // gets the Gameobject with the Tag Map
            if (Physics.Raycast(ray, out hitInfo))                                           // if the raycast hits anything atall
            {
                if (hitInfo.collider.tag == "Hex")
                {
                    //check that we dont accidently click hexes WILL change later to get hexes selected unit
                }
                else
                {
                    GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;             // gets the gameobject that was hit in this case the enemy
                    Debug.Log("We selected: " + ourHitObject.name);                                    // display if enemy was hit by the raycast
                    wMap.GetComponent<HexMap>().selectedUnit = ourHitObject;                    // gets the mapcomponents script HexMap to change the selectedUnit
                }

            }
        }
    }
}
