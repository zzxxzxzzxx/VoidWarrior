# -*- coding: utf-8 -*- 

import pubdefines
import mutexlock
import json
import pubglobalmanager

class CCommandManager:
    def __init__(self):
        self.InitCommand()
    
    def InitCommand(self):
        self.m_CommandDict = {
        "updatepos" : ("demo","OnCommand"),
        "Login" : ("demo","OnLogin"),
        "GetRank" : ("demo", "OnGetRank"),
        "Submit" : ("demo", "OnSubmit"),
        "Registe":("demo","OnRegiste") ##Register optional qsg
        }

    def TrasnData(self, obj):
        if isinstance(obj, dict):
            res = {}
            for key, value in obj.iteritems():
                tempkey = self.TrasnData(key)
                tempvalue = self.TrasnData(value)
                res[tempkey] = tempvalue
            return res
        elif isinstance(obj, list):
            res = []
            for value in obj:
                res.append(self.TrasnData(value))
            return res
        elif isinstance(obj, unicode):
            return obj.encode("utf-8")
        else:
            return obj

    @mutexlock.AutoLock("oncommand")
    def OnCommand(self, oClient, dData):
        sAction = dData.get("Action", "")
        dData = dData.get("JsonData")
        dData = json.loads(dData, encoding="utf-8")
        dData = self.TrasnData(dData)
        if not sAction or not sAction in self.m_CommandDict:
            pubdefines.FormatPrint("未定义客户端的调用%s" % sAction)
            return 
        sImport, sFunc = self.m_CommandDict[sAction]
        oModule = __import__(sImport)
        oFunc = getattr(oModule, sFunc, None)
        if not oFunc:
            pubdefines.FormatPrint("客户端触发%s行为，%s模块未找到接口%s" % (sAction, sImport, sFunc))
            return
        oFunc(oClient, dData)