using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
  
  public GameObject localPlayerPrefab;

  public GameState state {
    get {
      if(_state == null)
        _state = new GameState();
      return _state;
    }
    set { _state = value; }
  }

  public PlayerState currentPlayerState {
        get {
            if(_currentPlayerState == null)
                _currentPlayerState = new PlayerState();
            return _currentPlayerState;
        }
        set { _currentPlayerState = value; }
    }

  private GameState _state;
  private PlayerState _currentPlayerState;

  private GameObject arena;
  private ArenaController arenaController;
  public HordeManager hordeManager;
  private PlayerController localPlayerController;

  void Awake() {
    // Get a reference to the current arena.
    arena = GameObject.FindGameObjectWithTag("Arena");
    if(arena != null) {
      arenaController = arena.GetComponent<ArenaController>();
    }
    else {
      Debug.LogError("Unable to find GameObject with tag Arena in this scene. Please add an arena, jeez.");
    }

    // Grab ref to HordeManager
    GameObject horde = GameObject.FindGameObjectWithTag("HordeManager");
    if(horde != null) {
      hordeManager = horde.GetComponent<HordeManager>();
    }
    else {
      Debug.LogError("Unable to find GameObject with tag HordeManager in this scene. Please add some enemies, jeez.");
    }

    // Instantiate the avatar for the localplayer.
    GameObject lp = Instantiate(localPlayerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    localPlayerController = lp.GetComponent<PlayerController>();
    localPlayerController.state = currentPlayerState;

    // Mark the time
    state.startTime = Time.time;
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
    //Debug.Log("GameManager - Got StartRound!");
    EventManager.TriggerEvent("PlayerRespawn");
    state.roundStarted = true;
  }

  void EndRound() {
    // Debug.Log("GameManager - Got ENDRound!");
    // Make ready for what's to come...
    // EventManager.TriggerEvent("NextRound");
  }

  void NextRound() {
    //Debug.Log("GameManager - Got NextRound!");
    state.round = state.round + 1;

    // Start the next round, if still alive.
    //EventManager.TriggerEvent("StartRound");
  }

  public void Respawn(PlayerController player) {
    // Don't force a respawn.
    if(player.state.isAlive) return;

    Vector3 spawnPoint = arenaController.GetRandomPlayerSpawnPoint();
    _currentPlayerState = player.state;
    player.Respawn(spawnPoint);
  }

  public Vector3 GetEnemySpawnPoint() {
    return arenaController.GetNearestEnemySpawnPoint(Vector3.zero);
  }

  public Weapon GetDefaultWeapon() {
    // Return a default weapon - usually a pistol.
    return new Weapon();
  }

  public void Kill(PlayerController pc, EnemyController ec) {
    Debug.Log("Counting your kill!");

    pc.state.kills = pc.state.kills + 1;
    hordeManager.Destroy(ec);
  }
}

[System.Serializable]
public class GameState : System.Object {
  public int round = 0;
  public float startTime;
  public bool roundStarted = false;
}