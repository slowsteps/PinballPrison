using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TargetGroupEffect : MonoBehaviour
{

[HideInInspector]
public List<Target> targets;

abstract public void AddTarget(Target inTarget);

abstract public void ReportTargetHit(Target inTarget);

}


