#coding:utf-8
import numpy as np
import matplotlib.pyplot as plt
import math
import pandas as pd
import matplotlib
plt.rcParams['font.sans-serif']=['mingliu'] #用來正常顯示中文標籤
plt.rcParams['axes.unicode_minus']=False #用來正常顯示負號
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


plt.scatter(enla,enld, c=ColorSeriesForChart[2] )
plt.scatter(posa,posd, c=ColorSeriesForChart[1] )
plt.scatter(enva,envd, c=ColorSeriesForChart[0] )
plt.xlabel(xlabel,fontsize=13)
plt.xlim(-1, 110)

plt.ylabel(ylabel,fontsize=13)
plt.savefig(str(ExcelFileName)+".svg")
plt.show()