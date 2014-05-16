using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public int life;
	public int strength;
	public float speed;
	public float tilt;
	public GameObject explosion;
	public Boundary boundary;

	public GameObject bolt;
	public Transform forwarGun;
	public float fireRate;

	private float nextFire;
	
	protected GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(bolt, forwarGun.position, forwarGun.rotation);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		rigidbody.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;

		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f,  rigidbody.velocity.x * -tilt)	;
	}

	public void Hit(GameObject other)
	{
		other.SendMessageUpwards("Damage", strength);
	}

	public void Damage(int strength)
	{
		life = life - strength;
		if (life <= 0) {
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);

			//gameController.Restart();
		}
	}
}
