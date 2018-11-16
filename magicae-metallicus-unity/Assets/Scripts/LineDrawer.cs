using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

    public Player player1;
    public Player player2;

    public Material lineMaterial;

    private Vector3 startVertex;
    private Vector3 mousePos;

    // Use this for initialization
    void Start () {

        startVertex = Vector3.zero;
        //lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
    }
	
	// Update is called once per frame
	void Update () {
        mousePos = Input.mousePosition;
    }

    void OnPostRender() {
        /*Debug.Log("????");
        if (!lineMaterial) {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        Debug.Log(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        GL.End();

        GL.PopMatrix();*/
    }
}
