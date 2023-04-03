using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AssetBundles;
using Cs.Engine.Util;
using NKC.Patcher;
using NKC.Publisher;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace NKC.InfraTool
{
	// Token: 0x02000893 RID: 2195
	public class PatchCheckController : IConfigChecker
	{
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005767 RID: 22375 RVA: 0x001A3DA0 File Offset: 0x001A1FA0
		public string VersionInfoStr
		{
			get
			{
				this._tempSb.Clear();
				this._tempSb.Append("DownLoadAddress      : ").Append(this._patcher.DownLoadAddress).AppendLine();
				NKCPatchInfo currentPatchInfo = this._patcher.CurrentPatchInfo;
				if (!string.IsNullOrEmpty((currentPatchInfo != null) ? currentPatchInfo.VersionString : null))
				{
					this._tempSb.Append("Current Version      : ").Append(this._patcher.CurrentPatchInfo.VersionString).AppendLine();
				}
				StringBuilder stringBuilder = this._tempSb.Append("Next Version         : ");
				NKCPatchInfo filteredPatchInfo = this._patcher.FilteredPatchInfo;
				stringBuilder.Append((filteredPatchInfo != null) ? filteredPatchInfo.VersionString : null).AppendLine();
				this._tempSb.Append("LoginServerIP        : ").Append(this._patcher.ServiceIP).AppendLine();
				this._tempSb.Append("LoginServerPort      : ").Append(this._patcher.ServicePort).AppendLine();
				return this._tempSb.ToString();
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005768 RID: 22376 RVA: 0x001A3EB4 File Offset: 0x001A20B4
		public string logStr
		{
			get
			{
				if (this.OpenTag || this.ContentTag)
				{
					return this._lastLog;
				}
				if (this._sb.Length > 0)
				{
					string text = this._sb.ToString();
					if (!string.IsNullOrEmpty(text) && !string.Equals(this._lastLog, text, StringComparison.Ordinal))
					{
						this._lastLog = text;
					}
				}
				return this._lastLog;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x001A3F16 File Offset: 0x001A2116
		// (set) Token: 0x0600576A RID: 22378 RVA: 0x001A3F20 File Offset: 0x001A2120
		public bool OpenTag
		{
			get
			{
				return this._openTag;
			}
			set
			{
				if (this._openTag == value)
				{
					return;
				}
				this._openTag = value;
				if (this._openTag)
				{
					this.ContentTag = false;
					this._lastLog = string.Empty;
					int count = TempLoginConnector.TempLoginConnectReceiver.OpenTagList.Count;
					for (int i = 0; i < count; i++)
					{
						string str = TempLoginConnector.TempLoginConnectReceiver.OpenTagList[i];
						this._lastLog = this._lastLog + "[" + str + "] ";
						if (i % 2 == 0)
						{
							this._lastLog += "\n";
						}
					}
				}
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600576B RID: 22379 RVA: 0x001A3FB2 File Offset: 0x001A21B2
		// (set) Token: 0x0600576C RID: 22380 RVA: 0x001A3FBC File Offset: 0x001A21BC
		public bool ContentTag
		{
			get
			{
				return this._contentTag;
			}
			set
			{
				if (this._contentTag == value)
				{
					return;
				}
				this._contentTag = value;
				if (this._contentTag)
				{
					this.OpenTag = false;
					this._lastLog = string.Empty;
					int count = TempLoginConnector.TempLoginConnectReceiver.ContentTagList.Count;
					for (int i = 0; i < count; i++)
					{
						string str = TempLoginConnector.TempLoginConnectReceiver.ContentTagList[i];
						this._lastLog = this._lastLog + "[" + str + "] ";
						if (i % 3 == 0)
						{
							this._lastLog += "\n";
						}
					}
				}
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x001A4057 File Offset: 0x001A2257
		// (set) Token: 0x0600576D RID: 22381 RVA: 0x001A404E File Offset: 0x001A224E
		public string ErrorSolutionStr { get; set; }

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600576F RID: 22383 RVA: 0x001A405F File Offset: 0x001A225F
		// (set) Token: 0x06005770 RID: 22384 RVA: 0x001A406C File Offset: 0x001A226C
		public string BaseFileServerAddress
		{
			get
			{
				return this._patcher.BaseFileServerAddress;
			}
			set
			{
				this._patcher.BaseFileServerAddress = value;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x001A407A File Offset: 0x001A227A
		// (set) Token: 0x06005772 RID: 22386 RVA: 0x001A409C File Offset: 0x001A229C
		public string ProtocolVersion
		{
			get
			{
				if (this._tempConnector == null)
				{
					return string.Empty;
				}
				return this._tempConnector.Protocol.ToString();
			}
			set
			{
				if (this._tempConnector == null || !this._tempConnector.IsConnected)
				{
					return;
				}
				int protocol;
				if (!int.TryParse(value, out protocol))
				{
					return;
				}
				this._tempConnector.Protocol = protocol;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x001A40D6 File Offset: 0x001A22D6
		public string PatchFileAddress
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06005774 RID: 22388 RVA: 0x001A40DD File Offset: 0x001A22DD
		public string DownLoadAddress
		{
			get
			{
				return this._patcher.DownLoadAddress;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06005775 RID: 22389 RVA: 0x001A40EA File Offset: 0x001A22EA
		public string DefaultVersionJson
		{
			get
			{
				return "/version.json";
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x001A40FA File Offset: 0x001A22FA
		// (set) Token: 0x06005776 RID: 22390 RVA: 0x001A40F1 File Offset: 0x001A22F1
		public string AssetBundleVersion { get; set; }

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06005778 RID: 22392 RVA: 0x001A4102 File Offset: 0x001A2302
		public NKCPatchDownloader.VersionStatus vs
		{
			get
			{
				return NKCPatchDownloader.VersionStatus.Error;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x001A4105 File Offset: 0x001A2305
		private TempPatcher _patcher
		{
			get
			{
				if (this.UseExtraAsset)
				{
					return this._extraPatcher;
				}
				return this._defaultPatcher;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600577A RID: 22394 RVA: 0x001A411C File Offset: 0x001A231C
		public bool IsSaveToTag
		{
			get
			{
				List<string> openTagList = TempLoginConnector.TempLoginConnectReceiver.OpenTagList;
				if (openTagList == null || openTagList.Count <= 0)
				{
					List<string> contentTagList = TempLoginConnector.TempLoginConnectReceiver.ContentTagList;
					return contentTagList != null && contentTagList.Count > 0;
				}
				return true;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x001A414C File Offset: 0x001A234C
		public NKCPatchInfo PatchInfo
		{
			get
			{
				if (this._patchInfo == null)
				{
					string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
					this._patchInfo = NKCPatchInfo.LoadFromJSON(localDownloadPath + "/LatestPatchInfo.json");
				}
				return this._patchInfo;
			}
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x001A4184 File Offset: 0x001A2384
		public void Init()
		{
			BetterStreamingAssets.Initialize();
			this._defaultPatcher = new TempPatcher(new Action<TempPatcher.VersionJsonErrorType, TempPatcher>(this.OnDownloadVersionError), new Action<TempPatcher.DownloadErrorType, TempPatcher>(this.OnDownLoadError));
			this._extraPatcher = new TempPatcher(new Action<TempPatcher.VersionJsonErrorType, TempPatcher>(this.OnDownloadVersionError), new Action<TempPatcher.DownloadErrorType, TempPatcher>(this.OnDownLoadError))
			{
				UseExtraDownload = true
			};
			NKCPublisherModule.InitInstance(new NKCPublisherModule.OnComplete(this.InitInstance));
			if (NKCDefineManager.DEFINE_EXTRA_ASSET() || NKCDefineManager.DEFINE_ZLONG_CHN())
			{
				LegacyPatchDownloader.InitInstance(new NKCPatchDownloader.OnError(this.DownLoaderInitError));
			}
			else
			{
				NKCPatchParallelDownloader.InitInstance(new NKCPatchDownloader.OnError(this.DownLoaderInitError));
			}
			Application.logMessageReceived -= this.Handling;
			Application.logMessageReceived += this.Handling;
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x001A424A File Offset: 0x001A244A
		public void DownLoaderInitError(string error)
		{
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x001A424C File Offset: 0x001A244C
		public void OnDownloadVersionError(TempPatcher.VersionJsonErrorType errorType, TempPatcher patcher)
		{
			switch (errorType)
			{
			case TempPatcher.VersionJsonErrorType.Ok:
				this.ErrorSolutionStr = string.Empty;
				return;
			case TempPatcher.VersionJsonErrorType.ConfigFilePathError:
				this.ErrorSolutionStr = "Config 파일 경로 오류 -> [" + patcher.ConfigAddress + "]";
				return;
			case TempPatcher.VersionJsonErrorType.DownServerAddressFail:
				this.ErrorSolutionStr = "Config 파일에 [ServerAddress 1,2] 확인 필요 \n 받아온 ServerAddress => [ " + patcher.PatchFileAddress + " ]";
				return;
			case TempPatcher.VersionJsonErrorType.VersionListNodeIsNull:
				this.ErrorSolutionStr = "Config 파일에 [versionList]를 찾지 못함 \n => [" + patcher.VersionAddress + "]";
				return;
			case TempPatcher.VersionJsonErrorType.VersionJsonParseFail:
				this.ErrorSolutionStr = "JSON 파싱 실패, 해당 주소 파일 확인하기 \n => [" + patcher.VersionAddress + "] ";
				return;
			case TempPatcher.VersionJsonErrorType.PatchInfoFail:
				this.ErrorSolutionStr = "새로운 Manifest 만들기 실패 인프라팀에 문의해주세요.";
				return;
			default:
				throw new ArgumentOutOfRangeException("errorType", errorType, null);
			}
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x001A4318 File Offset: 0x001A2518
		public void OnDownLoadError(TempPatcher.DownloadErrorType errorType, TempPatcher patcher)
		{
			switch (errorType)
			{
			case TempPatcher.DownloadErrorType.Ok:
			case TempPatcher.DownloadErrorType.NotExistDownloadFile:
				this.Handling("DownLoad OK!", "ss", LogType.Log);
				this.ErrorSolutionStr = string.Empty;
				return;
			case TempPatcher.DownloadErrorType.FilesToDownloadIsNull:
				this.ErrorSolutionStr = "다운로드 패치가 안되어있음 [Config Update] 버튼을 다시 눌러주세요.";
				return;
			case TempPatcher.DownloadErrorType.ErrorStop:
				this.ErrorSolutionStr = "다운로드 중 특정 파일에서 실패";
				return;
			case TempPatcher.DownloadErrorType.DownLoadError:
				this.ErrorSolutionStr = "다운 중 실패. [" + patcher.AssetBundleVersion + "] \nversion.json 파일이 제대로 되어있는지 확인 후 문제 없다면 인프라팀에 알려주세요.";
				return;
			default:
				throw new ArgumentOutOfRangeException("errorType", errorType, null);
			}
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x001A43A3 File Offset: 0x001A25A3
		public void InitInstance(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalError = null)
		{
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x001A43A5 File Offset: 0x001A25A5
		public void Lua()
		{
			NKCAssetResourceManager.LoadAssetBundleNamingFromLua("AB_SCRIPT", "LUA_ASSET_BUNDLE_FILE_LIST");
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x001A43B8 File Offset: 0x001A25B8
		public void Handling(string log, string stackTrace, LogType type)
		{
			if (string.IsNullOrEmpty(stackTrace))
			{
				return;
			}
			if (!log.Contains("[Patcher]"))
			{
				return;
			}
			this._sb.Append(string.Format("[{0}] ", type)).Append(log).AppendLine();
			if (log.Contains("[Error]"))
			{
				this._sb.Append("================================ [error stack trace] ================================").AppendLine();
				this._sb.Append(stackTrace);
				this._sb.Append("================================ =================== ================================").AppendLine();
			}
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x001A4449 File Offset: 0x001A2649
		public IEnumerator ConfigRequest()
		{
			this.ErrorSolutionStr = string.Empty;
			this._sb.Clear();
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [ConfigRequest]", "ss", LogType.Log);
			this._patcher.Init();
			this._tempConnector.SimulateDisconnect();
			yield return this._patcher.GetAddressOrLoginServer();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				yield break;
			}
			yield return this._patcher.GetVersionJson();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				yield break;
			}
			yield return this._patcher.GetPatchInfo();
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [ConfigRequest] [End]", "ss", LogType.Log);
			yield break;
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001A4458 File Offset: 0x001A2658
		public IEnumerator StartDownLoad()
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [StartDownLoad]", "ss", LogType.Log);
			yield return this._patcher.DownloadProcess();
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [StartDownLoad] [End]", "ss", LogType.Log);
			yield break;
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x001A4467 File Offset: 0x001A2667
		public IEnumerator UpdateServerInfo()
		{
			yield return this.UpdateDownloadServerAddressProcess();
			if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				Debug.Log("UpdateDownloadServerAddressProcess Failed!");
			}
			yield return this.UpdateServerInfomationProcess();
			if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				Debug.Log("UpdateServerInfomationProcess Failed!");
			}
			yield return this.UpdateServerContentTagProcess();
			if (this.m_eResultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				Debug.Log("UpdateServerContentTagProcess Failed!");
			}
			yield break;
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x001A4476 File Offset: 0x001A2676
		private IEnumerator UpdateDownloadServerAddressProcess()
		{
			string localDownloadPath = AssetBundleManager.GetLocalDownloadPath();
			string str = "CSConfigServerAddress.txt";
			if (NKCDefineManager.DEFINE_SB_GB())
			{
				str = "csconfigserveraddress.txt";
			}
			string text = Application.streamingAssetsPath + "/" + str;
			Debug.Log("CSConfigServerAddressPath : " + text);
			if (NKCPatchUtility.IsFileExists(text))
			{
				Debug.Log("CSConfigServerAddress exist");
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
					string targetFileName = Path.Combine(localDownloadPath, "CSConfig.txt");
					string vidSavePath = targetFileName;
					string url = jsonnode["address"];
					string text2 = jsonnode["languageTag"];
					if (text2 != null)
					{
						NKCPublisherModule.Localization.SetDefaultLanage((NKM_NATIONAL_CODE)Enum.Parse(typeof(NKM_NATIONAL_CODE), text2));
					}
					if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
					}
					int tryCountMax = 1;
					if (NKCDefineManager.DEFINE_ZLONG())
					{
						tryCountMax = 10;
					}
					bool flag = false;
					int i = 0;
					while (i < tryCountMax)
					{
						using (UnityWebRequest uwr = new UnityWebRequest(url))
						{
							uwr.method = "GET";
							uwr.downloadHandler = new DownloadHandlerFile(vidSavePath)
							{
								removeFileOnAbort = true
							};
							yield return uwr.SendWebRequest();
							if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
							{
								Debug.Log(uwr.error);
								if (i + 1 >= tryCountMax)
								{
									this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_DOWNLOAD_ADDRESS;
									yield break;
								}
								yield return new WaitForSecondsRealtime(1f);
								goto IL_462;
							}
							else
							{
								Debug.Log("Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
								flag = true;
								if (NKCPatchUtility.IsFileExists(targetFileName))
								{
									aJSON = File.ReadAllText(targetFileName);
									JSONNode jsonnode2 = JSONNode.Parse(aJSON);
									if (jsonnode2 != null)
									{
										NKCConnectionInfo.DownloadServerAddress = jsonnode2["PatchServerAddress1"];
										NKCConnectionInfo.DownloadServerAddress2 = jsonnode2["PatchServerAddress2"];
										NKCDownloadConfig.s_ServerID = jsonnode2["ServerId"];
										NKCDownloadConfig.s_ServerName = jsonnode2["ServerName"];
										NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, jsonnode2["CSLoginServerIP"], int.Parse(jsonnode2["CSLoginServerPort"]), null);
										JSONNode jsonnode3 = jsonnode2["IgnoreVariantList"];
										foreach (JSONNode d in ((jsonnode3 != null) ? jsonnode3.AsArray : null).Children)
										{
											NKCConnectionInfo.IgnoreVariantList.Add(d);
										}
										if (NKCDefineManager.DEFINE_USE_CUSTOM_SERVERS())
										{
											Debug.Log("Defined custom servers - checking for ServiceAddress Redirection");
											string @string = PlayerPrefs.GetString("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP");
											int @int = PlayerPrefs.GetInt("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT");
											if (!string.IsNullOrEmpty(@string))
											{
												Debug.Log(string.Concat(new string[]
												{
													"ServiceIP Redirected [",
													NKCConnectionInfo.ServiceIP,
													"] -> [",
													@string,
													"]"
												}));
												NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, @string, -1, null);
											}
											if (@int != 0)
											{
												Debug.Log(string.Format("ServicePort Redirected [{0}] -> [{1}]", NKCConnectionInfo.ServicePort, @int));
												NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.LOGIN_SERVER_TYPE.Default, "", @int, null);
											}
										}
									}
								}
							}
						}
						goto JumpOutOfTryFinally-3;
						IL_475:
						int num = i;
						i = num + 1;
						continue;
						JumpOutOfTryFinally-3:
						UnityWebRequest uwr = null;
						if (flag)
						{
							break;
						}
						IL_462:
						goto IL_475;
					}
					targetFileName = null;
					vidSavePath = null;
					url = null;
				}
			}
			this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			yield break;
			yield break;
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x001A4485 File Offset: 0x001A2685
		private IEnumerator UpdateServerInfomationProcess()
		{
			string serverConfigPath = NKCPublisherModule.ServerInfo.GetServerConfigPath();
			Debug.Log("ServerInfo from " + serverConfigPath);
			using (UnityWebRequest uwr = new UnityWebRequest(serverConfigPath))
			{
				uwr.downloadHandler = new DownloadHandlerBuffer();
				yield return uwr.SendWebRequest();
				if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.Log(uwr.error);
					this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_SERVERINFO_UPDATE;
					yield break;
				}
				NKCConnectionInfo.LoadFromJSON(uwr.downloadHandler.text);
			}
			UnityWebRequest uwr = null;
			this.m_eResultCode = NKC_PUBLISHER_RESULT_CODE.NPRC_OK;
			yield break;
			yield break;
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x001A4494 File Offset: 0x001A2694
		private IEnumerator UpdateServerContentTagProcess()
		{
			string serviceIP = NKCConnectionInfo.ServiceIP;
			Debug.Log("Trying to retrieve server tag from " + serviceIP);
			yield return ContentsVersionChecker.GetVersion(serviceIP, -1, NKCPublisherModule.ServerInfo.GetUseLocalSaveLastServerInfoToGetTags());
			this.m_eResultCode = ((ContentsVersionChecker.Ack != null) ? NKC_PUBLISHER_RESULT_CODE.NPRC_OK : NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_TAG);
			if (this.m_eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
			{
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagProvided, 0, null);
			}
			else if (this.m_eResultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_SERVERINFO_FAIL_FETCH_TAG)
			{
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagGetFailed, 0, null);
			}
			yield break;
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x001A44A3 File Offset: 0x001A26A3
		public IEnumerator RunAll()
		{
			this.UseExtraAsset = false;
			yield return this.ConfigRequest();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				this.Handling("[Patcher] ConfigRequest fail", "ss", LogType.Log);
				yield break;
			}
			yield return this.StartDownLoad();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				this.Handling("[Patcher] StartDownLoad fail", "ss", LogType.Log);
				yield break;
			}
			this.RequestLoginConnection();
			yield break;
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x001A44B2 File Offset: 0x001A26B2
		public IEnumerator ExtraAssetRunAll()
		{
			this.UseExtraAsset = true;
			yield return this.ConfigRequest();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				this.Handling("[Patcher] ConfigRequest fail", "ss", LogType.Log);
				yield break;
			}
			yield return this.StartDownLoad();
			if (!string.IsNullOrEmpty(this.ErrorSolutionStr))
			{
				this.Handling("[Patcher] StartDownLoad fail", "ss", LogType.Log);
				yield break;
			}
			this.RequestLoginConnection();
			yield break;
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x001A44C4 File Offset: 0x001A26C4
		public void RequestLoginConnection()
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [Connection]", "ss", LogType.Log);
			this._tempConnector.SimulateDisconnect();
			this._tempConnector.Connect(this._patcher.ServiceIP, this._patcher.ServicePort, new Action(this.OnFailedConnect), new Action(this.OnConnected), new Action(this.OnDisconnected));
			this._tempConnector.SetPacketReceiveAction(new TempLoginConnector.TempLoginConnectReceiver.OnFailPacket(this.OnFailPacket));
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x001A4549 File Offset: 0x001A2749
		public void OnFailedConnect()
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [Connection] [Fail]", "ss", LogType.Log);
			this.ErrorSolutionStr = "연결 실패! 서버가 실행되고 있는지 확인해주세요.";
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x001A4567 File Offset: 0x001A2767
		public void OnConnected()
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [Connection] [Success]", "ss", LogType.Log);
			this._tempConnector.SendDevLogin();
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x001A4585 File Offset: 0x001A2785
		public void OnDisconnected()
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [Connection] [Disconnected]", "ss", LogType.Log);
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x001A4598 File Offset: 0x001A2798
		public void OnFailPacket(string str)
		{
			this.Handling("[Patcher] ------------------------------------------------------------------------------------------------------------- [Packet Error]", "ss", LogType.Log);
			this.ErrorSolutionStr = str;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x001A45B2 File Offset: 0x001A27B2
		public void Update()
		{
			this._tempConnector.Update();
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x001A45C0 File Offset: 0x001A27C0
		public void SaveLogToText()
		{
			if (this._sb.Length == 0)
			{
				return;
			}
			string text = Application.persistentDataPath + "/Patch";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = string.Format("{0}/{1}_{2}_{3}_PatchLog.txt", new object[]
			{
				text,
				DateTime.Now.Hour,
				DateTime.Now.Minute,
				DateTime.Now.Second
			});
			FileMode mode = FileMode.Create;
			this.Handling("[Patcher] filePath => " + text2, "SaveLogToText", LogType.Log);
			if (File.Exists(text2))
			{
				mode = FileMode.Truncate;
			}
			new StreamWriter(File.Open(text2, mode, FileAccess.Write, FileShare.Read))
			{
				NewLine = null,
				AutoFlush = false,
				AutoFlush = true
			}.Write(this._sb.ToString());
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x001A46A4 File Offset: 0x001A28A4
		public void SaveTagListToText()
		{
			List<string> contentTagList = TempLoginConnector.TempLoginConnectReceiver.ContentTagList;
			if (contentTagList == null || contentTagList.Count == 0)
			{
				return;
			}
			List<string> openTagList = TempLoginConnector.TempLoginConnectReceiver.OpenTagList;
			if (openTagList == null || openTagList.Count == 0)
			{
				return;
			}
			string text = Application.persistentDataPath + "/Patch";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = string.Format("{0}/{1}_{2}_{3}_PatchTag.txt", new object[]
			{
				text,
				DateTime.Now.Hour,
				DateTime.Now.Minute,
				DateTime.Now.Second
			});
			FileMode mode = FileMode.Create;
			this.Handling("[Patcher] filePath => " + text2, "SaveTagListToText", LogType.Log);
			if (File.Exists(text2))
			{
				mode = FileMode.Truncate;
			}
			StreamWriter streamWriter = new StreamWriter(File.Open(text2, mode, FileAccess.Write, FileShare.Read));
			streamWriter.AutoFlush = true;
			streamWriter.Write("ip : " + this._patcher.ServiceIP);
			streamWriter.WriteLine();
			streamWriter.Write("version : " + this._patcher.VersionAddress);
			streamWriter.WriteLine();
			streamWriter.Write("------------------ content tag ------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			foreach (string value in contentTagList)
			{
				streamWriter.Write(value);
				streamWriter.WriteLine();
			}
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Write("------------------- open tag --------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			foreach (string value2 in openTagList)
			{
				streamWriter.Write(value2);
				streamWriter.WriteLine();
			}
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Write("-------------------------------------------------");
			streamWriter.WriteLine();
			streamWriter.Close();
		}

		// Token: 0x04004533 RID: 17715
		public const string LogKey = "[Patcher]";

		// Token: 0x04004534 RID: 17716
		private const string lineStr = "-------------------------------------------------------------------------------------------------------------";

		// Token: 0x04004535 RID: 17717
		private string _lastLog;

		// Token: 0x04004536 RID: 17718
		private bool _openTag;

		// Token: 0x04004537 RID: 17719
		private bool _contentTag;

		// Token: 0x0400453A RID: 17722
		private readonly StringBuilder _tempSb = new StringBuilder();

		// Token: 0x0400453B RID: 17723
		private readonly StringBuilder _sb = new StringBuilder();

		// Token: 0x0400453C RID: 17724
		private TempPatcher _defaultPatcher;

		// Token: 0x0400453D RID: 17725
		private TempPatcher _extraPatcher;

		// Token: 0x0400453E RID: 17726
		public bool UseExtraAsset;

		// Token: 0x0400453F RID: 17727
		private readonly TempLoginConnector _tempConnector = new TempLoginConnector(typeof(TempLoginConnector.TempLoginConnectReceiver));

		// Token: 0x04004540 RID: 17728
		private NKCPatchInfo _patchInfo;

		// Token: 0x04004541 RID: 17729
		private NKC_PUBLISHER_RESULT_CODE m_eResultCode;
	}
}
