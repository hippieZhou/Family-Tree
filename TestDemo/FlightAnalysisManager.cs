using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestDemo
{
    #region MyRegion
    public struct TrPlaneTrackingParam
    {
        public float fLearningSensitivity;  //背景学习灵敏度，默认为0.5，参数范围（0.1-0.9）,注：表示算法对场景的学习速度，值越大更新越快
        public float fDetectSensitivity;    //前景检测灵敏度，默认为0.7，参数范围（0.1-0.9）,注：表示对前景分割的灵敏度，值越大分割的前景越小
        public float fTrackSensitivity;     //目标跟踪灵敏度，默认为0.5，参jiajiao数范围（0.1-0.9）,注：表示目标匹配的track范围，越大匹配的区域也越大
        public float fDistanceSensitivity;  //目标运动距离灵敏度，默认为0.5，参数范围（0.1-0.9）,注：表示目标在单位时间内的运动距离，值越大表示目标单位时间内运动距离越大，可以把目标运动距离小的过滤掉

        //注：远处的rect要小于对应近处的rect
        public TrRectF nearMaxPlaneDetect; //近处检测的最大飞机rect
        public TrRectF nearMinPlaneDetect; //近处检测的最小飞机rect
        public TrRectF farMaxPlaneDetect;  //远处检测的最大飞机rect
        public TrRectF farMinPlaneDetect;  //远处检测的最小飞机rect
        public float fImageResizeRatioW;  //图像宽的缩放比例（0.1—1）
        public float fImageResizeRatioH;  //图像高的缩放比例（0.1—1）
    };
    public struct TrTrackData
    {
        int nObjectID;		 //目标ID
        int nTrajectoryNum;  //轨迹个数
        int nCountNums;
        float fObjectWidth;	 //检测目标的宽，为一个统计平均值
        float fObjectHeight; //检测目标的高，为一个统计平均值
        Rect curBlobF;       //当前的blob
        Point[] centreF;     //存放轨迹点[50]
        Rect[] blobF;        //存储历史的rect[50]
        DateTime startTime;  //目标开始时间
        DateTime endTime;    //目标结束时间
    };
    public struct TrPlaneTrackingOutInfo
    {
        int nTrackCount;          //飞机目标个数
        TrTrackData[] trackData;  //飞机跟踪的数据信息,30
    };
    #endregion

    public class FlightAnalysisManager
    {
        public const int TR_PT_MAX_TRACK_COUNT = 30;//目标的最大个数
        public const int TR_PT_MAX_TRAJECTORY_COUNT = 50;//目标轨迹的最大个数
        public const int TY_PT_DETECT_MIN_SIZE = 50;//检测目标的最小面积
        public const int TY_PT_DETECT_MAX_SIZE = 1000000;//检测目标的最大面积
        public const int TR_PT_BLOB_RANGE = 230;//相邻团块合并范围
        public const int TR_PT_ANGLE = 25;//夹角,目标检测参数输入定义jiajiao

        public const string dllName = "TRPlaneTrackingAnalysis.dll";

        //先创建一个算法句柄
        [System.Runtime.InteropServices.DllImport(dllName, EntryPoint = "tr_PTEngineCreate", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr PTEngineCreate();

        ////pEngine为算法创建的数据句柄
        ////pAlgParam为算法分析参数
        ////pDetectRegionF为禁区检测区域参数
        //TRAPI(TrError) tr_PTEngineSetConfig(_INPUT TRPTEngine pEngine,_INPUT TrPlaneTrackingParam *pAlgParam,_INPUT TrDetectRegionF *pDetectRegionF);
        [System.Runtime.InteropServices.DllImport(dllName, EntryPoint = "tr_PTEngineSetConfig", CallingConvention = CallingConvention.Cdecl)]
        public static extern TrError PTEngineSetConfig(IntPtr hWnd, TrPlaneTrackingParam pAlgParam, TrDetectRegionF pDetectRegionF);


        ////TRAPI(TrError) tr_PTEngineAnalyzeFrame(_INPUT TRPTEngine pEngine,_INPUT TrImage *pImage,_INPUT TrTime curTime,_OUTPUT TrPlaneTrackingOutInfo *pOutInfo);
        [System.Runtime.InteropServices.DllImport(dllName, EntryPoint = "tr_PTEngineAnalyzeFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern TrError PTEngineAnalyzeFrame(IntPtr hWnd, TrImage pImage, DateTime curTime, out TrPlaneTrackingOutInfo pOutInfo);

        ////消息销毁
        [System.Runtime.InteropServices.DllImport(dllName, EntryPoint = "tr_PTEngineAnalyzeFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern TrError PTEngineDestroy(IntPtr hWnd);
    }
}
