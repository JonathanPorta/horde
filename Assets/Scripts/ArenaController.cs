using UnityEngine;
using System.Collections;

public class ArenaController : MonoBehaviour {

    private GameObject[] playerSpawnPoints;
    private GameObject[] enemySpawnPoints;

    void Awake() {
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");

        Debug.Log("Found " + playerSpawnPoints.Length + " player spawn points.");
        Debug.Log("Found " + enemySpawnPoints.Length + " enemy spawn points.");

        if(playerSpawnPoints.Length == 0 || enemySpawnPoints.Length == 0) {
            Debug.LogError("This arena is missing either enemy or player spawnpoints. Fiiiiix it!");
        }

    }

    public Vector3 GetRandomPlayerSpawnPoint () {
        return playerSpawnPoints[Random.Range(0, playerSpawnPoints.Length)].transform.position;
    }

	// Find the enemy spawn point closet to the given position.
	public Vector3 GetNearestEnemySpawnPoint (Vector3 position) {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        // Iterate through them and find the closest one
        foreach(GameObject sp in enemySpawnPoints) {
            Vector3 diff = sp.transform.position - position;
            float d = diff.sqrMagnitude;
            if(d < distance) {
                closest = sp.transform.position;
                distance = d;
            }
        }
        return closest;
    }
}
