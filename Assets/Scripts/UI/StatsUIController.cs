using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsUIController : MonoBehaviour {

    public Text weapon;
    public Text ammo;

    private GameManager gameManager;

    void Awake() {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if(gm != null) {
            gameManager = gm.GetComponent<GameManager>();
        }
        else {
            Debug.LogError("Unable to find GameObject with tag GameManager in this scene. Please add a manager, jeez.");
        }
    }

    void Update() {
        PlayerState ps = gameManager.currentPlayerState;
        UpdateUI(ps);
    }

    void UpdateUI(PlayerState ps) {
        weapon.text = ps.weapon.name;
        ammo.text = ps.weapon.clipAmmo + "/" + ps.weapon.ammo;
    }
}
