#ifndef _TR_DATA_DEF_H_
#define _TR_DATA_DEF_H_

////****算法基本参数定义****////

/////***天睿空间缩写为天睿,算法均以TR代替***/////
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


///输入输出标示////
#define _INPUT
#define _OUTPUT

#define  TR_QJ_MAX_WIDTH    2000//全景视频处理的最大宽
#define  TR_QJ_LIMIT_WIDTH  1000//全景视频处理的限制宽
#define  TR_QJ_MAX_HEIGHT   500//全景视频处理的最大高

#define  TR_DL_MAX_WIDTH    1000//单路视频处理的最大宽
#define  TR_DL_MAX_HEIGHT   1000//单路视频处理的最大高

#define  TR_MAX(a,b)        ((a)>(b)?(a):(b))
#define  TR_MIN(a,b)        ((a)>(b)?(b):(a))

typedef	 int  TrBool;
#define  TR_TRUE            0x01
#define  TR_FALSE           0x00
typedef  unsigned char unchar;


////错误信息标识
typedef enum
{
	TR_ERROR_SUCESS = 0,     //成功
	TR_ERROR_NULLRETURN = 1, //空指针返回
	TR_ERROR_SOFTDOG = 2, //加密狗异常
	TR_ERROR_RULESPARAMER,	//规则参数错误
	TR_ERROR_ALGORITHMPARAMER,//算法参数错误
	TR_ERROR_IMAGEPARAMER,	//图像参数错误
	TR_ERROR_MEMORYALLOC    //内存分配失败
}TrError;

//时间定义
typedef struct _TrTime
{
	int	dwYear;			 // 年          
	int	dwMonth;		 // 月
	int	dwDay;			 // 日
	int	dwHour;			 // 时
	int	dwMinute;		 // 分
	int	dwSecond;		 // 秒
	int	dwMilliseconds;  // 毫秒
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

//矩形
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

////区域的定义
typedef struct _TrRegionF
{
	int nPointCount;
	TrPointF points[TR_MAX_REGION_POINTS];
}TrRegionF;

#define TR_MAX_REGION_COUNT 10//最大区域个数
////算法分析区域
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

#define TR_DETECT_MIN_SIZE 50//检测目标的最小面积
#define TR_DETECT_MAX_SIZE 1000000//检测目标的最大面积

////图像数据类型
typedef enum _TrImageType
{
	TR_RGB = 0, // pImageData[0]-rgb rgbrgbrgb交叉存储方式    
	TR_YUV420 = 1,// pImageData[0]-y,pImageData[1]-u,pImageData[2]-v
}TrImageType;

////视频类型
typedef enum
{
	TR_SINGLEVIDEO = 0,//单路视频
	TR_PANORAMICVIDEO//全景视频
}TrVideoType;

////图像数据定义
typedef struct _TrImage
{
	int   nWidth;//图像宽
	int   nHeight;//图像高
	int   nWidthStep;//图像每行的字节数(注：rgb数据，参考opencv，yuv420以y分量为基准)
	TrImageType imageType;//输入图像的类型
	unchar *pImageData[3];//与imageType对应
}TrImage;


#endif