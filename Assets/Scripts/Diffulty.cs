using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty  {

	public static float secondsToMaxDif = 120;

	public static float getDifPercent() {
		return Mathf.Clamp01 (Time.timeSinceLevelLoad / secondsToMaxDif);
	}
}