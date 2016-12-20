using UnityEngine;
using System.Collections;

[System.Serializable]
public class Batas {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float kecepatan;
	public float rotasi;
	public Batas boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	private Quaternion calibrationQuaternion;

	void Start () {
		KalibrasiAselerometer ();
	}
	
	void Update (){
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	//kalibrasi masukkan data dari sensor aselerometer
	void KalibrasiAselerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	//dapatkan data yang sudah terkalibrasi
	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void FixedUpdate(){
		//tahap1: komen kode sebelumnya
		//float gerakHorisontal = Input.GetAxis ("Horizontal");
		//float gerakVertikal = Input.GetAxis ("Vertical");
		//Vector3 gerakan = new Vector3(gerakHorisontal, 0.0f, gerakVertikal);

		//tahap2: tambahkan data dari sensor
		Vector3 dataSensor = Input.acceleration;
		Vector3 aselerasi = FixAcceleration (dataSensor);
		Vector3 gerakan = new Vector3 (aselerasi.x, 0.0f, aselerasi.y);

		GetComponent<Rigidbody>().velocity = gerakan * kecepatan;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, 
		                                                       GetComponent<Rigidbody>().velocity.x * -rotasi);
	}
}