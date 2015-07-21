using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// generate stats
// description of their history
// generate names
public class PatientGenerator : MonoBehaviour {
	public List<string> PossibleNames = new List<string>();
	public List<string> Descriptions = new List<string>();
	public List<PatientStats> MyStatsPresets = new List<PatientStats>();

	// Use this for initialization
	void Start () {
		DefaultNames ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DefaultNames() {
		PossibleNames.Add ("Marz");
		PossibleNames.Add ("James");
		PossibleNames.Add ("Coreey");
		PossibleNames.Add ("Angel");
		PossibleNames.Add ("Amy");
		PossibleNames.Add ("Sally");
	}
	public void UpdatePatientName(GameObject MyPatient) {
		int NameIndex = Random.Range (0, PossibleNames.Count);
		gameObject.name = PossibleNames [NameIndex];
	}
	public void UpdatePatientWithStats(GameObject MyPatient, int StatIndex) {
		StatIndex = Mathf.Clamp (StatIndex, 0, MyStatsPresets.Count);
		MyPatient.GetComponent<Patient> ().MyStats = MyStatsPresets [StatIndex];
	}
}
