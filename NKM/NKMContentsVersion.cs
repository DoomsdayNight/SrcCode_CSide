using System;
using System.Text.RegularExpressions;

namespace NKM
{
	// Token: 0x020003BC RID: 956
	public sealed class NKMContentsVersion : IComparable<NKMContentsVersion>
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x00067844 File Offset: 0x00065A44
		private NKMContentsVersion(int first, int second, char third)
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
			this.Literal = string.Format("{0}.{1}.{2}", first, second, third);
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00067883 File Offset: 0x00065A83
		public static NKMContentsVersion MinValue { get; } = new NKMContentsVersion(int.MinValue, int.MinValue, 'a');

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0006788A File Offset: 0x00065A8A
		public static NKMContentsVersion MaxValue { get; } = new NKMContentsVersion(int.MaxValue, int.MaxValue, 'z');

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x00067891 File Offset: 0x00065A91
		public int First { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00067899 File Offset: 0x00065A99
		public int Second { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000678A1 File Offset: 0x00065AA1
		public char Third { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000678A9 File Offset: 0x00065AA9
		public string Literal { get; }

		// Token: 0x06001917 RID: 6423 RVA: 0x000678B4 File Offset: 0x00065AB4
		public static NKMContentsVersion Create(string literal)
		{
			Match match = Regex.Match(literal, "\\b([\\d]).([\\d]).([a-z])\\b");
			if (!match.Success)
			{
				return null;
			}
			return new NKMContentsVersion(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value[0]);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00067920 File Offset: 0x00065B20
		public static bool TryCreate(string literal, out NKMContentsVersion version)
		{
			Match match = Regex.Match(literal, "\\b([\\d]).([\\d]).([a-z])\\b");
			if (!match.Success)
			{
				version = null;
				return false;
			}
			version = new NKMContentsVersion(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), match.Groups[3].Value[0]);
			return true;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00067991 File Offset: 0x00065B91
		public static bool operator ==(NKMContentsVersion left, NKMContentsVersion right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000679A2 File Offset: 0x00065BA2
		public static bool operator !=(NKMContentsVersion left, NKMContentsVersion right)
		{
			return !(left == right);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000679AE File Offset: 0x00065BAE
		public static bool operator <(NKMContentsVersion left, NKMContentsVersion right)
		{
			if (left != null)
			{
				return left.CompareTo(right) < 0;
			}
			return right != null;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000679C2 File Offset: 0x00065BC2
		public static bool operator <=(NKMContentsVersion left, NKMContentsVersion right)
		{
			return left == null || left.CompareTo(right) <= 0;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000679D6 File Offset: 0x00065BD6
		public static bool operator >(NKMContentsVersion left, NKMContentsVersion right)
		{
			return left != null && left.CompareTo(right) > 0;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000679E7 File Offset: 0x00065BE7
		public static bool operator >=(NKMContentsVersion left, NKMContentsVersion right)
		{
			if (left != null)
			{
				return left.CompareTo(right) >= 0;
			}
			return right == null;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00067A00 File Offset: 0x00065C00
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			NKMContentsVersion nkmcontentsVersion = other as NKMContentsVersion;
			return nkmcontentsVersion != null && this.Equals(nkmcontentsVersion);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00067A2C File Offset: 0x00065C2C
		public bool Equals(NKMContentsVersion other)
		{
			return other != null && this.First == other.First && this.Second == other.Second && this.Third == other.Third && this.Literal == other.Literal;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00067A7F File Offset: 0x00065C7F
		public override int GetHashCode()
		{
			return this.Literal.GetHashCode();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00067A8C File Offset: 0x00065C8C
		public int CompareTo(NKMContentsVersion other)
		{
			if (this.First != other.First)
			{
				return this.First.CompareTo(other.First);
			}
			if (this.Second != other.Second)
			{
				return this.Second.CompareTo(other.Second);
			}
			return this.Third.CompareTo(other.Third);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00067AF3 File Offset: 0x00065CF3
		public override string ToString()
		{
			return this.Literal;
		}
	}
}
