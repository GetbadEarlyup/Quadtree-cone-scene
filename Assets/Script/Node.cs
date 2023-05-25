
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class Node
    {
        public Bounds bound;//包围盒
        public int myDepth;//当前层数
        public Tree tree;
        public List<ObjData> datas = new List<ObjData>();//集合列表
        public Node[] childs;

        public Vector2[] bit = new Vector2[] //分割点
        {
            new Vector2(-1,1),
            new Vector2(1,1),
            new Vector2(-1,-1),
            new Vector2(1,-1)
        };

        public Node(Bounds bound,int myDepth,Tree tree) 
        {
            this.bound = bound;
            this.myDepth = myDepth;
            this.tree = tree;
        }

        public void InstanceData(ObjData data)
        {
            if (myDepth<tree.maxDepth&&childs==null)//判定 小于树的最大深度 子集为空
            {
                CreatChild();//创建 子树
            }

            if (childs!=null)//判断子节点不为空
            {
                for (int i = 0; i < childs.Length; i++)//遍历所有子节点
                {
                    if (childs[i].bound.Contains(data.pos))//判断子节点的包围盒 是否在里面
                    {
                        childs[i].InstanceData(data);//循环创建放入下一级
                        break;//跳出
                    }
                }
            }
            else//判空就放入
            {
                datas.Add(data);//放入集合列表中 等待空的包围盒
            }
            
        }

        private void CreatChild()
        {
            childs = new Node[tree.maxChildCount];//创建四叉树
            for (int i = 0; i < tree.maxChildCount; i++)//循环遍历
            {
                //计算分割点 包围盒中心点位置的点位坐标
                Vector3 center = new Vector3(bit[i].x*bound.size.x/4,0,bit[i].y*bound.size.z/4);
                
                //计算盒体尺寸大小  size.x 为盒体的宽度 size.x 为盒体的宽度
                Vector3 size = new Vector3(bound.size.x/2,0,bound.size.z/2);
                
                //创建包围盒 分割点+包围盒中心点 ，size该盒体的总大小
                Bounds childbound = new Bounds(center+bound.center,size);
                
                //创建节点 一个节点对应一个子树
                childs[i] = new Node(childbound,myDepth+1,tree);
            }
        }

        //绘制盒
        public void DrawBound()
        {
            if (datas.Count!=0)//判断有东西 蓝盒
            {
                Gizmos.color=Color.blue;
                Gizmos.DrawWireCube(bound.center,bound.size-Vector3.one*0.1f);
            }
            else//没东西 绿盒
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(bound.center,bound.size-Vector3.one*0.1f);
            }

            if (childs!=null)//如果子节点不为空
            {
                for (int i = 0; i < childs.Length; i++)
                {
                    childs[i].DrawBound(); //递归调用
                }
            }
        }

        public void TriggerMove(Plane[] planes)//视锥体剔除
        {
            if (childs!=null)//如果子节点不为空
            {
                for (int i = 0; i < childs.Length; i++)//便利所有子节点对象
                {
                    childs[i].TriggerMove(planes);//递归下一节点计算
                }
            }

            for (int i = 0; i < datas.Count; i++)//便利所有集合列表
            {
                datas[i].prefab.SetActive(GeometryUtility.TestPlanesAABB(planes,bound));//控制对象预制体开关
            }
        }
        
    }
