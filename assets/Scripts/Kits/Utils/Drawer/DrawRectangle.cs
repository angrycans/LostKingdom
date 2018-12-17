using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Acans.Tools
{


  public class DrawRectangle
  {

    //public 
    // private void Start()
    // {
    //   var grid = gameObject.GetComponent<Grid>();
    //   if (grid)
    //   {
    //     //Log.info("grid=", Dumper.Dump(gameObject));
    //     //DumpObjecter.Dump(gameObject);
    //     //Log.info("grid=>", ObjectDumper.Dump(grid));
    //     Log.info("grid is grid", grid is Grid);
    //     drawRectangle(new Vector2(0, 0), new Vector2(100, 100));
    //     drawRectangleOutline(new Vector2(0, 0), new Vector2(100, 100));
    //   }


    // }
    public static void draw(GameObject gameObject, Vector2 v0, Vector2 v1)
    {

      // Set up game object with mesh;
      var meshRenderer = gameObject.AddComponent<MeshRenderer>();
      meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

      var _meshFilter = gameObject.AddComponent<MeshFilter>();
      _meshFilter.mesh = createRectangleMesh(v0, v1, Color.red);


    }

    public static void drawOutline(GameObject gameObject, Vector2 v0, Vector2 v1)
    {
      var _vertices = getVertices(v0, v1);
      var _lineRenderer = gameObject.AddComponent<LineRenderer>();
      _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
      _lineRenderer.material.color = Color.white;
      _lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
      _lineRenderer.receiveShadows = false;
      _lineRenderer.allowOcclusionWhenDynamic = false;
      _lineRenderer.loop = true;
      _lineRenderer.useWorldSpace = false;
      _lineRenderer.widthMultiplier = 2f;
      //_lineRenderer.endWidth = 0.025f;
      _lineRenderer.positionCount = _vertices.Length;
      _lineRenderer.SetPositions(_vertices);

    }
    public static Vector3[] ToVector3(Vector2[] vectors)
    {
      return System.Array.ConvertAll<Vector2, Vector3>(vectors, v => v);
    }

    public static Vector3[] getVertices(Vector2 v0, Vector2 v1)
    {
      // Calculate implied verticies from corner vertices
      // Note: vertices must be adjacent to each other for Triangulator to work properly
      var v2 = new Vector2(v0.x, v1.y);
      var v3 = new Vector2(v1.x, v0.y);
      var rectangleVertices = new[] { v0, v2, v1, v3 };


      return ToVector3(rectangleVertices);



    }
    public static Mesh createRectangleMesh(Vector2 v0, Vector2 v1, Color fillColor)
    {
      // Calculate implied verticies from corner vertices
      // Note: vertices must be adjacent to each other for Triangulator to work properly
      var v2 = new Vector2(v0.x, v1.y);
      var v3 = new Vector2(v1.x, v0.y);
      var rectangleVertices = new[] { v0, v2, v1, v3 };

      // Find all the triangles in the shape
      var triangles = new Triangulator(rectangleVertices).Triangulate();

      // Assign each vertex the fill color
      var colors = Enumerable.Repeat(fillColor, rectangleVertices.Length).ToArray();

      var mesh = new Mesh
      {
        name = "Rectangle",
        vertices = ToVector3(rectangleVertices),
        triangles = triangles,
        colors = colors
      };

      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      mesh.RecalculateTangents();

      return mesh;
    }
  }

}