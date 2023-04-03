using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using AssetBundles;
using Cs.Logging;
using Cs.Memory;
using KeraLua;
using NKC;
using NKC.Converter;
using NKC.Patcher;
using NLua;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000426 RID: 1062
	public sealed class NKMLua : IDisposable
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x000850F5 File Offset: 0x000832F5
		public NKMLua()
		{
			this.m_TableDepth = 0;
			this.state = this.m_LuaSvr.State;
			this.state.Encoding = Encoding.UTF8;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00085131 File Offset: 0x00083331
		public bool DoString(string str)
		{
			this.m_LuaSvr.DoString(str, "chunk");
			return true;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00085146 File Offset: 0x00083346
		public void LuaClose()
		{
			if (this.disposed)
			{
				return;
			}
			this.m_LuaSvr.Dispose();
			this.disposed = true;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00085163 File Offset: 0x00083363
		public void Dispose()
		{
			this.LuaClose();
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0008516B File Offset: 0x0008336B
		private bool Get(string name)
		{
			if (this.m_TableDepth > 0)
			{
				this.state.PushString(name);
				return this.state.GetTable(-2) > LuaType.Nil;
			}
			return this.state.GetGlobal(name) > LuaType.Nil;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000851A2 File Offset: 0x000833A2
		private bool Get(int iIndex)
		{
			if (this.m_TableDepth <= 0)
			{
				return false;
			}
			this.state.PushNumber((double)iIndex);
			this.state.GetTable(-2);
			return true;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000851CB File Offset: 0x000833CB
		public bool OpenTable(string tableName)
		{
			if (this.Get(tableName) && this.state.IsTable(-1))
			{
				this.m_TableDepth++;
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00085201 File Offset: 0x00083401
		public bool OpenTable(int iIndex)
		{
			if (this.Get(iIndex) && this.state.IsTable(-1))
			{
				this.m_TableDepth++;
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00085237 File Offset: 0x00083437
		public IDisposable OpenTable(string tableName, string errorMessage, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (!this.OpenTable(tableName))
			{
				Log.ErrorAndExit(errorMessage, file, line);
				return null;
			}
			return new NKMLuaTableOpener(this);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00085253 File Offset: 0x00083453
		public IDisposable OpenTable(int index, string errorMessage, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (!this.OpenTable(index))
			{
				Log.ErrorAndExit(errorMessage, file, line);
				return null;
			}
			return new NKMLuaTableOpener(this);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0008526F File Offset: 0x0008346F
		public bool CloseTable()
		{
			if (this.m_TableDepth > 0)
			{
				this.state.Pop(1);
				this.m_TableDepth--;
				return true;
			}
			return false;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00085298 File Offset: 0x00083498
		public DateTime GetDateTime(string keyName)
		{
			DateTime result;
			if (!this.GetData(keyName, out result, DateTime.MinValue))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 138);
			}
			return result;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000852DC File Offset: 0x000834DC
		public DateTime GetDateTime(string keyName, DateTime defaultValue)
		{
			DateTime result;
			if (!this.GetData(keyName, out result, DateTime.MinValue))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x000852FC File Offset: 0x000834FC
		public TimeSpan GetTimeSpan(string keyName)
		{
			TimeSpan result;
			if (!this.GetData(keyName, out result, TimeSpan.MinValue))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 158);
			}
			return result;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00085340 File Offset: 0x00083540
		public TimeSpan GetTimeSpan(string keyName, TimeSpan defaultValue)
		{
			TimeSpan result;
			if (!this.GetData(keyName, out result, TimeSpan.MinValue))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00085360 File Offset: 0x00083560
		public string GetString(string keyName)
		{
			string result;
			if (!this.GetData(keyName, out result, null))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 178);
			}
			return result;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000853A0 File Offset: 0x000835A0
		public bool GetBoolean(string keyName)
		{
			bool result = false;
			if (!this.GetData(keyName, ref result))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 189);
			}
			return result;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000853E0 File Offset: 0x000835E0
		public int GetInt32(string keyName)
		{
			int result = 0;
			if (!this.GetData(keyName, ref result))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 200);
			}
			return result;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00085420 File Offset: 0x00083620
		public int GetInt32(string keyName, int defaultValue)
		{
			int result;
			this.GetData(keyName, out result, defaultValue);
			return result;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0008543C File Offset: 0x0008363C
		public string GetString(string keyName, string defaultValue)
		{
			string result;
			this.GetData(keyName, out result, defaultValue);
			return result;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00085458 File Offset: 0x00083658
		public float GetFloat(string keyName, float defaultValue)
		{
			float result;
			this.GetData(keyName, out result, defaultValue);
			return result;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00085474 File Offset: 0x00083674
		public bool GetBoolean(string keyName, bool defaultValue)
		{
			bool result;
			this.GetData(keyName, out result, defaultValue);
			return result;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00085490 File Offset: 0x00083690
		public long GetInt64(string keyName)
		{
			long result = 0L;
			if (!this.GetData(keyName, ref result))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 235);
			}
			return result;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000854D4 File Offset: 0x000836D4
		public T GetEnum<T>(string keyName) where T : Enum
		{
			T result;
			if (!this.GetDataEnum<T>(keyName, out result))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 245);
			}
			return result;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00085514 File Offset: 0x00083714
		public T GetEnum<T>(string keyName, T defaultValue) where T : Enum
		{
			T result;
			if (!this.GetDataEnum<T>(keyName, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00085530 File Offset: 0x00083730
		public float GetFloat(string keyName)
		{
			float result = 0f;
			if (!this.GetData(keyName, ref result))
			{
				Log.ErrorAndExit("get lua value failed. keyName:" + keyName + " filename:" + this.fileNameForDebug, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 266);
			}
			return result;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00085574 File Offset: 0x00083774
		public bool GetData(string keyName, out bool rbValue, bool defValue)
		{
			rbValue = defValue;
			if (this.Get(keyName) && this.state.IsBoolean(-1))
			{
				rbValue = this.state.ToBoolean(-1);
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000855C4 File Offset: 0x000837C4
		public bool GetData(int iIndex, out bool rbValue, bool defValue)
		{
			rbValue = defValue;
			if (this.Get(iIndex) && this.state.IsBoolean(-1))
			{
				rbValue = this.state.ToBoolean(-1);
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00085614 File Offset: 0x00083814
		public bool GetData(string pszName, out int rValue, int defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = (int)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 317);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00085690 File Offset: 0x00083890
		public bool GetData(int iIndex, out int rValue, int defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (int)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 337);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00085710 File Offset: 0x00083910
		public bool GetData(string pszName, out short rValue, short defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = (short)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 357);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0008578C File Offset: 0x0008398C
		public bool GetData(int iIndex, out short rValue, short defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (short)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 377);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0008580C File Offset: 0x00083A0C
		public bool GetData(string pszName, out byte rValue, byte defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = (byte)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 397);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00085888 File Offset: 0x00083A88
		public bool GetData(int iIndex, out byte rValue, byte defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (byte)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 417);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00085908 File Offset: 0x00083B08
		public bool GetData(string pszName, out long rValue, long defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = (long)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 437);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00085984 File Offset: 0x00083B84
		public bool GetData(int iIndex, out long rValue, long defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (long)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 457);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00085A04 File Offset: 0x00083C04
		public bool GetData(string pszName, out float rValue, float defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 477);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00085A80 File Offset: 0x00083C80
		public bool GetData(int iIndex, out float rValue, float defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 497);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00085B00 File Offset: 0x00083D00
		public bool GetData(string pszName, out double rValue, double defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				rValue = this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 517);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00085B7C File Offset: 0x00083D7C
		public bool GetData(int iIndex, out double rValue, double defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 537);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00085BFC File Offset: 0x00083DFC
		public bool GetData(string pszName, List<float> listFloat, int index)
		{
			if (this.Get(pszName) && this.state.IsNumber(-1))
			{
				listFloat[index] = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 556);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00085C78 File Offset: 0x00083E78
		public bool GetData(int iIndex, List<float> listFloat, int index)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				listFloat[index] = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 575);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00085CFC File Offset: 0x00083EFC
		public bool GetData(string pszName, out string rValue, string defValue)
		{
			rValue = defValue;
			if (this.Get(pszName) && this.state.IsString(-1))
			{
				rValue = string.Intern(this.state.ToString(-1));
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00085D54 File Offset: 0x00083F54
		public bool GetData(int iIndex, out string rValue, string defValue)
		{
			rValue = defValue;
			if (this.Get(iIndex) && this.state.IsString(-1))
			{
				rValue = string.Intern(this.state.ToString(-1));
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x00085DA9 File Offset: 0x00083FA9
		public bool GetData(string pszName, out DateTime rValue, DateTime defValue)
		{
			rValue = defValue;
			return this.GetData(pszName, ref rValue);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00085DBA File Offset: 0x00083FBA
		public bool GetData(string pszName, out TimeSpan rValue, TimeSpan defValue)
		{
			rValue = defValue;
			return this.GetData(pszName, ref rValue);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00085DCC File Offset: 0x00083FCC
		public bool GetData(string pszName, ICollection<int> listInt)
		{
			if (listInt == null)
			{
				return false;
			}
			if (this.OpenTable(pszName))
			{
				int num = 1;
				int item = 0;
				while (this.GetData(num, ref item))
				{
					listInt.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00085E10 File Offset: 0x00084010
		public bool GetData(string pszName, ICollection<string> listString)
		{
			if (listString == null)
			{
				return false;
			}
			if (this.OpenTable(pszName))
			{
				int num = 1;
				string item = "";
				while (this.GetData(num, ref item))
				{
					listString.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00085E58 File Offset: 0x00084058
		public bool GetDataEnum<T>(string pszName, out T result) where T : Enum
		{
			this.Get(pszName);
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				result = default(T);
				return false;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			return data.TryParse(out result, false);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00085EB4 File Offset: 0x000840B4
		public bool GetDataEnum<T>(int iIndex, out T result) where T : Enum
		{
			if (this.Get(iIndex) && !this.state.IsString(-1))
			{
				this.state.Pop(1);
				result = default(T);
				return false;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			return data.TryParse(out result, false);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00085F14 File Offset: 0x00084114
		public bool GetData(string keyName, ref bool rbValue)
		{
			this.Get(keyName);
			if (this.state.IsBoolean(-1))
			{
				rbValue = this.state.ToBoolean(-1);
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00085F60 File Offset: 0x00084160
		public bool GetData(int iIndex, ref bool rbValue)
		{
			if (this.Get(iIndex) && this.state.IsBoolean(-1))
			{
				rbValue = this.state.ToBoolean(-1);
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00085FB0 File Offset: 0x000841B0
		public bool GetData(string pszName, ref int rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = (int)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 752);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00086028 File Offset: 0x00084228
		public bool GetData(int iIndex, ref int rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (int)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 771);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000860A4 File Offset: 0x000842A4
		public bool GetData(string pszName, ref short rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = (short)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 791);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008611C File Offset: 0x0008431C
		public bool GetData(int iIndex, ref short rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (short)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 810);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00086198 File Offset: 0x00084398
		public bool GetData(string pszName, ref byte rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = (byte)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 830);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00086210 File Offset: 0x00084410
		public bool GetData(int iIndex, ref byte rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (byte)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 849);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008628C File Offset: 0x0008448C
		public bool GetData(string pszName, ref long rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = (long)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 869);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00086304 File Offset: 0x00084504
		public bool GetData(int iIndex, ref long rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (long)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 888);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00086380 File Offset: 0x00084580
		public bool GetData(string pszName, ref float rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 908);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000863F8 File Offset: 0x000845F8
		public bool GetData(int iIndex, ref float rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = (float)this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 927);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00086474 File Offset: 0x00084674
		public bool GetData(string pszName, ref double rValue)
		{
			this.Get(pszName);
			if (this.state.IsNumber(-1))
			{
				rValue = this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + pszName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 947);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000864EC File Offset: 0x000846EC
		public bool GetData(int iIndex, ref double rValue)
		{
			if (this.Get(iIndex) && this.state.IsNumber(-1))
			{
				rValue = this.state.ToNumber(-1);
				this.state.Pop(1);
				return true;
			}
			if (this.state.IsString(-1))
			{
				Log.Error("WrongType: " + iIndex.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 966);
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00086568 File Offset: 0x00084768
		public bool GetData(string pszName, ref string rValue)
		{
			this.Get(pszName);
			if (this.state.IsStringOrNumber(-1))
			{
				rValue = string.Intern(this.state.ToString(-1));
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000865BC File Offset: 0x000847BC
		public bool GetData(int iIndex, ref string rValue)
		{
			if (this.Get(iIndex) && this.state.IsStringOrNumber(-1))
			{
				rValue = string.Intern(this.state.ToString(-1));
				this.state.Pop(1);
				return true;
			}
			this.state.Pop(1);
			return false;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00086610 File Offset: 0x00084810
		public bool GetData(string pszName, ref DateTime rValue)
		{
			this.Get(pszName);
			if (this.state.IsString(-1))
			{
				string text = string.Intern(this.state.ToString(-1));
				this.state.Pop(1);
				if (!DateTime.TryParse(text, out rValue))
				{
					Log.Error("invalid date format:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 1015);
					return false;
				}
				return true;
			}
			else
			{
				if (this.state.IsNumber(-1))
				{
					double d = this.state.ToNumber(-1);
					this.state.Pop(1);
					rValue = DateTime.FromOADate(d);
					return true;
				}
				this.state.Pop(1);
				return false;
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000866BC File Offset: 0x000848BC
		public bool GetData(string pszName, ref TimeSpan rValue)
		{
			this.Get(pszName);
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				return false;
			}
			string text = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			if (!TimeSpan.TryParse(text, out rValue))
			{
				Log.Error("invalid date format:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 1046);
				return false;
			}
			return true;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00086734 File Offset: 0x00084934
		public bool GetData<T>(string pszName, ref T result) where T : struct, Enum
		{
			this.Get(pszName);
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				return false;
			}
			string text = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T t;
			if (!Enum.TryParse<T>(text, out t))
			{
				Log.Error(string.Concat(new string[]
				{
					"[",
					typeof(T).Name,
					"] undefined type. key:",
					pszName,
					" value:",
					text
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMLua.cs", 1070);
				return false;
			}
			result = t;
			return true;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000867E4 File Offset: 0x000849E4
		public bool GetDataList(string pszName, out List<int> result)
		{
			result = new List<int>();
			if (this.OpenTable(pszName))
			{
				int num = 1;
				int item = 0;
				while (this.GetData(num, ref item))
				{
					result.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00086828 File Offset: 0x00084A28
		public bool GetDataList(string pszName, out List<float> result)
		{
			result = new List<float>();
			if (this.OpenTable(pszName))
			{
				int num = 1;
				float item = 0f;
				while (this.GetData(num, ref item))
				{
					result.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00086870 File Offset: 0x00084A70
		public bool GetDataList(string pszName, out List<string> result)
		{
			result = new List<string>();
			if (this.OpenTable(pszName))
			{
				int num = 1;
				string item = "";
				while (this.GetData(num, ref item))
				{
					result.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000868B8 File Offset: 0x00084AB8
		public bool GetDataList(string pszName, out HashSet<int> result)
		{
			result = new HashSet<int>();
			if (this.OpenTable(pszName))
			{
				int num = 1;
				int item = 0;
				while (this.GetData(num, ref item))
				{
					result.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00086900 File Offset: 0x00084B00
		public bool GetDataListEnum<T>(string pszName, ICollection<T> result, bool bClearList = true) where T : Enum
		{
			if (result == null)
			{
				return false;
			}
			if (bClearList)
			{
				result.Clear();
			}
			if (this.OpenTable(pszName))
			{
				int num = 1;
				T item;
				while (this.GetDataEnum<T>(num, out item))
				{
					result.Add(item);
					num++;
				}
				this.CloseTable();
				return true;
			}
			return false;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00086948 File Offset: 0x00084B48
		public bool GetExplicitEnum<T>(string pszName, ref T? result) where T : struct, Enum
		{
			this.Get(pszName);
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				return true;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T value;
			if (!data.TryParse(out value, false))
			{
				return false;
			}
			result = new T?(value);
			return true;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000869B0 File Offset: 0x00084BB0
		public bool GetExplicitEnum<T>(int iIndex, ref T? result) where T : struct, Enum
		{
			if (!this.Get(iIndex) || !this.state.IsString(-1))
			{
				this.state.Pop(1);
				return false;
			}
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				return true;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T value;
			if (!data.TryParse(out value, false))
			{
				return false;
			}
			result = new T?(value);
			return true;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00086A34 File Offset: 0x00084C34
		public bool GetData<T>(string pszName, out T result, T defValue) where T : Enum
		{
			this.Get(pszName);
			if (!this.state.IsString(-1))
			{
				this.state.Pop(1);
				result = defValue;
				return false;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T t;
			if (!data.TryParse(out t, false))
			{
				result = defValue;
				return false;
			}
			result = t;
			return true;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00086AA4 File Offset: 0x00084CA4
		public bool GetData<T>(int iIndex, ref T result) where T : Enum
		{
			if (this.Get(iIndex) && !this.state.IsString(-1))
			{
				this.state.Pop(1);
				return false;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T t;
			if (!data.TryParse(out t, false))
			{
				return false;
			}
			result = t;
			return true;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00086B08 File Offset: 0x00084D08
		public bool GetData<T>(int iIndex, out T result, T defValue) where T : Enum
		{
			if (this.Get(iIndex) && !this.state.IsString(-1))
			{
				this.state.Pop(1);
				result = defValue;
				return false;
			}
			string data = string.Intern(this.state.ToString(-1));
			this.state.Pop(1);
			T t;
			if (!data.TryParse(out t, false))
			{
				result = defValue;
				return false;
			}
			result = t;
			return true;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00086B7C File Offset: 0x00084D7C
		public bool LoadCommonPath(string bundleName, string fileName, bool bAddCompiledLuaPostFix = true)
		{
			string text = "";
			if (!this.LoadCommonPathBase(bundleName, fileName, bAddCompiledLuaPostFix, NKCDefineManager.DEFINE_USE_DEV_SCRIPT(), ref text))
			{
				Log.ErrorAndExit(string.Concat(new string[]
				{
					"LUA Loading Error. FileName:",
					fileName,
					" BundleName:",
					bundleName,
					" error:",
					text
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMLuaEx.cs", 114);
				Log.ErrorAndExit(text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMLuaEx.cs", 115);
				return false;
			}
			return true;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00086BEC File Offset: 0x00084DEC
		public bool LoadCommonPathBase(string bundleName, string fileName, bool bAddCompiledLuaPostFix, bool bUseDevScript, ref string errorMessage)
		{
			string text = NKMLua.GetEncryptedFileName(fileName);
			if (NKCDefineManager.DEFINE_EXTRA_ASSET())
			{
				string str = ".bytes";
				if (bAddCompiledLuaPostFix)
				{
					str = "_C.bytes";
				}
				string text2 = Path.Combine(Path.Combine(NKCUtil.GetExtraDownloadPath(), bundleName.ToUpper()), text + str);
				if (NKCPatchUtility.IsFileExists(text2))
				{
					byte[] array = null;
					if (text2.Contains("jar:"))
					{
						array = BetterStreamingAssets.ReadAllBytes(NKCAssetbundleInnerStream.GetJarRelativePath(text2));
					}
					else
					{
						array = File.ReadAllBytes(text2);
					}
					Crypto2.Decrypt(array, array.Length);
					string chunk;
					using (MemoryStream memoryStream = new MemoryStream(array, 0, array.Length, false))
					{
						using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
						{
							chunk = streamReader.ReadToEnd();
						}
					}
					if (NKCDefineManager.DEFINE_USE_COMPILED_LUA())
					{
						bool flag = true;
						if (text.Contains("LUA_ASSET_BUNDLE_FILE_LIST"))
						{
							flag = false;
						}
						if (flag)
						{
							this.m_LuaSvr.DoString(array, text, "b");
						}
						else
						{
							this.m_LuaSvr.DoString(chunk, text);
						}
					}
					else
					{
						this.m_LuaSvr.DoString(chunk, text);
					}
					return true;
				}
			}
			NKCAssetResourceData nkcassetResourceData = null;
			bool result;
			try
			{
				if (bAddCompiledLuaPostFix)
				{
					text += "_c";
				}
				nkcassetResourceData = NKCAssetResourceManager.OpenResource<TextAsset>(bundleName, text, false, null);
				TextAsset asset = nkcassetResourceData.GetAsset<TextAsset>();
				if (asset == null)
				{
					Log.Error("Resources.Load null: " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMLuaEx.cs", 234);
				}
				if (bUseDevScript)
				{
					string fileName2 = fileName + "_DEV";
					if (NKMLua.CheckCommonFileExist(bundleName, fileName2, bAddCompiledLuaPostFix))
					{
						NKCAssetResourceManager.CloseResource(nkcassetResourceData);
						return this.LoadCommonPathBase(bundleName, fileName2, bAddCompiledLuaPostFix, false, ref errorMessage);
					}
				}
				if (NKMLua.m_LUA_STATIC_BUF_SIZE < asset.bytes.Length)
				{
					NKMLua.m_LUA_STATIC_BUF_SIZE = asset.bytes.Length * 2 + 20;
					NKMLua.m_LUA_STATIC_BUF = new byte[NKMLua.m_LUA_STATIC_BUF_SIZE];
					Log.Debug(string.Format("루아 버퍼 확장 : {0} Mb", NKMLua.m_LUA_STATIC_BUF_SIZE / 1024 / 1024), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMLuaEx.cs", 262);
				}
				Array.Clear(NKMLua.m_LUA_STATIC_BUF, 0, asset.bytes.Length + 10);
				Buffer.BlockCopy(asset.bytes, 0, NKMLua.m_LUA_STATIC_BUF, 0, asset.bytes.Length);
				Crypto2.Decrypt(NKMLua.m_LUA_STATIC_BUF, asset.bytes.Length);
				string chunk2;
				using (MemoryStream memoryStream2 = new MemoryStream(NKMLua.m_LUA_STATIC_BUF, 0, asset.bytes.Length, false))
				{
					using (StreamReader streamReader2 = new StreamReader(memoryStream2, Encoding.UTF8))
					{
						chunk2 = streamReader2.ReadToEnd();
					}
				}
				if (NKCDefineManager.DEFINE_USE_COMPILED_LUA())
				{
					bool flag2 = true;
					if (text.Contains("LUA_ASSET_BUNDLE_FILE_LIST"))
					{
						flag2 = false;
					}
					if (flag2)
					{
						this.m_LuaSvr.DoString(NKMLua.m_LUA_STATIC_BUF, text, "b");
					}
					else
					{
						this.m_LuaSvr.DoString(chunk2, text);
					}
				}
				else
				{
					this.m_LuaSvr.DoString(chunk2, text);
				}
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
				result = true;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				Debug.LogError("Lua parse Error from " + fileName + " : " + errorMessage);
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
				result = false;
			}
			return result;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00086F8C File Offset: 0x0008518C
		public bool LoadServerPath(string fileName)
		{
			throw new Exception("trying to load server path sript. fileName:" + fileName);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00086FA0 File Offset: 0x000851A0
		public static bool CheckCommonFileExist(string bundleName, string fileName, bool bAddCompiledLuaPostFix)
		{
			string text = NKMLua.GetEncryptedFileName(fileName);
			if (bAddCompiledLuaPostFix)
			{
				text += "_c";
			}
			return AssetBundleManager.IsAssetExists(bundleName, text);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00086FCA File Offset: 0x000851CA
		public static string GetDecryptedFileName(string fileName)
		{
			if (!NKCDefineManager.DEFINE_USE_CONVERTED_FILENAME())
			{
				return fileName;
			}
			if (fileName.Contains("LUA_ASSET_BUNDLE_FILE_LIST"))
			{
				return fileName;
			}
			return NKMLua._converter.Decryption(fileName);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00086FF0 File Offset: 0x000851F0
		private static string GetEncryptedFileName(string fileName)
		{
			if (!NKCDefineManager.DEFINE_USE_CONVERTED_FILENAME())
			{
				return fileName;
			}
			if (fileName.Contains("LUA_ASSET_BUNDLE_FILE_LIST"))
			{
				return fileName;
			}
			if (NKCDefineManager.DEINFE_USE_CONVERTED_FILENAME_TO_UPPERCASE())
			{
				Log.Info("[GetEncryptedFileName FileName convert to uppercase] : " + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMLuaEx.cs", 374);
				fileName = fileName.ToUpper();
			}
			return NKMLua._converter.Encryption(fileName);
		}

		// Token: 0x04001BE8 RID: 7144
		private readonly NLua.Lua m_LuaSvr = new NLua.Lua(true);

		// Token: 0x04001BE9 RID: 7145
		private readonly KeraLua.Lua state;

		// Token: 0x04001BEA RID: 7146
		private int m_TableDepth;

		// Token: 0x04001BEB RID: 7147
		private bool disposed;

		// Token: 0x04001BEC RID: 7148
		private string fileNameForDebug;

		// Token: 0x04001BED RID: 7149
		private static int m_LUA_STATIC_BUF_SIZE = 2097152;

		// Token: 0x04001BEE RID: 7150
		private static byte[] m_LUA_STATIC_BUF = new byte[NKMLua.m_LUA_STATIC_BUF_SIZE];

		// Token: 0x04001BEF RID: 7151
		private static IStrConverter _converter = new EasyStrConverter();
	}
}
