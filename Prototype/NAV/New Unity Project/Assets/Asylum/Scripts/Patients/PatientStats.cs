using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TreatmentState {
	SingleTherapy,
	GroupTherapy,
	Lobotomy,
	Trephination,
	InsulinComaTherapy,
	Bloodletting,
	ShockTherapy,
	IceBaths,
	Restraints,
	Solitary,
	FeverTherapy,
	SpinningTherapy
};

public class PatientStats {
	public TreatmentState MyTreatmentState;
	public int Aggression, 
				Hallucinations, 
				PhysicalHealth, 
				Hunger, 
				Fatigue;
	public string Description;

	public PatientStats() {
		Aggression = 5;
		Hallucinations = 5;
		PhysicalHealth = 5;
		Hunger = 5;
		Fatigue = 5;
	}
	public void IncreaseTurn() {
		switch (MyTreatmentState) 
		{
		case TreatmentState.Bloodletting:

			Aggression -= 1;
			Hallucinations += 1;
			PhysicalHealth -= 1;

			break;
		case TreatmentState.FeverTherapy:

			Aggression -=1;
			Hallucinations += 1;
			PhysicalHealth -= 1;
			Fatigue += 1;

			break;
		case TreatmentState.GroupTherapy:

			Aggression += 1;
			Hunger += 1;
			Fatigue += 1;

			break;
		case TreatmentState.IceBaths:

			Aggression -= 1;
			Hallucinations += 1;
			Hunger += 1;
			Fatigue += 1;

			break;
		case TreatmentState.InsulinComaTherapy:

			Aggression -= 1;
			Hallucinations += 1;
			PhysicalHealth -= 1;
			Fatigue -= 1;

			break;
		case TreatmentState.Lobotomy:

			Aggression -= 1;
			Hallucinations -= 1;
			PhysicalHealth -= 1;

			break;
		case TreatmentState.Restraints:

			Aggression -= 1;
			Hallucinations -= 1;
			Fatigue -= 1;

			break;
		case TreatmentState.ShockTherapy:

			Aggression += 1;
			Hallucinations -= 1;
			Hunger -= 1;

			break;
		case TreatmentState.SingleTherapy:

			Aggression -= 1;
			Hunger += 1;
			Fatigue += 1;

			break;
		case TreatmentState.Solitary:

			Aggression -= 1;
			Hallucinations += 1;
			Fatigue -= 1;

			break;
		case TreatmentState.SpinningTherapy:

			Aggression -= 1;
			Hallucinations -= 1;
			Hunger += 1;
			Fatigue += 1;

			break;
		case TreatmentState.Trephination:

			Aggression -= 1;
			Hallucinations += 1;
			PhysicalHealth -= 1;

			break;

		}
	}
};