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
    private string setText;

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
                setText = "Start";
                break;
            case (TurnState.USER_MOVE):                
                ST.SetSelectedUnits("User");
                setText = "Player Move";
                //player move  
                break;
            case (TurnState.USER_ATTACK):
                setText = "Player Attack!";
                //player attack
                break;
            case (TurnState.ENEMY_MOVE):
                ST.SetSelectedUnits("Enemy");
                setText = "Enemy Move";
                //enemy move
                break;
            case (TurnState.ENEMY_ATTACK):
                setText = "Enemy Attack!";
                //enemy attack
                break;
            case (TurnState.WIN):
                setText = "Win Condition";
                //win conditions
                break;
            case (TurnState.LOSE):
                setText = "Lose Condition";
                //lose conditions
                break;

        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 170, 160), "Menu");
        GUI.Label(new Rect(20, 30, 170, 160),"State : " + setText);
        GUI.Button(new Rect(20, 70, 150, 25), "Attack! ");


        if (GUI.Button(new Rect(20, 105, 150, 25), "Move Unit") && (currentState == TurnState.USER_MOVE ))
        {
            ST.UnitMove();
            if (ST.ActionCheck() == false && currentState == TurnState.USER_MOVE) // check if the unit has turns left, if not go to enemy turn WILL ADD TOTAL TURN COUNT when more units are added
            {
                Debug.Log(ST.ActionCheck());
                currentState = TurnState.ENEMY_MOVE; // change to Enemy Turn
                ST.ResetActionsLeft(); // reset the moves for the enemy
                ST.ResetCurrentPath();
            }
            else
            {
                Debug.Log(ST.ActionCheck());
                currentState = TurnState.USER_MOVE; // change to User Turn
                ST.ResetActionsLeft(); // reset the moves for the user
                ST.ResetCurrentPath();

            }
            Debug.Log(ST.ActionCheck()); // check if sets correctly
        }

        if (GUI.Button(new Rect(20, 140, 150, 25), "End Turn"))
        {
            if (currentState == TurnState.START)
            {
                currentState = TurnState.USER_MOVE;
                ST.ResetActionsLeft();
                ST.ResetCurrentPath();               
            }
            else if (currentState == TurnState.USER_MOVE)
            {
                currentState = TurnState.USER_ATTACK;
                ST.ResetCurrentPath();

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
                ST.ResetCurrentPath();

            }
            else if (currentState == TurnState.WIN)
            {
                currentState = TurnState.LOSE;
                ST.ResetCurrentPath();
            }
            else if (currentState == TurnState.LOSE)
            {
                currentState = TurnState.USER_MOVE;
                ST.ResetCurrentPath();
            }




        }
    }

    public string getTurnState()
    {
        string state = "";
        switch (currentState)
        {
            case (TurnState.START):
                state = "START";
                break;
            case (TurnState.USER_MOVE):
                state = "USER_MOVE";
                break;
            case (TurnState.USER_ATTACK):
                state = "USER_ATTACK";
                break;
            case (TurnState.ENEMY_MOVE):
                state = "ENEMY_MOVE";
                break;
            case (TurnState.ENEMY_ATTACK):
                state = "ENEMY_ATTACK";
                break;
            case (TurnState.WIN):
                state = "WIN";
                break;
            case (TurnState.LOSE):
                state = "LOSE";
                break;

        }
        return state;
    }


}
