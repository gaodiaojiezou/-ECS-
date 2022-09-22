using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
public abstract class DataAction 
{
    protected Entity actionCreator;
    protected List<Entity> actionAims;
    public abstract IEnumerator StartAction(); 
}
