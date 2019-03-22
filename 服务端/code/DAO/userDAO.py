# -*- coding: utf-8 -*-
import sys
sys.path.append('..')
import pubglobalmanager
from DTO.user import user


class userDAO():
    result = user("" ,"")
    
    
    def GetUserByUsername(self, userName):
        sSql = "select password from user where name = '%s'" % (userName) 
        result = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql)
        if result:
            self.result = user(userName, result[0][0])
        else:
            self.result = user("", "")

    
    def AddUser(self, userName, password):
        sSql = "insert into user values('%s', '%s')" % (userName, password) 
        pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql)
        
            