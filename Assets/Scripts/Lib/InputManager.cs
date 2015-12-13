using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") != 0.0f) {
            EventManager.TriggerEvent("LocalPlayerMove");
        }

        if (Input.GetAxis("Horizontal") != 0.0f) {
            EventManager.TriggerEvent("LocalPlayerRotate");
        }

        if(Input.GetButton("Fire1")) {
            EventManager.TriggerEvent("LocalPlayerShoot");
        }

        if(Input.GetButton("Reload")) {
            EventManager.TriggerEvent("LocalPlayerReload");
        }

        // Debugging Hotkeys
        if(Input.GetKeyDown("f1")) {
            EventManager.TriggerEvent("StartRound");
        }

        if(Input.GetKeyDown("f2")) {
            EventManager.TriggerEvent("EndRound");
        }

        if(Input.GetKeyDown("f3")) {
            EventManager.TriggerEvent("NextRound");
        }
    }
}
