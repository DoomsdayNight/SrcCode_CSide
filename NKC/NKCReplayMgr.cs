using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClientPacket.Game;
using ClientPacket.Pvp;
using ClientPacket.Service;
using Cs.GameServer.Replay;
using Cs.Logging;
using Cs.Protocol;
using NKC.UI;
using NKC.UI.Option;
using NKC.UI.Result;
using NKM;

namespace NKC
{
	// Token: 0x020006C2 RID: 1730
	public class NKCReplayMgr
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x0012F400 File Offset: 0x0012D600
		public ReplayData CurrentReplayData
		{
			get
			{
				return this.m_currentReplayData;
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0012F408 File Offset: 0x0012D608
		public static bool IsReplayLobbyTabOpened()
		{
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.FIREBASE_CRASH_TEST);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0012F418 File Offset: 0x0012D618
		public static bool IsReplayRecordingOpened()
		{
			return NKCScenManager.GetScenManager().GetNKCReplayMgr() != null && (NKCScenManager.GetScenManager().GetGameClient() == null || !NKCScenManager.GetScenManager().GetGameClient().IsObserver(NKCScenManager.CurrentUserData())) && (NKCReplayMgr.IsReplayOpened() || NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY_RECORDING));
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0012F46B File Offset: 0x0012D66B
		public static bool IsReplayOpened()
		{
			return NKCScenManager.GetScenManager().GetNKCReplayMgr() != null && NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x0012F486 File Offset: 0x0012D686
		public static bool IsRecording()
		{
			return NKCScenManager.GetScenManager().GetNKCReplayMgr() != null && NKCScenManager.GetScenManager().GetNKCReplayMgr().m_currentRMS == NKCReplayMgr.ReplayMgrState.RMS_Recording;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x0012F4AC File Offset: 0x0012D6AC
		public static bool IsPlayingReplay()
		{
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_REPLAY) && NKCScenManager.GetScenManager().GetNKCReplayMgr() != null && (NKCScenManager.GetScenManager().GetNKCReplayMgr().m_currentRMS == NKCReplayMgr.ReplayMgrState.RMS_Playing || NKCScenManager.GetScenManager().GetNKCReplayMgr().m_currentRMS == NKCReplayMgr.ReplayMgrState.RMS_PlayingResult);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x0012F4F8 File Offset: 0x0012D6F8
		public static NKCReplayMgr GetNKCReplaMgr()
		{
			if (NKCScenManager.GetScenManager() == null)
			{
				return null;
			}
			return NKCScenManager.GetScenManager().GetNKCReplayMgr();
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x0012F514 File Offset: 0x0012D714
		public void CreateNewReplayData(NKMGameData cNKMGameData, NKMGameRuntimeData cNKMGameRuntimeData)
		{
			if (!NKCReplayMgr.IsReplayRecordingOpened())
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_None)
			{
				return;
			}
			if (cNKMGameData == null || cNKMGameRuntimeData == null)
			{
				return;
			}
			if (this.m_replayRecorder != null)
			{
				this.SaveReplayData();
			}
			string text = this.MakeReplayDataFileName(cNKMGameData.m_GameUID);
			NKMGameData gameData = cNKMGameData.DeepCopy<NKMGameData>();
			this.ResetLoadingData(ref gameData);
			try
			{
				this.m_replayRecorder = new ReplayRecorder(text, gameData, cNKMGameRuntimeData);
				this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_Recording;
				Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Create New ReplayData [{0}] GameType[{1}]</color>", text, cNKMGameData.GetGameType()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 188);
			}
			catch
			{
				this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_None;
				Log.Info("<color=#FFFFF0FF>[Replay] Create New ReplayData Failed </color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 193);
			}
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0012F5D0 File Offset: 0x0012D7D0
		public void FillReplayData(NKMPacket_GAME_EMOTICON_NOT cNKMPacket_GAME_EMOTICON_NOT)
		{
			if (this.m_replayRecorder == null)
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Recording)
			{
				return;
			}
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null || gameClient.GetGameRuntimeData() == null)
			{
				return;
			}
			new NKMPacket_GAME_EMOTICON_NOT().DeepCopyFrom(cNKMPacket_GAME_EMOTICON_NOT);
			ReplayData.EmoticonData emoticonData = new ReplayData.EmoticonData();
			emoticonData.not = new NKMPacket_GAME_EMOTICON_NOT();
			emoticonData.not.DeepCopyFrom(cNKMPacket_GAME_EMOTICON_NOT);
			emoticonData.time = gameClient.GetGameRuntimeData().m_GameTime;
			this.m_replayRecorder.AddEmoticonData(emoticonData);
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x0012F64C File Offset: 0x0012D84C
		public void FillReplayData(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT cNKMPacket_GAME_SYNC_DATA_PACK_NOT)
		{
			if (this.m_replayRecorder == null)
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Recording)
			{
				return;
			}
			this.m_replayRecorder.AddSyncData(cNKMPacket_GAME_SYNC_DATA_PACK_NOT.DeepCopy<NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT>());
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0012F674 File Offset: 0x0012D874
		public void FillReplayData(NKMPacket_ASYNC_PVP_GAME_END_NOT cNKMPacket_ASYNC_PVP_GAME_END_NOT)
		{
			if (this.m_replayRecorder == null)
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Recording)
			{
				return;
			}
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			this.m_replayRecorder.SetGameResult(cNKMPacket_ASYNC_PVP_GAME_END_NOT.result, gameClient.GetGameRuntimeData().m_GameTime, cNKMPacket_ASYNC_PVP_GAME_END_NOT.gameRecord.DeepCopy<NKMGameRecord>());
			Log.Info("<color=#FFFFF0FF>[Replay] - GAME_END_NOT</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 257);
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0012F6DC File Offset: 0x0012D8DC
		public void FillReplayData(NKMPacket_GAME_END_NOT cNKMPacket_GAME_END_NOT)
		{
			if (this.m_replayRecorder == null)
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Recording)
			{
				return;
			}
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			this.m_replayRecorder.SetGameResult(cNKMPacket_GAME_END_NOT.pvpResultData.result, gameClient.GetGameRuntimeData().m_GameTime, cNKMPacket_GAME_END_NOT.gameRecord.DeepCopy<NKMGameRecord>());
			Log.Info("<color=#FFFFF0FF>[Replay] - GAME_END_NOT</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 275);
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x0012F747 File Offset: 0x0012D947
		public void StopRecording(bool saveData)
		{
			Log.Info("<color=#FFFFF0FF>[Replay] - StopRecording</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 280);
			if (saveData)
			{
				this.SaveReplayData();
			}
			this.m_replayRecorder = null;
			this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_None;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x0012F774 File Offset: 0x0012D974
		private void SaveReplayData()
		{
			if (this.m_replayRecorder == null)
			{
				this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_None;
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Recording)
			{
				return;
			}
			string userUIDString = "0";
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				userUIDString = nkmuserData.m_UserUID.ToString();
			}
			ReplayRecorder recorder = this.m_replayRecorder;
			Task.Run(() => recorder.FinishAsync(userUIDString));
			this.m_replayRecorder = null;
			this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_None;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x0012F7F4 File Offset: 0x0012D9F4
		public void ReadReplayData()
		{
			if (this.m_dicReplaydata == null)
			{
				this.m_dicReplaydata = new Dictionary<string, string>();
			}
			string path = "0";
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				path = nkmuserData.m_UserUID.ToString();
			}
			string text = Path.Combine(Path.Combine(NKCLogManager.GetSavePath(), "Replay"), path);
			if (!Directory.Exists(text))
			{
				Log.Info("<color=#FFFFF0FF>[Replay] Folder doesn't exist : " + text + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 336);
				return;
			}
			string[] files = Directory.GetFiles(text);
			Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Files in Folder : {0}</color>", files.Length), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 341);
			foreach (string text2 in files)
			{
				string text3 = Path.GetFileNameWithoutExtension(text2).ToLower();
				if (!text3.Contains("rp2_"))
				{
					Log.Info("<color=#FFFFF0FF>[Replay] Deleting File - old style : " + text2 + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 350);
					File.Delete(text2);
				}
				else if (!this.CheckForExistingPVPHistory(text3))
				{
					Log.Info("<color=#FFFFF0FF>[Replay] Deleting File - old gameUID : " + text2 + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 358);
					File.Delete(text2);
				}
				else if (!this.IsInReplayDataFileList(text3))
				{
					this.m_dicReplaydata.Add(text3, text2);
				}
			}
			Log.Info(string.Format("<color=#FFFFF0FF>[Replay] ReplayCount : {0}</color>", this.m_dicReplaydata.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 372);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x0012F972 File Offset: 0x0012DB72
		public string MakeReplayDataFileName(long gameUID)
		{
			return string.Format("rp2_{0}", gameUID);
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x0012F984 File Offset: 0x0012DB84
		public bool IsInReplayDataFileList(string fileName)
		{
			return this.m_dicReplaydata != null && this.m_dicReplaydata.ContainsKey(fileName.ToLower());
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x0012F9A4 File Offset: 0x0012DBA4
		public bool IsInReplayDataFileList(long gameUID)
		{
			string fileName = this.MakeReplayDataFileName(gameUID).ToLower();
			return this.IsInReplayDataFileList(fileName);
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x0012F9C8 File Offset: 0x0012DBC8
		public ReplayData GetReplayDataByUID(long gameUID)
		{
			if (this.m_dicReplaydata == null)
			{
				return null;
			}
			string text = this.MakeReplayDataFileName(gameUID).ToLower();
			if (!this.m_dicReplaydata.ContainsKey(text.ToLower()))
			{
				return null;
			}
			string path = "0";
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				path = nkmuserData.m_UserUID.ToString();
			}
			string text2 = Path.Combine(Path.Combine(NKCLogManager.GetSavePath(), "Replay"), path);
			if (!Directory.Exists(text2))
			{
				Log.Info("<color=#FFFFF0FF>[Replay] Folder doesn't exist 2: " + text2 + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 425);
				return null;
			}
			return ReplayLoader.Load(Path.Combine(text2, text + ".replay"));
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x0012FA74 File Offset: 0x0012DC74
		private bool CheckForExistingPVPHistory(string fileName)
		{
			string[] array = fileName.Split(new char[]
			{
				'_'
			});
			if (array == null || array.Length < 2)
			{
				return false;
			}
			long gameUID;
			if (!long.TryParse(array[1], out gameUID))
			{
				return false;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			return nkmuserData == null || nkmuserData.m_AsyncPvpHistory.GetDataByGameUID(gameUID) != null || nkmuserData.m_SyncPvpHistory.GetDataByGameUID(gameUID) != null || nkmuserData.m_LeaguePvpHistory.GetDataByGameUID(gameUID) != null || nkmuserData.m_PrivatePvpHistory.GetDataByGameUID(gameUID) != null;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x0012FAF8 File Offset: 0x0012DCF8
		public void OnGameScenEnd()
		{
			if (NKCReplayMgr.IsPlayingReplay())
			{
				this.StopPlaying();
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x0012FB07 File Offset: 0x0012DD07
		public void StopPlaying()
		{
			Log.Info("<color=#FFFFF0FF>[Replay] - StopPlaying</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 490);
			this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_None;
			this.m_currentReplayData = null;
			this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x0012FB34 File Offset: 0x0012DD34
		public void LeavePlaying()
		{
			Log.Info("<color=#FFFFF0FF>[Replay] - LeavePlaying</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 498);
			NKCScenManager.GetScenManager().GetGameClient().GetGameRuntimeData().m_GameTime = this.m_currentReplayData.gameEndTime;
			NKCScenManager.GetScenManager().GetGameClient().UI_GAME_PAUSE();
			NKCUIGameOption.Instance.RemoveCloseCallBack();
			NKCUIGameOption.Instance.Close();
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x0012FB98 File Offset: 0x0012DD98
		public void StartPlaying(ReplayData cReplayData)
		{
			if (!NKCReplayMgr.IsReplayOpened())
			{
				return;
			}
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_None)
			{
				return;
			}
			this.m_currentReplayData = null;
			this.m_currentReplayData = cReplayData;
			this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_Playing;
			this.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
			this.m_bFinishState = false;
			this.ResetLoadingData(ref this.m_currentReplayData.gameData);
			NKCScenManager.GetScenManager().GetGameClient().SetGameDataDummy(this.m_currentReplayData.gameData, false);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAME, true);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x0012FC14 File Offset: 0x0012DE14
		public void StartPlaying(long gameUID)
		{
			ReplayData replayDataByUID = this.GetReplayDataByUID(gameUID);
			if (replayDataByUID != null)
			{
				this.StartPlaying(replayDataByUID);
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x0012FC34 File Offset: 0x0012DE34
		private void ResetLoadingData(ref NKMGameData gameData)
		{
			if (gameData.m_NKMGameTeamDataA.m_listDynamicRespawnUnitData.Count > 0)
			{
				foreach (NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData in gameData.m_NKMGameTeamDataA.m_listDynamicRespawnUnitData)
				{
					nkmdynamicRespawnUnitData.m_bLoadedClient = false;
					nkmdynamicRespawnUnitData.m_bLoadedServer = false;
				}
			}
			if (gameData.m_NKMGameTeamDataB.m_listDynamicRespawnUnitData.Count > 0)
			{
				foreach (NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData2 in gameData.m_NKMGameTeamDataB.m_listDynamicRespawnUnitData)
				{
					nkmdynamicRespawnUnitData2.m_bLoadedClient = false;
					nkmdynamicRespawnUnitData2.m_bLoadedServer = false;
				}
			}
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x0012FD08 File Offset: 0x0012DF08
		public void SetPlayingGameSpeedType(NKM_GAME_SPEED_TYPE gameSpeedType)
		{
			this.m_NKM_GAME_SPEED_TYPE = gameSpeedType;
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x0012FD11 File Offset: 0x0012DF11
		public NKM_GAME_SPEED_TYPE GetPlayingGameSpeedType()
		{
			return this.m_NKM_GAME_SPEED_TYPE;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x0012FD1C File Offset: 0x0012DF1C
		public void PrintPoolingData(NKMGameData cNKMGameData)
		{
			Log.Info("<color=#FFFFF0FF>[Replay] --------------PrintPoolingData--------------</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 571);
			foreach (NKMUnitData nkmunitData in cNKMGameData.m_NKMGameTeamDataA.m_listUnitData)
			{
				Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Create TeamA - [{0}]</color>", nkmunitData.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 574);
			}
			foreach (NKMUnitData nkmunitData2 in cNKMGameData.m_NKMGameTeamDataB.m_listUnitData)
			{
				Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Create TeamB - [{0}]</color>", nkmunitData2.m_UnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 579);
			}
			if (cNKMGameData.m_NKMGameTeamDataA.m_listDynamicRespawnUnitData.Count > 0)
			{
				foreach (NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData in cNKMGameData.m_NKMGameTeamDataA.m_listDynamicRespawnUnitData)
				{
					foreach (short num in nkmdynamicRespawnUnitData.m_NKMUnitData.m_listGameUnitUID)
					{
						Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Create TeamA D - [{0}]</color>", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 588);
					}
				}
			}
			if (cNKMGameData.m_NKMGameTeamDataB.m_listDynamicRespawnUnitData.Count > 0)
			{
				foreach (NKMDynamicRespawnUnitData nkmdynamicRespawnUnitData2 in cNKMGameData.m_NKMGameTeamDataB.m_listDynamicRespawnUnitData)
				{
					foreach (short num2 in nkmdynamicRespawnUnitData2.m_NKMUnitData.m_listGameUnitUID)
					{
						Log.Info(string.Format("<color=#FFFFF0FF>[Replay] Create TeamB D - [{0}]</color>", num2), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 599);
					}
				}
			}
			Log.Info("<color=#FFFFF0FF>[Replay] --------------PrintPoolingData End---------------</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 603);
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x0012FF8C File Offset: 0x0012E18C
		private void PrintRespawnAndDieInfo(ReplayData cReplayData)
		{
			Log.Info("<color=#FFFFF0FF>[Replay] --------------PrintRespawnAndDieInfo--------------</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 608);
			foreach (NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT in cReplayData.syncList)
			{
				foreach (NKMGameSyncData_Base nkmgameSyncData_Base in nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameSyncDataPack.m_listGameSyncData)
				{
					foreach (NKMGameSyncData_Unit nkmgameSyncData_Unit in nkmgameSyncData_Base.m_NKMGameSyncData_Unit)
					{
						if (nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_bRespawnThisFrame)
						{
							Log.Info(string.Format("<color=#FFFFF0FF>[Replay] [{0}] respawn unit[{1}]</color>", nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT.gameTime, nkmgameSyncData_Unit.m_NKMGameUnitSyncData.m_GameUnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 617);
						}
					}
				}
			}
			Log.Info("<color=#FFFFF0FF>-------------------------------------------</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 624);
			foreach (NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT2 in cReplayData.syncList)
			{
				foreach (NKMGameSyncData_Base nkmgameSyncData_Base2 in nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT2.gameSyncDataPack.m_listGameSyncData)
				{
					foreach (NKMGameSyncData_DieUnit nkmgameSyncData_DieUnit in nkmgameSyncData_Base2.m_NKMGameSyncData_DieUnit)
					{
						foreach (short num in nkmgameSyncData_DieUnit.m_DieGameUnitUID)
						{
							Log.Info(string.Format("<color=#FFFFF0FF>[Replay] [{0}] die unit [{1}]</color>", nkmpacket_NPT_GAME_SYNC_DATA_PACK_NOT2.gameTime, num.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 633);
						}
					}
				}
			}
			Log.Info("<color=#FFFFF0FF>[Replay] --------------PrintRespawnAndDieInfo End--------------</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 638);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x00130204 File Offset: 0x0012E404
		public void Update(NKMGameRuntimeData cNKMGameRuntimeData)
		{
			if (this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_Playing && this.m_currentRMS != NKCReplayMgr.ReplayMgrState.RMS_PlayingResult)
			{
				return;
			}
			if (cNKMGameRuntimeData.m_GameTime >= this.m_currentReplayData.gameEndTime && this.m_currentRMS == NKCReplayMgr.ReplayMgrState.RMS_Playing)
			{
				BATTLE_RESULT_TYPE battleResultType;
				if (this.m_currentReplayData.pvpResult == PVP_RESULT.WIN)
				{
					battleResultType = BATTLE_RESULT_TYPE.BRT_WIN;
				}
				else if (this.m_currentReplayData.pvpResult == PVP_RESULT.LOSE)
				{
					battleResultType = BATTLE_RESULT_TYPE.BRT_LOSE;
				}
				else
				{
					battleResultType = BATTLE_RESULT_TYPE.BRT_DRAW;
				}
				NKCUIBattleStatistics.BattleData battleData = NKCUIBattleStatistics.MakeBattleData(NKCScenManager.GetScenManager().GetGameClient(), this.m_currentReplayData.gameRecord, this.m_currentReplayData.gameData.GetGameType());
				NKCUIResult.BattleResultData resultData = NKCUIResult.MakePvPResultData(battleResultType, null, battleData, this.m_currentReplayData.gameData.GetGameType());
				NKCScenManager.GetScenManager().Get_SCEN_GAME().ReserveGameEndData(resultData);
				this.m_currentRMS = NKCReplayMgr.ReplayMgrState.RMS_PlayingResult;
				return;
			}
			while (this.m_currentReplayData.emoticonList.Count > 0)
			{
				if (this.m_currentReplayData.emoticonList[0].time > cNKMGameRuntimeData.m_GameTime)
				{
					IL_392:
					while (this.m_currentReplayData.syncList.Count > 0 && this.m_currentReplayData.syncList[0].gameTime <= cNKMGameRuntimeData.m_GameTime)
					{
						foreach (NKMGameSyncData_Base nkmgameSyncData_Base in this.m_currentReplayData.syncList[0].gameSyncDataPack.m_listGameSyncData)
						{
							if (nkmgameSyncData_Base.m_NKMGameSyncData_GameState != null)
							{
								foreach (NKMGameSyncData_GameState nkmgameSyncData_GameState in nkmgameSyncData_Base.m_NKMGameSyncData_GameState)
								{
									switch (nkmgameSyncData_GameState.m_NKM_GAME_STATE)
									{
									case NKM_GAME_STATE.NGS_START:
									case NKM_GAME_STATE.NGS_PLAY:
									case NKM_GAME_STATE.NGS_END:
										Log.Info("<color=#FFFFF0FF>[Replay] SyncGameState [" + nkmgameSyncData_GameState.m_NKM_GAME_STATE.ToString() + "]</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 706);
										break;
									case NKM_GAME_STATE.NGS_FINISH:
										this.m_bFinishState = true;
										if (this.m_currentReplayData.pvpResult == PVP_RESULT.WIN)
										{
											cNKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_A1;
										}
										else if (this.m_currentReplayData.pvpResult == PVP_RESULT.LOSE)
										{
											cNKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_B1;
										}
										else
										{
											cNKMGameRuntimeData.m_WinTeam = NKM_TEAM_TYPE.NTT_DRAW;
										}
										Log.Info("<color=#FFFFF0FF>[Replay] SyncGameState [" + nkmgameSyncData_GameState.m_NKM_GAME_STATE.ToString() + "]</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 726);
										break;
									}
								}
							}
						}
						NKCScenManager.GetScenManager().GetGameClient().OnRecv(this.m_currentReplayData.syncList[0]);
						this.m_currentReplayData.syncList.RemoveAt(0);
						if (this.m_currentReplayData.syncList.Count <= 0 && !this.m_bFinishState)
						{
							Log.Info("<color=#FFFFF0FF>[Replay] This replay data has some logic error. Add SyncData include finish state. </color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCReplayMgr.cs", 740);
							NKM_TEAM_TYPE winTeam;
							if (this.m_currentReplayData.pvpResult == PVP_RESULT.WIN)
							{
								winTeam = NKM_TEAM_TYPE.NTT_A1;
							}
							else if (this.m_currentReplayData.pvpResult == PVP_RESULT.LOSE)
							{
								winTeam = NKM_TEAM_TYPE.NTT_B1;
							}
							else
							{
								winTeam = NKM_TEAM_TYPE.NTT_DRAW;
							}
							NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT item = new NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT
							{
								gameSyncDataPack = new NKMGameSyncDataPack
								{
									m_listGameSyncData = new List<NKMGameSyncData_Base>
									{
										new NKMGameSyncData_Base
										{
											m_NKMGameSyncData_GameState = new List<NKMGameSyncData_GameState>
											{
												new NKMGameSyncData_GameState
												{
													m_NKM_GAME_STATE = NKM_GAME_STATE.NGS_FINISH,
													m_WinTeam = winTeam
												}
											}
										}
									}
								}
							};
							this.m_currentReplayData.syncList.Add(item);
						}
					}
					return;
				}
				NKCScenManager.GetScenManager().GetGameClient().GetGameHud().GetNKCGameHudEmoticon().OnRecv(this.m_currentReplayData.emoticonList[0].not);
				this.m_currentReplayData.emoticonList.RemoveAt(0);
			}
			goto IL_392;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x001305F0 File Offset: 0x0012E7F0
		public void OnRecv(NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ cNKMPacket_INFORM_MY_LOADING_PROGRESS_REQ)
		{
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x001305F4 File Offset: 0x0012E7F4
		public void OnRecv(NKMPacket_GAME_LOAD_COMPLETE_REQ cNKMPacket_GAME_LOAD_COMPLETE_REQ)
		{
			NKMPacket_GAME_LOAD_COMPLETE_ACK nkmpacket_GAME_LOAD_COMPLETE_ACK = new NKMPacket_GAME_LOAD_COMPLETE_ACK();
			nkmpacket_GAME_LOAD_COMPLETE_ACK.gameRuntimeData = new NKMGameRuntimeData();
			nkmpacket_GAME_LOAD_COMPLETE_ACK.gameRuntimeData.m_NKM_GAME_STATE = NKM_GAME_STATE.NGS_START;
			nkmpacket_GAME_LOAD_COMPLETE_ACK.gameRuntimeData.m_NKM_GAME_SPEED_TYPE = NKM_GAME_SPEED_TYPE.NGST_1;
			NKCScenManager.GetScenManager().GetGameClient().OnRecv(nkmpacket_GAME_LOAD_COMPLETE_ACK);
			NKMPacket_GAME_START_NOT cNKMPacket_GAME_START_NOT = new NKMPacket_GAME_START_NOT();
			NKCScenManager.GetScenManager().Get_SCEN_GAME().OnRecv(cNKMPacket_GAME_START_NOT);
		}

		// Token: 0x04003548 RID: 13640
		private NKCReplayMgr.ReplayMgrState m_currentRMS;

		// Token: 0x04003549 RID: 13641
		private ReplayRecorder m_replayRecorder;

		// Token: 0x0400354A RID: 13642
		private ReplayData m_currentReplayData;

		// Token: 0x0400354B RID: 13643
		private NKM_GAME_SPEED_TYPE m_NKM_GAME_SPEED_TYPE;

		// Token: 0x0400354C RID: 13644
		private bool m_bFinishState;

		// Token: 0x0400354D RID: 13645
		private Dictionary<string, string> m_dicReplaydata = new Dictionary<string, string>();

		// Token: 0x02001389 RID: 5001
		private enum ReplayMgrState
		{
			// Token: 0x04009A7A RID: 39546
			RMS_None,
			// Token: 0x04009A7B RID: 39547
			RMS_Recording,
			// Token: 0x04009A7C RID: 39548
			RMS_Playing,
			// Token: 0x04009A7D RID: 39549
			RMS_PlayingResult
		}
	}
}
