# VoidWarrior
虚空战士
1. 服务端保存分数排行数据
2. 实现简单登录界面（任意输入账号与密码，当已经有此账号时直接登录到该账号，点击注册输入账号和密码即可注册
3. 登录时自动同步服务端数据到本地显示并开始游戏

## 操作说明
1. WASD控制前进方向
2. 左键控制攻击，右键控制视角旋转
## 玩法说明
1. 击倒怪物概率掉落增加攻击力的道具和增加时间与生命的道具
2. 击倒一个怪物获得10分，游戏结束统计得分进行排名
## 总结
1. 解决了点击移动不灵敏的问题
2. 完成了服务端与客户端之间的交互，使用json传递数据
3. 给服务端加了注释，并添加了数据层和数据模型层
### 有部分功能不完善：
2. 客户端数据模型全部使用public,因为json在转换字符串和类的时候需要进行引用，不知道具体细节，所以直接全部设为public，应部分可设置为private

#by znanLee
1. Instruct_Scene完成



