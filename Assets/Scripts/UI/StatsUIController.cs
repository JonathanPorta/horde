using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsUIController : MonoBehaviour {

  public Text weapon;
  public Text ammo;
  public Text round;
  public Text health;
  public Text score;
  public Text time;


  public Text spawnTimeout;
  public Text numberToSpawn;
  public Text spawnCount;
  public Text lastSpawn;
  public Text enemiescount;
  public Text roundStarted;

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
    GameState gs = gameManager.state;
    HordeManager hm = gameManager.hordeManager;
    UpdateUI(ps, gs, hm);
  }

  void UpdateUI(PlayerState ps, GameState gs, HordeManager hm) {
    weapon.text = ps.weapon.name;
    ammo.text = ps.weapon.clipAmmo + "/" + ps.weapon.ammo;

    round.text = "" + gs.round;

    health.text = ps.health + "/" + ps.maxHealth;

    score.text = "" + ps.kills;

    time.text = "" + Mathf.FloorToInt(Time.time - gs.startTime);


    spawnTimeout.text = hm.state.spawnTimeout + "";
    numberToSpawn.text = hm.state.numberToSpawn + "";
    spawnCount.text = hm.state.spawnCount + "";
    lastSpawn.text = hm.state.lastSpawn + "";
    enemiescount.text = hm.state.enemies.Count + "";
    roundStarted.text = gs.roundStarted + "";
  }
}
