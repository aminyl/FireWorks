using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
	public GameObject fireWorks;
	FireManager fm;
	float t;
	
	// Use this for initialization
	void Start ()
	{
		fm = GetComponent<FireManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			t = -1;
			launch ();
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			t = 0;
		}

		if(t >= 0)
			t += Time.deltaTime;

		if (t > 0.08f) {
			launch ();
			t = 0;
		}
	}

	void launch(){
		Vector3 r = new Vector3(Random.Range(-5, 5), Random.Range(-5, 0), Random.Range(0, 1f));
		GameObject obj = Instantiate (fireWorks, transform.position + r, transform.rotation) as GameObject;
		obj.GetComponent<FireWorks> ().fm = fm;
		obj.transform.parent = transform.parent = this.transform;
	}
}
