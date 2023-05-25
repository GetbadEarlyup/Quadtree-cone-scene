
    using System;
    using UnityEngine;

    public class Tree
    {
        public Bounds bound;//包围盒
        private Node root;//根节点
        public int maxDepth = 6;//最大深度 层级
        public int maxChildCount = 4;//最大 子节树

        public Tree(Bounds bound)//创建树
        {
            this.bound = bound;
            this.root = new Node(bound,0,this);//创建节点
        }

        //插入数据
        public void Instance(ObjData data)
        {
            root.InstanceData(data);//放到根节点
        }

        public void DrawBound()
        {
            root.DrawBound();//根节点调用绘制盒
        }

        public void TriggerMove(Plane[] planes)
        {
            root.TriggerMove(planes);//根节点传递 六个视锥体面
        }

    }
