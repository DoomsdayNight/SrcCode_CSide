using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cs.Memory;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000694 RID: 1684
	public class NKCLogManager
	{
		// Token: 0x0600375D RID: 14173 RVA: 0x0011D3A0 File Offset: 0x0011B5A0
		public static void Init()
		{
			NKCLogManager.OpenNewLogFile();
			NKCLogManager.DeleteOldLogFile();
			if (!NKCLogManager.m_logMessageHandled)
			{
				Application.logMessageReceivedThreaded += NKCLogManager.HandleLog;
				NKCLogManager.m_logMessageHandled = true;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0011D3CC File Offset: 0x0011B5CC
		public static void Update()
		{
			Action action;
			while (NKCLogManager.m_ReservedActionQueue.TryDequeue(out action))
			{
				action();
			}
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x0011D3F0 File Offset: 0x0011B5F0
		public static string[] FlushLogList()
		{
			string[] result = null;
			object lockObject = NKCLogManager.m_LockObject;
			lock (lockObject)
			{
				result = NKCLogManager.m_LogList.ToArray();
				NKCLogManager.m_LogList.Clear();
			}
			return result;
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x0011D444 File Offset: 0x0011B644
		public static string GetSavePath()
		{
			if (NKCDefineManager.DEFINE_STEAM())
			{
				return Application.dataPath;
			}
			return Application.persistentDataPath;
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x0011D458 File Offset: 0x0011B658
		public static string GetLogSavePath()
		{
			string text = Path.Combine(NKCLogManager.GetSavePath(), "Log");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x0011D485 File Offset: 0x0011B685
		public static string[] GetLogFileList()
		{
			return (from x in Directory.GetFiles(NKCLogManager.GetLogSavePath(), "*_Log*.TXT")
			orderby new FileInfo(x).CreationTimeUtc.Date
			select x).ToArray<string>();
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x0011D4C0 File Offset: 0x0011B6C0
		public static void CreateNewLogFile()
		{
			object lockObject = NKCLogManager.m_LockObject;
			lock (lockObject)
			{
				NKCLogManager.m_LogList.Clear();
				NKCLogManager.OpenNewLogFile();
			}
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x0011D508 File Offset: 0x0011B708
		public static List<string> GetCurrentOpenedLogs()
		{
			return NKCLogManager.m_listLogFilePath;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x0011D510 File Offset: 0x0011B710
		public static void OpenNewLogFile()
		{
			if (string.IsNullOrEmpty(NKCLogManager.m_logFileTime))
			{
				NKCLogManager.m_logFileTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
				NKCLogManager.m_logFileTime = NKCLogManager.m_logFileTime.Replace('/', '_');
				NKCLogManager.m_logFileTime = NKCLogManager.m_logFileTime.Replace(' ', '_');
				NKCLogManager.m_logFileTime = NKCLogManager.m_logFileTime.Replace(':', '_');
			}
			NKCLogManager.m_logFileNum++;
			string text = string.Concat(new string[]
			{
				NKCLogManager.GetLogSavePath(),
				"/CounterSide_",
				NKCLogManager.m_logFileTime,
				"_",
				string.Format("{0:D3}", NKCLogManager.m_logFileNum),
				"_Log.TXT"
			});
			FileMode mode = FileMode.Create;
			if (File.Exists(text))
			{
				mode = FileMode.Truncate;
			}
			if (NKCLogManager.m_LogFileBinaryWriter != null)
			{
				NKCLogManager.m_LogFileBinaryWriter.Close();
			}
			if (NKCLogManager.m_LogFileStreamWriter != null)
			{
				NKCLogManager.m_LogFileStreamWriter.Close();
			}
			if (NKCLogManager.m_LogFileStream != null)
			{
				NKCLogManager.m_LogFileStream.Close();
			}
			NKCLogManager.m_LogFileStream = File.Open(text, mode, FileAccess.Write, FileShare.Read);
			if (NKCLogManager.NeedsLogEncryption())
			{
				NKCLogManager.m_LogFileBinaryWriter = new BinaryWriter(NKCLogManager.m_LogFileStream);
			}
			else
			{
				NKCLogManager.m_LogFileStreamWriter = new StreamWriter(NKCLogManager.m_LogFileStream);
				NKCLogManager.m_LogFileStreamWriter.AutoFlush = true;
			}
			NKCLogManager.m_listLogFilePath.Add(text);
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x0011D65C File Offset: 0x0011B85C
		private static void AddLogAndWriteFile(string logText)
		{
			string[] array = logText.Split(new string[]
			{
				"\n",
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			object lockObject = NKCLogManager.m_LockObject;
			lock (lockObject)
			{
				for (int i = 0; i < array.Length; i++)
				{
					NKCLogManager.m_LogList.Add(array[i]);
					if (NKCLogManager.NeedsLogEncryption())
					{
						byte[] bytes = Encoding.UTF8.GetBytes(array[i]);
						Crypto2.Encrypt(bytes, bytes.Length);
						byte[] bytes2 = BitConverter.GetBytes(bytes.Length);
						if (NKCLogManager.m_LogFileBinaryWriter != null)
						{
							NKCLogManager.m_LogFileBinaryWriter.Write(bytes2, 0, bytes2.Length);
							NKCLogManager.m_LogFileBinaryWriter.Write(bytes, 0, bytes.Length);
						}
					}
					else if (NKCLogManager.m_LogFileStreamWriter != null)
					{
						NKCLogManager.m_LogFileStreamWriter.WriteLine(array[i]);
					}
					if (NKCLogManager.m_LogList.Count >= 1000)
					{
						NKCLogManager.m_LogList.Clear();
						NKCLogManager.OpenNewLogFile();
					}
				}
				if (NKCLogManager.m_LogFileBinaryWriter != null)
				{
					NKCLogManager.m_LogFileBinaryWriter.Flush();
				}
			}
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x0011D770 File Offset: 0x0011B970
		private static void DeleteOldLogFile()
		{
			string[] files = Directory.GetFiles(NKCLogManager.GetLogSavePath(), "*_Log*.TXT");
			DateTime now = DateTime.Now;
			for (int i = 0; i < files.Length; i++)
			{
				DateTime lastAccessTime = File.GetLastAccessTime(files[i]);
				if ((now - lastAccessTime).TotalDays > 3.0)
				{
					File.Delete(files[i]);
				}
			}
			files = Directory.GetFiles(Application.persistentDataPath, "*_Log*.TXT");
			for (int j = 0; j < files.Length; j++)
			{
				DateTime lastAccessTime2 = File.GetLastAccessTime(files[j]);
				if ((now - lastAccessTime2).TotalDays > 3.0)
				{
					File.Delete(files[j]);
				}
			}
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x0011D820 File Offset: 0x0011BA20
		private static string ConvertToColoredMsg(string msg, LogType type)
		{
			string arg;
			switch (type)
			{
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				arg = "#FF0000FF";
				break;
			case LogType.Warning:
				arg = "#FFE400FF";
				break;
			case LogType.Log:
				arg = "#FFFFFFFF";
				break;
			default:
				arg = "#FFFFFFFF";
				break;
			}
			return string.Format("<color={0}>{1}</color>", arg, msg).Replace("\n", string.Format("</color>\n<color={0}>", arg));
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x0011D890 File Offset: 0x0011BA90
		private static void HandleLog(string msg, string stackTrace, LogType type)
		{
			if (msg.Contains("set as Camera.targetTexture!"))
			{
				return;
			}
			string msg2;
			if (type == LogType.Error || type == LogType.Exception)
			{
				msg2 = string.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), string.Join("\n", new string[]
				{
					msg,
					stackTrace
				}));
			}
			else
			{
				msg2 = string.Format("[{0}] {1}", DateTime.Now.ToLongTimeString(), msg);
			}
			NKCLogManager.AddLogAndWriteFile(NKCLogManager.ConvertToColoredMsg(msg2, type));
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x0011D90F File Offset: 0x0011BB0F
		private static bool NeedsLogEncryption()
		{
			return !NKCDefineManager.DEFINE_PURE_LOG() && NKCDefineManager.DEFINE_SAVE_LOG() && !NKCDefineManager.DEFINE_USE_CHEAT();
		}

		// Token: 0x04003418 RID: 13336
		private const int MAX_LOG_LINE = 1000;

		// Token: 0x04003419 RID: 13337
		private static FileStream m_LogFileStream;

		// Token: 0x0400341A RID: 13338
		private static StreamWriter m_LogFileStreamWriter;

		// Token: 0x0400341B RID: 13339
		private static BinaryWriter m_LogFileBinaryWriter;

		// Token: 0x0400341C RID: 13340
		private static List<string> m_LogList = new List<string>();

		// Token: 0x0400341D RID: 13341
		private static object m_LockObject = new object();

		// Token: 0x0400341E RID: 13342
		private static bool m_logMessageHandled = false;

		// Token: 0x0400341F RID: 13343
		private static ConcurrentQueue<Action> m_ReservedActionQueue = new ConcurrentQueue<Action>();

		// Token: 0x04003420 RID: 13344
		private static int m_logFileNum = 0;

		// Token: 0x04003421 RID: 13345
		private static string m_logFileTime;

		// Token: 0x04003422 RID: 13346
		private static List<string> m_listLogFilePath = new List<string>();
	}
}
