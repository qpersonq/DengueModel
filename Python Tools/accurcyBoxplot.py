#coding:utf-8
import numpy as np
import matplotlib.pyplot as plt
import math
import pandas as pd
import matplotlib
import seaborn as sns
plt.rcParams['font.sans-serif']=['mingliu'] #用來正常顯示中文標籤
plt.rcParams['axes.unicode_minus']=False #用來正常顯示負號
modelnames=["使用環境因子","使用位置因子","放大時空半徑"]
xlabel="日預測率 (%)";
ylabel="密度 (case/(km^2 day))";
ColorSeriesForChart=['red','lime','blue','g','c','m'];
print("Please input Excel Files");
ExcelFileName=input("Day Accuracy file>>");

tmpExcelContainer=pd.read_excel(ExcelFileName);
enva=tmpExcelContainer['ENVA'].tolist();
for idx in range(0,len(enva)):
	enva[idx]=enva[idx]*100
	

envd=tmpExcelContainer['ENVD'].tolist();

posa=tmpExcelContainer['POSA'].tolist();
for idx in range(0,len(posa)):
	posa[idx]=posa[idx]*100
posd=tmpExcelContainer['POSD'].tolist();

enla=tmpExcelContainer['ENLA'].tolist();
for idx in range(0,len(enla)):
	enla[idx]=enla[idx]*100
enld=tmpExcelContainer['ENLD'].tolist();

BigAccuArr=[enva,posa,enla]
BigDenArr=[envd,posd,enld]

df1=pd.DataFrame({modelnames[0]: enva,modelnames[1]: posa,modelnames[2]: enla})
plt.figure(figsize=(5,5))
sns.boxplot( data=df1,palette=ColorSeriesForChart,width=.5,fliersize=0.0)
plt.ylabel(xlabel,fontsize=13)
plt.xlabel("模式",fontsize=13)
plt.ylim(-1, 110)
plt.savefig(str(ExcelFileName)+"ACC"+".svg")
plt.show()

df1=pd.DataFrame({modelnames[0]: envd,modelnames[1]: posd,modelnames[2]: enld})
plt.figure(figsize=(5,5))
sns.boxplot( data=df1,palette=ColorSeriesForChart,width=.5,fliersize=0.0)
plt.ylabel(ylabel,fontsize=13)
plt.xlabel("模式",fontsize=13)
#plt.ylim(-1, 100)
plt.savefig(str(ExcelFileName)+"DEN"+".svg")
plt.show()

'''
plt.scatter(enla,enld, c=ColorSeriesForChart[2] )
plt.scatter(posa,posd, c=ColorSeriesForChart[1] )
plt.scatter(enva,envd, c=ColorSeriesForChart[0] )
plt.xlabel(xlabel,fontsize=13)
plt.xlim(-1, 110)

plt.ylabel(ylabel,fontsize=13)
plt.savefig(str(ExcelFileName)+".svg")
plt.show()
'''