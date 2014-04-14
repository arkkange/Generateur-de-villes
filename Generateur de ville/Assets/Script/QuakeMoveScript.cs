using UnityEngine;
using System.Collections;

public class QuakeMoveScript : MonoBehaviour {
    [SerializeField]
    private float _walkSpeed = 5;
    public float WalkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }

    [SerializeField]
    private Transform _myTransform;
    public Transform MyTransform
    {
        get { return _myTransform; }
        set { _myTransform = value; }
    }

    private Vector3 _direction;

	// Update is called once per frame
	void Update () {

        //_direction = Vector3.forward * Input.GetAxis("Vertical") +Vector3.right * Input.GetAxis("Horizontal");
        //MyTransform.position +=  MyTransform.rotation * _direction.normalized *  WalkSpeed * Time.deltaTime;

		if(Input.GetKey(KeyCode.Z)){
			MyTransform.position += MyTransform.rotation * Vector3.forward * WalkSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q)){
			MyTransform.position += MyTransform.rotation * Vector3.left * WalkSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			MyTransform.position += MyTransform.rotation * Vector3.right * WalkSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			MyTransform.position += MyTransform.rotation * Vector3.back * WalkSpeed * Time.deltaTime;
		}

		if( Input.GetKey(KeyCode.Space) ){
				MyTransform.position += Vector3.up * WalkSpeed * Time.deltaTime;

		}
		if( Input.GetKey(KeyCode.C) ){
			if(MyTransform.position.y > 0.30){
				MyTransform.position += Vector3.down * WalkSpeed * Time.deltaTime;
			}
		}

		//changement de vitesse
		if( Input.GetKeyDown(KeyCode.R)){
			if(_walkSpeed == 5){
				_walkSpeed = 1;
			}
			else{
				_walkSpeed = 5;
			}
		}
	}
}
