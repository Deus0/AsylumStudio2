using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PatientGuiManager : MonoBehaviour {
	public Patient MyPatient;
	public BarGui MyBar1;
	public BarGui MyBar2;
	public BarGui MyBar3;
	public BarGui MyBar4;
	public BarGui MyBar5;
	public GameObject NyNameLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//	public int Aggression, Hallucinations, PhysicalHealth, Hunger, Fatigue;
		MyBar1.Stat = MyPatient.MyStats.PhysicalHealth;
		MyBar2.Stat = MyPatient.MyStats.Hunger;
		MyBar3.Stat = MyPatient.MyStats.Fatigue;
		MyBar4.Stat = MyPatient.MyStats.Hallucinations;
		MyBar5.Stat = MyPatient.MyStats.Aggression;
		NyNameLabel.GetComponent<Text>().text = MyPatient.name;
	}
}
