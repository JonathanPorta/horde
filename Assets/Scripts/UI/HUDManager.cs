using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

  public GameObject roundInterstitial;
  public GameObject statsSuper;

  void OnEnable() {
    EventManager.StartListening("StartRound", StartRound);
    EventManager.StartListening("EndRound", EndRound);
  }

  void OnDisable() {
    EventManager.StopListening("StartRound", StartRound);
    EventManager.StopListening("EndRound", EndRound);
  }

  void StartRound() {
    //Debug.Log("HUDManager - Got Start Round!");
    roundInterstitial.SetActive(true);
    statsSuper.SetActive(true);
  }

  void EndRound() {
    //Debug.Log("HUDManager - Got END Round!");
    roundInterstitial.SetActive(false);
    statsSuper.SetActive(false);
  }
}
