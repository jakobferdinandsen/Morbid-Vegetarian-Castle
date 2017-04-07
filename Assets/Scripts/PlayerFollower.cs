using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent navMesh;

	// Use this for initialization
	void Start () {
		navMesh = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		if (player) {
			navMesh.SetDestination(player.position);	
		}
	}
}
