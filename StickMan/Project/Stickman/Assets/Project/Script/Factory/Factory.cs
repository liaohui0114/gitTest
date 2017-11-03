using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Data;
using System;

public abstract class Factory<T> where T:ICloneable
{
    protected string CollectionName;

    protected Factory(string collection)
    {
        this.CollectionName = collection;
    }

    protected Dictionary<string, T> pool = new Dictionary<string, T>();

    protected abstract T Build(DocumentWrapper property);


    public T Create(string id)
    {
        if (!pool.ContainsKey(id))
        {
            DocumentWrapper property = FileDataBase.Instance.Find(this.CollectionName, "id", id);
            T charPose = Build(property);
            pool.Add(id, charPose);
        }
        return (T)pool[id].Clone();
    }
}
