using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponController))]
public class PlayerController : MonoBehaviour {

    public float movementSpeed = 0.5f;
    public float rotationSpeed = 10.0f;

    public PlayerState state {
        get {
            if(_state == null)
                _state = gameManager.currentPlayerState;
            return _state;
        }
        set { _state = value; }
    }

    public Weapon weapon {
        get { return state.weapon; }
        set { state.weapon = value; }
    }

    private PlayerState _state;
    private bool isAlive = false; // That's right, you start out dead. Deal with it!

    private GameManager gameManager;
    private WeaponController weaponController;
    

    void Awake() {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if(gm != null) {
            gameManager = gm.GetComponent<GameManager>();
        }
        else {
            Debug.LogError("Unable to find GameObject with tag GameManager in this scene. Please add a manager, jeez.");
        }
        gameManager.currentPlayerState = state;
        weaponController = GetComponent<WeaponController>();
    }

    void OnEnable() {
        EventManager.StartListening("LocalPlayerMove", Move);
        EventManager.StartListening("LocalPlayerRotate", Rotate);

        EventManager.StartListening("LocalPlayerShoot", Shoot);
        EventManager.StartListening("LocalPlayerReload", Reload);

        EventManager.StartListening("PlayerRespawn", Respawn);
    }

    void OnDisable() {
        EventManager.StopListening("LocalPlayerMove", Move);
        EventManager.StopListening("LocalPlayerRotate", Rotate);

        EventManager.StopListening("LocalPlayerShoot", Shoot);
        EventManager.StopListening("LocalPlayerReload", Reload);

        EventManager.StopListening("PlayerRespawn", Respawn);
    }

    bool Alive() {
        if(isAlive) return true;
        // Make debugging a lil easier.
        Debug.Log("Can't do anything. The player, he's dead, Jim...");
        return false;
    }

    void Move() {
        if(!Alive()) return;
        Vector3 direction = Vector3.forward * Input.GetAxis("Vertical") * movementSpeed;
        transform.Translate(direction);
    }

    void Rotate() {
        if(!Alive()) return;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, rotation, 0);
    }

    void Respawn() {
        gameManager.Respawn(this);
    }
    
    void Shoot() {
        if(!Alive()) return;
        weaponController.Shoot(weapon, transform.forward);
    }

    void Reload() {
        if(!Alive()) return;
        weaponController.Reload(weapon);
    }

    public void Respawn(Vector3 position) {
        transform.position = position;
        isAlive = true;
        // Make sure player can defend themselves after a respawn.
        weapon = gameManager.GetDefaultWeapon();
    }
}


[System.Serializable]
public class HealthProperties : System.Object {
    public float maxHealth;
    public float regenerationRate;
}

[System.Serializable]
public class PlayerState : System.Object {
    public Weapon weapon = new Weapon();

    public float health = 100.0f;
    public int kills = 0;
    public int deaths = 0;
}