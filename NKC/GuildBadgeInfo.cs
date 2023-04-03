using System;
using System.Text;

namespace NKC
{
	// Token: 0x0200068A RID: 1674
	public class GuildBadgeInfo
	{
		// Token: 0x06003696 RID: 13974 RVA: 0x001197DB File Offset: 0x001179DB
		private GuildBadgeInfo()
		{
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x00119800 File Offset: 0x00117A00
		public GuildBadgeInfo(long badgeId)
		{
			string text = badgeId.ToString();
			if (text.Length > 9 && text.Length <= 12)
			{
				this.BadgeId = badgeId;
				string s = text.Substring(0, text.Length - 9);
				this.FrameId = int.Parse(s);
				string s2 = text.Substring(text.Length - 9, 3);
				this.FrameColorId = int.Parse(s2);
				string s3 = text.Substring(text.Length - 6, 3);
				this.MarkId = int.Parse(s3);
				string s4 = text.Substring(text.Length - 3, 3);
				this.MarkColorId = int.Parse(s4);
				return;
			}
			this.FrameId = 1;
			this.FrameColorId = 1;
			this.MarkId = 1;
			this.MarkColorId = 1;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.FrameId.ToString("D3"));
			stringBuilder.Append(this.FrameColorId.ToString("D3"));
			stringBuilder.Append(this.MarkId.ToString("D3"));
			stringBuilder.Append(this.MarkColorId.ToString("D3"));
			this.BadgeId = long.Parse(stringBuilder.ToString());
			stringBuilder.Clear();
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x00119968 File Offset: 0x00117B68
		public GuildBadgeInfo(int frameId, int frameColorId, int markId, int markColorId)
		{
			this.FrameId = frameId;
			this.FrameColorId = frameColorId;
			this.MarkId = markId;
			this.MarkColorId = markColorId;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(frameId.ToString("D3"));
			stringBuilder.Append(frameColorId.ToString("D3"));
			stringBuilder.Append(markId.ToString("D3"));
			stringBuilder.Append(markColorId.ToString("D3"));
			this.BadgeId = long.Parse(stringBuilder.ToString());
			stringBuilder.Clear();
		}

		// Token: 0x040033E0 RID: 13280
		public long BadgeId;

		// Token: 0x040033E1 RID: 13281
		public int FrameId = 1;

		// Token: 0x040033E2 RID: 13282
		public int FrameColorId = 1;

		// Token: 0x040033E3 RID: 13283
		public int MarkId = 1;

		// Token: 0x040033E4 RID: 13284
		public int MarkColorId = 1;
	}
}
