using System;
using System.IO;
using ClientPacket.Pvp;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Util;
using Cs.Logging;
using Cs.Protocol;

namespace Cs.GameServer.Replay
{
	// Token: 0x020010BC RID: 4284
	internal static class ReplayLoader
	{
		// Token: 0x06009CBB RID: 40123 RVA: 0x00336764 File Offset: 0x00334964
		public static ReplayData Load(string fullPath)
		{
			if (!File.Exists(fullPath))
			{
				Log.Error("[ReplayData] file not exist. path:" + fullPath, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayLoader.cs", 17);
				return null;
			}
			ReplayData result;
			try
			{
				long bytes = 0L;
				ZeroCopyBuffer zeroCopyBuffer = null;
				using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
				{
					bytes = fileStream.Length;
					zeroCopyBuffer = Lz4Util.Decompress(fileStream);
				}
				long bytes2 = (long)zeroCopyBuffer.CalcTotalSize();
				Log.Debug("[ReplayData] fileSize:" + bytes.ToByteFormat() + " decompressed:" + bytes2.ToByteFormat(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayLoader.cs", 32);
				using (zeroCopyBuffer.Hold())
				{
					using (PacketReader packetReader = new PacketReader(zeroCopyBuffer.GetReader()))
					{
						string @string = packetReader.GetString();
						if (@string != "RV004")
						{
							Log.Warn("[ReplayData] version mismatched. current:RV004 saved:" + @string, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayLoader.cs", 40);
							result = null;
						}
						else
						{
							ReplayData replayData = new ReplayData();
							packetReader.GetWithoutNullBit(replayData);
							Log.Debug(string.Format("[ReplayData] syncCount:{0}", replayData.syncList.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayLoader.cs", 46);
							result = replayData;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error("[ReplayData] load failed. path:" + fullPath + " exception:" + ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Replay/ReplayLoader.cs", 52);
				result = null;
			}
			return result;
		}
	}
}
