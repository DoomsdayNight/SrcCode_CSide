using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Common
{
	// Token: 0x0200105D RID: 4189
	public sealed class NKMShipModuleCandidate : ISerializable
	{
		// Token: 0x06009B78 RID: 39800 RVA: 0x003331EC File Offset: 0x003313EC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.shipUid);
			stream.PutOrGet(ref this.moduleId);
			stream.PutOrGet<NKMShipCmdModule>(ref this.slotCandidate);
		}

		// Token: 0x04008F69 RID: 36713
		public long shipUid;

		// Token: 0x04008F6A RID: 36714
		public int moduleId;

		// Token: 0x04008F6B RID: 36715
		public NKMShipCmdModule slotCandidate;
	}
}
