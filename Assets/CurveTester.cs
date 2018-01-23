using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierCurve))]
public class CurveTester : MonoBehaviour {
	float pastTime = 0f;
	BezierCurve bc;
	public GameObject objectToBeMoved;
	public int from = 0;
	public int to = 1;
	public float moveTime = 5f;
	private BezierPoint[] bcPoints;
	private float[] length;
	private float totalMoveLength = 0f;
	private int curPoint;
	private float previousCurveTime = 0f;

	private bool isDone = false;
	// Use this for initialization
	void Start () {
		bc = GetComponent<BezierCurve>();
		bcPoints = bc.GetAnchorPoints();
		length = new float[bcPoints.Length];
		if (from < 0 || to > bcPoints.Length)
			throw new System.Exception("Invalid from or to value");
		
		
		for (int i = from; i < to; i++)
		{
			if (i == bcPoints.Length - 1)
			{
				length[i] = BezierCurve.ApproximateLength(bcPoints[i], bcPoints[from]);
			}
			else
			{
				length[i] = BezierCurve.ApproximateLength(bcPoints[i], bcPoints[i + 1]);
			}
			totalMoveLength += length[i];
		}
		curPoint = from;
		print("totalMoveLength = "+ totalMoveLength.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		if (isDone)
			return;
		pastTime += Time.deltaTime;
		float t = 0f;
		do
		{
			if (t >= 1f)
			{
				if(curPoint >= to - 1) //hit the very target point
				{
					isDone = true;
					print("--- Object movement done ---");
					return;
				}
				else
				{
					float timeSpentOnThisPart = (length[curPoint] / totalMoveLength) * moveTime;
					previousCurveTime += timeSpentOnThisPart;
					pastTime -= timeSpentOnThisPart;
					curPoint++;
				}
			}
			float curPercentage = length[curPoint] / totalMoveLength;
			t = (pastTime / moveTime) / curPercentage;
		} while (t >= 1f);
		Vector3 pos;
		if (curPoint == bcPoints.Length - 1)
			pos = BezierCurve.GetPoint(bcPoints[curPoint], bcPoints[from], t);
		else
			pos = BezierCurve.GetPoint(bcPoints[curPoint], bcPoints[curPoint+1], t);
		objectToBeMoved.transform.position = pos;

	}

	
}
