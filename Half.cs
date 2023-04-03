using System;
using System.Diagnostics;
using System.Globalization;

namespace NKM
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	public struct Half : IComparable, IFormattable, IConvertible, IComparable<Half>, IEquatable<Half>
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x0005A407 File Offset: 0x00058607
		public Half(float value)
		{
			this = HalfHelper.SingleToHalf(value);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005A415 File Offset: 0x00058615
		public Half(int value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0005A41F File Offset: 0x0005861F
		public Half(long value)
		{
			this = new Half((float)value);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0005A429 File Offset: 0x00058629
		public Half(double value)
		{
			this = new Half((float)value);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0005A433 File Offset: 0x00058633
		public Half(decimal value)
		{
			this = new Half((float)value);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0005A442 File Offset: 0x00058642
		public Half(uint value)
		{
			this = new Half(value);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0005A44D File Offset: 0x0005864D
		public Half(ulong value)
		{
			this = new Half(value);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0005A458 File Offset: 0x00058658
		public static Half Negate(Half half)
		{
			return -half;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0005A460 File Offset: 0x00058660
		public static Half Add(Half half1, Half half2)
		{
			return half1 + half2;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0005A469 File Offset: 0x00058669
		public static Half Subtract(Half half1, Half half2)
		{
			return half1 - half2;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0005A472 File Offset: 0x00058672
		public static Half Multiply(Half half1, Half half2)
		{
			return half1 * half2;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0005A47B File Offset: 0x0005867B
		public static Half Divide(Half half1, Half half2)
		{
			return half1 / half2;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0005A484 File Offset: 0x00058684
		public static Half operator +(Half half)
		{
			return half;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0005A487 File Offset: 0x00058687
		public static Half operator -(Half half)
		{
			return HalfHelper.Negate(half);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0005A48F File Offset: 0x0005868F
		public static Half operator ++(Half half)
		{
			return (Half)(half + 1f);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0005A4A2 File Offset: 0x000586A2
		public static Half operator --(Half half)
		{
			return (Half)(half - 1f);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0005A4B5 File Offset: 0x000586B5
		public static Half operator +(Half half1, Half half2)
		{
			return (Half)(half1 + half2);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0005A4CB File Offset: 0x000586CB
		public static Half operator -(Half half1, Half half2)
		{
			return (Half)(half1 - half2);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005A4E1 File Offset: 0x000586E1
		public static Half operator *(Half half1, Half half2)
		{
			return (Half)(half1 * half2);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005A4F7 File Offset: 0x000586F7
		public static Half operator /(Half half1, Half half2)
		{
			return (Half)(half1 / half2);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0005A50D File Offset: 0x0005870D
		public static bool operator ==(Half half1, Half half2)
		{
			return !Half.IsNaN(half1) && half1.value == half2.value;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0005A527 File Offset: 0x00058727
		public static bool operator !=(Half half1, Half half2)
		{
			return half1.value != half2.value;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0005A53A File Offset: 0x0005873A
		public static bool operator <(Half half1, Half half2)
		{
			return half1 < half2;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0005A54C File Offset: 0x0005874C
		public static bool operator >(Half half1, Half half2)
		{
			return half1 > half2;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0005A55E File Offset: 0x0005875E
		public static bool operator <=(Half half1, Half half2)
		{
			return half1 == half2 || half1 < half2;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0005A572 File Offset: 0x00058772
		public static bool operator >=(Half half1, Half half2)
		{
			return half1 == half2 || half1 > half2;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005A586 File Offset: 0x00058786
		public static implicit operator Half(byte value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005A58F File Offset: 0x0005878F
		public static implicit operator Half(short value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0005A598 File Offset: 0x00058798
		public static implicit operator Half(char value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005A5A1 File Offset: 0x000587A1
		public static implicit operator Half(int value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005A5AA File Offset: 0x000587AA
		public static implicit operator Half(long value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0005A5B3 File Offset: 0x000587B3
		public static explicit operator Half(float value)
		{
			return new Half(value);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005A5BC File Offset: 0x000587BC
		public static explicit operator Half(double value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0005A5C5 File Offset: 0x000587C5
		public static explicit operator Half(decimal value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005A5D3 File Offset: 0x000587D3
		public static explicit operator byte(Half value)
		{
			return (byte)value;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005A5DD File Offset: 0x000587DD
		public static explicit operator char(Half value)
		{
			return (char)value;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005A5E7 File Offset: 0x000587E7
		public static explicit operator short(Half value)
		{
			return (short)value;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005A5F1 File Offset: 0x000587F1
		public static explicit operator int(Half value)
		{
			return (int)value;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0005A5FB File Offset: 0x000587FB
		public static explicit operator long(Half value)
		{
			return (long)value;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0005A605 File Offset: 0x00058805
		public static implicit operator float(Half value)
		{
			return HalfHelper.HalfToSingle(value);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0005A60E File Offset: 0x0005880E
		public static implicit operator double(Half value)
		{
			return (double)value;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0005A618 File Offset: 0x00058818
		public static explicit operator decimal(Half value)
		{
			return (decimal)value;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0005A626 File Offset: 0x00058826
		public static implicit operator Half(sbyte value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0005A62F File Offset: 0x0005882F
		public static implicit operator Half(ushort value)
		{
			return new Half((float)value);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0005A638 File Offset: 0x00058838
		public static implicit operator Half(uint value)
		{
			return new Half(value);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0005A642 File Offset: 0x00058842
		public static implicit operator Half(ulong value)
		{
			return new Half(value);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0005A64C File Offset: 0x0005884C
		public static explicit operator sbyte(Half value)
		{
			return (sbyte)value;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0005A656 File Offset: 0x00058856
		public static explicit operator ushort(Half value)
		{
			return (ushort)value;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0005A660 File Offset: 0x00058860
		public static explicit operator uint(Half value)
		{
			return (uint)value;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0005A66A File Offset: 0x0005886A
		public static explicit operator ulong(Half value)
		{
			return (ulong)value;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0005A674 File Offset: 0x00058874
		public int CompareTo(Half other)
		{
			int result = 0;
			if (this < other)
			{
				result = -1;
			}
			else if (this > other)
			{
				result = 1;
			}
			else if (this != other)
			{
				if (!Half.IsNaN(this))
				{
					result = 1;
				}
				else if (!Half.IsNaN(other))
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005A6D4 File Offset: 0x000588D4
		public int CompareTo(object obj)
		{
			int result;
			if (obj == null)
			{
				result = 1;
			}
			else
			{
				if (!(obj is Half))
				{
					throw new ArgumentException("Object must be of type Half.");
				}
				result = this.CompareTo((Half)obj);
			}
			return result;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005A70D File Offset: 0x0005890D
		public bool Equals(Half other)
		{
			return other == this || (Half.IsNaN(other) && Half.IsNaN(this));
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0005A734 File Offset: 0x00058934
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Half)
			{
				Half half = (Half)obj;
				if (half == this || (Half.IsNaN(half) && Half.IsNaN(this)))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0005A778 File Offset: 0x00058978
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0005A785 File Offset: 0x00058985
		public TypeCode GetTypeCode()
		{
			return (TypeCode)255;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005A78C File Offset: 0x0005898C
		public static byte[] GetBytes(Half value)
		{
			return BitConverter.GetBytes(value.value);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0005A799 File Offset: 0x00058999
		public static ushort GetBits(Half value)
		{
			return value.value;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005A7A1 File Offset: 0x000589A1
		public static Half ToHalf(byte[] value, int startIndex)
		{
			return Half.ToHalf((ushort)BitConverter.ToInt16(value, startIndex));
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005A7B0 File Offset: 0x000589B0
		public static Half ToHalf(ushort bits)
		{
			return new Half
			{
				value = bits
			};
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005A7CE File Offset: 0x000589CE
		public static int Sign(Half value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			if (value != 0)
			{
				throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
			}
			return 0;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005A80A File Offset: 0x00058A0A
		public static Half Abs(Half value)
		{
			return HalfHelper.Abs(value);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005A812 File Offset: 0x00058A12
		public static Half Max(Half value1, Half value2)
		{
			if (!(value1 < value2))
			{
				return value1;
			}
			return value2;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005A820 File Offset: 0x00058A20
		public static Half Min(Half value1, Half value2)
		{
			if (!(value1 < value2))
			{
				return value2;
			}
			return value1;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0005A82E File Offset: 0x00058A2E
		public static bool IsNaN(Half half)
		{
			return HalfHelper.IsNaN(half);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005A836 File Offset: 0x00058A36
		public static bool IsInfinity(Half half)
		{
			return HalfHelper.IsInfinity(half);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005A83E File Offset: 0x00058A3E
		public static bool IsNegativeInfinity(Half half)
		{
			return HalfHelper.IsNegativeInfinity(half);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005A846 File Offset: 0x00058A46
		public static bool IsPositiveInfinity(Half half)
		{
			return HalfHelper.IsPositiveInfinity(half);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005A84E File Offset: 0x00058A4E
		public static Half Parse(string value)
		{
			return (Half)float.Parse(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005A860 File Offset: 0x00058A60
		public static Half Parse(string value, IFormatProvider provider)
		{
			return (Half)float.Parse(value, provider);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005A86E File Offset: 0x00058A6E
		public static Half Parse(string value, NumberStyles style)
		{
			return (Half)float.Parse(value, style, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005A881 File Offset: 0x00058A81
		public static Half Parse(string value, NumberStyles style, IFormatProvider provider)
		{
			return (Half)float.Parse(value, style, provider);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005A890 File Offset: 0x00058A90
		public static bool TryParse(string value, out Half result)
		{
			float num;
			if (float.TryParse(value, out num))
			{
				result = (Half)num;
				return true;
			}
			result = default(Half);
			return false;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005A8C0 File Offset: 0x00058AC0
		public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out Half result)
		{
			bool result2 = false;
			float num;
			if (float.TryParse(value, style, provider, out num))
			{
				result = (Half)num;
				result2 = true;
			}
			else
			{
				result = default(Half);
			}
			return result2;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		public override string ToString()
		{
			return this.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005A91C File Offset: 0x00058B1C
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0005A940 File Offset: 0x00058B40
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005A968 File Offset: 0x00058B68
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString(format, formatProvider);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005A98B File Offset: 0x00058B8B
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005A999 File Offset: 0x00058B99
		TypeCode IConvertible.GetTypeCode()
		{
			return this.GetTypeCode();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005A9A1 File Offset: 0x00058BA1
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005A9B4 File Offset: 0x00058BB4
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005A9C7 File Offset: 0x00058BC7
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "Half", "Char"));
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005A9E7 File Offset: 0x00058BE7
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Invalid cast from '{0}' to '{1}'.", "Half", "DateTime"));
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005AA07 File Offset: 0x00058C07
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0005AA1A File Offset: 0x00058C1A
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005AA2D File Offset: 0x00058C2D
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005AA40 File Offset: 0x00058C40
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0005AA53 File Offset: 0x00058C53
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0005AA66 File Offset: 0x00058C66
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0005AA79 File Offset: 0x00058C79
		string IConvertible.ToString(IFormatProvider provider)
		{
			return Convert.ToString(this, CultureInfo.InvariantCulture);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005AA91 File Offset: 0x00058C91
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this).ToType(conversionType, provider);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0005AAAB File Offset: 0x00058CAB
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005AABE File Offset: 0x00058CBE
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005AAD1 File Offset: 0x00058CD1
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x04000EFF RID: 3839
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal ushort value;

		// Token: 0x04000F00 RID: 3840
		public static readonly Half Epsilon = Half.ToHalf(1);

		// Token: 0x04000F01 RID: 3841
		public static readonly Half MaxValue = Half.ToHalf(31743);

		// Token: 0x04000F02 RID: 3842
		public static readonly Half MinValue = Half.ToHalf(64511);

		// Token: 0x04000F03 RID: 3843
		public static readonly Half NaN = Half.ToHalf(65024);

		// Token: 0x04000F04 RID: 3844
		public static readonly Half NegativeInfinity = Half.ToHalf(64512);

		// Token: 0x04000F05 RID: 3845
		public static readonly Half PositiveInfinity = Half.ToHalf(31744);
	}
}
