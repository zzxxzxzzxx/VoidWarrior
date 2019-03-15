# -*- coding: utf-8 -*-

#用户数据模型
class user():
    def __init__(self, userName, password):
        self.userName = userName #用户名
        self.password = password #密码

    #用户名Get、Set方法
    def GetUserName(self):
        return self.userName

    def SetUserName(self, userName):
        self.userName = userName

    #密码Get、Set方法
    def GetPassword(self):
        return self.password

    def SetPassword(self, password):
        self.password = password
