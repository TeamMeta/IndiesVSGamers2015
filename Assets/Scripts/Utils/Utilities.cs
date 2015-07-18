using UnityEngine;
using System.Collections;


/// <summary>
///Using C# Extension Methods to add useful methods to vector3.
///Vector3 is a struct so can't just modify the object, thus returning. 
/// </summary>
public static class Utilities  {

	public static Vector3 ZeroZ(this Vector3 vec){
		return new Vector3(vec.x, vec.y, 0);
	}
	public static Vector3 ZeroY(this Vector3 vec){
		return new Vector3(vec.x, 0, vec.z);
	}
	public static Vector3 ZeroX(this Vector3 vec){
		return new Vector3(0, vec.y, vec.z);
	}

	public static Vector3 SetX(this Vector3 vec, float val){
		return new Vector3(val, vec.y, vec.z);
	}
	public static Vector3 SetY(this Vector3 vec, float val){
		return new Vector3(vec.x, val, vec.z);
	}
	public static Vector3 SetZ(this Vector3 vec, float val){
		return new Vector3(vec.x, vec.y, val);
	}

}
