using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D71 RID: 3441
	[PacketId(ClientPacketId.kNKMPacket_PVP_GAME_MATCH_REQ)]
	public sealed class NKMPacket_PVP_GAME_MATCH_REQ : ISerializable
	{
		// Token: 0x060095DD RID: 38365 RVA: 0x0032AEF8 File Offset: 0x003290F8
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectDeckIndex);
			stream.PutOrGetEnum<NKM_GAME_TYPE>(ref this.gameType);
		}

		// Token: 0x040087DC RID: 34780
		public byte selectDeckIndex;

		// Token: 0x040087DD RID: 34781
		public NKM_GAME_TYPE gameType;
	}
}
