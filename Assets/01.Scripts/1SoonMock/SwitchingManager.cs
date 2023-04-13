using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchingManager : MonoBehaviour
{
    //사용법 : 스위칭 매니저는 1개, 깜빡이길 바라는 오브젝트에 SwitchingPlatform
    private List<SwitchingPlatform> _platforms;
    [SerializeField]private List<SwitchingPlatform> _aTypes= new List<SwitchingPlatform>()  ;
    [SerializeField]private List<SwitchingPlatform> _bTypes = new List<SwitchingPlatform>();
    public float delay;
    private void Awake()
    {
        _platforms = FindObjectsOfType<SwitchingPlatform>().ToList<SwitchingPlatform>();
        
        foreach(SwitchingPlatform st in _platforms)
        {
            if(st.thisType ==SwitchingPlatform.PlatformType.A)
            {
                _aTypes.Add(st);
            }else
            {
                _bTypes.Add(st);
            }
        }
        StartCoroutine(Switching());
    }

    IEnumerator Switching()
    {
        bool state = false;
        while(true)
        {

            foreach (SwitchingPlatform st in _aTypes)
            {
                st.SetState(state);
            }
            foreach (SwitchingPlatform st in _bTypes)
            {
                st.SetState(!state);
            }
            state = !state;
            yield return new WaitForSeconds(delay);

        }
    }
}
