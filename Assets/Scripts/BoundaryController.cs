using UnityEngine;
using System.Collections;

public class BoundaryController : MonoBehaviour
{
	void OnTriggerExit(Collider other)
	{
 		Destroy(other.gameObject);
	}
}
