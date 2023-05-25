using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatCube : MonoBehaviour
{
    public GameObject cube;//对象

    public Bounds mainBound;//包围盒
    private Tree tree;//树

    private bool startEnd = false;//控制结束
    public Camera cam;//相机
    private Plane[] _planes;//存储视锥体六个面
    
    void Start()
    {
        _planes = new Plane[6];//初始化
        Bounds bounds = new Bounds(transform.position,new Vector3(100,0,100));//生成包围盒
        tree = new Tree(bounds);//初始化行为树

        for (int x = -50; x < 50; x++)//随机生成对象放到树中
        {
            for (int z = -50; z < 50; z++)
            {
                if (Random.Range(0,10)<1)
                {
                    GameObject c = Instantiate(cube,transform);
                    c.transform.position = new Vector3(x,0,z);
                    c.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                    
                    tree.Instance(new ObjData(c,c.transform.position,c.transform.eulerAngles));
                }
            }
        }

        startEnd = true;

    }

   
    void Update()
    {
        if (startEnd)
        {
            //获取摄像机视锥体六个面 
            GeometryUtility.CalculateFrustumPlanes(cam, _planes);
            tree.TriggerMove(_planes);//传六个面
        }
    }

    private void OnDrawGizmos()
    {
        if (startEnd)
        {
            tree.DrawBound();//开始绘制包围盒 用树组绘制盒
        }
        else
        {
            //初始化盒体
            //使用 center 和 size 绘制一个线框盒体。
            //mainBound.center中心节点   盒体大小
            Gizmos.DrawWireCube(mainBound.center, mainBound.size);
        }
    }
}
