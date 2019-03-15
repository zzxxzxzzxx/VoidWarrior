# -*- coding: utf-8 -*-
import sys
sys.path.append('..')
import pubglobalmanager
from DTO.rank import rank

class rankDAO():
    result = rank([])
    maxNum = 10 #排名数量

    #获取所用排名
    def GetAllRank(self):
        rankList = []
        i = 0
        while (i < self.maxNum):
            sSql = "select * from rank where rank_id = '%d'" % (i)
            result = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql)
            for row in result:
                rankItem ={
                    "user_name" : row[1],
                    "score" : row[2]
                } 
                rankList.append(rankItem)
            i = i + 1        

        self.result.SetRank(rankList)

    #提交分数
    def SubmitRecord(self, name, score):

        sSql =  "select * from rank"
        result = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql)

        if not result: 
            self.CreatRank()
            sSql =  "select * from rank"
            result = pubglobalmanager.CallManagerFunc("dbctrl", "Query", sSql)

        i = 0
        while (i < self.maxNum):
            if score > result[i][2] or result[i][2] is None:
                tempN = result[i][1]
                tempS = result[i][2]
                sSql = "update rank set user_name = '%s', score = '%d' where rank_id = '%s'" %(name, score, i)
                pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql)
                name = tempN
                score = tempS
            i = i + 1

    #创建排名列表，初始值为-1
    def CreatRank(self):
        i = 0
        while (i < self.maxNum):
            sSql = "insert into rank values('%d', '%s', '%d')" % (i, "None", -1) 
            pubglobalmanager.CallManagerFunc("dbctrl", "ExecSql", sSql)
            i = i + 1

