using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundInterstitialUIController : MonoBehaviour {

    public Text title;
    public Text roundLabel;
    public float duration = 3.0f;
    public bool fadeout = true;

    private int round = 0;
    private Color labelColor;

    void OnAwake() {
        labelColor = roundLabel.color;
    }

    void OnEnable() {
        EventManager.StartListening("StartRound", StartRound);
        EventManager.StartListening("NextRound", NextRound);
    }

    void OnDisable() {
        EventManager.StopListening("StartRound", StartRound);
        EventManager.StopListening("NextRound", NextRound);
    }

    void UpdateUI() {
        roundLabel.text = "" + round;
        FadeIn();
        if(fadeout) {
            FadeOut();
        }
    }

    void StartRound() {
        UpdateUI();
    }

    // TODO: Should I get this from the GameManager, pass it as an event, or just guess - like it is now.
    void NextRound() {
        round = round + 1;
        UpdateUI();
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
