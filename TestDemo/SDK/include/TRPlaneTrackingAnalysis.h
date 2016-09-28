#ifndef _TR_PLANETRACKINGANALYSIS_H_
#define _TR_PLANETRACKINGANALYSIS_H_

////�ɻ��켣����

#include "TRDataDef.h"

typedef  void* TRPTEngine;
////***PlaneTracking == PT***////

#define TR_PT_MAX_TRACK_COUNT  50//Ŀ���������
#define TR_PT_MAX_TRAJECTORY_COUNT  200//Ŀ��켣��������
#define TY_PT_DETECT_MIN_SIZE 50//���Ŀ�����С���
#define TY_PT_DETECT_MAX_SIZE 1000000//���Ŀ���������
#define TR_PT_BLOB_RANGE      230//�����ſ�ϲ���Χ
#define TR_PT_ANGLE           25//�н�
////Ŀ����������붨��jiajiao
typedef struct _TrPlaneTrackingParam
{
	float   fLearningSensitivity;//����ѧϰ�����ȣ�Ĭ��Ϊ0.5��������Χ��0.1-0.9��
								 //ע����ʾ�㷨�Գ�����ѧϰ�ٶȣ�ֵԽ�����Խ��
	float   fDetectSensitivity;//ǰ����������ȣ�Ĭ��Ϊ0.7��������Χ��0.1-0.9��
							   //ע����ʾ��ǰ���ָ�������ȣ�ֵԽ��ָ��ǰ��ԽС
	float   fTrackSensitivity;//Ŀ����������ȣ�Ĭ��Ϊ0.5����jiajiao����Χ��0.1-0.9��,
							  //ע����ʾĿ��ƥ���track��Χ��Խ��ƥ�������ҲԽ��
	float   fDistanceSensitivity;//Ŀ���˶����������ȣ�Ĭ��Ϊ0.5��������Χ��0.1-0.9��,
							     //ע����ʾĿ���ڵ�λʱ���ڵ��˶����룬ֵԽ���ʾĿ�굥λʱ�����˶�����Խ�󣬿��԰�Ŀ���˶�����С�Ĺ��˵�

	////ע��Զ����rectҪС�ڶ�Ӧ������rect
	TrRectF nearMaxPlaneDetect;//�����������ɻ�rect
	TrRectF nearMinPlaneDetect;//����������С�ɻ�rect
	TrRectF farMaxPlaneDetect;//Զ���������ɻ�rect
	TrRectF farMinPlaneDetect;//Զ��������С�ɻ�rect

	float   fImageResizeRatioW;//ͼ�������ű�����0.1��1��
	float   fImageResizeRatioH;//ͼ��ߵ����ű�����0.1��1��
}TrPlaneTrackingParam;

////�ɻ��켣�����������
////TY_PLANETRACKING
typedef struct _TrTrackData
{
	int          nObjectID;				//Ŀ��ID
	int          nTrajectoryNum;                  //�켣����
	float        fObjectWidth;					  //���Ŀ��Ŀ�Ϊһ��ͳ��ƽ��ֵ
	float        fObjectHeight;					  //���Ŀ��ĸߣ�Ϊһ��ͳ��ƽ��ֵ
	TrRectF      curBlobF;                        //��ǰ��blob
	TrPointF     centreF[TR_PT_MAX_TRAJECTORY_COUNT];//��Ź켣��
	TrRectF      blobF[TR_PT_MAX_TRAJECTORY_COUNT];  //�洢��ʷ��rect
	TrTime       startTime;  //Ŀ�꿪ʼʱ��
	TrTime       endTime;    //Ŀ�����ʱ��
}TrTrackData;

typedef struct _TrPlaneTrackingOutInfo
{
	int		      nTrackCount;                   //�ɻ�Ŀ�����
	TrTrackData   trackData[TR_PT_MAX_TRACK_COUNT];//�ɻ����ٵ�������Ϣ
}TrPlaneTrackingOutInfo;


////�ȴ���һ���㷨���
TRAPI(TRPTEngine) tr_PTEngineCreate();


////pEngineΪ�㷨���������ݾ��
////pAlgParamΪ�㷨��������
////pDetectRegionFΪ��������������
TRAPI(TrError) tr_PTEngineSetConfig(_INPUT TRPTEngine pEngine,_INPUT TrPlaneTrackingParam *pAlgParam,_INPUT TrDetectRegionF *pDetectRegionF);


////pEngineΪ�㷨���������ݾ��
////pImageΪ����ͼ����Ҫ����TrImage�趨
////curTime����ʱ�����ͼ���Ӧ��ʱ��
////pOutInfo��ʾ�㷨��������Ľ����Ϣ
TRAPI(TrError) tr_PTEngineAnalyzeFrame(_INPUT TRPTEngine pEngine,_INPUT TrImage *pImage,_INPUT TrTime curTime,_OUTPUT TrPlaneTrackingOutInfo *pOutInfo);


////�㷨����
TRAPI(TrError) tr_PTEngineDestroy(_INPUT TRPTEngine pEngine);


#endif