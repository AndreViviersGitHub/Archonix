using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexMap : MonoBehaviour {

    public GameObject selectedUnit;
    //public GameObject selectedU;

    public HexType[] hexType;

    Node[,] graph;
    int[,] Hexes;

    //make unit array public Unit[] units;

    int MapSizeX = 11;
    int MapSizeY = 8;

    private void Start()
    {       
        GenerateMapArea();
        GeneratePathfindingGraph();
        GenerateMapVisualHexes();
        SetUnitsToHexes();
    }
    private void Update()
    {
        GameObject wMap = GameObject.FindGameObjectWithTag("Map");
        if (wMap.GetComponent<HexMap>().selectedUnit == null)
        {
            Debug.Log("No Unit Selected!!!");
        }
        else
        {
            ////setup selectedunit's variable
            selectedUnit.GetComponent<Unit>().hexX = (int)WorldCoordToHexCoord(selectedUnit.transform.position.x,
                                                                                selectedUnit.transform.position.y).x;
            selectedUnit.GetComponent<Unit>().hexY = (int)WorldCoordToHexCoord(selectedUnit.transform.position.x,
                                                                                    selectedUnit.transform.position.y).y;
            selectedUnit.GetComponent<Unit>().map = this;
        }

    }
    void GenerateMapArea()
    {
        //allocate map hexes
        Hexes = new int[MapSizeX, MapSizeY];

        //initialize map hexes
        for (int x = 0; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                Hexes[x, y] = 0;
            }
        }

        //random unwalkable mountains
        //will later be generated or selected from set maps
        Hexes[3, 0] = 2;
        Hexes[3, 1] = 2;
        Hexes[4, 1] = 2;

        Hexes[4, 5] = 2;
        Hexes[5, 4] = 2;
        Hexes[6, 3] = 2;
        Hexes[6, 4] = 2;
        Hexes[7, 4] = 2;
        Hexes[8, 4] = 2;

        Hexes[4, 5] = 2;
        Hexes[5, 6] = 2;
        Hexes[6, 5] = 2;
        Hexes[7, 6] = 2;
       
    }

    void SetUnitsToHexes()
    {
        GameObject enemyUnit = GameObject.FindWithTag("EnemyUnit");
        GameObject hex_10_4 = GameObject.Find("Hex_10_4");
        hex_10_4.GetComponent<ClickableHex>().setSelectedUnit(enemyUnit);

        GameObject playetUnit = GameObject.FindWithTag("PlayerUnit");
        GameObject hex_0_4 = GameObject.Find("Hex_0_4");
        hex_0_4.GetComponent<ClickableHex>().setSelectedUnit(playetUnit);
    }


    void GeneratePathfindingGraph()
    {
        //initialize the array
        graph = new Node[MapSizeX, MapSizeY];
     
        //initialize each node
        for (int x = 0; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {                             
                //6-edge hex map
                //Up
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                //down
                if (y < MapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                
                if (x % 2 == 0) //or could be x don't know yet
                {
                    //top-right
                    if ((x < MapSizeX - 1) && (y > 0))
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    //bottom-right
                    if (x < MapSizeX - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    //top-left
                    if ((x > 0) && (y > 0))
                        graph[x, y].neighbours.Add(graph[x -1, y - 1]);
                    //bottom-left
                    if (x > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                }
                else
                {
                    //top-right
                    if (x < MapSizeX - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    //bottom-right
                    if ((x < MapSizeX - 1) && (y < MapSizeY - 1))
                    graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                    //top-left
                    if (x > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                    //bottom-left
                    if ((x > 0) && (y < MapSizeY - 1))
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                //uses odd-q vertical layout
            }
        }
    }
    
    void GenerateMapVisualHexes()
    {
        double r = 0, c = 0, z = 0;

        for (int x = 0; x < MapSizeX; x++)
        {
            if (x % 2 == 0)
            {
                r = 0;
            }
            else
            {
                r = 0.95;
            }
             
            for (int y = 0; y < MapSizeY; y++)
            {
                HexType ht = hexType[Hexes[x, y]];

                //different levels of hex types (buffs, mountains, debuffs)
                switch (Hexes[x, y])
                {
                    case 0: z = 0;
                        break;
                    case 1: z = 0.5;
                        break;
                    case 2: z = -0.5;
                        break;
                    default: z = 0;
                        break; 
                }
                
                //Used to set the prefab on the map as well as able to get the object that was clicked on.
                GameObject gObject = (GameObject)Instantiate(ht.VisualHexPrefab, new Vector3((float)r, (float)c, (float)z), Quaternion.identity);
                gObject.name = "Hex_" + x + "_" + y;
                gObject.transform.SetParent(this.transform);
                gObject.GetComponent<ClickableHex>().clearSelectedUnit();
                ClickableHex clickhex = gObject.GetComponent<ClickableHex>();

                clickhex.HexX = x;
                clickhex.HexY = y;
                //Debug.Log(clickhex.HexX + " ,  " + clickhex.HexY);
                clickhex.map = this;

                r = r + 1.9;             //X coordinates  
            }
            c = c + 1.6; // Y coordinates
        }


    }


    float CostOfTile(int SourceX, int SourceY, int TargetX, int TargetY)
    {
        HexType ht = hexType[Hexes[TargetX, TargetY]];
        float cost = ht.movementCost;

        if (SourceX != TargetX && SourceY != TargetY)
        {
            cost += 0.001f;
        }
        return ht.movementCost;
    }



    public void GeneratePathTo(int x, int y)
    {
        //selectedUnit.GetComponent<Unit>().hexX = x;
         //selectedUnit.GetComponent<Unit>().hexY = y;
        // selectedUnit.transform.position = HexCoordToWorldCoord(x, y);

        // Clear out our unit's old path.
        selectedUnit.GetComponent<Unit>().currentPath = null;



        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().hexX,
                            selectedUnit.GetComponent<Unit>().hexY
                            ];

        Node target = graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        // Initialize everything to have INFINITY distance, since
        // we don't know any better right now. Also, it's possible
        // that some nodes CAN'T be reached from the source,
        // which would make INFINITY a reasonable value
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                float alt = dist[u] + CostOfTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null )
        {
            // No route between our target and the source
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    public Vector3 HexCoordToWorldCoord(int X, int Y)
    {
        //x and y values are swapped on the physical representation
        float tempY = 1.6f * X;
        float tempX;
        if (X % 2 == 0)
        { tempX = 1.9f * Y; }
        else { tempX = 1.9f * Y + 0.95f; }

        return new Vector3(tempX, tempY, (float)-1.6);
    }

    public Vector3 WorldCoordToHexCoord(float X, float Y)
    {
        float tempX = Y / 1.6f;

        //x and y values are swapped on the physical representation
        float tempY;
        if (tempX % 2 == 0)
        { tempY = X / 1.9f; }
        else
        { tempY = (X - 0.95f) / 1.9f; } //Need to round of because of issues with float arithmatic

        tempY = Mathf.Round(tempY);

       // Debug.Log(tempX.ToString() + "X/Y" + tempY.ToString());

        return new Vector3(tempX, tempY, (float)-1.6);
    }
}
