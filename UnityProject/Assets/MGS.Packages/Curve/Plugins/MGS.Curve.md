[TOC]

# MGS.Curve.dll

## Summary

- Smooth 3D curve for Unity project develop.

## Environment

- .Net Framework 3.5 or above.
- Unity 5.0 or above.

## Platform

- Windows

## Implemented

```C#
public class SinCurve : ITimeCurve{}
public class EllipseCurve : ITimeCurve{}
public class HelixCurve : ITimeCurve{}
public class BezierCurve : ITimeCurve{}
public class HermiteCurve : ITimeCurve{}
```

## Technology

### Bezier Polynomial

```C#
return Mathf.Pow(1 - t, 3) * anchor.from + 3 * t * Mathf.Pow(1 - t, 2) * anchor.frTangent +3 * (1 - t) * Mathf.Pow(t, 2) * anchor.toTangent + Mathf.Pow(t, 3) * anchor.to;
```

### Hermite Polynomial

```C#
/*  Designed By Mogoson.
 *  Hermite Polynomial Structure
 *  Base: H(t) = v0a0(t) + v1a1(t) + m0b0(t) + m1b1(t)
 * 
 *                     t-t0    t-t1  2
 *        a0(t) = (1+2------)(------)
 *                     t1-t0   t0-t1
 *                    
 *                     t-t1    t-t0  2
 *        a1(t) = (1+2------)(------)
 *                     t0-t1   t1-t0
 * 
 *                        t-t1  2
 *        b0(t) = (t-t0)(------)
 *                        t0-t1
 * 
 *                        t-t0  2
 *        b1(t) = (t-t1)(------)
 *                        t1-t0
 * 
 *  Let:  d0 = t-t0, d1 = t-t1, d = t0-t1
 * 
 *              d0          d1
 *        q0 = ---- , q1 = ----
 *              d           d
 * 
 *               t-t1  2     d1  2     2          t-t0  2     d0  2     2
 *        p0 = (------)  = (----)  = q1  , p1 = (------)  = (----)  = q0
 *               t0-t1       d                    t1-t0       -d
 * 
 *  Get:  H(t) = (1-2q0)v0p0 + (1+2q1)v1p1 + mod0p0 + m1d1p1
 */

var d0 = t - t0;
var d1 = t - t1;
var d = t0 - t1;

var q0 = d0 / d;
var q1 = d1 / d;

var p0 = q1 * q1;
var p1 = q0 * q0;

return (1 - 2 * q0) * v0 * p0 + (1 + 2 * q1) * v1 * p1 + m0 * d0 * p0 + m1 * d1 * p1;
```

### Tangent Smooth

```C#
//Designed By Mogoson.
KeyFrame k0, k1, k2;
if (index == 0 || index == frames.Count - 1)
{
    if (frames[0].value != frames[frames.Count - 1].value)
    {
        var frame = frames[index];
        frame.inTangent = frame.outTangent = Vector3.zero;
        frames[index] = frame;
        return;
    }

    k0 = frames[frames.Count - 2];
    k1 = frames[index];
    k2 = frames[1];

    if (index == 0)
    {
        k0.time -= frames[frames.Count - 1].time;
    }
    else
    {
        k2.time += frames[frames.Count - 1].time;
    }
}
else
{
    k0 = frames[index - 1];
    k1 = frames[index];
    k2 = frames[index + 1];
}

var weight01 = (1 + weight) / 2;
var weight12 = (1 - weight) / 2;
var t01 = (k1.value - k0.value) / (k1.time - k0.time);
var t12 = (k2.value - k1.value) / (k2.time - k1.time);
k1.inTangent = k1.outTangent = t01 * weight01 + t12 * weight12;
frames[index] = k1;
```

## Usage

- New a Curve and Set Args.

```C#
var curve = new HermiteCurve();
curve.AddFrames(frames);
curve.SmoothTangents();//If need SmoothTangents Auto.
```

- Evaluate the curve at time.

```C#
var p0 = curve.Evaluate(frames[0].time);
for (float t = frames[0].time; t < frames[frames.Length - 1].time; t += delta)
{
    var p1 = curve.Evaluate(t);
    //Just for demo, you can use p0,p1 to do more things.
    Gizmos.DrawLine(transform.TransformPoint(p0), transform.TransformPoint(p1));
    p0 = p1;
}
```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com