using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedStateManager : MonoBehaviour {

	public enum TurnState
    {
        START,
        USER_MOVE,
        USER_ATTACK,
        ENEMY_MOVE,
        ENEMY_ATTACK,
        LOSE,
        WIN
    }

    public TurnState currentState;
    public StateScripts ST;
    void Start()
    {
        currentState = TurnState.START;
        ST = new StateScripts();
    }

    void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.START):
                //setup battle scene
                ST.SetSelectedUnits("User");
                currentState = TurnState.USER_MOVE;
                break;
            case (TurnState.USER_MOVE):                
                ST.SetSelectedUnits("User");                
                //player move  
                break;
            case (TurnState.USER_ATTACK):
                //player attack
                break;
            case (TurnState.ENEMY_MOVE):
                ST.SetSelectedUnits("Enemy");
                //enemy move
                break;
            case (TurnState.ENEMY_ATTACK):
                //enemy attack
                break;
            case (TurnState.WIN):
                //win conditions
                break;
            case (TurnState.LOSE):
                //lose conditions
                break;

        }
    }
    void OnGUI()
    {
        
        GUI.Box(new Rect(10, 10, 170, 160), "Menu");
        if (GUI.Button(new Rect(20, 105, 150, 25), "Move Unit") && (currentState == TurnState.USER_MOVE || currentState == TurnState.ENEMY_MOVE))
        {
            ST.UnitMove();
            if (ST.ActionCheck() == false && currentState == TurnState.USER_MOVE) // check if the unit has turns left, if not go to enemy turn WILL ADD TOTAL TURN COUNT when more units are added
            {
                Debug.Log(ST.ActionCheck());
                currentState = TurnState.ENEMY_MOVE; // change to Enemy Turn
                ST.ResetActionsLeft(); // reset the moves for the enemy
            }
            else
            {
                Debug.Log(ST.ActionCheck());
                currentState = TurnState.USER_MOVE; // change to User Turn
                ST.ResetActionsLeft(); // reset the moves for the user
            }
            Debug.Log(ST.ActionCheck()); // check if sets correctly
        }

        if (GUI.Button(new Rect(20, 140, 150, 25), "End Turn"))
        {
            if (currentState == TurnState.START)
            {
                currentState = TurnState.USER_MOVE;
                ST.ResetActionsLeft();                
            }
            else if (currentState == TurnState.USER_MOVE)
            {
                currentState = TurnState.USER_ATTACK;
            }
            else if (currentState == TurnState.USER_ATTACK)
            {
                currentState = TurnState.ENEMY_MOVE;
                ST.ResetActionsLeft();
            }
            else if (currentState == TurnState.ENEMY_MOVE)
            {
                currentState = TurnState.ENEMY_ATTACK;
            }
            else if (currentState == TurnState.ENEMY_ATTACK)
            {
                currentState = TurnState.WIN;
            }
            else if (currentState == TurnState.WIN)
            {
                currentState = TurnState.LOSE;
            }
            else if (currentState == TurnState.LOSE)
            {
                currentState = TurnState.USER_MOVE;
            }

        

          
        }
    }


}
