using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class FSM : MonoBehaviour {
    // next Destination position of the agent
    protected Vector3 DestPos;
    [SerializeField]
    protected Transform PlayerTransform;
    protected AICharacterControl AICharacterControl;

    protected virtual void Initialize() {}
    protected virtual void FSMUpdate() {}
    protected  virtual void FSMFixedUpdate() {}

    private void Start() {
        Initialize();
    }

    private void Update() {
        FSMUpdate();
    }

    private void FixedUpdate() {
        FSMFixedUpdate();
    }
}