using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Anything that isn't Guard related activities don't belong here in Guardville

public class Guard : MonoBehaviour {

	// scene references
	public GuardStats MyStats;
	public TurnManager MyTurnManager;
	public Transform followPatient;
	public NavMeshAgent agent;
	public bool FollowingPatient = false;
	public bool isSelected = false;
	public GameObject LastSelectPatient;
	public bool HasBeenOrdered = false;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		MyTurnManager = GetManager.GetTurnManager ();
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateSelectionEffects ();

		if (FollowingPatient) {
			agent.SetDestination (followPatient.position);
		}
	}

	public void UpdateSelectionEffects() {
		// update selected mesh
		if (HasBeenOrdered) {
			isSelected = false;
			gameObject.transform.GetChild (1).gameObject.SetActive (HasBeenOrdered);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		} else {
			gameObject.transform.GetChild(0).gameObject.SetActive(isSelected);
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
		}
	}



	// All the guard commands
	
	public void MoveToLocation(Vector3 NewLocation) {
		//Debug.Log ("Mouse Hit Tile!");
		agent.SetDestination (NewLocation);
		//Debug.DrawLine (hitInfo.point, hitInfo.point + new Vector3 (0, 3, 0), Color.red, 3);
		isSelected = false;
		FollowingPatient = false;
		HasBeenOrdered = true;
	}
	
	public void MoveToPatient() 
	{
		MyTurnManager.AddTurnMoveToPatient (gameObject, LastSelectPatient);
		HasBeenOrdered = true;
	}
	
	public void FollowPatient(GameObject PatientTransform) {
		followPatient = PatientTransform.transform; 
		FollowingPatient = true;
		isSelected = false;
		HasBeenOrdered = true;
	}
}
