using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnceSolved : MonoBehaviour {

	private Enigm e;
	public string sceneToLoad ;

	void Start(){
		e=this.GetComponent<Enigm> ();
	}

	// Update is called once per frame
	void Update () {
		if(e.solve)
			SceneManager.LoadScene (sceneToLoad);
	}
}
