using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D75 RID: 3445
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)]
	public sealed class NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT : ISerializable
	{
		// Token: 0x060095E5 RID: 38373 RVA: 0x0032AF50 File Offset: 0x00329150
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMGameData>(ref this.gameData);
		}

		// Token: 0x040087E0 RID: 34784
		public NKMGameData gameData;
	}
}
