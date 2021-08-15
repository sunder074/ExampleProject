using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤 상속받아 사용
public class ObjectPoolManager : SingletonMonoBehaviour<ObjectPoolManager>
{
    // 이펙트의 경우 활성화되는 본인과 오브젝트간 최대거리
    const float MAX_ACTIVE_EFFECT_DISTANCE = 20f;

    // 오브젝트 풀링 리스트
    [SerializeField] List<ObjectPool> _objectPoolList;

    // 풀링된 전체 오브젝트를 저장하는 딕션어리
    // Key : 오브젝트 이름
    Dictionary<string, ObjectPool> _dicObjectPoolList = new Dictionary<string, ObjectPool>();

    private void Awake()
    {
        for(int i = 0; i < _objectPoolList.Count; i++)
        {
            _objectPoolList[i].CreateObject();
            _dicObjectPoolList.Add(_objectPoolList[i].name, _objectPoolList[i]);
        }
    }

    /// <summary>
    /// 풀링된 오브젝트를 사용하는 매서드
    /// </summary>
    /// <param name="objName">오브젝트의 이름(키값)</param>
    /// <param name="pos">활성화될 오브젝트의 위치</param>
    /// <param name="forward">오브젝트의 방향</param>
    /// <param name="isEffect">해당 오브젝트가 이펙트인지 여부</param>
    public void ActiveObject(string objName, Vector3 pos, Vector3 forward, bool isEffect = true)
    {
        // 본인 오브젝트가 있는지 체크
        if (GamePlayStatics.LocalPlayer == null)
            return;

        // 본인과 거리가 멀때 이펙트 x
        if (Vector3.Distance(GamePlayStatics.LocalPlayer.transform.position, pos) > MAX_ACTIVE_EFFECT_DISTANCE && isEffect)
            return;

        // 해당 오브젝트를 가지고있는지 체크
        if (_dicObjectPoolList.ContainsKey(objName))
        {
            _dicObjectPoolList[objName].UseObject(pos, forward);
        }
    }
}
