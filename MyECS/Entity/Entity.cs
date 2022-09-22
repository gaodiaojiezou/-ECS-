using System;
using System.Collections.Generic;
using ECS.Component;
using UnityEngine;
namespace ECS.EcsEntity
{
    public class Entity : IEquatable<Entity>,IComparable<Entity>
    {
        public int Index;
        public Dictionary<string,IComponentData> _components = new  Dictionary<string,IComponentData>();
        public int CompareTo(Entity other)
        {
            return Index - other.Index;
        }
        public bool Equals(Entity entity)
        {
            return entity.Index == Index ;
        }
        public override int GetHashCode()
        {
            return Index;
        }

    }
}