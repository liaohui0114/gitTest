using System.Collections.Generic;
using System;
using UnityEngine;

namespace  Data
{
    public class Collection
    {
        private Dictionary<string, Document> collection;

        public string primaryKey;

        public Collection()
        {
            collection = new Dictionary<string, Document>();
        }

        public void Insert(string key, Document prop)
        {
            collection.Add(key, prop);
        }

        public Document Find(string fieldName, string condition)
        {
            Debug.Assert(primaryKey == fieldName, "DB Error: Invaild field name.");
            //int key = Convert.ToInt32(condition);
            Debug.Assert(collection.ContainsKey(condition), "DB Error: Invaild key.");
            return collection[condition];
        }

        public int Count()
        {
            return collection.Count;
        }
    }
}
