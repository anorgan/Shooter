using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
	public float tumble;
	public float speed;
	public GameObject explosion;
	public int life;
	public int strength;
	public int score;

	protected GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		tumble *= Random.Range(0.8f, 1.2f);
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
		rigidbody.velocity = transform.forward * speed;	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary") {
			return;
		}

		// Maybe do next 2 lines only if game object is damagable (how to test?)

		// Deal damage
		Hit(other.gameObject);

		// Now, I receive damage
		other.gameObject.SendMessageUpwards("Hit", gameObject);
	}

	public void Damage(int strength)
	{
		life = life - strength;
		if (life <= 0) {
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);

			gameController.AddScore(score);
		}
	}

	public void Hit(GameObject other)
	{
		other.SendMessageUpwards("Damage", strength);
	}
}
