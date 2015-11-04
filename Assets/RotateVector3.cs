using UnityEngine;
using System.Collections;

public class RotateVector3 : MonoBehaviour
{
	float alpha, beta, gamma;
	float cosa, cosb, cosg, sina, sinb, sing;
	float a11, a12, a13, a21, a22, a23, a31, a32, a33;

	// Use this for initialization
	void Start ()
	{
	
	}

	public void setEulerDeg (float x, float y, float z)
	{
		calcSinCos (x, y, z);
		a11 = cosa * cosb * cosg - sina * sing;
		a12 = -cosa * cosb * sing - sina * cosg;
		a13 = cosa * sinb;
		a21 = sina * cosb * cosg + cosa * sing;
		a22 = -sina * cosb * sing + cosa * cosg;
		a23 = sina * sinb;
		a31 = -sinb * cosg;
		a32 = sinb * sing;
		a33 = cosb;
	}

	public void setRoleDeg (float x, float y, float z)
	{
		calcSinCos (x, y, z);
		a11 = cosa * cosb;
		a12 = cosa * sinb * sing - sina * cosg;
		a13 = cosa * sinb * cosg + sina * sing;
		a21 = sina * cosb;
		a22 = sina * sinb * sing + cosa * cosg;
		a23 = sina * sinb * cosg - cosa * sing;
		a31 = -sinb;
		a32 = cosb * sing;
		a33 = cosb * cosg;
	}

	public Vector3 rotateEuler (Vector3 v)
	{
		Vector3 res;
		res.x = a11 * v.x + a12 * v.y + a13 * v.z;
		res.y = a21 * v.x + a22 * v.y + a23 * v.z;
		res.z = a31 * v.x + a32 * v.y + a33 * v.z;
		return res;
	}

	public Vector3 rotateRole (Vector3 v)
	{
		Vector3 res;
		res.x = a11 * v.x + a12 * v.y + a13 * v.z;
		res.y = a21 * v.x + a22 * v.y + a23 * v.z;
		res.z = a31 * v.x + a32 * v.y + a33 * v.z;
		return res;
	}

	void calcSinCos (float x, float y, float z)
	{
		alpha = x;
		beta = y;
		gamma = z;
		cosa = Mathf.Cos (Mathf.Deg2Rad * alpha);
		cosb = Mathf.Cos (Mathf.Deg2Rad * beta);
		cosg = Mathf.Cos (Mathf.Deg2Rad * gamma);
		sina = Mathf.Sin (Mathf.Deg2Rad * alpha);
		sinb = Mathf.Sin (Mathf.Deg2Rad * beta);
		sing = Mathf.Sin (Mathf.Deg2Rad * gamma);
	}

	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
