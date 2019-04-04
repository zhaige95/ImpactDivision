
using UnityEngine;

public class C_Bullet : MonoBehaviour
{

    public string effectTag = "bulletBlue";
    public float speed = 150f;
    public bool isActive = false;
    public bool visible = true;
    public bool isLocal = true;

    public GameObject impactEffect;
    public LayerMask layerMask;

    public Attack attack;
    public RaycastHit hit;

    public float time = 2f;
    public Timer timer = new Timer();

    public void SetActive(Attack attack, bool local = true)
    {
        this.timer.Enter(time);
        this.attack = attack;
        this.isLocal = local;
        this.isActive = true;
    }

    public void SetVisible(bool isVisible)
    {
        transform.localScale = isVisible ? Vector3.one : Vector3.zero;
        visible = isVisible;
    }

}
