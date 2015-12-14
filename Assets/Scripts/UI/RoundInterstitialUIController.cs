using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundInterstitialUIController : MonoBehaviour {

  public Text title;
  public Text roundLabel;
  public float duration = 3.0f;
  public bool fadeout = true;

  private GameManager gameManager;
  private Color labelColor;

  void Awake() {
    GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
    if(gm != null) {
      gameManager = gm.GetComponent<GameManager>();
    }
    else {
      Debug.LogError("Unable to find GameObject with tag GameManager in this scene. Please add a manager, jeez.");
    }

    // Preserve the orginal color so we can put it back later.
    labelColor = roundLabel.color;
  }

  void OnEnable() {
    //Debug.Log("Interstich - Got onENABLE!!!");
    UpdateUI();
    CrossFade();
    //gameObject.SetActive(false);
    //EventManager.StartListening("StartRound", CrossFade);
    //EventManager.StartListening("EndRound", UpdateUI);
    //EventManager.StartListening("NextRound", UpdateUI);
  }

  void OnDisable() {
    //Debug.Log("Interstich - Got disable.");
    FadeOut();
    //EventManager.StopListening("StartRound", CrossFade);
    //EventManager.StopListening("EndRound", UpdateUI);
    //EventManager.StopListening("NextRound", UpdateUI);
  }
  
  void Update() {
    //GameState gs = gameManager.state;
    UpdateUI();
  }

  void UpdateUI() {
    GameState gs = gameManager.state;
    roundLabel.text = "" + gs.round;
  }

  void CrossFade() {
    FadeIn();
    if(fadeout) {
      FadeOut();
    }
  }

  void FadeIn() {
    roundLabel.CrossFadeAlpha(1.0f, 0.02f, false);
    if(title != null) {
      title.CrossFadeAlpha(1.0f, 0.02f, false);
    }
  }

  void FadeOut() {
    roundLabel.CrossFadeAlpha(0.0f, duration, false);
    if(title != null) {
      title.CrossFadeAlpha(0.0f, duration, false);
    }
  }
}
