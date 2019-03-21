# VoidWarrior
虚空战士
1. 服务端保存分数排行数据
2. 实现简单登录界面（任意输入账号与密码，当已经有此账号时直接登录到该账号，如果没有此账号则直接创建一个账号并登录，行为如游客账号登录）
3. 登录时自动同步服务端数据到本地显示并开始游戏

## 操作说明
1. WASD控制前进方向
2. 左键控制攻击，右键控制视角旋转
## 总结
1. 解决了点击移动不灵敏的问题
2. 完成了服务端与客户端之间的交互，使用json传递数据
3. 给服务端加了注释，并添加了数据层和数据模型层
### 有部分功能不完善：
2. 客户端数据模型全部使用public,因为json在转换字符串和类的时候需要进行引用，不知道具体细节，所以直接全部设为public，应部分可设置为private

#by znanLee
1. 添加Scene_Instruct和相关素材
2. enum里，新添加SceneType:Instruct,GameState:Teaching,修改代码使正常显示
3. 修改代码，使得Scene_Instruct可以正常加载
4. Menu scene添加了进入Instruct scene的button
5. 和zzxxzxzzxx-registe分支合并


