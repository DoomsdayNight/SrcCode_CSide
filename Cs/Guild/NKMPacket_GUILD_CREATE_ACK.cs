using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000ED0 RID: 3792
	[PacketId(ClientPacketId.kNKMPacket_GUILD_CREATE_ACK)]
	public sealed class NKMPacket_GUILD_CREATE_ACK : ISerializable
	{
		// Token: 0x06009880 RID: 39040 RVA: 0x0032EA02 File Offset: 0x0032CC02
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemDataList);
			stream.PutOrGet<NKMGuildData>(ref this.guildData);
			stream.PutOrGet<PrivateGuildData>(ref this.privateGuildData);
		}

		// Token: 0x04008B24 RID: 35620
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008B25 RID: 35621
		public List<NKMItemMiscData> costItemDataList = new List<NKMItemMiscData>();

		// Token: 0x04008B26 RID: 35622
		public NKMGuildData guildData = new NKMGuildData();

		// Token: 0x04008B27 RID: 35623
		public PrivateGuildData privateGuildData = new PrivateGuildData();
	}
}
