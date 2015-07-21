using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A different state will be stored per guard
// at the end of each turn (2mins) the functions related will activate
public enum MoveType {
	MoveToPosition,
	FollowPatient,
	MedicatePatient,	//will open inventory to select a medicine type then will stop patient from moving
	RestrainPatient,	//will open inventory to select straitjacket type then will trigger animation
	SendToTreatment,	//send to treatment room, trigger animation, enter treatment state
	SendToCafeteria		// send to cafeteria ... rest is unknown
};

public class PlayerMove {
	public MoveType MyMoveType;
	public GameObject Gaurd;
	public GameObject Doctor;
	public GameObject Patient;
	public Vector3 MoveToPosition;

	public PlayerMove(GameObject GaurdObject, Vector3 NewPosition_) {
		Gaurd = GaurdObject;
		MoveToPosition = NewPosition_;
		MyMoveType = MoveType.MoveToPosition;
	}
	public PlayerMove(MoveType NewMoveType, GameObject GaurdObject, GameObject PatientToMedicate_) {
		Gaurd = GaurdObject;
		Patient = PatientToMedicate_;
		MyMoveType = NewMoveType;
	}
};


public class TurnManager : MonoBehaviour {
	public PatientManager MyPatientManager;
	public List<PlayerMove> PlayerMoves = new List<PlayerMove>();
	public List<Room> MyRooms = new List<Room>();
	
	public Transform SelectedGuard;
	public Transform SelectedPatient;
	public bool guard1Selected = false;
	public bool IsPatientSelected = false;
	RaycastHit hitInfo;
	public GameObject LastSelectPatient;
	public bool HasBeenOrdered = false;


	public void Awake() 
	{
		// get all Rooms in level and Add to MyRoomsList
	}

	void Update () 
	{
		InitialGuardSelect ();
		CheckMouseHit ();
	}

	public void InitialGuardSelect() 
	{
		if (Input.GetMouseButtonDown (0))
		{
			Debug.Log ("Mouse is down");
			
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			
			if (hit) 
			{
				Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Guard") 
				{
					Debug.Log ("Guard selected");
					SelectedGuard = hitInfo.transform;
					SelectedGuard.GetComponent<Guard>().isSelected = true;
					UnSelectPatient ();
				}
				else if (hitInfo.transform.gameObject.tag == "Tile") 
				{
					UnSelectPatient ();
				} else 
				{
					Debug.Log ("guard not selected");
				}
			} else 
				
				Debug.Log ("No hit");
		}
		 
	}

	public void CheckMouseHit() 
	{
		hitInfo = new RaycastHit();
		bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
		if (hit) 
		{
			
			// Select patient code
			if (hitInfo.transform.gameObject.tag == "Patient")
			{
				if (Input.GetMouseButtonDown(0)) 
				{
					Debug.Log ("Mouse Hit Patient!");
					SelectedPatient = hitInfo.transform;
					if (guard1Selected) 
					{
						Patient MyPatient = hitInfo.transform.gameObject.GetComponent<Patient>();
						MyPatient.Select(false);
					} 
					else 
					{
						// select profile for patient
						Patient MyPatient = hitInfo.transform.gameObject.GetComponent<Patient>();
						MyPatient.Select(true);
					}
				}
			} 
			
			// move gaurd code
			else if(hitInfo.transform.gameObject.tag == "Tile") 
			{
				if (Input.GetMouseButtonDown(1)) 
				{
					if (SelectedGuard != null) 
					{
						Debug.Log ("Mouse Hit Patient!");
						AddMoveToTurn(SelectedGuard.gameObject, hitInfo.point);
						SelectedGuard.GetComponent<Guard>().HasBeenOrdered = true;
					}
				}
			}
			
		}
		else 
		{
			Debug.Log("No hit");
			
		}
	}

	public void UnSelectPatient () 
	{
		IsPatientSelected = false;
		SelectedPatient = null;	// set targetted patient to nothing
	}


	public void EndTurn() 
	{
		CloseGui ();
		for (int i = PlayerMoves.Count-1; i >= 0; i--) 
		{
			PlayerMoves[i].Gaurd.GetComponent<Guard>().HasBeenOrdered = false;
			if (PlayerMoves[i].MyMoveType == MoveType.MoveToPosition) 
			{
				PlayerMoves[i].Gaurd.GetComponent<Guard>().MoveToLocation(PlayerMoves[i].MoveToPosition);
			} 
			else if (PlayerMoves[i].MyMoveType == MoveType.FollowPatient) 
			{
				PlayerMoves[i].Gaurd.GetComponent<Guard>().FollowPatient(PlayerMoves[i].Patient);
			}
			
			PlayerMoves[i].Gaurd.GetComponent<Guard>().HasBeenOrdered = false;
			PlayerMoves.RemoveAt (i);
		}
		DoThing ();	// does the things
	}
	public void DoThing() {
		for (int i = 0; i < MyPatientManager.MyPatients.Count; i++) {
			MyPatientManager.MyPatients[i].MyStats.IncreaseTurn();
		}
	}
	// grabs all the 'patients' in the scene and sets their movement state to true
	public void CloseGui() {
		// now start up all the patients movement again! incase any where disabled
		Patient[] MyPatients = GameObject.FindObjectsOfType(typeof(Patient)) as Patient[];
		for (int i = 0; i < MyPatients.Length; i++) {
			MyPatients[i].CloseGui(false);
		}
	}
	public void AddPlayerMove(PlayerMove NewMove) {

		bool IsGuardInList = false;
		/// check if gaurd is already in the list
		for (int i = 0; i<PlayerMoves.Count; i++) 
		{
			if(PlayerMoves[i].Gaurd == NewMove.Gaurd) {
				IsGuardInList = true;
				i = PlayerMoves.Count;
			}
		}
		if (!IsGuardInList) 
		{
			PlayerMoves.Add (NewMove);
		}

	}
	public void AddMoveToTurn(GameObject GaurdToMove, Vector3 MoveToPosition) 
	{
		PlayerMove NewMove = new PlayerMove(GaurdToMove, MoveToPosition);
		AddPlayerMove (NewMove);
	}

	public void AddTurnMedicate(GameObject DoctorObject,GameObject PatientObject) 
	{
		PlayerMove NewMove = new PlayerMove(MoveType.MedicatePatient, DoctorObject, PatientObject);
		AddPlayerMove (NewMove);
	}

	public void AddTurnMoveToPatient(GameObject GaurdObject, GameObject PatientObject)
	{
		PlayerMove NewMove = new PlayerMove (MoveType.FollowPatient, GaurdObject, PatientObject);
		AddPlayerMove (NewMove);
	}
}
