using UnityEngine;
using System;

[Serializable] 
public class SceneShotBookmarkModel
{
	public string nickname;
	public Vector3 position;
	public Quaternion rotation;
	public bool orthographic;
	public float orthographicSize;
}