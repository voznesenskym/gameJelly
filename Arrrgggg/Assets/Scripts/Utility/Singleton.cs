using UnityEngine;
using System.Collections;

/********************************************************************
 * 
 * Script - Singleton
 * 
 * This is a base singleton class.
 * 
 * Use - A common use of a singleton is for manager type classes.
 * 
 * Author - Jason Roth
 * 
 *******************************************************************/

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	protected static T instance;
	
	public static T Instance {
		get {
			if (instance == null) {                                
				instance = (T)FindObjectOfType(typeof(T));
				
				if (instance == null) {
					Debug.LogError("An instance of" + typeof(T) +
					               " is needed in the scene, but there is none.");
				}
			}
			return instance;
		}
	}
	
	protected virtual void OnDestroy() {
		Debug.Log("Destroying " + gameObject.name);
		instance = null;
	}
}