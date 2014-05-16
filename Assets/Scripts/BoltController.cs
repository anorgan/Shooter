using UnityEngine;
using System.Collections;

public class BoltController : MonoBehaviour
{
	public int life = 1;
	public int strength = 1;
	public float speed;

	void Start ()
	{
		rigidbody.velocity = transform.forward * speed;	
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
		}
	}
}
