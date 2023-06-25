#coding:utf-8
import numpy as np
import matplotlib.pyplot as plt
import math
import pandas as pd
import matplotlib
plt.rcParams['font.sans-serif']=['mingliu'] #用來正常顯示中文標籤
plt.rcParams['axes.unicode_minus']=False #用來正常顯示負號

def calculateLowerAndUpperBound(ExcelContainer ,Mode,AxisXLabelMaxMin=None,ForSaveFid=0):
	'''AxisXLabelMaxMinMin Max'''

	x = [100,200,300,400,1500]
	lby1 =[0,0,0,0,0]
	lby2= [0,1,2,3,4]
	lby3= [2,3,4,5,6]
	uby1= [9,10,11,12,13]
	uby2= [10,12,13,14,15]
	uby3= [12,13,14,15,16]



	#init vce line
	x = ExcelContainer['Count'].tolist()



	ubcolor="red"
	lbcolor="green"

	#xlabel="Number of cases (case) "
	xlabel="病例數 (case) "
	
	ylabel="ylb"

	xstart=0
	xend=10
	xinterval=1
	ystart=0
	yend=52
	yinterval=2

	#Mode=input("Please enter Mode d/l >>")
	if(Mode=='l'):
		#ylabel="Lag (day)"
		ylabel="發病間距 (day)"
		ystart=0
		yend=52
		yinterval=2
		ubcolor="blue"
		lbcolor="orange"
		'''lag'''
		lby1 =ExcelContainer['Lag lower bound Q1'].tolist()
		lby2= ExcelContainer['Lag lower bound Q2'].tolist()
		lby3= ExcelContainer['Lag lower bound Q3'].tolist()
		uby1= ExcelContainer['Lag upper bound Q1'].tolist()
		uby2= ExcelContainer['Lag upper bound Q2'].tolist()
		uby3= ExcelContainer['Lag upper bound Q3'].tolist()

	elif(Mode=='d'):
		#ylabel="Distance (meter)"
		ylabel="傳染半徑 (meter)"
		ystart=0
		yend=1100
		yinterval=50
		ubcolor="red"
		lbcolor="green"
		'''d'''
		lby1 =ExcelContainer['Distance lower bound Q1'].tolist()
		lby2= ExcelContainer['Distance lower bound Q2'].tolist()
		lby3= ExcelContainer['Distance lower bound Q3'].tolist()
		uby1= ExcelContainer['Distance upper bound Q1'].tolist()
		uby2= ExcelContainer['Distance upper bound Q2'].tolist()
		uby3= ExcelContainer['Distance upper bound Q3'].tolist()
		
	else:
		ylabel="error"
		exit(-1)
	#process xend
	#print((math.log(10,max(x))))
	lcmaxx=0
	lcminx=0
	if(AxisXLabelMaxMin!=None):
		lcminx=AxisXLabelMaxMin[0]
		lcmaxx=AxisXLabelMaxMin[1]
	else:
		lcmaxx=max(x)
		lcminx=min(x)
		
		
	if((math.log10(lcmaxx)-1)<2.0):
		xstart=0
		xend=int((lcmaxx/10)+3)*10
	else:
		'''
		xstart= (int(lcminx/(10**int(math.log10(lcminx)-1)  ))-2)*(10**int(math.log10(lcminx)-1))
		if(xstart<100):
			xstart=0
		'''
		xstart=0
		xend=(int(lcmaxx/(10**int(math.log10(lcmaxx)-1)  )))*(10**int(math.log10(lcmaxx)-1))

	#print(xstart)
	#print(xend)  
	xinterval=int((xend-xstart+1)/9)
	xinterval=int(xinterval/10)*10
	
	xend=(xstart+xinterval*10)+1
	print("X axis start: "+str(xstart))
	print("Y axis end: "+str(xend))
	plt.figure(figsize=(6,5))
	plt.xlim(xstart, xend)
	plt.xticks(np.arange(xstart, xend, xinterval))    
	plt.ylim(ystart, yend)
	plt.yticks(np.arange(ystart, yend+1, yinterval))

	#plt.plot(x,lby1,color='white',alpha=0)
	#plt.plot(x,lby2,color=lbcolor,alpha=1)
	#plt.plot(x,lby3,color='white',alpha=0) 

	plt.plot(x,uby1,color='white',alpha=0)
	plt.plot(x,uby2,color=ubcolor,alpha=1)
	plt.plot(x,uby3,color='white',alpha=0) 

	#plt.fill_between(x,lby1,lby3,color=lbcolor,alpha=.2,linewidth=0,edgecolor=matplotlib.colors.to_rgba('white', 0.0))
	plt.fill_between(x,uby1,uby3,color=ubcolor,alpha=.2,linewidth=0,edgecolor=matplotlib.colors.to_rgba('white', 0.0))

	#grey tone
	#plt.fill_between(x,uby1,lby3,color="grey",alpha=.1,linewidth=0,edgecolor=matplotlib.colors.to_rgba('white', 0.0))
	plt.xlabel(xlabel,fontsize=13)
	plt.ylabel(ylabel,fontsize=13)
	#sv fig
	if(Mode=="l"):
		plt.savefig(str(ForSaveFid)+"l.svg")
	else:
		plt.savefig(str(ForSaveFid)+"d.svg")
	
	plt.show()
	#end of function
#start 
	
print("Please input 3 Excel Files");
FileNumber=3;
ExcelContainer=[]
for i in range(FileNumber):
	ExcelFileName=input("Quartile Excel>>");
	tmpExcelContainer=pd.read_excel(ExcelFileName);
	ExcelContainer.append(tmpExcelContainer);
#find x min max
AllXset=[];
for exclele in ExcelContainer:
	AllXset=AllXset+exclele['Count'].tolist();
CXMinMax=[int(min(AllXset)),int(max(AllXset))];
# write fig
for i in range(FileNumber):	
	calculateLowerAndUpperBound(ExcelContainer[i],"l",CXMinMax,i);
	calculateLowerAndUpperBound(ExcelContainer[i],"d",CXMinMax,i);