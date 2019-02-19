
using UnityEngine;

public class C_Effect : MonoBehaviour {

    public string effectTag = "Tag Name";
    public bool isActive = false;
    public float time = 2f;
    public Timer timer = new Timer();
    public string useModel = "normal"; // "normal" : 重复生成  "one": 只生产一个，重复利用
}
