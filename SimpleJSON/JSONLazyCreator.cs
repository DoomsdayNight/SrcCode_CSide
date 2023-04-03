using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x0200003D RID: 61
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000944A File Offset: 0x0000764A
		public override JSONNodeType Tag
		{
			get
			{
				return JSONNodeType.None;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000944D File Offset: 0x0000764D
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009463 File Offset: 0x00007663
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009479 File Offset: 0x00007679
		private void Set(JSONNode aVal)
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
		}

		// Token: 0x1700006D RID: 109
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set(new JSONArray
				{
					value
				});
			}
		}

		// Token: 0x1700006E RID: 110
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				this.Set(new JSONObject
				{
					{
						aKey,
						value
					}
				});
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009504 File Offset: 0x00007704
		public override void Add(JSONNode aItem)
		{
			this.Set(new JSONArray
			{
				aItem
			});
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009528 File Offset: 0x00007728
		public override void Add(string aKey, JSONNode aItem)
		{
			this.Set(new JSONObject
			{
				{
					aKey,
					aItem
				}
			});
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000954A File Offset: 0x0000774A
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || a == b;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009555 File Offset: 0x00007755
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009561 File Offset: 0x00007761
		public override bool Equals(object obj)
		{
			return obj == null || this == obj;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000956C File Offset: 0x0000776C
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009570 File Offset: 0x00007770
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00009594 File Offset: 0x00007794
		public override int AsInt
		{
			get
			{
				JSONNumber aVal = new JSONNumber(0.0);
				this.Set(aVal);
				return 0;
			}
			set
			{
				JSONNumber aVal = new JSONNumber((double)value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000095B0 File Offset: 0x000077B0
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000095D8 File Offset: 0x000077D8
		public override float AsFloat
		{
			get
			{
				JSONNumber aVal = new JSONNumber(0.0);
				this.Set(aVal);
				return 0f;
			}
			set
			{
				JSONNumber aVal = new JSONNumber((double)value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000095F4 File Offset: 0x000077F4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009620 File Offset: 0x00007820
		public override double AsDouble
		{
			get
			{
				JSONNumber aVal = new JSONNumber(0.0);
				this.Set(aVal);
				return 0.0;
			}
			set
			{
				JSONNumber aVal = new JSONNumber(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000963C File Offset: 0x0000783C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00009658 File Offset: 0x00007858
		public override bool AsBool
		{
			get
			{
				JSONBool aVal = new JSONBool(false);
				this.Set(aVal);
				return false;
			}
			set
			{
				JSONBool aVal = new JSONBool(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009674 File Offset: 0x00007874
		public override JSONArray AsArray
		{
			get
			{
				JSONArray jsonarray = new JSONArray();
				this.Set(jsonarray);
				return jsonarray;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009690 File Offset: 0x00007890
		public override JSONObject AsObject
		{
			get
			{
				JSONObject jsonobject = new JSONObject();
				this.Set(jsonobject);
				return jsonobject;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000096AB File Offset: 0x000078AB
		internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
		{
			aSB.Append("null");
		}

		// Token: 0x04000143 RID: 323
		private JSONNode m_Node;

		// Token: 0x04000144 RID: 324
		private string m_Key;
	}
}
