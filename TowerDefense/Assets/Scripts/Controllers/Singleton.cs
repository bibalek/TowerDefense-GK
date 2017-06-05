using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T) FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError(string.Format("An instance of {0} is needed in the scene, but there is none.", typeof(T)));
                }
            }
            return instance;
        }
    }

    void CheckInstance(T thisInstance)
    {
        if (instance == null)
        {
            instance = thisInstance;
        }
        else if (instance != thisInstance)
        {
            DestroyImmediate(thisInstance);
        }
    }

    protected void Awake()
    {
        CheckInstance(this as T);
        Initiate();
    }

    protected virtual void Initiate() { }

}