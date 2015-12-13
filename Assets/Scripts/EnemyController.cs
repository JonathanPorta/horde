using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent nav;
    private GameObject target;

    void Awake() {
        nav = GetComponent<NavMeshAgent>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        target = FindClosestTarget();

        if(target != null) {
            nav.SetDestination(target.transform.position);
            transform.LookAt(target.transform.position);
        }
        else {
            Debug.Log("Unable to find target.");
        }
	}

    private GameObject FindClosestTarget() {
        Vector3 position = transform.position;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject t in targets) {
            Vector3 diff = t.transform.position - position;
            float d = diff.sqrMagnitude;
            if(d < distance) {
                closest = t;
                distance = d;
            }
        }
        return closest;
    }
}
