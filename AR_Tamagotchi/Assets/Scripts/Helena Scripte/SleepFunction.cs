using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class SleepFunction : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject canvas;
	public GameObject btnSleep;
	
	public void Start() 
	{ 
		btnSleep.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}

	void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonBehaviour behaviour) 
	{ 
		Debug.Log("Sleep button Pressed");
		var tamaScript = canvas.GetComponent<Tamagotchi>();
		tamaScript.SleepTheFino();
	}

	void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour behaviour) 
	{ 
		Debug.Log("Sleep button Released");
	}
}
