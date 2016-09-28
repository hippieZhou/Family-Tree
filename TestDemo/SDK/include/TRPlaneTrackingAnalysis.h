#ifndef _TR_PLANETRACKINGANALYSIS_H_
#define _TR_PLANETRACKINGANALYSIS_H_

////飞机轨迹跟踪

#include "TRDataDef.h"

typedef  void* TRPTEngine;
////***PlaneTracking == PT***////

#define TR_PT_MAX_TRACK_COUNT  50//目标的最大个数
#define TR_PT_MAX_TRAJECTORY_COUNT  200//目标轨迹的最大个数
#define TY_PT_DETECT_MIN_SIZE 50//检测目标的最小面积
#define TY_PT_DETECT_MAX_SIZE 1000000//检测目标的最大面积
#define TR_PT_BLOB_RANGE      230//相邻团块合并范围
#define TR_PT_ANGLE           25//夹角
////目标检测参数输入定义jiajiao
typedef struct _TrPlaneTrackingParam
{
	float   fLearningSensitivity;//背景学习灵敏度，默认为0.5，参数范围（0.1-0.9）
								 //注：表示算法对场景的学习速度，值越大更新越快
	float   fDetectSensitivity;//前景检测灵敏度，默认为0.7，参数范围（0.1-0.9）
							   //注：表示对前景分割的灵敏度，值越大分割的前景越小
	float   fTrackSensitivity;//目标跟踪灵敏度，默认为0.5，参jiajiao数范围（0.1-0.9）,
							  //注：表示目标匹配的track范围，越大匹配的区域也越大
	float   fDistanceSensitivity;//目标运动距离灵敏度，默认为0.5，参数范围（0.1-0.9）,
							     //注：表示目标在单位时间内的运动距离，值越大表示目标单位时间内运动距离越大，可以把目标运动距离小的过滤掉

	////注：远处的rect要小于对应近处的rect
	TrRectF nearMaxPlaneDetect;//近处检测的最大飞机rect
	TrRectF nearMinPlaneDetect;//近处检测的最小飞机rect
	TrRectF farMaxPlaneDetect;//远处检测的最大飞机rect
	TrRectF farMinPlaneDetect;//远处检测的最小飞机rect

	float   fImageResizeRatioW;//图像宽的缩放比例（0.1—1）
	float   fImageResizeRatioH;//图像高的缩放比例（0.1—1）
}TrPlaneTrackingParam;

////飞机轨迹跟踪输出定义
////TY_PLANETRACKING
typedef struct _TrTrackData
{
	int          nObjectID;				//目标ID
	int          nTrajectoryNum;                  //轨迹点数
	float        fObjectWidth;					  //检测目标的宽，为一个统计平均值
	float        fObjectHeight;					  //检测目标的高，为一个统计平均值
	TrRectF      curBlobF;                        //当前的blob
	TrPointF     centreF[TR_PT_MAX_TRAJECTORY_COUNT];//存放轨迹点
	TrRectF      blobF[TR_PT_MAX_TRAJECTORY_COUNT];  //存储历史的rect
	TrTime       startTime;  //目标开始时间
	TrTime       endTime;    //目标结束时间
}TrTrackData;

typedef struct _TrPlaneTrackingOutInfo
{
	int		      nTrackCount;                   //飞机目标个数
	TrTrackData   trackData[TR_PT_MAX_TRACK_COUNT];//飞机跟踪的数据信息
}TrPlaneTrackingOutInfo;


////先创建一个算法句柄
TRAPI(TRPTEngine) tr_PTEngineCreate();


////pEngine为算法创建的数据句柄
////pAlgParam为算法分析参数
////pDetectRegionF为禁区检测区域参数
TRAPI(TrError) tr_PTEngineSetConfig(_INPUT TRPTEngine pEngine,_INPUT TrPlaneTrackingParam *pAlgParam,_INPUT TrDetectRegionF *pDetectRegionF);


////pEngine为算法创建的数据句柄
////pImage为输入图像，需要根据TrImage设定
////curTime调用时送入的图像对应的时间
////pOutInfo表示算法分析输出的结果信息
TRAPI(TrError) tr_PTEngineAnalyzeFrame(_INPUT TRPTEngine pEngine,_INPUT TrImage *pImage,_INPUT TrTime curTime,_OUTPUT TrPlaneTrackingOutInfo *pOutInfo);


////算法销毁
TRAPI(TrError) tr_PTEngineDestroy(_INPUT TRPTEngine pEngine);


#endif