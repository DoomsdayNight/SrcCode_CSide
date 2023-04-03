using System;
using System.IO;
using Cs.Engine.Network.Buffer.Detail;

namespace AssetBundles
{
	// Token: 0x02000059 RID: 89
	public class NKCAssetbundleCryptoStream : FileStream
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000C00C File Offset: 0x0000A20C
		public NKCAssetbundleCryptoStream(string path, FileMode mode, FileAccess access) : base(path, mode, access)
		{
			this.decryptSize = Math.Min(this.Length, 212L);
			base.Read(this.decryptedArray, 0, (int)this.decryptSize);
			Crypto.Encrypt(this.decryptedArray, (int)this.decryptSize, AssetBundleManager.GetMaskList(path, false));
			this.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C084 File Offset: 0x0000A284
		public override int Read(byte[] array, int offset, int count)
		{
			long position = this.Position;
			int result = base.Read(array, offset, count);
			if (position >= this.decryptSize)
			{
				return result;
			}
			if (position + (long)count < this.decryptSize)
			{
				Array.Copy(this.decryptedArray, position, array, (long)offset, (long)count);
			}
			else
			{
				int num = (int)(this.decryptSize - position);
				Array.Copy(this.decryptedArray, position, array, (long)offset, (long)num);
			}
			return result;
		}

		// Token: 0x040001EB RID: 491
		private byte[] decryptedArray = new byte[212L];

		// Token: 0x040001EC RID: 492
		private long decryptSize;
	}
}
