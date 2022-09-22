using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;

public abstract class PerformanceAction 
{
    protected Entity actionCreator;
    protected List<Entity> actionAims;
    public abstract IEnumerator StartAction();
}
