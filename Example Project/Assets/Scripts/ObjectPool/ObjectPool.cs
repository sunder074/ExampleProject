using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    // 풀링할 오브젝트
    [SerializeField] GameObject _obj;

    // 처음 생성될 오브젝트 개수
    [SerializeField] int _maxCount;

    // 해당 오브젝트의 리스트
    List<GameObject> _objectList = new List<GameObject>();

    // 현재까지 활성화된 오브젝트 리스트의 인덱스 위치
    private int _index = 0;


    // 씬이 시작하고 해당 오브젝트들 미리 생성
    public void CreateObject()
    {
        if (_obj != null)
        {
            // 지정한 개수맘큼 오브젝트 생성
            for (int i = 0; i < _maxCount; i++)
            {
                GameObject targetObj = Instantiate(_obj, transform);
                _objectList.Add(targetObj);
                targetObj.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 오브젝트를 해당위치, 방향으로 활성화하는 매서드
    /// </summary>
    /// <param name="pos">활성화될 오브젝트의 위치</param>
    /// <param name="forward">오브젝트의 방향</param>
    public void UseObject(Vector3 pos, Vector3 forward)
    {
        int count = 0;

        for (count = 0; count <= _objectList.Count; count++) 
        {
            // 오브젝트 리스트 전체를 돌았는데 사용할 수 있는 오브젝트가 없을때 새로 생성후 풀링 리스트에 넣어준다
            if(count >= _objectList.Count)
            {
                GameObject targetObj = Instantiate(_obj, transform);
                _objectList.Add(targetObj);
                targetObj.SetActive(false);

                // 새로 생성된 오브젝트의 인덱스로 설정
                _index = _objectList.Count - 1;
                break;
            }

            // 해당 오브젝트가 이미 사용중일때 인덱스를 1 올리고 다음 오브젝트를 체크
            if (_objectList[_index].activeInHierarchy)
            {
                _index++;

                // 인덱스가 오브젝트 리스트의 크기를 넘어갈때 인덱스를 0으로 설정
                if(_objectList.Count <= _index)
                {
                    _index = 0;
                }
            }

            // 해당 오브젝트를 사용가능할때 break;
            else
            {
                break;
            }
        }

        // 사용 가능한 오브젝트를 켜주고 위치 및 방향 설정 후 켜준다
        _objectList[_index].transform.position = pos;
        if (!forward.Equals(Vector3.zero))
        {
            _objectList[_index].transform.forward = forward;
        }

        _objectList[_index].SetActive(true);
    }
}
