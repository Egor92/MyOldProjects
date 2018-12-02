using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LinesDrawer : MonoBehaviour
{
	public Color Color
	{
		get
		{
			return this.GetComponent<MeshRenderer>().material.color;
		}
		set
		{
			this.GetComponent<MeshRenderer>().material.color = value;
		}
	}

	public float Thickness;

	List<Vector3> vertices = new List<Vector3>();
	List<int> triangles = new List<int>();
	Vector3 currentPosition;
	Vector3 prevDirection;

	void Start()
	{

	}

	public void DrawLine(Vector3 start, Vector3 end)
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter==null)
		{
			Debug.LogError("MeshFilter not found!");
			return;
		}

		Vector3 direction = end - start;
		Vector3 normalToDirection = Quaternion.Euler(0f,0f,90f) * direction.normalized * Thickness/2;

		int si = vertices.Count();

		if (si == 0 || start != currentPosition)
		{
			vertices.Add(start + normalToDirection - new Vector3(0f,0f,.1f));
			vertices.Add(start - normalToDirection - new Vector3(0f,0f,.1f));
		}
		else
		{
			vertices.Add(start + normalToDirection - new Vector3(0f,0f,.1f));
			vertices.Add(start - normalToDirection - new Vector3(0f,0f,.1f));
		}
//		else if (prevDirection != Vector3.zero)
//		{
//			vertices.Add(start + normalToDirection);
//			vertices.Add(start - normalToDirection);
//
//			float angle = Vector3.Angle(direction, prevDirection);
//			if (angle > 0.1f)
//				triangles.AddRange(new int[] { si-2, si-1, si, si-1, si, si+1 });
//			else
//				triangles.AddRange(new int[] { si-2, si-1, si, si-1, si, si+1 });
//		}
//		else
//			si -= 2;

		vertices.Add(end + normalToDirection);
		vertices.Add(end - normalToDirection);

		triangles.AddRange(new int[] { si+2, si+1, si, si+1, si+2, si+3 });
				
		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null){
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.Optimize();

		currentPosition = end;
		prevDirection = direction;
	}

	void DrawCircle()
	{

	}
}
