# -*- coding: utf-8  -*-
DBCTRL_MANAGER_NAME = "dbctrl"
DATABASE_NAME = "db_demo"


TABLE_GLOBAL = """
CREATE TABLE tbl_global
(
    rl_sName varchar(30) NOT NULL COMMENT '名字',
    rl_dmSaveTime datetime NOT NULL COMMENT '存档时间',
    rl_sData MEDIUMBLOB NOT NULL COMMENT '数据块',
    PRIMARY KEY (rl_sName)
)ENGINE=InnoDB default charset=utf8 comment='全局数据'
"""

USER = """
CREATE TABLE user 
(
  name varchar(30) NOT NULL COMMENT '用户名',
  password varchar(30) COMMENT '密码',
  PRIMARY KEY (name)
)ENGINE=InnoDB default charset=utf8 comment='用户'
"""

RANK = """
CREATE TABLE rank
(
  rank_id int(11) NOT NULL,
  user_name varchar(30),
  score int(11),
  PRIMARY KEY (rank_id)
)ENGINE=InnoDB default charset=utf8 comment='排名'
"""

TABLE_ALL = {
    "tbl_global":TABLE_GLOBAL,
    "user":USER,
    "rank":RANK
}