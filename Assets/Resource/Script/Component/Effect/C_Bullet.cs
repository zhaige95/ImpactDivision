
using UnityEngine;

public class C_Bullet : MonoBehaviour
{

    public string effectTag = "bulletBlue";
    public float speed = 150f;
    public bool isActive = false;

    public GameObject impactEffect;
    public LayerMask layerMask;

    public Attack attack;
    public RaycastHit hit;

    public float time = 2f;
    public Timer timer = new Timer();

    public void SetActive(Attack attack)
    {
        this.timer.Enter(time);
        this.attack = attack;
        this.isActive = true;
    }


}
