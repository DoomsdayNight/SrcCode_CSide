using System;
using System.IO;
using System.Threading.Tasks;
using ClientPacket.Game;
using ClientPacket.Pvp;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Util;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKM;

namespace Cs.GameServer.Replay
{
	// Token: 0x020010BD RID: 4285
	internal sealed class ReplayRecorder
	{
		// Token: 0x06009CBC RID: 40124 RVA: 0x003368E8 File Offset: 0x00334AE8
		public ReplayRecorder(string name, NKMGameData _gameData, NKMGameRuntimeData _runtimeData)
		{
			this.replayData = new ReplayData
			{
				replayName = name,
				replayVersion = "RV004",
				gameData = _gameData.DeepCopy<NKMGameData>(),
				gameRuntimeData = _runtimeData.DeepCopy<NKMGameRuntimeData>()
			};
			Log.Debug(string.Format("[Replay] GameType Debug  ReplayGameData[{0}] OrgGameData[{1}]", this.replayData.gameData.GetGameType(), _gameData.GetGameType()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayRecorder.cs", 30);
		}

		// Token: 0x06009CBD RID: 40125 RVA: 0x00336971 File Offset: 0x00334B71
		public void AddEmoticonData(ReplayData.EmoticonData data)
		{
			this.replayData.emoticonList.Add(data);
			Log.Debug(string.Format("[Replay] record emoticon data... count:{0}", this.replayData.emoticonList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayRecorder.cs", 36);
		}

		// Token: 0x06009CBE RID: 40126 RVA: 0x003369AF File Offset: 0x00334BAF
		public void AddSyncData(NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT data)
		{
			this.replayData.syncList.Add(data);
		}

		// Token: 0x06009CBF RID: 40127 RVA: 0x003369C2 File Offset: 0x00334BC2
		public void SetGameResult(PVP_RESULT pvpResult, float gameEndTime, NKMGameRecord gameRecord)
		{
			this.replayData.pvpResult = pvpResult;
			this.replayData.gameEndTime = gameEndTime;
			this.replayData.gameRecord = gameRecord;
		}

		// Token: 0x06009CC0 RID: 40128 RVA: 0x003369E8 File Offset: 0x00334BE8
		public void Finish(string userUIDString)
		{
			ZeroCopyBuffer zeroCopyBuffer = new ZeroCopyBuffer();
			using (PacketWriter packetWriter = new PacketWriter(zeroCopyBuffer.GetWriter()))
			{
				packetWriter.PutString("RV004");
				packetWriter.PutWithoutNullBit(this.replayData);
			}
			using (zeroCopyBuffer.Hold())
			{
				int bytes = zeroCopyBuffer.CalcTotalSize();
				zeroCopyBuffer.Lz4Compress();
				int bytes2 = zeroCopyBuffer.CalcTotalSize();
				Log.Debug(string.Format("[ReplayRecoder] totalSize:{0} compressedSize:{1} #syncData:{2}", bytes.ToByteFormat(), bytes2.ToByteFormat(), this.replayData.syncList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayRecorder.cs", 66);
				string filePath = Path.Combine(ReplayRecorder.ReplaySavePath, userUIDString);
				zeroCopyBuffer.WriteToFile(filePath, this.replayData.replayName + ".replay");
			}
			this.CloseSyncNotPackets();
		}

		// Token: 0x06009CC1 RID: 40129 RVA: 0x00336AD8 File Offset: 0x00334CD8
		public async Task FinishAsync(string userUIDString)
		{
			ZeroCopyBuffer zeroCopyBuffer = new ZeroCopyBuffer();
			using (PacketWriter packetWriter = new PacketWriter(zeroCopyBuffer.GetWriter()))
			{
				packetWriter.PutString("RV004");
				packetWriter.PutWithoutNullBit(this.replayData);
			}
			using (zeroCopyBuffer.Hold())
			{
				int bytes = zeroCopyBuffer.CalcTotalSize();
				zeroCopyBuffer.Lz4Compress();
				int bytes2 = zeroCopyBuffer.CalcTotalSize();
				Log.Debug(string.Format("[ReplayRecoder] totalSize:{0} compressedSize:{1} #syncData:{2}", bytes.ToByteFormat(), bytes2.ToByteFormat(), this.replayData.syncList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayRecorder.cs", 89);
				string filePath = Path.Combine(ReplayRecorder.ReplaySavePath, userUIDString);
				await zeroCopyBuffer.WriteToFileAsync(filePath, this.replayData.replayName + ".replay");
			}
			IDisposable holder = null;
			this.CloseSyncNotPackets();
		}

		// Token: 0x06009CC2 RID: 40130 RVA: 0x00336B28 File Offset: 0x00334D28
		private void CloseSyncNotPackets()
		{
			foreach (NKMPacket_NPT_GAME_SYNC_DATA_PACK_NOT obj in this.replayData.syncList)
			{
				NKCPacketObjectPool.CloseObject(obj);
			}
		}

		// Token: 0x0400907A RID: 36986
		public static string ReplaySavePath = Path.Combine(NKCLogManager.GetSavePath(), "Replay");

		// Token: 0x0400907B RID: 36987
		private readonly ReplayData replayData = new ReplayData();
	}
}
