using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E3D RID: 3645
	[PacketId(ClientPacketId.kNKMPacket_DIVE_SELECT_ARTIFACT_REQ)]
	public sealed class NKMPacket_DIVE_SELECT_ARTIFACT_REQ : ISerializable
	{
		// Token: 0x0600976A RID: 38762 RVA: 0x0032CEB2 File Offset: 0x0032B0B2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.artifactID);
		}

		// Token: 0x0400898A RID: 35210
		public int artifactID;
	}
}
