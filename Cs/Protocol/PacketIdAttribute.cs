using System;

namespace Cs.Protocol
{
	// Token: 0x020010C2 RID: 4290
	[AttributeUsage(AttributeTargets.Class)]
	public class PacketIdAttribute : Attribute
	{
		// Token: 0x06009D08 RID: 40200 RVA: 0x00337284 File Offset: 0x00335484
		public PacketIdAttribute(object packetId)
		{
			string text = packetId.ToString();
			this.PacketId = (ushort)Enum.Parse(packetId.GetType(), text);
			this.PacketIdStr = string.Format("[{0}] {1}", this.PacketId, text);
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06009D09 RID: 40201 RVA: 0x003372D1 File Offset: 0x003354D1
		public ushort PacketId { get; }

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06009D0A RID: 40202 RVA: 0x003372D9 File Offset: 0x003354D9
		public string PacketIdStr { get; }

		// Token: 0x04009089 RID: 37001
		public const ushort InvalidPacketId = 65535;
	}
}
