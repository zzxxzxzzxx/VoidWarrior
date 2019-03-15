# -*- coding: utf-8 -*-
"""
记录登录
"""
import dbctrl.saveobject
import timecontrol
import pubcore
import pubdefines
import pubglobalmanager
import math
import json
import struct
from DAO.userDAO import userDAO
from DAO.rankDAO import rankDAO

class CDemo(dbctrl.saveobject.CSaveData):
    def GetKey(self):
        return "loginrecord"

    def Init(self):
        super(CDemo, self).Init()
        self.CheckTimer()

    def CheckTimer(self):
        timecontrol.Remove_Call_Out("loginrecord")
        pubdefines.FormatPrint("定时器统计：目前总连接记录 %s" % self.m_Data.get("total", 0))
        timecontrol.Call_Out(pubcore.Functor(self.CheckTimer), 300, "loginrecord")

    def NewItem(self):
        self.m_Data.setdefault("total", 0)
        self.m_Data["total"] += 1
        self.Save()

    def CalPos(self, oClient, dData):
        r1 = dData["radius1"]
        r2 = dData["radius2"]
        lPos1 = dData["pos1"]
        lPos2 = dData["pos2"]
        iDicstacne = int(math.sqrt(pow(lPos1[0]-lPos2[0],2) + pow(lPos1[1]-lPos2[1],2)))
        dReturn = {
            "action": "show",
            "flag" : iDicstacne <= r1+r2,
        }
        oClient.Send(dReturn)

    #登录操作
    def Login(self, oClient, dData):
        userName = dData["UserName"]
        inputPassWord = dData["UserPW"]
        user = userDAO()
        user.GetUserByUsername(userName)
        if user.result.GetUserName() != "": #判断是否存在用户
            if inputPassWord == user.result.GetPassword(): 
                #密码正确
                dReturn = {
                    "Return": "Success"  
                }
                dReturn = json.dumps(dReturn)
                dReturn = {
                    "Action": "Login",
                    "JsonData": dReturn
                }
            else: 
                #密码错误
                dReturn = {
                    "Return": "Fail"  
                }
                dReturn = json.dumps(dReturn)
                dReturn = {
                    "action": "Login",
                    "JsonData": dReturn
                }
        else: 
            #新建账号
            user.AddUser(userName, inputPassWord)
            dReturn = {
                "Return": "Success"  
            }
            dReturn = json.dumps(dReturn)
            dReturn = {
                "Action": "Login",
                "JsonData": dReturn
            }
        oClient.Send(dReturn)

    #获取排名
    def GetRank(self, oClient, dData):
        rank = rankDAO()
        rank.GetAllRank() #调用DAO中获取所有排名
        rankList = json.dumps(rank.result.GetRank()) #转换成字符串
        rankList = {
            "action": "GetRank",
            "JsonData": rankList
        }
        oClient.Send(rankList)
    
    #提交分数，更新排名
    def Submit(self, oClient, dData):
        score = dData["score"]
        name = dData["name"]
        rank = rankDAO()
        rank.SubmitRecord(name, score) #向DAO中提交分数
def Init():
    if pubglobalmanager.GetGlobalManager("demo"):
        return
    oManger = CDemo()
    pubglobalmanager.SetGlobalManager("demo", oManger)
    oManger.Init()

def Record():
    pubglobalmanager.CallManagerFunc("demo", "NewItem")

def OnCommand(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "CalPos", oClient, dData)

def OnLogin(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "Login", oClient, dData)

def OnGetRank(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "GetRank", oClient, dData)

def OnSubmit(oClient, dData):
    pubglobalmanager.CallManagerFunc("demo", "Submit", oClient, dData)


