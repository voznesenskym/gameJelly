using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

#region System.Collections Extensions
namespace System.Collections {
	public class Tuple<T1, T2>
	{
	    public T1 First { get; private set; }
	    public T2 Second { get; private set; }
	    internal Tuple(T1 first, T2 second)
	    {
	        First = first;
	        Second = second;
	    }
	}
	
	public static class Tuple
	{
	    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
	    {
	        var tuple = new Tuple<T1, T2>(first, second);
	        return tuple;
	    }
	}
}
#endregion

public static class ExtensionMethods {
	
	#region DateTime Extension Methods
	public static int DateTimeToUnixTimestamp(this DateTime dateTime)
	{
		return (int)(DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
	}
	#endregion

	#region String Extension Methods
	public class CompareAlphanumeric
	{
		public static int Compare(object x, object y)
		{
			string s1 = x as string;
			if (s1 == null)
			{
				return 0;
			}
			string s2 = y as string;
			if (s2 == null)
			{
				return 0;
			}
			
			int len1 = s1.Length;
			int len2 = s2.Length;
			int marker1 = 0;
			int marker2 = 0;
			
			// Walk through two the strings with two markers.
			while (marker1 < len1 && marker2 < len2)
			{
				char ch1 = s1[marker1];
				char ch2 = s2[marker2];
				
				// Some buffers we can build up characters in for each chunk.
				char[] space1 = new char[len1];
				int loc1 = 0;
				char[] space2 = new char[len2];
				int loc2 = 0;
				
				// Walk through all following characters that are digits or
				// characters in BOTH strings starting at the appropriate marker.
				// Collect char arrays.
				do
				{
					space1[loc1++] = ch1;
					marker1++;
					
					if (marker1 < len1)
					{
						ch1 = s1[marker1];
					}
					else
					{
						break;
					}
				} while (char.IsDigit(ch1) == char.IsDigit(space1[0]));
				
				do
				{
					space2[loc2++] = ch2;
					marker2++;
					
					if (marker2 < len2)
					{
						ch2 = s2[marker2];
					}
					else
					{
						break;
					}
				} while (char.IsDigit(ch2) == char.IsDigit(space2[0]));
				
				// If we have collected numbers, compare them numerically.
				// Otherwise, if we have strings, compare them alphabetically.
				string str1 = new string(space1);
				string str2 = new string(space2);
				
				int result;
				
				if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
				{
					int thisNumericChunk = int.Parse(str1);
					int thatNumericChunk = int.Parse(str2);
					result = thisNumericChunk.CompareTo(thatNumericChunk);
				}
				else
				{
					result = str1.CompareTo(str2);
				}
				
				if (result != 0)
				{
					return result;
				}
			}
			return len1 - len2;
		}
	}
	#endregion
		
	#region Dictionary Extension Methods
	public static bool UpdateKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey fromKey, TKey toKey) {
		TValue value;
		
		if (!dic.TryGetValue(fromKey, out value)) {
			return false;
		}
		
		dic.Remove(fromKey);
		dic[toKey] = value;
		return true;
	}
	
	#endregion
	
	#region Vector3 Extension Methods
	public static Vector3 XY(this Vector3 v) {
		return new Vector3(v.x, v.y, 0f);
	}
	
	public static Vector3 XZ(this Vector3 v) {
		return new Vector3(v.x, 0f, v.z);
	}
	
	public static Vector3 YZ(this Vector3 v) {
		return new Vector3(0f, v.y, v.z);
	}

	public static Vector3 OnlyX(this Vector3 v) {
		return new Vector3(v.x, 0f, 0f);
	}

	public static Vector3 OnlyY(this Vector3 v) {
		return new Vector3(0f, v.y, 0f);
	}

	public static Vector3 OnlyZ(this Vector3 v) {
		return new Vector3(0f, 0f, v.z);
	}
	#endregion

	#region Array Extension Methods
	public static List<T> ToList<T>(this T[] arr) {
		List<T> list = new List<T>();
		for (int i = 0, l = arr.Length; i < l; ++i) {
			list.Add(arr[i]);
		}

		return list;
	}
	#endregion
	
	#region GameObject Extension Methods
	public static T GetComponentInParent<T>(this GameObject obj) where T : Component {
		Transform parent = obj.transform.parent;
		return parent ? parent.GetComponent<T>() : null;
	}

