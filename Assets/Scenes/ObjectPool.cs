using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public class cObjectData
    {
        //public Object src;
        public GameObject go;
        public bool bUsing;
    }
    public List<List<cObjectData>> mObjects;
    public List<Object> mSrcObjectData;
    private void Awake()
    {
        mObjects = new List<List<cObjectData>>();
        mSrcObjectData = new List<Object>();
    }
    public int InitObjectsInPool(Object o,int iCount) 
    {
        List<cObjectData> pList = new List<cObjectData>();
        int i = 0;
        for (i=0;i<iCount;i++)
        {
            cObjectData data = new cObjectData();
            data.go=Instantiate(o) as GameObject;
            data.go.SetActive(false);
            data.bUsing = false;
            pList.Add(data);
        }
        mSrcObjectData.Add(o);
        mObjects.Add(pList);
        int iList = mObjects.Count - 1;
        return iList;
    }
    public GameObject LoadObjectPool(int iList)
    {
        if (iList < 0 || iList >= mObjects.Count) 
        {
            return null;
        }
        List<cObjectData> pList= mObjects[iList];
        int iCount = pList.Count;
        for (int i=0;i<iCount;i++)
        {
            if (pList[i].bUsing==false)
            {
                pList[i].bUsing = true;
                return pList[i].go;
            }
        }
        //full
        Object o= mSrcObjectData[iList];
        cObjectData data = new cObjectData();
        data.go = Instantiate(o) as GameObject;
        data.go.SetActive(false);
        data.bUsing = true;
        pList.Add(data);
        return data.go;
        //return null;
    }
    public void ClearObject() 
    {
        mObjects.Clear();
    }

    public void UnloadObjectToPool(int iList,GameObject go )
    {
        if (iList < 0 || iList >= mObjects.Count)
        {
            return;
        }
        List<cObjectData> pList = mObjects[iList];
        int iCount = pList.Count;
        for (int i = 0; i < iCount; i++)
        {
            if (pList[i].bUsing == false)
            {
                pList[i].go.SetActive(false);
                pList[i].bUsing = false;
                return;
            }
        }
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
