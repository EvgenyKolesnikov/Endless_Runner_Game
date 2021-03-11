using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public List<GameObject> Pool;                   //сюда кладутся префабы
    public List<GameObject> PooledObject;          //сюда инициализируются объекты
    public static Queue<GameObject> Stages_Queue; 

    void Start()
    {
        Init(); 
    }

    
    private void Init()
    {
        GameObject tmp;
        Stages_Queue = new Queue<GameObject>();
        Stages_Queue.Enqueue(transform.GetChild(0).gameObject);

        for (int i = 0; i < Pool.Count; i++)
        {
            tmp = Instantiate(Pool[i], gameObject.transform);
            tmp.SetActive(false);
            PooledObject.Add(tmp);
        }
    }

    public void GetObject(Vector3 position, Quaternion rotation) 
        {
        
        int Index_Object = Random.Range(0, Pool.Count);
        GameObject tmp;
        
        while(PooledObject[Index_Object].activeInHierarchy) //если объект уже создан, берет соседний
        {
           if (Index_Object > 0)
            {
                Index_Object--;
            }
           else
            {
                Index_Object++;
            }
        }
        Stages_Queue.Enqueue(PooledObject[Index_Object]);

        PooledObject[Index_Object].SetActive(true);
        PooledObject[Index_Object].transform.position = position;
        PooledObject[Index_Object].transform.rotation = rotation;
    }

  
    public void ReturnObject() 
    {
        GameObject tmp;
        tmp = Stages_Queue.Dequeue();
        tmp.SetActive(false);
    }  
}




