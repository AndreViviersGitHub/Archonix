using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : MonoBehaviour {

    public int HexX;
    public int HexY;
    public HexMap map;
    public GameObject selectedUnit;

    private void OnMouseUp()
    {
        Debug.Log(HexX + "," + HexY);

        //map.GeneratePathTo(HexX, HexY);           //original

        //generating path to friendlyUnit on every click         //andre can just comment this out or delete it
        map.selectedU.GetComponent<FriendlyUnit>().hexX = (int)map.WorldCoordToHexCoord(map.selectedU.transform.position.x, map.selectedU.transform.position.y).x;
        map.selectedU.GetComponent<FriendlyUnit>().hexY = (int)map.WorldCoordToHexCoord(map.selectedU.transform.position.x, map.selectedU.transform.position.y).y;
        map.selectedU.GetComponent<FriendlyUnit>().map = map;
        map.GeneratePathTo(map.selectedU.GetComponent<FriendlyUnit>().hexX, map.selectedU.GetComponent<FriendlyUnit>().hexY);
    }

}
