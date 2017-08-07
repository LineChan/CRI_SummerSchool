using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/*
 * Grammar for soluce :
 * int >0 take value onto stack
 * -1 logical Not the next value 
 * -2 And with next value
 * -3 Or with next value
 * 
 * The Stack start with false value into
 * 
 */

public class FeedBackLight : MonoBehaviour {

	public bool status;
	public List<int> soluce;
	public string animatorTriggerName;

	private Enigm enigm;


	private int cursor;
	private bool stackState;
	private int maxVal;


	void Start(){
		enigm = this.GetComponentInParent<Enigm> ();
		maxVal = enigm.nbLevers;
	}

	public void CheckSoluce(){
		cursor = 0;
		stackState = false;
		bool oldStatus = status;
		int op = 0;
		bool NotVal = false;
		bool tmp = false;

		while (cursor < soluce.Count) {
			if (soluce [cursor] == -1) { //Si c'est un not
				NotVal = true;

			}else if (soluce[cursor]<-1 ){ //Si c'est un op
				op = soluce[cursor];
			
			} else{
				if (op == -2) { //And
					tmp = enigm.levers [soluce [cursor]].status;
					if (NotVal)
						tmp = !tmp;
					stackState = stackState && tmp;
				} else if (op == -3) { //Or
					tmp = enigm.levers [soluce [cursor]].status;
					if (NotVal)
						tmp = !tmp;
					stackState = stackState && tmp;
					
				} else if (op == 0) { // Stack init
					tmp = enigm.levers [soluce [cursor]].status;
					if (NotVal)
						tmp = !tmp;
					stackState = tmp;
				} else {
					Debug.LogError ("Operateur Iconnu");
				}
			}

			cursor++;
		}

		status = stackState;

		if (oldStatus != status && animatorTriggerName != null) {
			this.GetComponent<Animator> ().SetBool (animatorTriggerName, status);
		}
	}

}