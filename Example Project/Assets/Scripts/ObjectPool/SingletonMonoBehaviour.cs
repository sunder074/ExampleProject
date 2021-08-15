using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	public static T Instance
	{
		get 
		{
			if(_instance == null)
			{
				_instance = (T) FindObjectOfType(typeof(T));
				if(_instance == null)
				{
					GameObject obj = new GameObject(typeof(T).ToString());
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance; 
		}
	}

	void OnApplicationQuit()
	{
		_instance = null;
		//Debug.Log ("OnApplicationQuit");
	}
        
	
}	

//public class EventManager : Singleton<EventManager>
//{
//}