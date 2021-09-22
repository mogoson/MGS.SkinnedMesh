[TOC]

# MGS.SkinnedMesh.dll

## Summary

- Unity plugin for create skinned mesh in scene.

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Dependence

- [MGS.Curve.dll](.\MGS.Curve.md)
- [MGS.MonoCurve.dll](.\MGS.MonoCurve.md)
- System.dll
- UnityEngine.dll

## Implemented

```C#
public abstract class MonoSkinnedMesh : MonoBehaviour, ISkinnedMesh{}
public abstract class MonoCurveHose : MonoSkinnedMesh, IMonoCurveHose{}
public class MonoCurveSkinnedHose : MonoCurveHose, IMonoCurveHose{}
public sealed class MeshUtility{}
```

## Technology

### Shared Mesh

```C#
meshRenderer.sharedMesh = mesh;
meshRenderer.localBounds = mesh.bounds;
if (meshCollider)
{
    meshCollider.sharedMesh = null;
    meshCollider.sharedMesh = mesh;
}
```

### Build Vertices

```C#
//Create polygon vertices.
var vertices = new List<Vector3>();
var sector = 2 * Mathf.PI / edge;
var radian = 0f;
for (int i = 0; i <= edge; i++)
{
    radian = sector * i;
    vertices.Add(center + rotation * new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * radius);
}
return vertices;

//Create polygon triangles index base on center vertice.
var triangles = new List<int>();
var offset = clockwise ? 0 : 1;
for (int i = 0; i < edge; i++)
{
    triangles.Add(start + i + offset);
    triangles.Add(start + i - offset + 1);
    triangles.Add(center);
}
return triangles;

//Create prism triangles index.
var triangles = new List<int>();
var polygonVs = polygon + 1;
var currentSegment = 0;
var nextSegment = 0;
for (int s = 0; s < segment - 1; s++)
{
    // Calculate start index.
    currentSegment = polygonVs * s;
    nextSegment = polygonVs * (s + 1);
    for (int p = 0; p < polygon; p++)
    {
        // Left-Bottom triangle.
        triangles.Add(start + currentSegment + p);
        triangles.Add(start + currentSegment + p + 1);
        triangles.Add(start + nextSegment + p + 1);

        // Right-Top triangle.
        triangles.Add(start + currentSegment + p);
        triangles.Add(start + nextSegment + p + 1);
        triangles.Add(start + nextSegment + p);
    }
}
return triangles;

//Create polygon uv.
var uv = new List<Vector2>();
var sector = 2 * Mathf.PI / edge;
var radian = 0f;
var center = Vector2.one * 0.5f;
for (int i = 0; i <= edge; i++)
{
    radian = sector * i;
    uv.Add(center + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * 0.5f);
}
return uv;

//Create prism uv.
var uv = new List<Vector2>();
var polygonVs = polygon + 1;
var vertices = polygonVs * segment;
var slice = 1.0f / polygon;
var u = 0f;
var v = 0f;
for (int i = 0; i < vertices; i++)
{
    u = slice * (i % polygonVs);
    v = (i / polygonVs) % 2;
    uv.Add(new Vector2(u, v));
}
return uv;
```

### Build Mesh

```C#
//Rebuild the mesh of hose.
mesh.vertices = CreateVertices(curve, segments, differ, isSeal);
mesh.triangles = CreateTriangles(segments, isSeal);
mesh.uv = CreateUV(segments, isSeal);
mesh.RecalculateNormals();
mesh.RecalculateBounds();
```

## Usage

- Attach mono curve component to a game object.
- Attach mono skinned mesh component to a game object.
- Adjust the args of curve and skinned component or edit curve in scene editor.

- Adjust the args of curve and Rebuild runtime, and MonoSkinnedMesh will auto Rebuild.

```C#
var curve = GetComponent<MonoHermiteCurve>();
curve.AddAnchor(new HermiteAnchor(point));
curve..Rebuild();//The MonoSkinnedMesh will auto Rebuild.
```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com