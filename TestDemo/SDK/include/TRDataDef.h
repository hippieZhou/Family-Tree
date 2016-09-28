#ifndef _TR_DATA_DEF_H_
#define _TR_DATA_DEF_H_

////****�㷨������������****////

/////***��ռ���дΪ���,�㷨����TR����***/////
#define TR_CDECL __cdecl
#define TR_STDCALL __stdcall
#ifndef TRAPI_EXPORTS
#define TR_EXPORTS __declspec(dllexport)
#else 
#define TR_EXPORTS __declspec(dllimport)
#endif 

#ifndef TR_EXTERN_C
#ifdef __cplusplus
#define TR_EXTERN_C extern "C"
#else //__cplusplus
#define TR_EXTERN_C
#endif //__cplusplus
#endif 

#ifndef TRAPI
#define TRAPI(rettype) TR_EXTERN_C TR_EXPORTS rettype TR_CDECL
#endif //SCAPI


///���������ʾ////
#define _INPUT
#define _OUTPUT

#define  TR_QJ_MAX_WIDTH    2000//ȫ����Ƶ���������
#define  TR_QJ_LIMIT_WIDTH  1000//ȫ����Ƶ��������ƿ�
#define  TR_QJ_MAX_HEIGHT   500//ȫ����Ƶ���������

#define  TR_DL_MAX_WIDTH    1000//��·��Ƶ���������
#define  TR_DL_MAX_HEIGHT   1000//��·��Ƶ���������

#define  TR_MAX(a,b)        ((a)>(b)?(a):(b))
#define  TR_MIN(a,b)        ((a)>(b)?(b):(a))

typedef	 int  TrBool;
#define  TR_TRUE            0x01
#define  TR_FALSE           0x00
typedef  unsigned char unchar;


////������Ϣ��ʶ
typedef enum
{
	TR_ERROR_SUCESS = 0,     //�ɹ�
	TR_ERROR_NULLRETURN = 1, //��ָ�뷵��
	TR_ERROR_SOFTDOG = 2, //���ܹ��쳣
	TR_ERROR_RULESPARAMER,	//�����������
	TR_ERROR_ALGORITHMPARAMER,//�㷨��������
	TR_ERROR_IMAGEPARAMER,	//ͼ���������
	TR_ERROR_MEMORYALLOC    //�ڴ����ʧ��
}TrError;

//ʱ�䶨��
typedef struct _TrTime
{
	int	dwYear;			 // ��          
	int	dwMonth;		 // ��
	int	dwDay;			 // ��
	int	dwHour;			 // ʱ
	int	dwMinute;		 // ��
	int	dwSecond;		 // ��
	int	dwMilliseconds;  // ����
}TrTime;

typedef struct _TrPoint
{
	int nX;
	int nY;
}TrPoint;

typedef struct _TrPointF
{
	float fX;
	float fY;
}TrPointF;

//����
typedef struct _TrRect
{
	int nLeft;
	int nTop;
	int nRight;
	int nBottom;
}TrRect;

typedef struct _TrRectF
{
	float fLeft;
	float fTop;
	float fRight;
	float fBottom;
}TrRectF;

#define TR_MAX_REGION_POINTS  10
typedef struct _TrRegion
{
	int nPointCount;
	TrPoint points[TR_MAX_REGION_POINTS];
}TrRegion;

////����Ķ���
typedef struct _TrRegionF
{
	int nPointCount;
	TrPointF points[TR_MAX_REGION_POINTS];
}TrRegionF;

#define TR_MAX_REGION_COUNT 10//����������
////�㷨��������
typedef struct _TrDetectRegion
{
	int nRegionCount;
	TrRegion regionF[TR_MAX_REGION_COUNT];
}TrDetectRegion;

typedef struct _TrDetectRegionF
{
	int nRegionCount;
	TrRegionF regionF[TR_MAX_REGION_COUNT];
}TrDetectRegionF;

#define TR_DETECT_MIN_SIZE 50//���Ŀ�����С���
#define TR_DETECT_MAX_SIZE 1000000//���Ŀ���������

////ͼ����������
typedef enum _TrImageType
{
	TR_RGB = 0, // pImageData[0]-rgb rgbrgbrgb����洢��ʽ    
	TR_YUV420 = 1,// pImageData[0]-y,pImageData[1]-u,pImageData[2]-v
}TrImageType;

////��Ƶ����
typedef enum
{
	TR_SINGLEVIDEO = 0,//��·��Ƶ
	TR_PANORAMICVIDEO//ȫ����Ƶ
}TrVideoType;

////ͼ�����ݶ���
typedef struct _TrImage
{
	int   nWidth;//ͼ���
	int   nHeight;//ͼ���
	int   nWidthStep;//ͼ��ÿ�е��ֽ���(ע��rgb���ݣ��ο�opencv��yuv420��y����Ϊ��׼)
	TrImageType imageType;//����ͼ�������
	unchar *pImageData[3];//��imageType��Ӧ
}TrImage;


#endif