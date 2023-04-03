using System;
using System.IO;
using Cs.Engine.Network.Buffer.Detail;

namespace AssetBundles
{
	// Token: 0x02000058 RID: 88
	public class NKCAssetbundleCryptoStreamMem : MemoryStream
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000BF30 File Offset: 0x0000A130
		public NKCAssetbundleCryptoStreamMem(byte[] buffer, string filePath) : base(buffer)
		{
			this.decryptSize = Math.Min(this.Length, 212L);
			base.Read(this.decryptedArray, 0, (int)this.decryptSize);
			Crypto.Encrypt(this.decryptedArray, (int)this.decryptSize, AssetBundleManager.GetMaskList(filePath, false));
			this.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
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

		// Token: 0x040001E9 RID: 489
		private byte[] decryptedArray = new byte[212L];

		// Token: 0x040001EA RID: 490
		private long decryptSize;
	}
}
