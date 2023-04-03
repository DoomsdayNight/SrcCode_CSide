using System;
using System.Collections.Generic;
using System.IO;
using AssetBundles;
using Cs.Logging;
using NKC.Patcher;
using NKM;
using SimpleJSON;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200073A RID: 1850
	public static class NKCConnectionInfo
	{
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x00162BE9 File Offset: 0x00160DE9
		private static NKCConnectionInfo.SERVER_INFO_TYPE ServerInfoType
		{
			get
			{
				if (NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					Log.Debug("[ServerInfoType] SelectServer", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 76);
					return NKCConnectionInfo.SERVER_INFO_TYPE.SelectServer;
				}
				Log.Debug("[ServerInfoType] Original", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 80);
				return NKCConnectionInfo.SERVER_INFO_TYPE.Original;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x00162C18 File Offset: 0x00160E18
		public static string CustomServerInfoAddress
		{
			get
			{
				string str = "CSConfigServerAddress.txt";
				if (NKCDefineManager.DEFINE_SB_GB())
				{
					str = "csconfigserveraddress.txt";
				}
				string text = Application.streamingAssetsPath + "/" + str;
				Debug.Log("[ServerInfoAddress] ServerInfoAddressPath : " + text);
				if (NKCPatchUtility.IsFileExists(text))
				{
					Debug.Log("[ServerInfoAddress] ServerInfoAddress exist");
					string aJSON;
					if (text.Contains("jar:"))
					{
						aJSON = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(text));
					}
					else
					{
						aJSON = File.ReadAllText(text);
					}
					JSONNode jsonnode = JSONNode.Parse(aJSON);
					if (jsonnode != null)
					{
						return jsonnode["address"];
					}
				}
				return "";
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x00162CB9 File Offset: 0x00160EB9
		public static string ServerInfoFileName
		{
			get
			{
				return NKCConnectionInfo.m_dicServerInfoTypeString[NKCConnectionInfo.ServerInfoType];
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x00162CCA File Offset: 0x00160ECA
		public static IEnumerable<NKCConnectionInfo.LoginServerInfo> LoginServerInfos
		{
			get
			{
				return NKCConnectionInfo.m_dicLoginServerInfo.Values;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00162CD6 File Offset: 0x00160ED6
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x00162CE6 File Offset: 0x00160EE6
		public static NKCConnectionInfo.LOGIN_SERVER_TYPE CurrentLoginServerType
		{
			get
			{
				if (!NKCDefineManager.DEFINE_SELECT_SERVER())
				{
					return NKCConnectionInfo.LOGIN_SERVER_TYPE.Default;
				}
				return NKCConnectionInfo.m_currentLoginServerType;
			}
			set
			{
				NKCConnectionInfo.m_currentLoginServerType = value;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00162CEE File Offset: 0x00160EEE
		public static NKCConnectionInfo.LOGIN_SERVER_TYPE LastLoginServerType
		{
			get
			{
				return (NKCConnectionInfo.LOGIN_SERVER_TYPE)PlayerPrefs.GetInt("LOCAL_SAVE_LAST_LOGIN_SERVER_TYPE", 0);
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00162CFC File Offset: 0x00160EFC
		public static string ServiceIP
		{
			get
			{
				if (!NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.CurrentLoginServerType))
				{
					Debug.LogError(string.Format("[ConnectionInfo] Not Setted Login Server. CurrentLoginServerType : {0}", NKCConnectionInfo.CurrentLoginServerType));
					return "";
				}
				return NKCConnectionInfo.m_dicLoginServerInfo[NKCConnectionInfo.CurrentLoginServerType].m_serviceIP;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00162D48 File Offset: 0x00160F48
		public static int ServicePort
		{
			get
			{
				if (!NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.CurrentLoginServerType))
				{
					Debug.LogError(string.Format("[ConnectionInfo] Not Setted Login Server. CurrentLoginServerType : {0}", NKCConnectionInfo.CurrentLoginServerType));
					return -1;
				}
				return NKCConnectionInfo.m_dicLoginServerInfo[NKCConnectionInfo.CurrentLoginServerType].m_servicePort;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x00162D85 File Offset: 0x00160F85
		public static HashSet<string> CurrentLoginServerTagSet
		{
			get
			{
				if (!NKCConnectionInfo.HasLoginServerInfo(NKCConnectionInfo.CurrentLoginServerType))
				{
					Debug.LogError(string.Format("[ConnectionInfo] Not Setted Login Server. CurrentLoginServerType : {0}", NKCConnectionInfo.CurrentLoginServerType));
					return null;
				}
				return NKCConnectionInfo.m_dicLoginServerInfo[NKCConnectionInfo.CurrentLoginServerType].m_defaultTagSet;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060049E8 RID: 18920 RVA: 0x00162DC2 File Offset: 0x00160FC2
		// (set) Token: 0x060049E9 RID: 18921 RVA: 0x00162DC9 File Offset: 0x00160FC9
		public static string DownloadServerAddress { get; set; }

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x00162DD1 File Offset: 0x00160FD1
		// (set) Token: 0x060049EB RID: 18923 RVA: 0x00162DD8 File Offset: 0x00160FD8
		public static string DownloadServerAddress2 { get; set; }

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x00162DE0 File Offset: 0x00160FE0
		// (set) Token: 0x060049ED RID: 18925 RVA: 0x00162DE7 File Offset: 0x00160FE7
		public static List<string> IgnoreVariantList { get; set; } = new List<string>();

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x00162DEF File Offset: 0x00160FEF
		// (set) Token: 0x060049EF RID: 18927 RVA: 0x00162DF6 File Offset: 0x00160FF6
		public static string VersionJson { get; private set; } = "/version.json";

		// Token: 0x060049F0 RID: 18928 RVA: 0x00162DFE File Offset: 0x00160FFE
		public static bool HasLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE loginServerType)
		{
			return NKCConnectionInfo.m_dicLoginServerInfo.ContainsKey(loginServerType);
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x00162E10 File Offset: 0x00161010
		public static int GetLoginServerCount()
		{
			return NKCConnectionInfo.m_dicLoginServerInfo.Count;
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x00162E1C File Offset: 0x0016101C
		public static NKCConnectionInfo.LOGIN_SERVER_TYPE GetFirstLoginServerType()
		{
			using (Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, NKCConnectionInfo.LoginServerInfo>.KeyCollection.Enumerator enumerator = NKCConnectionInfo.m_dicLoginServerInfo.Keys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return NKCConnectionInfo.LOGIN_SERVER_TYPE.None;
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x00162E74 File Offset: 0x00161074
		public static string GetLoginServerString(NKCConnectionInfo.LOGIN_SERVER_TYPE serverType, string strID, bool bSkipErrorCheck = false)
		{
			return NKCStringTable.GetString(strID + NKCConnectionInfo.m_dicLoginServerPostFix[serverType], bSkipErrorCheck);
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x00162E8D File Offset: 0x0016108D
		public static string GetCurrentLoginServerString(string strID, bool bSkipErrorCheck = false)
		{
			return NKCConnectionInfo.GetLoginServerString(NKCConnectionInfo.CurrentLoginServerType, strID, bSkipErrorCheck);
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x00162E9C File Offset: 0x0016109C
		public static void SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE loginServerType, string ip = "", int port = -1, JSONArray defaultTagSet = null)
		{
			Debug.Log(string.Format("[ConnectionInfo] ConnectionInfo Updated : type {0}, ip {1}, port {2}, protocol {3}, data {4}", new object[]
			{
				loginServerType,
				ip,
				port,
				845,
				NKMDataVersion.DataVersion
			}));
			NKCConnectionInfo.LoginServerInfo loginServerInfo;
			if (!NKCConnectionInfo.HasLoginServerInfo(loginServerType))
			{
				loginServerInfo = new NKCConnectionInfo.LoginServerInfo(ip, port);
				NKCConnectionInfo.m_dicLoginServerInfo.Add(loginServerType, loginServerInfo);
			}
			else
			{
				loginServerInfo = NKCConnectionInfo.m_dicLoginServerInfo[loginServerType];
				if (!string.IsNullOrEmpty(ip))
				{
					loginServerInfo.m_serviceIP = ip;
				}
				if (port > -1)
				{
					loginServerInfo.m_servicePort = port;
				}
			}
			if (defaultTagSet != null)
			{
				foreach (JSONNode d in defaultTagSet.Children)
				{
					loginServerInfo.m_defaultTagSet.Add(d);
				}
			}
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x00162F88 File Offset: 0x00161188
		public static void SaveCurrentLoginServerType()
		{
			Debug.Log(string.Format("[ConnectionInfo] SaveLocalTag {0}", NKCConnectionInfo.CurrentLoginServerType));
			PlayerPrefs.SetInt("LOCAL_SAVE_LAST_LOGIN_SERVER_TYPE", (int)NKCConnectionInfo.CurrentLoginServerType);
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x00162FB2 File Offset: 0x001611B2
		public static void DeleteLocalTag()
		{
			Debug.Log("[ConnectionInfo] DeleteLocalTag");
			PlayerPrefs.DeleteKey("LOCAL_SAVE_LAST_LOGIN_SERVER_TYPE");
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x00162FC8 File Offset: 0x001611C8
		public static void Clear()
		{
			NKCConnectionInfo.m_dicLoginServerInfo.Clear();
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x00162FD4 File Offset: 0x001611D4
		public static void LoadFromJSON(string jsonString)
		{
			JSONNode jsonnode = JSON.Parse(jsonString);
			if (jsonnode != null)
			{
				NKCConnectionInfo.LoadFromJSON(jsonnode);
				NKCConnectionInfo.SetConfigJSONString(jsonString);
			}
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x00163000 File Offset: 0x00161200
		public static bool IsServerUnderMaintenance()
		{
			if (!NKMContentsVersionManager.HasTag("CHECK_MAINTENANCE"))
			{
				if (NKCDefineManager.DEFINE_USE_CHEAT())
				{
					Log.Debug("[IsServerUnderMaintenance] Need Tag", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 311);
					NKMContentsVersionManager.PrintCurrentTagSet();
				}
				return false;
			}
			Path.Combine(AssetBundleManager.GetLocalDownloadPath(), "manifest_test.ini");
			if (File.Exists("localDownloadPath"))
			{
				Log.Debug("[IsServerUnderMaintenance] manifest_test.ini exists", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 322);
				return false;
			}
			return NKCConnectionInfo.m_isUnderMaintenance;
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x00163074 File Offset: 0x00161274
		public static bool CheckDownloadInterval()
		{
			if (string.IsNullOrEmpty(NKCConnectionInfo.m_downloadedConfigJSONString))
			{
				Log.Debug("[PatcherManager][Maintenance][CheckDownloadInterval] configString is empty", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 333);
				return true;
			}
			if (DateTime.Now.Subtract(NKCConnectionInfo.m_lastDownloadedTime).TotalSeconds < NKCConnectionInfo.DOWNLOAD_INTERVAL)
			{
				Log.Debug("[PatcherManager][Maintenance][CheckDownloadInterval] too early", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 341);
				return false;
			}
			return true;
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x001630DB File Offset: 0x001612DB
		public static void SetConfigJSONString(string jsonString)
		{
			NKCConnectionInfo.m_downloadedConfigJSONString = jsonString;
			NKCConnectionInfo.m_lastDownloadedTime = DateTime.Now;
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x001630F0 File Offset: 0x001612F0
		public static void LoadMaintenanceDataFromJSON()
		{
			Log.Debug("LoadMaintenanceDataFromJSON - Start", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 357);
			NKCConnectionInfo.m_isUnderMaintenance = false;
			if (string.IsNullOrEmpty(NKCConnectionInfo.m_downloadedConfigJSONString))
			{
				Debug.LogError("LoadMaintenanceDataFromJSON - Downloaded JSON is empty");
				return;
			}
			JSONNode jsonnode = JSON.Parse(NKCConnectionInfo.m_downloadedConfigJSONString);
			if (jsonnode == null)
			{
				Debug.LogError("LoadMaintenanceDataFromJSON - Parse failed");
				return;
			}
			JSONNode jsonnode2 = jsonnode["server"][NKCConnectionInfo.m_currentLoginServerType.ToString()];
			if (jsonnode2 == null)
			{
				Debug.LogError(string.Format("LoadMaintenanceDataFromJSON - CurrentLoginServerType Node is null!! type[{0}]", NKCConnectionInfo.m_currentLoginServerType));
				return;
			}
			JSONNode jsonnode3 = jsonnode2["Maintenance"];
			if (jsonnode3 == null)
			{
				Debug.LogError(string.Format("LoadMaintenanceDataFromJSON - maintenanceNode Node is null!! type[{0}]", NKCConnectionInfo.m_currentLoginServerType));
				return;
			}
			if (jsonnode3["Interval"] != null)
			{
				NKCConnectionInfo.DOWNLOAD_INTERVAL = jsonnode3["Interval"].AsDouble;
			}
			if (jsonnode3["Use"] != null)
			{
				NKCConnectionInfo.m_isUnderMaintenance = jsonnode3["Use"].AsBool;
				Log.Debug(string.Format("LoadMaintenanceDataFromJSON - Use : {0}", NKCConnectionInfo.m_isUnderMaintenance), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConnectionInfo.cs", 397);
			}
			if (NKCConnectionInfo.m_isUnderMaintenance)
			{
				NKM_NATIONAL_CODE nationalCode = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_KOREA);
				string text = jsonnode3["Description"][nationalCode.ToString()];
				if (text == null)
				{
					text = jsonnode3["Description"]["DEFAULT"];
				}
				if (!string.IsNullOrEmpty(text))
				{
					NKCStringTable.AddString(nationalCode, "SI_SYSTEM_NOTICE_MAINTENANCE_DESC", text, true);
				}
			}
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0016329C File Offset: 0x0016149C
		public static void LoadFromJSON(JSONNode node)
		{
			NKCConnectionInfo.SERVER_INFO_TYPE serverInfoType = NKCConnectionInfo.ServerInfoType;
			if (serverInfoType == NKCConnectionInfo.SERVER_INFO_TYPE.Original || serverInfoType != NKCConnectionInfo.SERVER_INFO_TYPE.SelectServer)
			{
				NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, node["ip"], node["port"].AsInt, null);
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					NKCConnectionInfo.LOGIN_SERVER_TYPE login_SERVER_TYPE = (NKCConnectionInfo.LOGIN_SERVER_TYPE)i;
					string aKey = login_SERVER_TYPE.ToString();
					if (!(node["server"][aKey] == null))
					{
						JSONNode jsonnode = node["server"][aKey]["defaultTagSet"];
						JSONArray defaultTagSet = (jsonnode != null) ? jsonnode.AsArray : null;
						NKCConnectionInfo.SetLoginServerInfo((NKCConnectionInfo.LOGIN_SERVER_TYPE)i, node["server"][aKey]["ip"], node["server"][aKey]["port"].AsInt, defaultTagSet);
						NKCConnectionInfo.m_isUnderMaintenance = false;
						JSONNode jsonnode2 = node["server"][aKey]["Maintenance"];
						if (jsonnode2 != null)
						{
							if (jsonnode2["Use"] != null)
							{
								NKCConnectionInfo.m_isUnderMaintenance = jsonnode2["Use"].AsBool;
							}
							if (NKCConnectionInfo.m_isUnderMaintenance)
							{
								NKM_NATIONAL_CODE nationalCode = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_KOREA);
								string text = jsonnode2["Description"][nationalCode.ToString()];
								if (text == null)
								{
									text = jsonnode2["Description"]["DEFAULT"];
								}
								if (!string.IsNullOrEmpty(text))
								{
									NKCStringTable.AddString(nationalCode, "SI_SYSTEM_NOTICE_MAINTENANCE_DESC", text, true);
								}
							}
						}
					}
				}
			}
			NKCConnectionInfo.s_ServerType = node["type"];
			if (node["cdn"] != null)
			{
				NKCConnectionInfo.DownloadServerAddress = node["cdn"];
			}
			if (node["versionJson"] != null)
			{
				NKCConnectionInfo.VersionJson = node["versionJson"];
			}
			JSONNode jsonnode3 = node["IgnoreVariantList"];
			foreach (JSONNode d in ((jsonnode3 != null) ? jsonnode3.AsArray : null).Children)
			{
				NKCConnectionInfo.IgnoreVariantList.Add(d);
			}
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x00163524 File Offset: 0x00161724
		public static void UpdateDataVersionOnly()
		{
			string fileFullPath = NKCPatchDownloader.Instance.GetFileFullPath("ConnectionInfo.json");
			if (NKCPatchUtility.IsFileExists(fileFullPath))
			{
				NKMDataVersion.DataVersion = JSONNode.LoadFromFile(fileFullPath)["dv"].AsInt;
			}
		}

		// Token: 0x040038BA RID: 14522
		public const string ConnectionFilePath = "ConnectionInfo.json";

		// Token: 0x040038BB RID: 14523
		public const string DataVersionJSONName = "dv";

		// Token: 0x040038BC RID: 14524
		public const string IPJSONName = "ip";

		// Token: 0x040038BD RID: 14525
		public const string PortJSONName = "port";

		// Token: 0x040038BE RID: 14526
		public const string ServerTypeJSONName = "type";

		// Token: 0x040038BF RID: 14527
		private const string ServerInfoJSONName = "server";

		// Token: 0x040038C0 RID: 14528
		private const string CDN = "cdn";

		// Token: 0x040038C1 RID: 14529
		private const string VERSION_JSON = "versionJson";

		// Token: 0x040038C2 RID: 14530
		private const string IGNORE_VARIANT_LIST = "IgnoreVariantList";

		// Token: 0x040038C3 RID: 14531
		private const string DEFAULT_TAG_SET = "defaultTagSet";

		// Token: 0x040038C4 RID: 14532
		public const int LOGIN_SERVER_PORT = 22000;

		// Token: 0x040038C5 RID: 14533
		public const string DEFAULT_SERVER_ADDRESS = "192.168.0.201";

		// Token: 0x040038C6 RID: 14534
		public const string m_LOCAL_SAVE_LAST_LOGIN_SERVER_TYPE = "LOCAL_SAVE_LAST_LOGIN_SERVER_TYPE";

		// Token: 0x040038C7 RID: 14535
		public static string s_LoginFailMsg = "";

		// Token: 0x040038C8 RID: 14536
		public static string s_ServerType;

		// Token: 0x040038C9 RID: 14537
		private static Dictionary<NKCConnectionInfo.SERVER_INFO_TYPE, string> m_dicServerInfoTypeString = new Dictionary<NKCConnectionInfo.SERVER_INFO_TYPE, string>
		{
			{
				NKCConnectionInfo.SERVER_INFO_TYPE.Original,
				"ServerInfo.json"
			},
			{
				NKCConnectionInfo.SERVER_INFO_TYPE.SelectServer,
				"ServerInfo_V2.json"
			}
		};

		// Token: 0x040038CA RID: 14538
		private static Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, NKCConnectionInfo.LoginServerInfo> m_dicLoginServerInfo = new Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, NKCConnectionInfo.LoginServerInfo>();

		// Token: 0x040038CB RID: 14539
		private static readonly Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, string> m_dicLoginServerPostFix = new Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, string>
		{
			{
				NKCConnectionInfo.LOGIN_SERVER_TYPE.None,
				""
			},
			{
				NKCConnectionInfo.LOGIN_SERVER_TYPE.Default,
				""
			},
			{
				NKCConnectionInfo.LOGIN_SERVER_TYPE.Korea,
				"_KOR"
			},
			{
				NKCConnectionInfo.LOGIN_SERVER_TYPE.Global,
				"_GLOBAL"
			},
			{
				NKCConnectionInfo.LOGIN_SERVER_TYPE.Max,
				""
			}
		};

		// Token: 0x040038CC RID: 14540
		private static NKCConnectionInfo.LOGIN_SERVER_TYPE m_currentLoginServerType = NKCConnectionInfo.LOGIN_SERVER_TYPE.None;

		// Token: 0x040038D1 RID: 14545
		private static DateTime m_lastDownloadedTime;

		// Token: 0x040038D2 RID: 14546
		private static double DOWNLOAD_INTERVAL = 180.0;

		// Token: 0x040038D3 RID: 14547
		public static string m_downloadedConfigJSONString;

		// Token: 0x040038D4 RID: 14548
		private static bool m_isUnderMaintenance = false;

		// Token: 0x040038D5 RID: 14549
		public const string MAINTENANCE_DESC_STRING_KEY = "SI_SYSTEM_NOTICE_MAINTENANCE_DESC";

		// Token: 0x02001404 RID: 5124
		public enum LOGIN_SERVER_TYPE
		{
			// Token: 0x04009CFC RID: 40188
			None,
			// Token: 0x04009CFD RID: 40189
			Default,
			// Token: 0x04009CFE RID: 40190
			Korea,
			// Token: 0x04009CFF RID: 40191
			Global,
			// Token: 0x04009D00 RID: 40192
			Max
		}

		// Token: 0x02001405 RID: 5125
		public enum SERVER_INFO_TYPE
		{
			// Token: 0x04009D02 RID: 40194
			Original,
			// Token: 0x04009D03 RID: 40195
			SelectServer
		}

		// Token: 0x02001406 RID: 5126
		public class LoginServerInfo
		{
			// Token: 0x1700180D RID: 6157
			// (get) Token: 0x0600A770 RID: 42864 RVA: 0x00349837 File Offset: 0x00347A37
			// (set) Token: 0x0600A771 RID: 42865 RVA: 0x0034983F File Offset: 0x00347A3F
			public string m_serviceIP { get; set; }

			// Token: 0x1700180E RID: 6158
			// (get) Token: 0x0600A772 RID: 42866 RVA: 0x00349848 File Offset: 0x00347A48
			// (set) Token: 0x0600A773 RID: 42867 RVA: 0x00349850 File Offset: 0x00347A50
			public int m_servicePort { get; set; }

			// Token: 0x0600A774 RID: 42868 RVA: 0x00349859 File Offset: 0x00347A59
			public LoginServerInfo(string ip = "", int port = 22000)
			{
				this.m_serviceIP = ip;
				this.m_servicePort = port;
			}

			// Token: 0x04009D06 RID: 40198
			public HashSet<string> m_defaultTagSet = new HashSet<string>();
		}
	}
}
