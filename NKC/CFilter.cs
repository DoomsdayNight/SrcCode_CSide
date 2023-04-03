using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000677 RID: 1655
	public class CFilter
	{
		// Token: 0x060034E5 RID: 13541 RVA: 0x0010B228 File Offset: 0x00109428
		public void Init()
		{
			StringValidSet.Init();
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0010B230 File Offset: 0x00109430
		public string Filter(string original)
		{
			if (string.IsNullOrEmpty(original))
			{
				return "";
			}
			if (this.root == null || this.dic.Count == 0)
			{
				return original;
			}
			string text = original;
			string text2 = original.ToUpper();
			int i = 0;
			while (i < text2.Length)
			{
				int num = this.Match(text2.Substring(i, text2.Length - i));
				if (num > 0)
				{
					text = this.Replace(text.ToCharArray(), i, num);
					i += num;
				}
				else
				{
					i++;
				}
			}
			return text;
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x0010B2AC File Offset: 0x001094AC
		private int Match(string text)
		{
			CFilter cfilter = this.root;
			bool flag = false;
			int num = 0;
			int b = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (StringValidSet.CheckIgnoreSet(text[i]) && cfilter != this.root)
				{
					if (flag)
					{
						return Mathf.Max(num, b);
					}
					b = i + 1;
				}
				else
				{
					if (!cfilter.dic.TryGetValue(text[i], out cfilter))
					{
						return num;
					}
					if (cfilter.isLeaf)
					{
						num = i + 1;
						flag = true;
					}
				}
			}
			return num;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x0010B330 File Offset: 0x00109530
		private string Replace(char[] charArray, int start, int count)
		{
			for (int i = start; i < start + count; i++)
			{
				charArray[i] = '*';
			}
			return new string(charArray);
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x0010B356 File Offset: 0x00109556
		public bool CheckFilter(string data, bool ignoreWhiteSpace)
		{
			return this.Check(data, ignoreWhiteSpace, 0);
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x0010B364 File Offset: 0x00109564
		public bool CheckNickNameFilter(char[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (!this.CheckForNickName(data, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x0010B38C File Offset: 0x0010958C
		public void AddFilterString(string data)
		{
			this.root = this;
			this.Add(data.ToUpper(), 0);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x0010B3A4 File Offset: 0x001095A4
		private void Add(string data, int index = 0)
		{
			if (data.Length == index)
			{
				this.isLeaf = true;
				return;
			}
			CFilter cfilter;
			if (!this.dic.TryGetValue(data[index], out cfilter))
			{
				cfilter = new CFilter();
				cfilter.root = this;
			}
			this.dic[data[index]] = cfilter;
			cfilter.Add(data, ++index);
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0010B408 File Offset: 0x00109608
		private bool Check(string data, bool ignoreWhiteSpace, int index = 0)
		{
			string text = data.ToUpper();
			if (text.Length == index || this.isLeaf)
			{
				return !this.isLeaf;
			}
			CFilter cfilter;
			return (ignoreWhiteSpace || !char.IsWhiteSpace(text[index])) && (!this.dic.TryGetValue(text[index], out cfilter) || cfilter.Check(text, ignoreWhiteSpace, ++index));
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x0010B470 File Offset: 0x00109670
		private bool CheckForNickName(char[] data, int index = 0)
		{
			if (data.Length == index || this.isLeaf)
			{
				return !this.isLeaf;
			}
			CFilter cfilter;
			return !char.IsWhiteSpace(data[index]) && StringValidSet.Valid(data[index]) && (!this.dic.TryGetValue(char.ToUpper(data[index]), out cfilter) || cfilter.CheckForNickName(data, ++index));
		}

		// Token: 0x040032EA RID: 13034
		private CFilter root;

		// Token: 0x040032EB RID: 13035
		private Dictionary<char, CFilter> dic = new Dictionary<char, CFilter>();

		// Token: 0x040032EC RID: 13036
		private bool isLeaf;
	}
}
