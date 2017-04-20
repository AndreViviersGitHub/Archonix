using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScripts : MonoBehaviour {
   
    private GameObject unit;
    private GameObject map;
    /// <summary>
    /// Sets the map objects selected unit at the start of the scene to friendly Unit
    /// </summary>
    /// <param name="whatPlayer"> used to get the type of unit that needs to be selected friendly or enemy</param>
    public void SetSelectedUnits(string whatPlayer)
    {
        if (whatPlayer == "User")
            whatPlayer = "enemyUnit";
        else
            whatPlayer = "friendlyUnit";

        map = GameObject.FindGameObjectWithTag("Map");
        unit = GameObject.Find(whatPlayer);
        unit = unit.transform.parent.gameObject;
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
}
