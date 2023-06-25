#coding:utf-8
import numpy as np
import matplotlib.pyplot as plt
import math
import pandas as pd
import matplotlib
import seaborn as sns
plt.rcParams['font.sans-serif']=['mingliu'] #用來正常顯示中文標籤
plt.rcParams['axes.unicode_minus']=False #用來正常顯示負號





def  MultiBoxPlot(ExcelFl,SheetName,BoxFileName,modelnames,Columntag,LineLowerBound,LineUpperBound):
	ColorSeriesForChart=['#FFC000','#00B050','#0000FF','#FF0000']
	#min([], default="EMPTY")
	ExcelContainer=pd.read_excel(ExcelFl,sheet_name=SheetName)
	DatalineContainer=[];
	for ctg in Columntag:
		DatalineContainer.append(ExcelContainer[ctg].tolist()[LineLowerBound:LineUpperBound]);
		
	tmpdic={};
	for i in range(len(modelnames)):
		tmpdic[modelnames[i]]=DatalineContainer[i]

	df1=pd.DataFrame(tmpdic)

	plt.figure(figsize=(2,3))
	
	sns.boxplot( data=df1,width=.8,palette=ColorSeriesForChart,fliersize=0.0)
	plt.ylabel(ylabel,fontsize=12)
	plt.xlabel(xlabel,fontsize=12)

	plt.tight_layout()
	if(str(SheetName)!="Density"):
		plt.ylim(0, 1)
	plt.savefig(str(BoxFileName)+".svg")
	plt.show()
	
	
modelnames=["一","二","三","四"]
Columntag=["SP","HIST","POS","ENV"]
xlabel="";
ylabel="";
ExcelFileName=input("Day Accuracy file>>");
SheetNames=["F1-score","Fixed recall","Fixed precision","Density"];
#SheetNames=["F1-score","Fixed recall","Fixed precision"];
LUBoundGenerator={"tr2010":[2,123],"p2010":[124,184],"tr2011":[186,307],"p2011":[308,368],"tr2012":[370,491],"p2012":[492,552],"tr2014":[554,676],"p2014":[677,737],"tr2015":[739,861],"p2015":[862,922]};




for fnm, ulbnd in LUBoundGenerator.items():
	for shnm in SheetNames:

		MultiBoxPlot(ExcelFileName,shnm,fnm+shnm,modelnames,Columntag,ulbnd[0]-2,ulbnd[1]-2);
		print(fnm+shnm)


'''
LineLowerBound=122
LineUpperBound=182



print("Please input Excel Files");
ExcelFileName=input("Day Accuracy file>>");

tmpExcelContainer=pd.read_excel(ExcelFileName,sheet_name=SheetName);


DatalineContainer=[];
for ctg in Columntag:
	DatalineContainer.append(tmpExcelContainer[ctg].tolist()[LineLowerBound:LineUpperBound]);


tmpdic={};
for i in range(len(modelnames)):
	tmpdic[modelnames[i]]=DatalineContainer[i]

df1=pd.DataFrame(tmpdic)

plt.figure(figsize=(2,3))
sns.boxplot( data=df1,palette=ColorSeriesForChart,width=.8,fliersize=0.0)
plt.ylabel(ylabel,fontsize=12)
plt.xlabel(xlabel,fontsize=12)

plt.tight_layout()
plt.ylim(0, 1)
plt.savefig(str(ExcelFileName)+"ACC"+".svg")
plt.show()
'''

