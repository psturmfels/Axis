using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFloatOnBar : MonoBehaviour {
	public Scrollbar[] dispBar;
		
	public void SetDispValue(float newDispValue, int index) {
		dispBar[index].size = newDispValue;
	}
}
