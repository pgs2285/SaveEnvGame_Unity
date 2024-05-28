using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : Component
    // 이를 통해서 T가 Component클래스 혹은 이를 상속받은것만 가능하게 보장함
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null )
            {
                _instance = FindObjectOfType<T>(); // 씬에 존재하는 모든 오브젝트를 검색해 원하는 타입의 오브젝트를 찾아낸다.
                if(_instance == null) // 만약에 찾았는데 없으면 하나 만들어준다.
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof( T ).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
