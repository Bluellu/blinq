using UnityEngine;
using System.Collections;

public class LineController : MonoBehaviour {
    private LineRenderer lineRenderer;
    public Transform PlayerPos;
    public Transform MarkerPos;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.2F, 0.02F);
        lineRenderer.SetVertexCount(2);
    }
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, PlayerPos.position);
        lineRenderer.SetPosition(1, MarkerPos.position);

    }
}
