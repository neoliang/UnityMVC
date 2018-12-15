using UnityEngine;
using System.Collections;

public class FrameWorkInit : MonoBehaviour {
    static FViewModelAgentMgr mgr;
    [SerializeField] GameObject prefab;
    private void Awake()
    {
        mgr = new FViewModelAgentMgr();
        mgr.Init();
        DontDestroyOnLoad(gameObject);
        Instantiate(prefab);
    }

}
