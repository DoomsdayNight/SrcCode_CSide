using System;

namespace NKM
{
	// Token: 0x02000516 RID: 1302
	public readonly struct TabId : IEquatable<TabId>
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x000C069B File Offset: 0x000BE89B
		public TabId(string type, int subIndex)
		{
			this.Type = type;
			this.SubIndex = subIndex;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000C06AB File Offset: 0x000BE8AB
		public static bool operator ==(TabId left, TabId right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000C06B5 File Offset: 0x000BE8B5
		public static bool operator !=(TabId left, TabId right)
		{
			return !(left == right);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000C06C4 File Offset: 0x000BE8C4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is TabId)
			{
				TabId other2 = (TabId)other;
				return this.Equals(other2);
			}
			return false;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000C06EE File Offset: 0x000BE8EE
		public bool Equals(TabId other)
		{
			return this.Type == other.Type && this.SubIndex == other.SubIndex;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000C0714 File Offset: 0x000BE914
		public override int GetHashCode()
		{
			return new ValueTuple<string, int>(this.Type, this.SubIndex).GetHashCode();
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000C0740 File Offset: 0x000BE940
		public override string ToString()
		{
			return string.Format("{0}[{1}]", this.Type, this.SubIndex);
		}

		// Token: 0x04002694 RID: 9876
		public readonly string Type;

		// Token: 0x04002695 RID: 9877
		public readonly int SubIndex;
	}
}
