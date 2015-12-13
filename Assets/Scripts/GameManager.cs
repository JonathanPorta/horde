using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int round = 0;
    public GameObject localPlayerPrefab;

    //public PlayerState currentPlayerState {
    //    get { return _currentPlayerState; }
    //    set { _currentPlayerState = value; }
    //}

    public PlayerState currentPlayerState {
        get {
            if(_currentPlayerState == null)
                _currentPlayerState = new PlayerState();
            return _currentPlayerState;
        }
        set { _currentPlayerState = value; }
    }

    private PlayerState _currentPlayerState;

    private GameObject arena;
    private ArenaController arenaController;
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

        // Instantiate the avatar for the localplayer.
        GameObject lp = Instantiate(localPlayerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        localPlayerController = lp.GetComponent<PlayerController>();
        localPlayerController.state = currentPlayerState;
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
        EventManager.TriggerEvent("PlayerRespawn");

    }

    void EndRound() {

    }

    void NextRound() {
        round = round + 1;
    }

    public void Respawn(PlayerController player) {
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
}
