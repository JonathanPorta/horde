using UnityEngine;
using System.Collections;
using SimpleJSON;

public class NetworkPlayerManager : MonoBehaviour {

	public string hostname = "localhost";
	public int port = 4000;

	public GameObject prefab;

	private Socket socket;

	void Awake(){
		socket = new Socket(hostname, port);
	}

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		if(socket.HasData()){
			JSONNode data = JSON.Parse(socket.Read());
			Debug.Log("Network Data: " + data);
		}
		JSONArray n = new JSONArray();

		n.Add("test", "test");
		Debug.Log(n.ToString());
	}

	private void OnPlayerJoined(){
	
	}
}
