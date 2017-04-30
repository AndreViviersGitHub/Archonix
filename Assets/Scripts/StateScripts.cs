using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScripts : MonoBehaviour {
   
    private GameObject unit;
    private GameObject map;
    /// <summary>
    /// Sets the map objects selected unit at the start of the scene to friendly Unit
    /// </summary>
    /// <param name="playerTag"> used to get the type of unit that needs to be selected friendly or enemy</param>
    public void SetSelectedUnits(string playerTag)
    {
        if (playerTag == "User")
            playerTag = "friendlyUnit";
        else
            playerTag = "enemyUnit";

        map = GameObject.FindGameObjectWithTag("Map");
        unit = GameObject.Find(playerTag);
        unit = unit.transform.gameObject;
        map.GetComponent<HexMap>().selectedUnit = unit;
    }

    public void UnitMove()
    {
        unit.GetComponent<Unit>().MoveNextTile();       

    }

    public void ResetActionsLeft()
    {
        unit.GetComponent<Unit>().ResetActionsLeft();
    }

    public bool ActionCheck()
    {
        bool isActionLeft;
        isActionLeft = unit.GetComponent<Unit>().ActionsLeft();
        return isActionLeft;
    }

    public void ResetCurrentPath()
    {
        unit.GetComponent<Unit>().ResetCurrentPath();
    }
}
