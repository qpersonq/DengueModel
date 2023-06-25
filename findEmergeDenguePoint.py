#coding:utf-8
import numpy as np
import matplotlib.pyplot as plt
import math
import pandas as pd
import matplotlib
ExcelFileName=input("BigTable>>");
data=pd.read_csv(ExcelFileName);
FidList=data['Fid'].tolist();
ConnectedPairNUMList=data['ConnectedPairNUM'].tolist();
PairFidList  =data['PairFid'].tolist();
ForwardFidList=[]
# make fid list
for i in range(len(PairFidList)):
	#print('process'+str(i));
	if(PairFidList[i]!=''):
		tmpfid=str(PairFidList[i]).split(';')
		for ss in range(len(tmpfid)-1) :
			ForwardFidList.append(int(tmpfid[ss]));
			#if(tmpfid[ss] not in ForwardFidList):
				#print(tmpfid[ss],end='')
				
# cal emerge
ForwardFidList = list( dict.fromkeys(ForwardFidList) )
NumEmerge=0;
for i in range(len(FidList)):
	#print(i);
	if((int(ConnectedPairNUMList[i])>0)   & (FidList[i] not in ForwardFidList) ):
		NumEmerge+=1
print("Number of Emerge:",end='');
print(NumEmerge);
'''
for i in range(len(FidList)):
 print(FidList[i],end=' ');
 print(ConnectedPairNUMList[i],end=' ');
 print('');
 '''