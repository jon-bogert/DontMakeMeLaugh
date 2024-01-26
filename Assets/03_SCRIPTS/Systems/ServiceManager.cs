using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _services = new GameObject[0];

    ServiceManager other = null;

    private void Awake()
    {
        ServiceManager[] others = FindObjectsOfType<ServiceManager>();
        if (others.Length > 1)
        {
            foreach (ServiceManager sm in others)
            {
                if (sm != this)
                {
                    other = sm;
                }
            }
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        if (other != null)
            return;

        for (int i = 0; i < _services.Length; ++i)
        {
            GameObject obj = Instantiate(_services[i], transform);
            DontDestroyOnLoad(obj);
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (other != null)
        {
            return other.Get<T>();
        }

        T result;
        for (int i = 0; i < _services.Length; ++i)
        {
            result = _services[i].GetComponent<T>();
            if (result != null)
                return result;
        }
        return null;
    }
}
