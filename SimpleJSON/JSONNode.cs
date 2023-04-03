using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000036 RID: 54
	public abstract class JSONNode
	{
		// Token: 0x1700003E RID: 62
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700003F RID: 63
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008106 File Offset: 0x00006306
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000810D File Offset: 0x0000630D
		public virtual string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000810F File Offset: 0x0000630F
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008112 File Offset: 0x00006312
		public virtual bool IsNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008115 File Offset: 0x00006315
		public virtual bool IsString
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008118 File Offset: 0x00006318
		public virtual bool IsBoolean
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000811B File Offset: 0x0000631B
		public virtual bool IsNull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000811E File Offset: 0x0000631E
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008121 File Offset: 0x00006321
		public virtual bool IsObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008124 File Offset: 0x00006324
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008126 File Offset: 0x00006326
		public virtual void Add(JSONNode aItem)
		{
			this.Add("", aItem);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008134 File Offset: 0x00006334
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008137 File Offset: 0x00006337
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000813A File Offset: 0x0000633A
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000813D File Offset: 0x0000633D
		public virtual IEnumerable<JSONNode> Children
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008146 File Offset: 0x00006346
		public IEnumerable<JSONNode> DeepChildren
		{
			get
			{
				foreach (JSONNode jsonnode in this.Children)
				{
					foreach (JSONNode jsonnode2 in jsonnode.DeepChildren)
					{
						yield return jsonnode2;
					}
					IEnumerator<JSONNode> enumerator2 = null;
				}
				IEnumerator<JSONNode> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008158 File Offset: 0x00006358
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, 0, JSONTextMode.Compact);
			return stringBuilder.ToString();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000817C File Offset: 0x0000637C
		public virtual string ToString(int aIndent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.WriteToStringBuilder(stringBuilder, 0, aIndent, JSONTextMode.Indent);
			return stringBuilder.ToString();
		}

		// Token: 0x06000197 RID: 407
		internal abstract void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000198 RID: 408
		public abstract JSONNodeType Tag { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000081A0 File Offset: 0x000063A0
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000081D1 File Offset: 0x000063D1
		public virtual double AsDouble
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000081E0 File Offset: 0x000063E0
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000081E9 File Offset: 0x000063E9
		public virtual int AsInt
		{
			get
			{
				return (int)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000081F3 File Offset: 0x000063F3
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000081FC File Offset: 0x000063FC
		public virtual float AsFloat
		{
			get
			{
				return (float)this.AsDouble;
			}
			set
			{
				this.AsDouble = (double)value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008208 File Offset: 0x00006408
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00008236 File Offset: 0x00006436
		public virtual bool AsBool
		{
			get
			{
				bool result = false;
				if (bool.TryParse(this.Value, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = (value ? "true" : "false");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000824D File Offset: 0x0000644D
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008255 File Offset: 0x00006455
		public virtual JSONObject AsObject
		{
			get
			{
				return this as JSONObject;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000825D File Offset: 0x0000645D
		public static implicit operator JSONNode(string s)
		{
			return new JSONString(s);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008265 File Offset: 0x00006465
		public static implicit operator string(JSONNode d)
		{
			if (!(d == null))
			{
				return d.Value;
			}
			return null;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008278 File Offset: 0x00006478
		public static implicit operator JSONNode(double n)
		{
			return new JSONNumber(n);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008280 File Offset: 0x00006480
		public static implicit operator double(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsDouble;
			}
			return 0.0;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000829B File Offset: 0x0000649B
		public static implicit operator JSONNode(float n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000082A4 File Offset: 0x000064A4
		public static implicit operator float(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsFloat;
			}
			return 0f;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000082BB File Offset: 0x000064BB
		public static implicit operator JSONNode(int n)
		{
			return new JSONNumber((double)n);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000082C4 File Offset: 0x000064C4
		public static implicit operator int(JSONNode d)
		{
			if (!(d == null))
			{
				return d.AsInt;
			}
			return 0;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000082D7 File Offset: 0x000064D7
		public static implicit operator JSONNode(bool b)
		{
			return new JSONBool(b);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000082DF File Offset: 0x000064DF
		public static implicit operator bool(JSONNode d)
		{
			return !(d == null) && d.AsBool;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000082F4 File Offset: 0x000064F4
		public static bool operator ==(JSONNode a, object b)
		{
			if (a == b)
			{
				return true;
			}
			bool flag = a is JSONNull || a == null || a is JSONLazyCreator;
			bool flag2 = b is JSONNull || b == null || b is JSONLazyCreator;
			return (flag && flag2) || a.Equals(b);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008343 File Offset: 0x00006543
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000834F File Offset: 0x0000654F
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008355 File Offset: 0x00006555
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008360 File Offset: 0x00006560
		internal static string Escape(string aText)
		{
			JSONNode.m_EscapeBuilder.Length = 0;
			if (JSONNode.m_EscapeBuilder.Capacity < aText.Length + aText.Length / 10)
			{
				JSONNode.m_EscapeBuilder.Capacity = aText.Length + aText.Length / 10;
			}
			int i = 0;
			while (i < aText.Length)
			{
				char c = aText[i];
				switch (c)
				{
				case '\b':
					JSONNode.m_EscapeBuilder.Append("\\b");
					break;
				case '\t':
					JSONNode.m_EscapeBuilder.Append("\\t");
					break;
				case '\n':
					JSONNode.m_EscapeBuilder.Append("\\n");
					break;
				case '\v':
					goto IL_FA;
				case '\f':
					JSONNode.m_EscapeBuilder.Append("\\f");
					break;
				case '\r':
					JSONNode.m_EscapeBuilder.Append("\\r");
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							goto IL_FA;
						}
						JSONNode.m_EscapeBuilder.Append("\\\\");
					}
					else
					{
						JSONNode.m_EscapeBuilder.Append("\\\"");
					}
					break;
				}
				IL_106:
				i++;
				continue;
				IL_FA:
				JSONNode.m_EscapeBuilder.Append(c);
				goto IL_106;
			}
			string result = JSONNode.m_EscapeBuilder.ToString();
			JSONNode.m_EscapeBuilder.Length = 0;
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008498 File Offset: 0x00006698
		private static void ParseElement(JSONNode ctx, string token, string tokenName, bool quoted)
		{
			if (quoted)
			{
				ctx.Add(tokenName, token);
				return;
			}
			string a = token.ToLower();
			if (a == "false" || a == "true")
			{
				ctx.Add(tokenName, a == "true");
				return;
			}
			if (a == "null")
			{
				ctx.Add(tokenName, null);
				return;
			}
			double n;
			if (double.TryParse(token, out n))
			{
				ctx.Add(tokenName, n);
				return;
			}
			ctx.Add(tokenName, token);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000852C File Offset: 0x0000672C
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			bool flag = false;
			bool flag2 = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				if (c <= ',')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
						case '\r':
							goto IL_33E;
						case '\v':
						case '\f':
							goto IL_330;
						default:
							if (c != ' ')
							{
								goto IL_330;
							}
							break;
						}
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
					}
					else if (c != '"')
					{
						if (c != ',')
						{
							goto IL_330;
						}
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
						}
						else
						{
							if (stringBuilder.Length > 0 || flag2)
							{
								JSONNode.ParseElement(jsonnode, stringBuilder.ToString(), text, flag2);
							}
							text = "";
							stringBuilder.Length = 0;
							flag2 = false;
						}
					}
					else
					{
						flag = !flag;
						flag2 = (flag2 || flag);
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != ':')
						{
							switch (c)
							{
							case '[':
								if (flag)
								{
									stringBuilder.Append(aJSON[i]);
									goto IL_33E;
								}
								stack.Push(new JSONArray());
								if (jsonnode != null)
								{
									jsonnode.Add(text, stack.Peek());
								}
								text = "";
								stringBuilder.Length = 0;
								jsonnode = stack.Peek();
								goto IL_33E;
							case '\\':
								i++;
								if (flag)
								{
									char c2 = aJSON[i];
									if (c2 <= 'f')
									{
										if (c2 == 'b')
										{
											stringBuilder.Append('\b');
											goto IL_33E;
										}
										if (c2 == 'f')
										{
											stringBuilder.Append('\f');
											goto IL_33E;
										}
									}
									else
									{
										if (c2 == 'n')
										{
											stringBuilder.Append('\n');
											goto IL_33E;
										}
										switch (c2)
										{
										case 'r':
											stringBuilder.Append('\r');
											goto IL_33E;
										case 't':
											stringBuilder.Append('\t');
											goto IL_33E;
										case 'u':
										{
											string s = aJSON.Substring(i + 1, 4);
											stringBuilder.Append((char)int.Parse(s, NumberStyles.AllowHexSpecifier));
											i += 4;
											goto IL_33E;
										}
										}
									}
									stringBuilder.Append(c2);
									goto IL_33E;
								}
								goto IL_33E;
							case ']':
								break;
							default:
								goto IL_330;
							}
						}
						else
						{
							if (flag)
							{
								stringBuilder.Append(aJSON[i]);
								goto IL_33E;
							}
							text = stringBuilder.ToString();
							stringBuilder.Length = 0;
							flag2 = false;
							goto IL_33E;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							goto IL_330;
						}
					}
					else
					{
						if (flag)
						{
							stringBuilder.Append(aJSON[i]);
							goto IL_33E;
						}
						stack.Push(new JSONObject());
						if (jsonnode != null)
						{
							jsonnode.Add(text, stack.Peek());
						}
						text = "";
						stringBuilder.Length = 0;
						jsonnode = stack.Peek();
						goto IL_33E;
					}
					if (flag)
					{
						stringBuilder.Append(aJSON[i]);
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (stringBuilder.Length > 0 || flag2)
						{
							JSONNode.ParseElement(jsonnode, stringBuilder.ToString(), text, flag2);
							flag2 = false;
						}
						text = "";
						stringBuilder.Length = 0;
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
				}
				IL_33E:
				i++;
				continue;
				IL_330:
				stringBuilder.Append(aJSON[i]);
				goto IL_33E;
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jsonnode;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008897 File Offset: 0x00006A97
		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000889C File Offset: 0x00006A9C
		public void SaveToStream(Stream aData)
		{
			BinaryWriter aWriter = new BinaryWriter(aData);
			this.Serialize(aWriter);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000088B7 File Offset: 0x00006AB7
		public void SaveToCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000088C3 File Offset: 0x00006AC3
		public void SaveToCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000088CF File Offset: 0x00006ACF
		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000088DC File Offset: 0x00006ADC
		public void SaveToFile(string aFileName)
		{
			Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
			using (FileStream fileStream = File.OpenWrite(aFileName))
			{
				this.SaveToStream(fileStream);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000892C File Offset: 0x00006B2C
		public string SaveToBase64()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008978 File Offset: 0x00006B78
		public static JSONNode Deserialize(BinaryReader aReader)
		{
			JSONNodeType jsonnodeType = (JSONNodeType)aReader.ReadByte();
			switch (jsonnodeType)
			{
			case JSONNodeType.Array:
			{
				int num = aReader.ReadInt32();
				JSONArray jsonarray = new JSONArray();
				for (int i = 0; i < num; i++)
				{
					jsonarray.Add(JSONNode.Deserialize(aReader));
				}
				return jsonarray;
			}
			case JSONNodeType.Object:
			{
				int num2 = aReader.ReadInt32();
				JSONObject jsonobject = new JSONObject();
				for (int j = 0; j < num2; j++)
				{
					string aKey = aReader.ReadString();
					JSONNode aItem = JSONNode.Deserialize(aReader);
					jsonobject.Add(aKey, aItem);
				}
				return jsonobject;
			}
			case JSONNodeType.String:
				return new JSONString(aReader.ReadString());
			case JSONNodeType.Number:
				return new JSONNumber(aReader.ReadDouble());
			case JSONNodeType.NullValue:
				return new JSONNull();
			case JSONNodeType.Boolean:
				return new JSONBool(aReader.ReadBoolean());
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jsonnodeType.ToString());
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008A5C File Offset: 0x00006C5C
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008A68 File Offset: 0x00006C68
		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008A74 File Offset: 0x00006C74
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008A80 File Offset: 0x00006C80
		public static JSONNode LoadFromStream(Stream aData)
		{
			JSONNode result;
			using (BinaryReader binaryReader = new BinaryReader(aData))
			{
				result = JSONNode.Deserialize(binaryReader);
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public static JSONNode LoadFromFile(string aFileName)
		{
			JSONNode result;
			using (FileStream fileStream = File.OpenRead(aFileName))
			{
				result = JSONNode.LoadFromStream(fileStream);
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public static JSONNode LoadFromBase64(string aBase64)
		{
			return JSONNode.LoadFromStream(new MemoryStream(Convert.FromBase64String(aBase64))
			{
				Position = 0L
			});
		}

		// Token: 0x0400013B RID: 315
		internal static StringBuilder m_EscapeBuilder = new StringBuilder();
	}
}
