[TOC]

﻿# MGS.CommonUtility

## Summary

- Common code for C# and Unity project develop.

## Environment

- Unity 5.0 or above.
- .Net Framework 3.5 or above.

## Platform

- Windows

## Module

### JsonUtilityPro

- Environment.

  **Unity 5.3 or above**.

- Implemented

  ```C#
  public class ListAvatar<T>{}
  public class DictionaryAvatar<TKey, TValue> : ISerializationCallbackReceiver{}
  public sealed class JsonUtilityPro{}
  ```
  
- Usage

  ```C#
  //Serialize List.
  var list = new List<string>()
  {
      "A","BB","CCC"
  };
  var json = JsonUtilityPro.ToJson(list);
  var list = JsonUtilityPro.FromJson<string>(json);
  
  //Serialize Dictionary.
  var dic = new Dictionary<int, string>()
  {
      { 0,"A"},{1,"BB" },{2,"CCC" }
  };
  var json = JsonUtilityPro.ToJson(dic);
  var dic = JsonUtilityPro.FromJson<int, string>(json);
  ```
  

### Dispatcher

- Implemented

  ```C#
  //Dispatcher base main thread; Auto create instance run time.
  public sealed class Dispatcher : MonoBehaviour{}
  ```

- Usage

  ```C#
  var thread = new Thread(() =>
  {
      while (isLoop)
      {
          Dispatcher.BeginInvoke(() =>
          {
              //Run code on main thread.
              var r = Random.Range(0, 1.0f);
              var g = Random.Range(0, 1.0f);
              var b = Random.Range(0, 1.0f);
              image.color = new Color(r, g, b);
          });
          Thread.Sleep(1000);
      }
  })
  { IsBackground = true };
  thread.Start();
  ```
  
## Demo

- Demos in the path "MGS.Packages/Common/Demo/" provide reference to you.

## Source

- https://github.com/mogoson/MGS.CommonUtility.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com