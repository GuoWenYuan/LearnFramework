using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SFramework.Core
{
    public partial class LogManager
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public  void Log(SLogLevel level, object message)
        {
            switch (level)
            {
                case SLogLevel.Debug:
                    UnityEngine.Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message.ToString()));
                    break;

                case SLogLevel.Info:
                    UnityEngine.Debug.Log(Utility.Text.Format("<color=#00FF7F>{0}</color>", message.ToString()));
                    break;

                case SLogLevel.Warning:
                    UnityEngine.Debug.Log(Utility.Text.Format("<color=#FFFF00>{0}</color>", message.ToString()));
                    break;

                case SLogLevel.Error:
                    UnityEngine.Debug.Log(Utility.Text.Format("<color=#DC143C>{0}</color>", message.ToString()));
                    break;

                default:
                    throw new SException(message.ToString());
            }
        }


        /// <summary>
        /// 初始化log日志
        /// </summary>
        public void InitLogReport()
        {
            
        }
    }

}
