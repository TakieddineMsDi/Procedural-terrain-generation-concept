using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class GroundGenerator_SP : MonoBehaviour
{
	public int width;
	public int height;
	public int generatedGrounds = 0;
	public string[] groundTypes;
	public int[] heights;
	public Dictionary<GroundController_SP.actions,GameObject> groundParts;
	bool _threadRunning;
	Thread _thread;
	// Use this for initialization
	void Start ()
	{
		//groundParts = Resources.LoadAll ("Prefabs/Grounds/Parts/");
		groundTypes = Enum.GetNames (typeof(GroundController_SP.actions));
		groundParts = new Dictionary<GroundController_SP.actions,GameObject> (groundTypes.Length);
		for (int i = 0; i < groundTypes.Length; i++) {
			groundParts.Add((GroundController_SP.actions)Enum.Parse (typeof(GroundController_SP.actions), groundTypes[i]),Resources.Load<GameObject> ("Mono/Prefabs/Grounds/Parts/"+groundTypes[i]));
		}
		heights = new int[width];
		generateGround ();
		//Choose (new float[]{ 5f, 20f, 25f, 50f });
		/*_thread = new Thread (ThreadedWord);
		_thread.Start ();*/
	}

	void ThreadedWord ()
	{
		_threadRunning = true;
		bool workDone = false;
		while (_threadRunning && !workDone) {
			for (int i = 0; i < 10; i++) {
				generateGround ();
			}
			workDone = true;
		}
		_threadRunning = false;
	}

	float Choose (float[] probs)
	{
		
		float total = 0;

		foreach (float elem in probs) {
			total += elem;
		}
		print ("Total : " + total);
		float randomPoint = UnityEngine.Random.value * total;
		print ("Random Point : " + randomPoint);

		for (int i = 0; i < probs.Length; i++) {
			if (randomPoint < probs [i]) {
				print ("RandomPoint : " + randomPoint + " < Probs[" + i + "] : " + probs [i] + ", Returning i = " + i);
				return i;
			} else {
				randomPoint -= probs [i];
			}
		}
		print ("Returning probs.length - 1 : " + (probs.Length - 1));
		return probs.Length - 1;
	}

	void OnDisable ()
	{
		if (_threadRunning) {
			_threadRunning = false;
			_thread.Join ();
		}

	}

	public void generateGround ()
	{
		int leftPart;
		int rightPart;
		int x;
		bool right = true;
		if ((width) % 2 != 0) {
			leftPart = (width) / 2;
			rightPart = leftPart + 1;
		} else {
			leftPart = rightPart = (width) / 2;
		}
		if (leftPart > rightPart) {
			right = false;
		} else if (leftPart == rightPart) {
			int side = UnityEngine.Random.Range (0, 2);
			if (side == 0) {
				right = true;
			} else {
				right = false;
			}
		}
		for (int i = 0; i < height; i++) {
			if (right) {
				x = (rightPart-1) * 2;
			} else {
				x = (rightPart) * 2;
			}
			for (int j = 0; j < rightPart+leftPart; j++) {
				//groundPart.transform.localScale = new Vector3 ((int)UnityEngine.Random.Range (2, 6),0,(int)UnityEngine.Random.Range (2, 6));
				instatiateGround (x, heights[j],i,j, "GroundPart" + i + j);
				x -= 2;
			}
		}

		generatedGrounds++;
	}

	public void instatiateGround (int x, int z,int i,int j, string name)
	{
		int r = UnityEngine.Random.Range (0, groundTypes.Length);
		string gtype = groundTypes [r];
		if (z == 0 || i == height - 1) {
			gtype = "Normal";
		}
		GameObject groundPart;
		GameObject obstacle = new GameObject();
		bool isObstacle = false;
		groundParts.TryGetValue ((GroundController_SP.actions)Enum.Parse (typeof(GroundController_SP.actions), gtype), out groundPart);
		groundPart.GetComponent<GroundController_SP> ().i = i;
		groundPart.GetComponent<GroundController_SP> ().j = j;
		heights [j] += 2;
		if (gtype.Equals ("Normal")) {
			int ro = UnityEngine.Random.Range (0, 2);
			if (ro == 0) {
				isObstacle = true;
				if (z == 0 || i == height - 1) {
					isObstacle = false;
				}
				if (isObstacle) {
					obstacle = Resources.Load<GameObject> ("Mono/Prefabs/Obstacles/Cube");
				}
			} else {
				isObstacle = false;
			}
		}
		groundPart.GetComponent<GroundController_SP> ().action = (GroundController_SP.actions)Enum.Parse (typeof(GroundController_SP.actions), gtype);
		if (groundPart.GetComponent<GroundController_SP> ().action == GroundController_SP.actions.Bounce) {
			heights [j] += groundPart.GetComponent<GroundController_SP> ().bounce;
		} else if (groundPart.GetComponent<GroundController_SP> ().action == GroundController_SP.actions.Move) {
			heights [j] += groundPart.GetComponent<GroundController_SP> ().move;
		}
		Instantiate (groundPart, new Vector3 (x, 0, z), new Quaternion (), transform);
		if (isObstacle) {
			Instantiate (obstacle, new Vector3 (x, 1.1f, z), new Quaternion (), transform);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		print ("collision");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
