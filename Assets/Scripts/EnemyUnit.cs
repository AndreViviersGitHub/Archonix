using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour {

    public int hexX;
    public int hexY;
    public HexMap map;

    public List<Node> currentPath = null;

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
    }
}
