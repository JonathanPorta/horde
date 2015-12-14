using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

  private NavMeshAgent nav;
  private GameObject target;
  private EnemyState _state;

  public EnemyState state {
    get {
      if(_state == null)
        _state = new EnemyState();
      return _state;
    }
    set { _state = value; }
  }

  void Awake() {
    nav = GetComponent<NavMeshAgent>();
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

  public void Hit(Weapon weapon) {
    state.health = state.health - weapon.damage;
  }
}

[System.Serializable]
public class EnemyState : System.Object {
  public Weapon weapon = new Weapon();
  public float health = 100.0f;
}