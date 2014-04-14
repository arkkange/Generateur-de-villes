using UnityEngine;
using System.Collections;

public class QuakeYawScript : MonoBehaviour {

    [SerializeField]
    private float _rotationSpeed = 90f;
    public float RotationSpeed
    {
        get { return _rotationSpeed; }
        set { _rotationSpeed = value; }
    }

    [SerializeField]
    private Transform _myTransform;
    public Transform MyTransform
    {
        get { return _myTransform; }
        set { _myTransform = value; }
    }
	
	// Update is called once per frame
	void Update () {
        MyTransform.Rotate(Vector3.up 
            * Input.GetAxis("Mouse X")
            * RotationSpeed
            * Time.deltaTime
            );
	}
}
