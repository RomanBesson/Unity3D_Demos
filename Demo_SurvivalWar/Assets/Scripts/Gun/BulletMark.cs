using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受击物体的枪痕，特效管理脚本
/// </summary>
[RequireComponent(typeof(ObjectPool))]                  //挂载该脚本的同时挂载对象池脚本
public class BulletMark : MonoBehaviour {

    private ObjectPool pool;                            //对象池.

    private Transform effectParent;                     //特效资源管理父物体.

    private Texture2D m_BulletMark;                     //弹痕贴图.
    private Texture2D m_MainTexture;                    //模型主贴图.（用于还原)
    private Texture2D m_MainTextureBackup;              //主贴图备份（用于显示以及贴图融合)

    [SerializeField]                                    //序列化，使得私有化数据在面板上也可以调试
    private MaterialType materialType;                  //材质
    private GameObject prefab_Effect;                   //弹痕特效.

    private Queue<Vector2> bulletMarkQueue = null;      //弹痕队列.

    [SerializeField] private int hp;                     //临时测试.生命值.  

    public int Hp 
    { 
        get { return hp; }
        set 
        { 
            hp = value;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start () {
        Init();
        InitGunMaterial();
	}

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        //如果是树木（树有多个材质球，取树干为主贴图）
        if(gameObject.name == "Conifer")
        {
            m_MainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().materials[2].mainTexture;
        }
        //其他只有一个贴图的
        else
        {
            m_MainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        }

        m_MainTextureBackup = GameObject.Instantiate<Texture2D>(m_MainTexture);
        //指定m_MainTextureBackup为显示的贴图
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = m_MainTextureBackup;
        bulletMarkQueue = new Queue<Vector2>();
        //对象池存在检测
        if (gameObject.GetComponent<ObjectPool>() != null)
        {
            pool = gameObject.GetComponent<ObjectPool>();
        }
        else
        {
            pool = gameObject.AddComponent<ObjectPool>();
        }
    }

    /// <summary>
    /// 初始化弹痕材质
    /// </summary>
    private void InitGunMaterial()
    {
        if (materialType == MaterialType.Stone)
        {
            //弹痕&弹痕特效以及对应的管理父物体的加载
            ResourcesLoad("Bullet Decal_Stone", "Bullet Impact FX_Stone", "Effect_Stone_Parent");
        }
        else if(materialType == MaterialType.Metal)
        {
            //弹痕&弹痕特效以及对应的管理父物体的加载
            ResourcesLoad("Bullet Decal_Metal", "Bullet Impact FX_Metal", "Effect_Metal_Parent");
        }
        else if (materialType == MaterialType.Wood)
        {
            //弹痕&弹痕特效以及对应的管理父物体的加载
            ResourcesLoad("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Wood_Parent");
        }
    }

    /// <summary>
    /// 弹痕&弹痕特效以及对应的管理父物体的加载
    /// </summary>
    private void ResourcesLoad(string bulletMark, string effect, string parent)
    {
        //加载枪痕贴图
        m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/" + bulletMark);
        //加载被击中特效
        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/" + effect);
        //设置对应的管理父物体
        effectParent = GameObject.Find("TempObject/" + parent).GetComponent<Transform>();
    }

    /// <summary>
    /// 弹痕融合.
    /// </summary>
    public void CreateBulletMark(RaycastHit hit)
    {
        //播放击中音效
        PlayAudios(hit);
       
        //textureCoord:贴图UV坐标点.（获取击打位置在主贴图上的位置）
        Vector2 uv = hit.textureCoord;
       
        //生成击碎特效
        PlayEffect(hit);
        
        //添加到弹痕队列
        bulletMarkQueue.Enqueue(uv);
        
        //宽度,横向,X轴.
        for (int i = 0; i < m_BulletMark.width; i++)
        {
            //高度,纵向.Y轴.
            for (int j = 0; j < m_BulletMark.height; j++)
            {
                //uv.x * 主贴图宽度- 弹痕贴图宽度/ 2 + i;
                float x = uv.x * m_MainTexture.width - m_BulletMark.width / 2 + i;

                //uv.y * 主贴图高度- 弹痕贴图高度/ 2 + j;               
                float y = uv.y * m_MainTexture.height - m_BulletMark.height / 2 + j;

                //获取到弹痕贴图上点的颜色.
                Color color = m_BulletMark.GetPixel(i, j);

                //主贴图位置融合弹痕贴图的颜色.(透明度高的像素点融合）
                if (color.a > 0.2f) m_MainTextureBackup.SetPixel((int)x, (int)y, color);
            }
        }
        m_MainTextureBackup.Apply();
        //2秒后清除弹痕
        Invoke("RemoveBulletMark", 2);
    }

    /// <summary>
    /// 移除弹痕.
    /// </summary>
    private void RemoveBulletMark()
    {
        if (bulletMarkQueue.Count > 0)
        {
            //要清楚弹痕的位置，同时清除队列里的
            Vector2 uv = bulletMarkQueue.Dequeue();

            for (int i = 0; i < m_BulletMark.width; i++) 
            {
                for (int j = 0; j < m_BulletMark.height; j++)
                {
                    float x = uv.x * m_MainTextureBackup.width - m_BulletMark.width / 2 + i;             
                    float y = uv.y * m_MainTextureBackup.height - m_BulletMark.height / 2 + j;

                    //使用没有弹痕的主图片给他填充
                    Color color = m_MainTexture.GetPixel((int)x, (int)y);
                    m_MainTextureBackup.SetPixel((int)x, (int)y, color);
                }

            }
            m_MainTextureBackup.Apply();

        }
    }

    /// <summary>
    /// 播放特效.
    /// </summary>
    private void PlayEffect(RaycastHit hit)
    {
        //储存当前使用的特效
        GameObject effect = null;
        //把特效加进对象池管理
        if (pool.Data())
        {
            effect = pool.GetObject();
            //调整生成位置
            effect.transform.position = hit.point;
            //调整生成方向
            effect.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            //在对应射线检测点的法线位置生成特效
            effect = GameObject.Instantiate<GameObject>(prefab_Effect, hit.point, Quaternion.LookRotation(hit.normal), effectParent);
            effect.name = "Effect_" + materialType; 
        }
        //延迟一会加入到对象池
        StartCoroutine(Delay(effect,1));
    }

    /// <summary>
    /// 播放三类击中音效.
    /// </summary>
    private void PlayAudios(RaycastHit hit)
    {
        switch (materialType)
        {
            case MaterialType.Metal:
                AudioManager.Instance.PlayAudioClipByName(ClipName.BulletImpactMetal, hit.point);
                break;
            case MaterialType.Stone:
                AudioManager.Instance.PlayAudioClipByName(ClipName.BulletImpactStone, hit.point);
                break;
            case MaterialType.Wood:
                AudioManager.Instance.PlayAudioClipByName(ClipName.BulletImpactWood, hit.point);
                break;
        }
    }

    /// <summary>
    /// 延迟一会加入到对象池
    /// </summary>
    /// <param name="go"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Delay(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);
    }
}
