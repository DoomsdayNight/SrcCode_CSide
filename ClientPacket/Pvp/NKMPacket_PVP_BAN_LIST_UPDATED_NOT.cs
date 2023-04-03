using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D7F RID: 3455
	[PacketId(ClientPacketId.kNKMPacket_PVP_BAN_LIST_UPDATED_NOT)]
	public sealed class NKMPacket_PVP_BAN_LIST_UPDATED_NOT : ISerializable
	{
		// Token: 0x060095F9 RID: 38393 RVA: 0x0032B0AF File Offset: 0x003292AF
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMPvpBanResult>(ref this.pvpBanResult);
		}

		// Token: 0x040087F4 RID: 34804
		public NKMPvpBanResult pvpBanResult = new NKMPvpBanResult();
	}
}
