using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    //错误信息标识
    public enum TrError
    {
        TR_ERROR_SUCESS = 0,     //成功
        TR_ERROR_NULLRETURN = 1, //空指针返回
        TR_ERROR_SOFTDOG = 2, //加密狗异常
        TR_ERROR_RULESPARAMER,	//规则参数错误
        TR_ERROR_ALGORITHMPARAMER,//算法参数错误
        TR_ERROR_IMAGEPARAMER,	//图像参数错误
        TR_ERROR_MEMORYALLOC    //内存分配失败
    };

    //时间定义
    public struct TrTime
    {
        public int dwYear;		  // 年          
        public int dwMonth;		  // 月
        public int dwDay;		  // 日
        public int dwHour;		  // 时
        public int dwMinute;		  // 分
        public int dwSecond;		  // 秒
        public int dwMilliseconds; // 毫秒
    };

    public struct TrPoint
    {
        public int nX;
        public int nY;
    };

    public struct TrPointF
    {
        public float fX;
        public float fY;
    };

    //矩形
    public struct TrRect
    {
        public int nLeft;
        public int nTop;
        public int nRight;
        public int nBottom;
    };

    public struct TrRectF
    {
        public float fLeft;
        public float fTop;
        public float fRight;
        public float fBottom;
    };

    public struct TrRegion
    {
        public int nPointCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public TrPoint[] points;//数组长度为10
    };

    //区域的定义
    public struct TrRegionF
    {
        public int nPointCount;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public TrPointF[] points;//数组长度为10
    };

    //算法分析区域
    public struct TrDetectRegion
    {
        public int nRegionCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public TrRegion[] regionF;//最大区域个数为10
    };

    public struct TrDetectRegionF
    {
        public int nRegionCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public TrRegionF[] regionF;//最大区域个数为10
    };

    //图像数据类型
    public enum TrImageType
    {
        TR_RGB = 0, // pImageData[0]-rgb rgbrgbrgb交叉存储方式    
        TR_YUV420 = 1,// pImageData[0]-y,pImageData[1]-u,pImageData[2]-v
    };

    //视频类型
    public enum TrVideoType
    {
        TR_SINGLEVIDEO = 0,//单路视频
        TR_PANORAMICVIDEO//全景视频
    };

    //图像数据定义
    public struct TrImage
    {
        public int nWidth;//图像宽
        public int nHeight;//图像高
        public int nWidthStep;//图像每行的字节数(注：rgb数据，参考opencv，yuv420以y分量为基准)
        public TrImageType imageType;//输入图像的类型
        public byte[] pImageData;//与imageType对应
    };
}
