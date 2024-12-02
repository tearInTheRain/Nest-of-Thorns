using UnityEngine;

public class Rock : MonoBehaviour
{
     public float speed = 10f;  // 投射物飞行速度
    public float lifetime = 5f; // 投射物的最大生存时间
    public Transform target; // 目标
    public bool useRotationAnimation = true;  // 是否使用旋转动画（朝向目标旋转）

    private Vector3 direction;  // 飞行方向
    private float timeAlive = 0f; // 投射物存活时间

    public delegate void OnHitTarget(); // 定义碰撞后的行为委托
    public OnHitTarget onHitTarget;  // 设置碰撞后的策略（爆炸、消失等）

    private Animator animator; // 动画控制器

    void Start()
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized; // 计算飞行方向
        }

        if (useRotationAnimation && TryGetComponent(out animator))
        {
            animator.SetTrigger("Fly");
        }

        // 设置投射物的生命周期
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }

        timeAlive += Time.deltaTime;

        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void MoveTowardsTarget()
    {
        // 持续朝目标飞行
        transform.position += direction * speed * Time.deltaTime;

        // 如果启用了旋转动画，则将投射物的旋转朝向目标
        if (useRotationAnimation && target != null)
        {
            RotateSelf();
        }
    }

    void RotateSelf()
    {
        // 让物体在飞行中绕 Z 轴自旋
        float spinSpeed = 200f; // 自旋速度，单位是度/秒
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime); // 每帧旋转一定角度
    }

    void RotateTowardsTarget()
    {
        // 计算目标的方向
        Vector3 targetDirection = target.position - transform.position;
        
        // 只考虑 2D 游戏中的 X 和 Y 轴
        targetDirection.z = 0; // 保证我们不考虑 Z 轴的影响

        if (targetDirection.magnitude > 0.1f)
        {
            // 计算目标角度
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            
            // 平滑旋转朝向目标角度
            float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * 5f); // 5f 是旋转的平滑速度
            
            // 应用旋转，只旋转 Z 轴
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Weapon") {
            if (onHitTarget != null)
            {
                onHitTarget.Invoke();  // 调用自定义的碰撞后策略
            }
            else
            {
                if(other.tag == "Monster") {
                    MonsterBase monsterBase = other.gameObject.GetComponent<MonsterBase>();
                    monsterBase.addHp(-10);
                }
                // 默认策略：直接消失
                Destroy(gameObject);
            }
        }
    }
    
}
