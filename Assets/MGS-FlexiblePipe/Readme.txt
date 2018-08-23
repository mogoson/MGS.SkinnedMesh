==========================================================================
  Copyright © 2018 Mogoson. All rights reserved.
  Name: MGS-FlexiblePipe
  Author: Mogoson   Version: 0.1.2   Date: 8/4/2018
==========================================================================
  [Summary]
    Unity plugin for create flexible pipe in scene.
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

    CurvePipe : Define CurvePipe to render dynamic pipe mesh base on
    center curve.

    BezierPipe : Render dynamic pipe mesh base on cubic bezier curve.

    AnchorPipe : Render dynamic pipe mesh base on anchor vector animation
    curve.

    CirclePipe : Render dynamic pipe mesh base on circle curve.

    EllipsePipe : Render dynamic pipe mesh base on ellipse curve.

    HelixPipe : Render dynamic pipe mesh base on helix curve.

    SinPipe : Render dynamic pipe mesh base on sin curve.

    Machine : Machine cable example.

    HelpUI : Draw help info in scene.
--------------------------------------------------------------------------
  [Usage]
    Create an empty gameobject and attach the pipe component BezierPipe
    or AnchorPipe to it.

    If BezierPipe attached, drag the green sphere to change tangent and
    drag the blue sphere to change it's position.

    if AnchorPipe attached, drag the blue sphere to change it's position,
    press the ALT key and click the green sphere to add anchor, press the
    SHIFT key and click the red sphere to remove anchor if you want.
--------------------------------------------------------------------------
  [Demo]
    Demos in the path "MGS-FlexiblePipe/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-FlexiblePipe.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------