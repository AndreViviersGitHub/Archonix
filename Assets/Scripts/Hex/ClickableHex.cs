using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : MonoBehaviour {

    public int HexX;
    public int HexY;
    public bool isTaken;
    public HexMap map;
    public GameObject selectedUnit;

    private void OnMouseUp()
    {
        GameObject wMap = GameObject.FindGameObjectWithTag("Map");
        if (wMap.GetComponent<HexMap>().selectedUnit == null)
        {
            Debug.Log("No Unit Selected!!!");
        }
        else
        {
            map.GeneratePathTo(HexX, HexY);   //start pathfinding
        }
    }

    public void setSelectedUnit(GameObject _selectedUnit)
    {
        selectedUnit = _selectedUnit;
        isTaken = true;
    }

    public void clearSelectedUnit()
    {
        selectedUnit = null;
        isTaken = false;
    }
    

}
