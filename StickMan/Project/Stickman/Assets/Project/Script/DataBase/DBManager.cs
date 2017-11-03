#define __USE_FILE_DATABASE__

using UnityEngine;
using System.Collections;

namespace Data
{
    public static class DBManager
    {
 
        public static DocumentWrapper Find(string collectionName, string fieldName, string fieldValue)
        {
#if __USE_FILE_DATABASE__
            return FileDataBase.Instance.Find(collectionName, fieldName, fieldValue);
#endif
        }

    }
}
