using System.Collections.Generic;

namespace Data
{
    public class DocumentWrapper : Document
    {
        public string tableName;
        public string fieldName;
        public string fieldValue;

        public DocumentWrapper() { }

        public DocumentWrapper(Document properties, string tableName, string fieldName, string fieldValue) 
            : base(properties)
        {
            this.tableName = tableName;
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
        }

        public void Redirect()
        {
            //this.dict = FileDataBase.Instance.Find(tableName, fieldName, fieldValue).dict;
        }

        public int TableCount()
        {
            return FileDataBase.Instance.Count(tableName);
        }
    }

    public class Document
    {
        protected Dictionary<string, string> dict;

        public Document()
        {
            dict = new Dictionary<string, string>();
        }

        protected Document(Document prop)
        {
            this.dict = prop.dict;
        }

        public void Add(string key, string value)
        {
            dict.Add(key, value);
        }

        public int GetIntValue(string key)
        {
            if (dict.ContainsKey(key)) 
            {
                return int.Parse(dict[key]);
            }
            return 0;
        }

        public float GetFloatValue(string key)
        {
            if (dict.ContainsKey(key)) 
            {
                return float.Parse(dict[key]);
            }
            return 0;
        }

        public string GetStringValue(string key)
        {
            if (dict.ContainsKey(key)) 
            {
                return dict[key];
            }
            return "";
        }
    }
}
