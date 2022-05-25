using UnityEngine;
using System.Collections;

public class LineFade : MonoBehaviour
{
	#region Variables
	[SerializeField] 
	private Color color;

	[SerializeField] 
	private float speed = 10f;

	LineRenderer lineRenderer;
	#endregion
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update()
	{
		//색 부드럽게
		color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * speed);

		//색
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
	}
}