	public static T GetComponentInParentRecursive<T>(this GameObject obj) where T : Component {
		Transform parent = obj.transform.parent;
		while (parent) {
			T component = parent.GetComponent<T>();
			if (component) return component;
			else parent = parent.transform.parent;
		}

		return null;
	}

	public static T[] GetComponentsInList<T>(this List<GameObject> gObjList) where T : Component {
		List<T> components = new List<T>();
		GameObject go = null;

		if (gObjList != null) {
			for (int i = 0, count = gObjList.Count; i < count; ++i) {
				go = gObjList[i];
				if (go != null) {
					T component = go.GetComponent<T>();
					if (component != null) components.Add(component);
				}
			}
		}

		return components.ToArray();
	}

	public static T[] GetComponentsInArray<T>(this GameObject[] gObjArray) where T : Component {
		List<T> components = new List<T>();
		GameObject go;

		for (int i = 0, count = gObjArray.Length; i < count; ++i) {
			go = gObjArray[i];
			T component = go.GetComponent<T>();
			if (component != null) components.Add(component);
		}

		return components.ToArray();
	}

	/// <summary>
	/// Returns all monobehaviours (casted to T)
	/// </summary>
	/// <typeparam name="T">interface type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetInterfaces<T>(this GameObject gObj) {
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		var mObjs = gObj.GetComponents<MonoBehaviour>();
		return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
	}
	
	/// <summary>
	/// Returns the first monobehaviour that is of the interface type (casted to T)
	/// </summary>
	/// <typeparam name="T">Interface type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetInterface<T>(this GameObject gObj) {
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		return gObj.GetInterfaces<T>().FirstOrDefault();
	}
	
	/// <summary>
	/// Returns the first instance of the monobehaviour that is of the interface type T (casted to T)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetInterfaceInChildren<T>(this GameObject gObj) {
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
	}
	
	/// <summary>
	/// Gets all monobehaviours in children that implement the interface of type T (casted to T)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetInterfacesInChildren<T>(this GameObject gObj) {
		if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
		var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();
		return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
	}

	/// <summary>
	/// Sets the GameObject's transform to t.
	/// </summary>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='t'>
	/// The target transform.
	/// </param>
	/// <param name='local'>
	/// Whether to set local or global values.
	/// </param>
	public static void SetTransform(this GameObject obj, Transform t, bool local = true) {
		if (local) {
			obj.transform.localPosition = t.localPosition;
			obj.transform.localRotation = t.localRotation;
			obj.transform.localScale = t.localScale;
		} else {
			obj.transform.position = t.position;
			obj.transform.rotation = t.rotation;
			obj.transform.localScale = t.localScale;
		}
	}
	
	/// <summary>
	/// Gets the GameObject's color. If the GameObject doesn't have a renderer, it will search the GameObject's children for the first color it finds.
	/// </summary>
	/// <returns>
	/// The GameObject's color.
	/// </returns>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	public static Color GetColor(this GameObject obj) {
		if (obj.renderer && obj.renderer.material) {
			return obj.renderer.material.color;
		}
		
		foreach (GameObject go in obj.GetChildGameObjects()) {
			if (go.renderer && go.renderer.material) {
				return go.renderer.material.color;
			}
		}
		
		return Color.clear;
	}

	/// <summary>
	/// Finds the GameObjects with the specified Layer.
	/// </summary>
	/// <returns>
	/// List of GameObjects with the specified Layer.
	/// </returns>
	/// <param name='layerName'>
	/// The name of the layer.
	/// </param>
	public static List<GameObject> FindGameObjectsWithLayer(string layerName) {
		int layer = LayerMask.NameToLayer(layerName);
		List<GameObject> objsInLayer = FindGameObjectsWithLayer(layer);
		
		return objsInLayer;
	}
	
	/// <summary>
	/// Finds the GameObjects with the specified Layer.
	/// </summary>
	/// <returns>
	/// List of GameObjects with the specified Layer.
	/// </returns>
	/// <param name='layer'>
	/// The int value of the layer.
	/// </param>
	public static List<GameObject> FindGameObjectsWithLayer(int layer) {
		GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		List<GameObject> objsInLayer = new List<GameObject>();
		for (int i = 0, count = objects.Length; i < count; i++) {
			if (objects[i].layer == layer) {
				objsInLayer.Add(objects[i]);
			}
		}
		
		if (objsInLayer.Count == 0) return null;
		
		return objsInLayer;
	}
	
	/// <summary>
	/// Sets the GameObject's layer recursively.
	/// </summary>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='layerName'>
	/// The name of the layer.
	/// </param>
	public static void SetLayerRecursive(this GameObject obj, string layerName) {
		int layer = LayerMask.NameToLayer(layerName);
		obj.SetLayerRecursive(layer);
	}
	
	public static void SetLayerRecursive(this GameObject obj, int layer) {
		obj.layer = layer;
		
		foreach (GameObject go in obj.GetChildGameObjects()) {
			go.SetLayerRecursive(layer);
		}
	}
	
	/// <summary>
	/// Sets the GameObject's color.
	/// </summary>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='color'>
	/// The specified color.
	/// </param>
	public static void SetColor(this GameObject obj, Color color) {
		if (obj.renderer && obj.renderer.material) {
			obj.renderer.material.color = color;
		}
	}
	
	/// <summary>
	/// Sets the GameObject's color recursively, affecting all child GameObjects as well.
	/// </summary>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='color'>
	/// The specified color.
	/// </param>
	public static void SetColorRecursive(this GameObject obj, Color color) {
		obj.SetColor(color);
		
		foreach (GameObject go in obj.GetChildGameObjects()) {
			go.SetColor(color);
		}
	}
	
	/// <summary>
	/// Gets the tag from each child GameObject.
	/// </summary>
	/// <returns>
	/// A List of tags.
	/// </returns>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	public static List<string> GetTagsInChildren(this GameObject obj) {
		List<string> tags = new List<string>();
		
		foreach (GameObject go in obj.GetChildGameObjects()) {
			tags.Add(go.tag);
		}
		
		if (tags.Count > 0) return tags;
		return null;
	}
	
	/// <summary>
	/// Gets the GameObject's parent game object.
	/// </summary>
	/// <returns>
	/// The GameObject's parent game object.
	/// </returns>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	public static GameObject GetParentGameObject(this GameObject obj) {
		if (obj.transform.parent != null) {
			return obj.transform.parent.gameObject;
		} else {
			return null;
		}
	}
	
	/// <summary>
	/// Sets this GameObject's parent to another GameObject.
	/// </summary>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='parentObj'>
	/// The target parent GameObject.
	/// </param>
	/// <returns>
	/// True if successfully set the parent, false if parent was null.
	/// </returns>
	public static bool SetParentGameObject(this GameObject obj, GameObject parentObj) {
		if (parentObj != null) {
			obj.transform.parent = parentObj.transform;
			return true;
		}
		
		return false;
	}
	
	/// <summary>
	/// Sets this GameObject's parent to a Transform.
	/// </summary>
	/// <returns>
	/// True if successfully set the parent, false if parent was null.
	/// </returns>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	/// <param name='t'>
	/// The target parent Transform.
	/// </param>
	public static bool SetParentTransform(this GameObject go, Transform t) {
		if (t != null) {
			go.transform.parent = t;
			return true;
		}
		
		return false;
	}

	/// <summary>
	/// Gets the sibling GameObjects of the specified GameObject.
	/// </summary>
	/// <returns>The specified GameObject's siblings.</returns>
	/// <param name="go">The GameObject</param>
	public static List<GameObject> GetSiblingGameObjects(this GameObject go) {
		List<GameObject> siblings = null;
		Transform t = go.transform.parent;

		if (t != null) {
			siblings = t.gameObject.GetChildGameObjects();
		}

		int index = -1;
		for (int i = 0, max = siblings.Count; i < max; ++i) {
			if (siblings[i] == go) {
				index = i;
			}
		}

		if (index != -1) siblings.RemoveAt(index);

		return siblings;
	}
	
	/// <summary>
	/// Gets the GameObject's child game objects.
	/// </summary>
	/// <returns>
	/// The child game objects.
	/// </returns>
	/// <param name='obj'>
	/// The GameObject.
	/// </param>
	public static List<GameObject> GetChildGameObjects(this GameObject go) {
		List<GameObject> children = new List<GameObject>();
		
		for (int i = 0; i < go.transform.childCount; ++i) {
			children.Add(go.transform.GetChild(i).gameObject);
		}
		
		return children;
	}

	public static List<GameObject> GetChildGameObjectsRecursive(this GameObject go) {
		List<GameObject> children = new List<GameObject>();
		GameObject child;

		for (int i = 0; i < go.transform.childCount; ++i) {
			child = go.transform.GetChild(i).gameObject;

			List<GameObject> grandchildren = child.GetChildGameObjectsRecursive();
			for (int j = 0, count = grandchildren.Count; j < count; ++j) {
				children.Add(grandchildren[j]);
			}

			children.Add(child);
		}

		return children;
	}
	
	/// <summary>
	/// Turns off the GameObject's collider and renderer.
	/// </summary>
	/// <returns>
	/// False if the GameObject is missing both components, true otherwise. Shuts off present Components if only one exists.
	/// </returns>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static bool ColliderRendererOff(this GameObject go) {
		if (go.collider == null && go.renderer == null) {
			return false;
		} else {
			if (go.collider) go.collider.enabled = false;
			if (go.renderer) go.renderer.enabled = false;
			
			return true;
		}
	}
	
	/// <summary>
	/// Turns off the GameObject's collider.
	/// </summary>
	/// <returns>
	/// False if the GameObject doesn't have a collider, true if successfully turned off.
	/// </returns>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static bool ColliderOff(this GameObject go) {
		if (go.collider == null) {
			return false;
		} else {
			go.collider.enabled = false;
			return true;
		}
	}
	
	/// <summary>
	/// Turns the GameObject's and its children's renderers and colliders off.
	/// </summary>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static void ColliderRendererOffRecursive(this GameObject go) {
		go.ColliderRendererOff();
		
		List<GameObject> children = go.GetChildGameObjects();
		GameObject child = null;
		
		for (int i = 0; i < children.Count; i++) {
			child = children[i];
			if (child.collider) child.collider.enabled = false;
			if (child.renderer) child.renderer.enabled = false;
			
			child.ColliderRendererOffRecursive();
		}
	}

	/// <summary>
	/// Turns the GameObject's and its children's colliders off.
	/// </summary>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static void ColliderOffRecursive(this GameObject go) {
		go.ColliderOff();
		
		List<GameObject> children = go.GetChildGameObjects();
		GameObject child = null;
		
		for (int i = 0; i < children.Count; i++) {
			child = children[i];
			if (child.collider) child.collider.enabled = false;
			
			child.ColliderOffRecursive();
		}
	}

	/// <summary>
	/// Turns on the GameObject's collider and renderer.
	/// </summary>
	/// <returns>
	/// False if the GameObject is missing both components, true otherwise. Turns on present Components if only one exists.
	/// </returns>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static bool ColliderRendererOn(this GameObject go) {
		if (go.collider == null && go.renderer == null) {
			return false;
		} else {
			if (go.collider) go.collider.enabled = true;
			if (go.renderer) go.renderer.enabled = true;
			
			return true;
		}
	}
	
	/// <summary>
	/// Turns on the GameObject's collider.
	/// </summary>
	/// <returns>
	/// False if the GameObject doesn't have a collider, true if successfully turned on.
	/// </returns>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static bool ColliderOn(this GameObject go) {
		if (go.collider == null) {
			return false;
		} else {
			go.collider.enabled = true;
			return true;
		}
	}
	
	/// <summary>
	/// Turns the GameObject's and its children's renderers and colliders on.
	/// </summary>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static void ColliderRendererOnRecursive(this GameObject go) {
		go.ColliderRendererOn();
		
		List<GameObject> children = go.GetChildGameObjects();
		GameObject child = null;
		
		for (int i = 0; i < children.Count; i++) {
			child = children[i];
			if (child.collider) child.collider.enabled = true;
			if (child.renderer) child.renderer.enabled = true;
			
			child.ColliderRendererOnRecursive();
		}
	}

	/// <summary>
	/// Turns the GameObject's and its children's colliders on.
	/// </summary>
	/// <param name='go'>
	/// The GameObject.
	/// </param>
	public static void ColliderOnRecursive(this GameObject go) {
		go.ColliderOn();
		
		List<GameObject> children = go.GetChildGameObjects();
		GameObject child = null;
		
		for (int i = 0; i < children.Count; i++) {
			child = children[i];
			if (child.collider) child.collider.enabled = true;
			
			child.ColliderOnRecursive();
		}
	}
	#endregion

	#region Mathf Methods
	public static float Fmodf(this Mathf mathf, float a, float b) {
		float remainder = a - Mathf.Floor(a / b) * b;
		
		return remainder;
	}
	#endregion
	
	#region Float Methods
	public static float Map(this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	#endregion
}
