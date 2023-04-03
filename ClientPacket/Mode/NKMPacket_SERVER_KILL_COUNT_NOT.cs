using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E50 RID: 3664
	[PacketId(ClientPacketId.kNKMPacket_SERVER_KILL_COUNT_NOT)]
	public sealed class NKMPacket_SERVER_KILL_COUNT_NOT : ISerializable
	{
		// Token: 0x06009790 RID: 38800 RVA: 0x0032D20A File Offset: 0x0032B40A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMServerKillCountData>(ref this.serverKillCountDataList);
		}

		// Token: 0x040089B9 RID: 35257
		public List<NKMServerKillCountData> serverKillCountDataList = new List<NKMServerKillCountData>();
	}
}
