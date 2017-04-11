using UnityEngine;
using UnityEngine.UI;

public class PlayerController_SP : MonoBehaviour
{
	public bool canMove = true;
	public bool moving = false;
	public float speed;
	//public float brakes;
	public float steering;
	private float timeToStart = 2f;
	void Start ()
	{


	}
	void Update()
	{

		if (canMove) {
			var steer = Input.GetAxis ("Horizontal") * Time.deltaTime * steering;
			var forward = Input.GetAxis ("Vertical") * Time.deltaTime * speed;
			//var backward = Input.GetAxis ("Vertical") * Time.deltaTime * brakes;
			if (!moving) {
				if (steer != 0 || forward != 0) {
					moving = !moving;
				}
			}
			if (moving) {
				/*if (backward < 0) {
					backward = -backward;
				} else {
					backward = 0;
				}*/
				/*if (forward < 0) {
					forward = 0;
			    }*/
				//transform.Find("Player").gameObject.transform.Rotate (forward, 0, 0/* - backward*/);
				transform.Translate (0,0,forward/100f);
				//transform.Find("Player").gameObject.transform.Translate (steer, 0, 0);
				transform.Translate (steer, 0, 0);
			}
		}
	}
}