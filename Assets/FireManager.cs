using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour
{
	public GameObject p;
	public GameObject[] fireObj; // ここに花火を入れる.
	public Vector3[][] v;

	// Use this for initialization
	void Start ()
	{
		// 花火の形を保持.
		int n = fireObj.GetLength (0);
		v = new Vector3[n][];
		for (int i = 0; i < n; i++) {
			v [i] = new Vector3[fireObj [i].transform.childCount - 1];
			int j = 0;
			foreach (Transform t in fireObj[i].transform) {
				Vector3 _v = t.localPosition;
				if (_v != Vector3.zero) {
					v [i][j] = _v;
					j++;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public Vector3[] getV(int n){
		return v[n];
	}
}
