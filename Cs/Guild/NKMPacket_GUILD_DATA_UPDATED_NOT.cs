using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000EDF RID: 3807
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DATA_UPDATED_NOT)]
	public sealed class NKMPacket_GUILD_DATA_UPDATED_NOT : ISerializable
	{
		// Token: 0x0600989E RID: 39070 RVA: 0x0032EC35 File Offset: 0x0032CE35
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMGuildData>(ref this.guildData);
		}

		// Token: 0x04008B40 RID: 35648
		public NKMGuildData guildData = new NKMGuildData();
	}
}
