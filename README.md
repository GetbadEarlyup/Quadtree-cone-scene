# Quadtree cone scene
 这是一个unity四叉树场景视锥体剔除的Demo

四叉树

每一层有一个根节点，然后分出四个子对象，美四个对象为一层

本案例中我们规定他的最大深度，也就是6层，每层分四个子节点对象

![20230525_135659](C:\Users\27797\Pictures\Screenshots\20230525_135659.jpg)

然后每个节点存数据，以根节点为基础，每次迭代分四叉，没数据就不管了，有才放数据，一直分到第六曾

每层保存数据：

ID标识 、预制体对象、坐标点、欧拉角

包围盒绘制：蓝绿盒

![20230525_140812](C:\Users\27797\Pictures\Screenshots\20230525_140812.jpg)

每次将空间四分，依照四叉树规则顺序下分，如图示例：

![20230525_141144](D:\ApowerREC\20230525_141144.gif)

然后每次能根据四分，获知盒体大小，进行绘制

视锥体剔除：

![20230525_141720Edit](D:\ApowerREC\20230525_141720Edit.gif)

通过unity相机获得六面，进行剔除，具体详见代码

CS:Node.TriggerMove
