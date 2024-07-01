using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public sbyte amountToPool;
    public bool shouldExpand;

}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool; //Objetos que se van a instanciar.
    public List<GameObject> pooledObjects;  //Lista de objetos instanciados previamente.

    private void Awake()
    {
        // Configurar el Singleton
        if (SharedInstance != null && SharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        SharedInstance = this;
        //DontDestroyOnLoad(gameObject);  // No destruir al cambiar de escena

        // Inicializar el pool
        InitializePool();
    }

    private void InitializePool()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool) 
        { 
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObjects (string tag)
    {
        for (int i=0; i<pooledObjects.Count;i++) //Si existe un objeto en la lista pooled con visibilidad falsa, lo devuelve.
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool) //si no existe objeto disponible en la pooled, agrega una nueva instancia en objectToPool y lo devuelve para ser usado.
        {
            if (item.objectToPool.CompareTag(tag))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

}
