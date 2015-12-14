using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Camera overheadCamera {
        get {
            if(_overheadCamera == null) {
                GameObject go = GameObject.FindGameObjectWithTag("OverheadCamera") as GameObject;
                _overheadCamera = go.GetComponent<Camera>();
            }
            return _overheadCamera;
        }
    }

    public Camera currentPlayerCamera {
        get {
            if(_currentPlayerCamera == null) {
                GameObject go = GameObject.FindGameObjectWithTag("CurrentPlayerCamera") as GameObject;
                _currentPlayerCamera = go.GetComponent<Camera>();
            }
            return _currentPlayerCamera;
        }
    }

    private Camera _overheadCamera;
    private Camera _currentPlayerCamera;

    void OnEnable() {
        EventManager.StartListening("StartRound", StartRound);
        EventManager.StartListening("EndRound", EndRound);
    }

    void OnDisable() {
        EventManager.StopListening("StartRound", StartRound);
        EventManager.StopListening("EndRound", EndRound);
    }
    
    void StartRound() {
      //Debug.Log("CameraManager - Got Start Round!");
      currentPlayerCamera.enabled = true;
        overheadCamera.enabled = false;
    }

    void EndRound() {
      //Debug.Log("CameraManager - Got END Round!");
    //currentPlayerCamera.enabled = false;
    //overheadCamera.enabled = true;
  }
}
