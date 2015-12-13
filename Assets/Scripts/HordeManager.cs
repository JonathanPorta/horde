using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HordeManager : MonoBehaviour {

    public float spawnTimeout = 2.0f;
    public GameObject enemyPrefab;

    private GameManager gameManager;
    private List<GameObject> enemies;

    private bool canSpawn = false;
    private float lastSpawn;

    //private health
    

    void Awake() {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if(gm != null) {
            gameManager = gm.GetComponent<GameManager>();
        }
        else {
            Debug.LogError("Unable to find GameObject with tag GameManager in this scene. Please add a manager, jeez.");
        }
        enemies = new List<GameObject>();
    }

    void Update() {
        if(Time.time - lastSpawn >= spawnTimeout) {
            Spawn();
        }
    }

    private void Spawn() {
        if(!canSpawn) return;
 
        lastSpawn = Time.time;
        Vector3 spawnPoint = gameManager.GetEnemySpawnPoint();
        GameObject e = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity) as GameObject;
        enemies.Add(e);
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
        canSpawn = true;
    }

    void EndRound() {
        canSpawn = false;
    }

    void NextRound() {
        
    }

}
