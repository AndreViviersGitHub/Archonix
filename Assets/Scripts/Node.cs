using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public List<Node> neighbours;
    public int x;
    public int y;

    public Node()
    {
        neighbours = new List<Node>();
    }

    /*   public float DistanceTo(Node n)
       {
           //Want to test if the hex to cube conversions are needed here
           //testing distance and always ends at 1, 1
           //Debug.LogError(Vector2.Distance(new Vector2(x, y),
           //                        new Vector2(n.x, n.y)));

           // return Vector2.Distance(new Vector2(x, y),
           //                         new Vector2(n.x, n.y));

           /*(abs(a.q - b.q)                 //test manhatten cube distance
         + abs(a.q + a.r - b.q - b.r)
         + abs(a.r - b.r)) / 2


           Vector3 start = new Vector3();
           Vector3 end = new Vector3();

           //# convert odd-q offset to cube
           //start
           start.x = y;
           start.z = x - (y - (y & 1)) / 2;
           start.y = -start.x - start.z;

           //end
           end.x = n.y;
           end.z = n.x - (n.y - (n.y & 1)) / 2;
           end.y = -end.x - end.z;

           Debug.LogError(Vector2.Distance(new Vector2(start.x,start.y),
                                    new Vector2(end.x, end.y)));
           //(Mathf.Abs(start.y - end.y) + Mathf.Abs(start.y + start.x - end.y - end.x) + Mathf.Abs(start.x - end.x)) / 2);
           return Vector2.Distance(new Vector2(start.x, start.y),
                                    new Vector2(end.x, end.y));


               //(Mathf.Abs(start.y - end.y) + Mathf.Abs(start.y + start.x - end.y - end.x) + Mathf.Abs(start.x - end.x)) / 2;
       }*/   //old

    //Quill Distance to
    public float DistanceTo(Node n)
    {
        if (n == null)
        {
            Debug.LogError("WTF?");
        }

        return Vector2.Distance(
            new Vector2(x, y),
            new Vector2(n.x, n.y)
            );
    }

}
