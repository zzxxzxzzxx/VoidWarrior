# -*- coding: utf-8 -*-
import sys
sys.path.append('..')
import pubglobalmanager
from DTO.user import user

#用户DAO
class userDAO():
    result = user("" ,"")
    
    #根据用户名获取用户
    def GetUserByUsername(self, userName):
        sSql = "select password from user where name = '%s'" % (userName) 
        result = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql)
        if result:
            self.result = user(userName, result[0][0])
        else:
            self.result = user("", "")

    #添加新用户
    def AddUser(self, userName, password):
        sSql = "insert into user values('%s', '%s')" % (userName, password) 
        pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql)
        
            