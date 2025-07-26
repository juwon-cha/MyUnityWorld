using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                // 해당 컴포넌트를 가지고 있는 게임 오브젝트를 찾아서 반환
                _instance = (T)FindAnyObjectByType(typeof(T));

                if(_instance == null)
                {
                    // 새로운 게임 오브젝트를 생성하여 해당 컴포넌트를 추가
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));

                    // 생성된 게임 오브젝트에서 해당 컴포넌트를 instance에 저장
                    _instance = obj.GetComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        // 해당 오브젝트가 자식 오브젝트라면
        if (transform.parent != null && transform.root != null)
        {
            // 부모 오브젝트를 DontDestroyOnLoad 처리
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            // 해당 오브젝트가 최상위 오브젝트라면 자신을 DontDestroyOnLoad 처리
            DontDestroyOnLoad(this.gameObject); 
        }
    }
}
