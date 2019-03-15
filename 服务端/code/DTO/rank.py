# -*- coding: utf-8 -*-

#排名据模型
class rank():
    def __init__(self, rankList):
        self.rankList = rankList #排名列表

    #排名Get、Set方法
    def GetRank(self):
        return self.rankList

    def SetRank(self, rankList):
        self.rankList = rankList