using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeMeshEffect : BaseMeshEffect {

    private Color minColor = Color.red;
    private Color maxColor = Color.green;

	public override void ModifyMesh(VertexHelper vh)
	{
        if (!IsActive())
        {
            return;
        }
        var xScale = GetComponent<RectTransform>().localScale.x;
        var currentColor = Color.Lerp(minColor, maxColor, xScale);
		List<UIVertex> vertices = new List<UIVertex>();
		vh.GetUIVertexStream(vertices);
		for (int i = 0; i < vertices.Count; i++)
		{
			UIVertex newVertex = vertices[i];
            newVertex.color = (i % 6 == 0 || i % 6 == 1 || i % 6 == 5) ? minColor : currentColor;
			vertices[i] = newVertex;
		}
		vh.Clear();
		vh.AddUIVertexTriangleStream(vertices);
	}

	public void Refresh()
	{
	    GetComponent<Graphic>().SetVerticesDirty();
	}
}
