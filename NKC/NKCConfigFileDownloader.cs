using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AssetBundles;
using Cs.Logging;
using NKC.Patcher;
using NKC.Publisher;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace NKC
{
	// Token: 0x02000739 RID: 1849
	public class NKCConfigFileDownloader : MonoBehaviour
	{
		// Token: 0x060049D9 RID: 18905 RVA: 0x001629EE File Offset: 0x00160BEE
		public void DownloadConfigFile(string configAddressFilePath, Action<NKC_PUBLISHER_RESULT_CODE> callBackResult)
		{
			this.m_ConfigAddressFilePath = configAddressFilePath;
			base.StartCoroutine(this._DownloadConfigFile(callBackResult));
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x00162A05 File Offset: 0x00160C05
		private IEnumerator _DownloadConfigFile(Action<NKC_PUBLISHER_RESULT_CODE> callBackResult)
		{
			bool bWait = true;
			NKC_PUBLISHER_RESULT_CODE resultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			base.StartCoroutine(this._DownloadConfigFileFromPath(delegate(NKC_PUBLISHER_RESULT_CODE code)
			{
				resultCode = code;
				bWait = false;
			}));
			while (bWait)
			{
				yield return null;
			}
			this.m_downLoadedConfigFileData = this.ReadConfigFile();
			if (this.m_downLoadedConfigFileData == null)
			{
				resultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_DOWNLOAD_ADDRESS;
			}
			if (callBackResult != null)
			{
				callBackResult(resultCode);
			}
			yield break;
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x00162A1B File Offset: 0x00160C1B
		private IEnumerator _DownloadConfigFileFromPath(Action<NKC_PUBLISHER_RESULT_CODE> callBack)
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			if (NKCPatchUtility.IsFileExists(this.m_ConfigAddressFilePath))
			{
				string aJSON;
				if (this.m_ConfigAddressFilePath.Contains("jar:"))
				{
					aJSON = BetterStreamingAssets.ReadAllText(NKCAssetbundleInnerStream.GetJarRelativePath(this.m_ConfigAddressFilePath));
				}
				else
				{
					aJSON = File.ReadAllText(this.m_ConfigAddressFilePath);
				}
				JSONNode jsonnode = JSONNode.Parse(aJSON);
				if (jsonnode != null)
				{
					string text = jsonnode["address"];
					string url = text;
					this.m_ConfigFilePath = Path.Combine(localDownloadPath, "CSConfig.txt");
					string vidSavePath = this.m_ConfigFilePath;
					if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
					}
					int tryCountMax = 1;
					if (NKCDefineManager.DEFINE_ZLONG() && NKCDefineManager.DEFINE_IOS())
					{
						tryCountMax = 10;
					}
					int i = 0;
					while (i < tryCountMax)
					{
						bool flag2;
						using (UnityWebRequest uwr = new UnityWebRequest(url))
						{
							uwr.method = "GET";
							uwr.downloadHandler = new DownloadHandlerFile(vidSavePath)
							{
								removeFileOnAbort = true
							};
							yield return uwr.SendWebRequest();
							bool flag = false;
							if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
							{
								if (NKCDefineManager.DEFINE_USE_CHEAT())
								{
									Log.Debug(string.Format("[SendWebRequest] Path[{0}] ConnectionError[{1}] Error[{2}]", url, uwr.result, uwr.error), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCConfigFileDownloader.cs", 135);
								}
								flag = true;
							}
							if (flag)
							{
								Debug.Log(uwr.error);
								if (i + 1 >= tryCountMax)
								{
									if (callBack != null)
									{
										callBack(NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_DOWNLOAD_ADDRESS);
									}
									yield break;
								}
								yield return new WaitForSecondsRealtime(1f);
								goto IL_29D;
							}
							else
							{
								Debug.Log("Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
								flag2 = true;
							}
						}
						goto JumpOutOfTryFinally-3;
						IL_2C4:
						int num = i;
						i = num + 1;
						continue;
						JumpOutOfTryFinally-3:
						UnityWebRequest uwr = null;
						if (flag2)
						{
							if (callBack == null)
							{
								break;
							}
							callBack(NKC_PUBLISHER_RESULT_CODE.NPRC_OK);
							break;
						}
						IL_29D:
						goto IL_2C4;
					}
					url = null;
					vidSavePath = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00162A34 File Offset: 0x00160C34
		private NKCConfigFileDownloader.NKCConfigFileData ReadConfigFile()
		{
			NKCConfigFileDownloader.NKCConfigFileData nkcconfigFileData = new NKCConfigFileDownloader.NKCConfigFileData();
			if (!NKCPatchUtility.IsFileExists(this.m_ConfigFilePath))
			{
				Debug.Log("TargetFile does not exist. FileName [" + this.m_ConfigFilePath + "]");
				return null;
			}
			JSONNode jsonnode = JSONNode.Parse(File.ReadAllText(this.m_ConfigFilePath));
			if (jsonnode == null)
			{
				Debug.Log("Invalid json. FileName [" + this.m_ConfigFilePath + "]");
				return null;
			}
			nkcconfigFileData.m_PatchServerAddress1 = jsonnode["PatchServerAddress1"];
			nkcconfigFileData.m_ServerID = jsonnode["ServerId"];
			nkcconfigFileData.m_ServerName = jsonnode["ServerName"];
			nkcconfigFileData.m_LoginServerIP = jsonnode["CSLoginServerIP"];
			nkcconfigFileData.m_LoginServerPort = int.Parse(jsonnode["CSLoginServerPort"]);
			if (NKCDefineManager.DEFINE_ZLONG())
			{
				nkcconfigFileData.m_PatchServerAddress2 = jsonnode["PatchServerAddress2"];
				string aKey = "LOGIN_FAIL_MSG";
				nkcconfigFileData.m_NoticeMsg = jsonnode[aKey];
				nkcconfigFileData.m_DefaultCountrySet = jsonnode["DefaultCountryTagSet"];
				if (NKCDefineManager.DEFINE_PC_FORCE_VERSION_UP())
				{
					nkcconfigFileData.m_VecAllowedVersion.Clear();
					if (jsonnode["PC_ALLOWED_VERSION_LIST"] != null)
					{
						JSONArray asArray = jsonnode["PC_ALLOWED_VERSION_LIST"].AsArray;
						for (int i = 0; i < asArray.Count; i++)
						{
							nkcconfigFileData.m_VecAllowedVersion.Add(asArray[i]);
						}
					}
				}
			}
			return nkcconfigFileData;
		}

		// Token: 0x040038B7 RID: 14519
		public string m_ConfigFilePath = "";

		// Token: 0x040038B8 RID: 14520
		public string m_ConfigAddressFilePath = "";

		// Token: 0x040038B9 RID: 14521
		public NKCConfigFileDownloader.NKCConfigFileData m_downLoadedConfigFileData;

		// Token: 0x02001400 RID: 5120
		public class NKCConfigFileData
		{
			// Token: 0x04009CE2 RID: 40162
			public string m_PatchServerAddress1;

			// Token: 0x04009CE3 RID: 40163
			public string m_PatchServerAddress2;

			// Token: 0x04009CE4 RID: 40164
			public string m_ServerID;

			// Token: 0x04009CE5 RID: 40165
			public string m_ServerName;

			// Token: 0x04009CE6 RID: 40166
			public string m_LoginServerIP;

			// Token: 0x04009CE7 RID: 40167
			public int m_LoginServerPort;

			// Token: 0x04009CE8 RID: 40168
			public string m_DefaultCountrySet;

			// Token: 0x04009CE9 RID: 40169
			public string m_NoticeMsg;

			// Token: 0x04009CEA RID: 40170
			public List<string> m_VecAllowedVersion = new List<string>();
		}
	}
}
