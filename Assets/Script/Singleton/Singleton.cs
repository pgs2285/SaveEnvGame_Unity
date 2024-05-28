using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : Component
    // �̸� ���ؼ� T�� ComponentŬ���� Ȥ�� �̸� ��ӹ����͸� �����ϰ� ������
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null )
            {
                _instance = FindObjectOfType<T>(); // ���� �����ϴ� ��� ������Ʈ�� �˻��� ���ϴ� Ÿ���� ������Ʈ�� ã�Ƴ���.
                if(_instance == null) // ���࿡ ã�Ҵµ� ������ �ϳ� ������ش�.
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
