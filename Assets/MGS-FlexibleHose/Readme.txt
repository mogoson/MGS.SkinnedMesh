==========================================================================
  Copyright © 2018 Mogoson. All rights reserved.
  Name: MGS-FlexibleHose
  Author: Mogoson   Version: 0.1.2   Date: 8/4/2018
==========================================================================
  [Summary]
    Unity plugin for create flexible hose in scene.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.0 or above.
--------------------------------------------------------------------------
  [Achieve]
    BezierCurve : Define bezier curve.

    HermiteCurve : Hermite curve in three dimensional space.

    EllipseCurve : Ellipse curve.

    HelixCurve : Helix curve.

    SinCurve : Sin curve.

    Skin : Define Skin to render dynamic mesh.

    CurveHose : Define CurveHose to render dynamic hose mesh base on
    center curve.

    BezierHose : Render dynamic hose mesh base on cubic bezier curve.

    AnchorHose : Render dynamic hose mesh base on anchor vector animation
    curve.

    CircleHose : Render dynamic hose mesh base on circle curve.

    EllipseHose : Render dynamic hose mesh base on ellipse curve.

    HelixHose : Render dynamic hose mesh base on helix curve.

    SinHose : Render dynamic hose mesh base on sin curve.

    Machine : Machine cable example.

    HelpUI : Draw help info in scene.
--------------------------------------------------------------------------
  [Usage]
    Create an empty gameobject and attach the hose component BezierHose
    or AnchorHose to it.

    If BezierHose attached, drag the green sphere to change tangent and
    drag the blue sphere to change it's position.

    if AnchorHose attached, drag the blue sphere to change it's position,
    press the ALT key and click the green sphere to add anchor, press the
    SHIFT key and click the red sphere to remove anchor if you want.
--------------------------------------------------------------------------
  [Demo]
    Demos in the path "MGS-FlexibleHose/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-FlexibleHose.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------