using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// to do:
// patient to move to room

public class Patient : MonoBehaviour {
	public TurnManager MyTurnManager;
	public PatientStats MyStats = new PatientStats();
	public float OriginalSpeed = 4f;
	public float MaxMovement = 4f;
	public Vector3 Direction;
	private float Dist;
	private float OrigX;
	private float Speed = 4f;
	public bool movingRight = false;
	private bool IsSelected = false;
	public List<string> PossibleNames = new List<string>();
	public bool IsWeirdingOut = false;
	public bool IsMovingToPosition = false;
	public Vector3 MoveToPosition;
	public NavMeshAgent agent;


	// Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		OrigX = transform.position.z;
		MyTurnManager = GetManager.GetTurnManager ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGui ();
		UpdateMoveTo ();
		if (Input.GetKeyDown (KeyCode.Z)) {
			//MoveToNewPosition(transform.position + new Vector3(Random.Range(-5,5),0,Random.Range(-5,5)));
			MoveToRoom (0);
		}
	}

	public void MoveToRoom(int RoomIndex) {
		MoveToNewPosition (MyTurnManager.MyRooms [RoomIndex].transform.position);
	}

	public void UpdateMoveTo() {
		if (IsMovingToPosition) {
			agent.SetDestination (MoveToPosition);
			// if tpatient is near position, it will stop moving
			if (Vector3.Distance(transform.position, MoveToPosition) < 1f) {
				IsMovingToPosition = false;
			}
		}
	}

	public void MoveToNewPosition(Vector3 NewPosition) {
				MoveToPosition = NewPosition;
		IsMovingToPosition = true;
	}

	// moves the patient back and forth between 2 positions
	public void GoCrazy() {
			if (OrigX - transform.position.z > MaxMovement) {
				Speed = OriginalSpeed;
				movingRight = true;
			} else if (OrigX - transform.position.z < -MaxMovement) {
				Speed = -OriginalSpeed;
				movingRight = false;
			}
			transform.Translate (0, 0, Speed * Time.deltaTime);
	}

	public void Select(bool IsProfile) 
	{
		GameObject GuardChild;
		GameObject ProfileChild;
		GuardChild = transform.FindChild ("Canvas").gameObject.transform.FindChild ("GuardActiveButtons").gameObject;
		ProfileChild = transform.FindChild ("Canvas").gameObject.transform.FindChild ("ProfileButton").gameObject;

		if (!IsProfile) 
		{
			GuardChild.SetActive (true);
			ProfileChild.SetActive (false);
		} 
		else 
		{
			ProfileChild.SetActive (IsSelected);
		}
		IsSelected = true;
	}

	// sets the transform variables of the 3d canvas to always face the main camera position
	public void UpdateGui() {
		GameObject Child = transform.GetChild (0).gameObject;
		if (Child == null) {
			Debug.LogError ("Patient has no 3d canvas attached to it");
			return;
		}
		Child.transform.LookAt(Camera.main.transform.position,
		                       Vector3.up);
		Child.transform.Rotate (new Vector3 (180, 0, 180));
		Child.SetActive (IsSelected);
	}
	public void CloseGui(bool IsGui) {
		IsSelected = IsGui;
		GameObject GuardChild = transform.FindChild ("Canvas").gameObject.transform.FindChild ("GuardActiveButtons").gameObject;
		GameObject ProfileChild = transform.FindChild ("Canvas").gameObject.transform.FindChild ("ProfileButton").gameObject;
		GuardChild.SetActive (false);
		ProfileChild.SetActive (false);

	}
	public void AlterMovement(bool IsMovement) {
		if (!IsMovement)
			Speed = 0;
		else
			Speed = OriginalSpeed;
		Rigidbody MyRigid = gameObject.GetComponent<Rigidbody> ();
		if (MyRigid != null)
			MyRigid.isKinematic = IsMovement;
	}
}
