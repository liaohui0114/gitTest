using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Data
{
    public class Table
    {
        public const string EQUIPMENT_MANIFEST = "EquipmentManifest";
        public const string CHAR_MANIFEST = "CharManifest";
        public const string WEAPON_MANIFEST = "WeaponManifest";
    }

    public class FileDataBase
    {
        public const string LOCAL_DB_SOURCE_PATH = "config/";

        private Dictionary<string, Collection> db;

        private static FileDataBase instance;

        public static FileDataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileDataBase();
                }
                return instance;
            }
        }

        private FileDataBase()
        {
            db = new Dictionary<string, Collection>();
        }

        public void Load(string collectionName)
        {
            string sourcePath = LOCAL_DB_SOURCE_PATH + collectionName;
            TextAsset xmlText = Resources.Load(sourcePath) as TextAsset;
            Debug.Assert(xmlText != null, "DB error: " + sourcePath + " not found!");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlText.text);
            XmlNode content = xmlDoc.SelectSingleNode("/content");

            Collection collection = new Collection();
            foreach (XmlNode node in content) //一行内所有的键值对
            {
                Document prop = new Document();
                string primaryKey = node.LocalName;
                string id = node.Attributes[primaryKey].Value;
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    string name = attribute.Name;
                    string value = attribute.Value;
                    prop.Add(name, value);
                }
                collection.primaryKey = primaryKey;
                collection.Insert(id, prop);
            }
            db.Add(collectionName, collection);

            xmlDoc = null;
        }

        public bool Exist(string collectionName)
        {
            return db.ContainsKey(collectionName);
        }

        public DocumentWrapper Find(string collectionName, string fieldName, string condition)
        {
            if (!Exist(collectionName))
            {
                Load(collectionName);
            }

            return new DocumentWrapper(db[collectionName].Find(fieldName, condition), collectionName, fieldName, condition);
        }

        public int Count(string collectionName)
        {
            if (Exist(collectionName))
            {
                return db[collectionName].Count();
            }

            return 0;
        }
    }
}
