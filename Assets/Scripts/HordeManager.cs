using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HordeManager : MonoBehaviour {

  public GameObject enemyPrefab;

  public RoundState state {
    get {
      if(_state == null)
        _state = new RoundState(gameManager);
      return _state;
    }
    set { _state = value; }
  }

  private RoundState _state;
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
    Spawn();
  }
  
  private void Spawn() {
    if(!state.CanSpawn()) return;

    state.lastSpawn = Time.time;
    Vector3 spawnPoint = gameManager.GetEnemySpawnPoint();
    GameObject e = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity) as GameObject;
    state.enemies.Add(e);

    state.spawnCount = state.spawnCount + 1;
  }

  void OnEnable() {
    EventManager.StartListening("StartRound", StartRound);
    EventManager.StartListening("EndRound", EndRound);
    EventManager.StartListening("NextRound", NextRound);
  }

  void OnDisable() {
    EventManager.StopListening("StartRound", StartRound);
    EventManager.StopListening("EndRound", EndRound);
    EventManager.StopListening("NextRound", NextRound);
  }

  void StartRound() {
    //roundStarted = true; // has the round started yet?
    //Debug.Log("THE ROUND HAS STARED!");
    state = new RoundState(gameManager);
  }

  void EndRound() {
    //roundStarted = false;
  }

  void NextRound() {
    // Reset some stuff
  }

  public void Destroy(EnemyController ec) {
    state.enemies.Remove(ec.gameObject);
    Destroy(ec.gameObject);

    // Let's see if that was the last enemy
    if(state.enemies.Count == 0 && state.spawnCount >= state.numberToSpawn) {
      EventManager.TriggerEvent("EndRound");
    }
  }
}


// Per round multipliers
[System.Serializable]
public class HoardeMultipliers : System.Object {
  public float spawnTimeout = 10.0f;
  public float spawnMultiplier = 3.4f;
  public float spawnRate = 0.89f;
  public float healthMultiplier = 1.73f;
  public float speedMultiplier = 0.23f;
}

[System.Serializable]
public class RoundState : System.Object {
  public int round = 0;
  public GameManager gameManager;

  public HoardeMultipliers hoardeMultipliers;
  public List<GameObject> enemies;

  public float spawnTimeout;
  public int numberToSpawn;
  public int spawnCount;
  public float lastSpawn;
 
  public RoundState(GameManager gm) {
    round = gm.state.round;
    gameManager = gm;

    hoardeMultipliers = new HoardeMultipliers();
    enemies = new List<GameObject>();

    spawnTimeout = hoardeMultipliers.spawnRate * hoardeMultipliers.spawnTimeout; // Time between spawns
    numberToSpawn = Mathf.CeilToInt((round + 1) * hoardeMultipliers.spawnMultiplier); // total number to spawn this round :)

    spawnCount = 0; // total number already spawned this round
    lastSpawn = 0.0f; // time we last spawned an enemy
  }

  public bool CanSpawn() {
    // Don't spawn when the round isn't running.
    if(!gameManager.state.roundStarted) return false;

    // Has it been enough time since we last spawned an enemy?
    if(Time.time - lastSpawn < spawnTimeout) return false;

    // Have we already spawned the max ammount of enemies for this round?
    if(spawnCount >= numberToSpawn) return false;

    // Woot.
    return true;
  }
}