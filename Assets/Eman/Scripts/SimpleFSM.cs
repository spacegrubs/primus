using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.Characters.ThirdPerson;

public class SimpleFSM : FSM {
    public enum FSMState {
        None,
        Patrol,
        Chase, 
        Attack,
        Dead,
    }

    // Current state that the NPC is reaching
    public FSMState currentState;

    protected override void Initialize() {
        AICharacterControl = GetComponent<AICharacterControl>();
        currentState = FSMState.Patrol;
        // Reference to Player object
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = objPlayer.transform;
    }

    protected override void FSMUpdate() {
        switch (currentState) {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
        }
    }
    /// <summary>
    /// Patrol State
    /// </summary>
    private void UpdatePatrolState() {
        AICharacterControl.SetTarget(null);
        if (Vector3.Distance(transform.position, PlayerTransform.position) <= 9.99f) {
            currentState = FSMState.Chase;
        }
    }
    /// <summary>
    /// Chase State
    /// </summary>
    private void UpdateChaseState() {
        AICharacterControl.SetTarget(PlayerTransform);
        float dist = Vector3.Distance(transform.position, PlayerTransform.position);        
        
        if (dist >= 10.0f) {
            currentState = FSMState.Patrol;
        }
    }
}