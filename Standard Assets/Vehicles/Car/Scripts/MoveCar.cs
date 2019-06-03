using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

	bool isPointerDown = false;
	float carSpeed = 0;
	public virtual void OnPointerUp(PointerEventData p)
	{
		isPointerDown = true;
	}
	public virtual void OnPointerDown(PointerEventData p)
	{
		isPointerDown = false;
	}

	public float accelarateCar()
	{
		if (isPointerDown == true)

		{
			carSpeed += 0.1f;
		}
		else
		{
			carSpeed = 0;
		}
		return carSpeed;
	}
}
