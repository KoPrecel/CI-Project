using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour {

    public Camera m_OrthographicCamera;
    public float size;
    public float wantedSize;
    float m_ViewWidth, m_ViewHeight;

	// Use this for initialization
	void Start () {
        m_OrthographicCamera.orthographic = true;
        size = m_OrthographicCamera.orthographicSize;
		
	}

    void Update()
    {
        size = m_OrthographicCamera.orthographicSize;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            m_OrthographicCamera.orthographicSize = wantedSize;
        }
    }
}
