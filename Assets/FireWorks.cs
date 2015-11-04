using UnityEngine;
using System.Collections;

public class FireWorks : MonoBehaviour {
	public FireManager fm;
	GameObject p;
	GameObject[] pf;
	int fireType;
	Vector3 rotation;
	float launchSpeed;
	Vector3[] v;
	int state = 0;
	TimerT t;
	int colorType;
	
	// Update is called once per frame
	void Update ()
	{
		if (state == 0) {
			setLaunch();
			startLaunch ();
		} else if (state == 1) {
			rise ();
			fadeStart ();
		} else if (state == 2) {
			rise ();
			endRise ();
		} else if (state == 3) {
			fireStart ();
		} else if (state == 4) {
			fireEnd ();
		} else if (state == 5) {
			closing ();
		}
	}
	
	public void setLaunch(){
		fireType = Random.Range (0, 2);
		rotation = new Vector3(Random.Range(-20, 20), Random.Range(-10, 10), Random.Range(0, 360));
		//rotation = Vector3.zero;
		launchSpeed = 8;
		colorType = Random.Range (0,7);
	}
	
	public void startLaunch ()
	{
		p = Instantiate (fm.p, transform.position, transform.rotation) as GameObject;
		p.transform.parent = this.transform;
		p.GetComponent<Rigidbody> ().velocity = Vector3.up * launchSpeed;
		p.GetComponent<Rigidbody> ().useGravity = true;
		setV (fireType, rotation);
		t = gameObject.AddComponent<TimerT>();
		state = 1;
	}
	
	void setV (int n, Vector3 r)
	{
		v = fm.getV (n);
		RotateVector3 rv3 = gameObject.AddComponent<RotateVector3> ();
		rv3.setEulerDeg (r.x, r.y, r.z);
		for (int i = 0; i < v.Length; i++)
			v [i] = rv3.rotateEuler (v [i] + Vector3.forward * 0);
		rv3.setEulerDeg (-326, 180 - 138 - 90, -3); // カメラの方向を向く.
		for (int i = 0; i < v.Length; i++)
			v [i] = rv3.rotateEuler (v [i]);
	}
	
	void rise ()
	{
		p.GetComponent<Renderer> ().material.SetColor ("_TintColor", getColor());
		Vector3 _v = p.GetComponent<Rigidbody> ().velocity + Vector3.up * Time.deltaTime * 8;
		_v.x = (Mathf.PingPong (Time.time * 10, 1.0f) - 0.5f) / (Time.time * 0.1f + 1);
		p.GetComponent<Rigidbody> ().velocity = _v;
	}
	
	void fadeStart ()
	{
		if (p.GetComponent<Rigidbody> ().velocity.y < 3.5f)
			if (!scaleDown(p))
				state = 2;
	}
	
	void endRise ()
	{
		if (p.GetComponent<Rigidbody> ().velocity.y <= 1.5f) {
			pf = new GameObject[v.Length];
			for (int i = 0; i < v.Length; i++) {
				pf [i] = Instantiate (fm.p, p.transform.position, transform.rotation) as GameObject;
				pf [i].transform.parent = this.transform;
				pf [i].GetComponent<Rigidbody> ().velocity = v [i];
				pf [i].GetComponent<Rigidbody> ().drag = 2;
			}
			Destroy (p.gameObject);
			state = 3;
		}
	}
	
	void fireStart ()
	{
		t.timerStart ();
		state = 4;
	}
	
	void fireEnd ()
	{
		for (int i = 0; i < v.Length; i++) {
			pf [i].GetComponent<Rigidbody> ().velocity += Vector3.down * Time.deltaTime * 6;
			pf [i].GetComponent<Renderer> ().material.SetColor ("_TintColor", getColor());
		}
		if (t.timerCheck (0.5f)) {
			if (!scaleDown (pf)) {
				state = 5;
				t.timerStart ();
			}
		}
	}

	Color getColor(){
		if(colorType == 0)
			return new Color (Random.value, Random.value, 0, 1);
		if(colorType == 1)
			return new Color (Random.value, 0, Random.value, 1);
		if(colorType == 2)
			return new Color (0, Random.value, Random.value, 1);
		if(colorType == 3)
			return new Color (0, 0, Random.value, 1);
		if(colorType == 4)
			return new Color (0, Random.value, 0, 1);
		if(colorType == 5)
			return new Color (Random.value, 0, 0, 1);
		return new Color (Random.value, Random.value, Random.value, 1);
	}
	
	bool scaleDown (GameObject[] p)
	{
		if (p[0].transform.localScale.x > 0) {
			for (int i = 0; i < v.Length; i++)
				scaleCh(p[i]);
			return true;
		}
		return false;
	}
	
	bool scaleDown (GameObject p)
	{
		if (p.transform.localScale.x > 0) {
			scaleCh(p);
			return true;
		}
		return false;
	}
	
	void scaleCh (GameObject p)
	{
		p.transform.localScale -= Vector3.one * Time.deltaTime * 0.2f;
	}
	
	void closing ()
	{
		if (t.timerCheck (1.0f))
			Destroy (this.gameObject);
	}

}
