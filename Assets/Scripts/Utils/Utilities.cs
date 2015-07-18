using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



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


	public static Vector2 ZeroY(this Vector2 vec){
		return new Vector2(vec.x, 0);
	}
	public static Vector2 ZeroX(this Vector2 vec){
		return new Vector2(0, vec.y);
	}
	
	public static Vector2 SetX(this Vector2 vec, float val){
		return new Vector2(val, vec.y);
	}
	public static Vector2 SetY(this Vector2 vec, float val){
		return new Vector2(vec.x, val);
	}

	public static Vector3 GetPreventOvershootMoveVector(Transform objectToMove, Vector3 initialPosition, Vector3 finalPosition, float moveSpeed){
		Vector3 moveVector = Vector3.zero;
		
		//If objects distance is NOT close to final position, the move vector is NOT ZERO and calculated
		if(!(Vector3.Distance(objectToMove.position.ZeroZ(), finalPosition.ZeroZ()) < 0.1)){
			moveVector = (finalPosition - initialPosition).normalized * moveSpeed * Time.deltaTime;
		}
		
		return moveVector;
	}

	public static bool CloseEnough(Vector3 v1, Vector3 v2){
		return (Vector3.Distance(v1, v2) < 0.1);
	}

	public static List<FailedOrgan> ValuesAsList( Dictionary<OrganType, FailedOrgan> dict ){
		FailedOrgan[] tempArray =  new FailedOrgan[dict.Count];
		dict.Values.CopyTo(tempArray, 0);

		List<FailedOrgan> toRet = new List<FailedOrgan>();
		for(int i=0; i < tempArray.Length; i++){
			toRet.Add(tempArray[i]);
		}

		return toRet;
	}

 
}
