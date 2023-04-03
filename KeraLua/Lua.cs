using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace KeraLua
{
	// Token: 0x02000087 RID: 135
	public class Lua : IDisposable
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00014E1C File Offset: 0x0001301C
		public IntPtr Handle
		{
			get
			{
				return this._luaState;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00014E24 File Offset: 0x00013024
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00014E2C File Offset: 0x0001302C
		public Encoding Encoding { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00014E35 File Offset: 0x00013035
		public IntPtr ExtraSpace
		{
			get
			{
				return this._luaState - IntPtr.Size;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00014E47 File Offset: 0x00013047
		public Lua MainThread
		{
			get
			{
				return this._mainState ?? this;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014E54 File Offset: 0x00013054
		public Lua(bool openLibs = true)
		{
			this.Encoding = Encoding.ASCII;
			this._luaState = NativeMethods.luaL_newstate();
			if (openLibs)
			{
				this.OpenLibs();
			}
			this.SetExtraObject<Lua>(this, true);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014E83 File Offset: 0x00013083
		public Lua(LuaAlloc allocator, IntPtr ud)
		{
			this.Encoding = Encoding.ASCII;
			this._luaState = NativeMethods.lua_newstate(allocator.ToFunctionPointer(), ud);
			this.SetExtraObject<Lua>(this, true);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014EB0 File Offset: 0x000130B0
		private Lua(IntPtr luaThread, Lua mainState)
		{
			this._mainState = mainState;
			this._luaState = luaThread;
			this.Encoding = mainState.Encoding;
			this.SetExtraObject<Lua>(this, false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014EE0 File Offset: 0x000130E0
		public static Lua FromIntPtr(IntPtr luaState)
		{
			if (luaState == IntPtr.Zero)
			{
				return null;
			}
			Lua extraObject = Lua.GetExtraObject<Lua>(luaState);
			if (extraObject != null && extraObject._luaState == luaState)
			{
				return extraObject;
			}
			return new Lua(luaState, extraObject.MainThread);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014F24 File Offset: 0x00013124
		~Lua()
		{
			this.Dispose(false);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014F54 File Offset: 0x00013154
		protected virtual void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014F5C File Offset: 0x0001315C
		public void Close()
		{
			if (this._luaState == IntPtr.Zero || this._mainState != null)
			{
				return;
			}
			NativeMethods.lua_close(this._luaState);
			this._luaState = IntPtr.Zero;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014F95 File Offset: 0x00013195
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014FA0 File Offset: 0x000131A0
		private void SetExtraObject<T>(T obj, bool weak) where T : class
		{
			GCHandle value = GCHandle.Alloc(obj, weak ? GCHandleType.Weak : GCHandleType.Normal);
			Marshal.WriteIntPtr(this._luaState - IntPtr.Size, GCHandle.ToIntPtr(value));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014FDC File Offset: 0x000131DC
		private static T GetExtraObject<T>(IntPtr luaState) where T : class
		{
			GCHandle gchandle = GCHandle.FromIntPtr(Marshal.ReadIntPtr(luaState - IntPtr.Size));
			if (!gchandle.IsAllocated)
			{
				return default(T);
			}
			return (T)((object)gchandle.Target);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001501E File Offset: 0x0001321E
		public int AbsIndex(int index)
		{
			return NativeMethods.lua_absindex(this._luaState, index);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001502C File Offset: 0x0001322C
		public void Arith(LuaOperation operation)
		{
			NativeMethods.lua_arith(this._luaState, (int)operation);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001503C File Offset: 0x0001323C
		public LuaFunction AtPanic(LuaFunction panicFunction)
		{
			IntPtr panicf = panicFunction.ToFunctionPointer();
			return NativeMethods.lua_atpanic(this._luaState, panicf).ToLuaFunction();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015061 File Offset: 0x00013261
		public void Call(int arguments, int results)
		{
			NativeMethods.lua_callk(this._luaState, arguments, results, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001507C File Offset: 0x0001327C
		public void CallK(int arguments, int results, int context, LuaKFunction continuation)
		{
			IntPtr k = continuation.ToFunctionPointer();
			NativeMethods.lua_callk(this._luaState, arguments, results, (IntPtr)context, k);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000150A5 File Offset: 0x000132A5
		public bool CheckStack(int nExtraSlots)
		{
			return NativeMethods.lua_checkstack(this._luaState, nExtraSlots) != 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000150B6 File Offset: 0x000132B6
		public bool Compare(int index1, int index2, LuaCompare comparison)
		{
			return NativeMethods.lua_compare(this._luaState, index1, index2, (int)comparison) != 0;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000150C9 File Offset: 0x000132C9
		public void Concat(int n)
		{
			NativeMethods.lua_concat(this._luaState, n);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000150D7 File Offset: 0x000132D7
		public void Copy(int fromIndex, int toIndex)
		{
			NativeMethods.lua_copy(this._luaState, fromIndex, toIndex);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000150E6 File Offset: 0x000132E6
		public void CreateTable(int elements, int records)
		{
			NativeMethods.lua_createtable(this._luaState, elements, records);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000150F5 File Offset: 0x000132F5
		public int Dump(LuaWriter writer, IntPtr data, bool stripDebug)
		{
			return NativeMethods.lua_dump(this._luaState, writer.ToFunctionPointer(), data, stripDebug ? 1 : 0);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015110 File Offset: 0x00013310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Error()
		{
			return NativeMethods.lua_error(this._luaState);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001511D File Offset: 0x0001331D
		public int GarbageCollector(LuaGC what, int data)
		{
			return NativeMethods.lua_gc(this._luaState, (int)what, data);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001512C File Offset: 0x0001332C
		public int GarbageCollector(LuaGC what, int data, int data2)
		{
			return NativeMethods.lua_gc(this._luaState, (int)what, data, data2);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001513C File Offset: 0x0001333C
		public LuaAlloc GetAllocFunction(ref IntPtr ud)
		{
			return NativeMethods.lua_getallocf(this._luaState, ref ud).ToLuaAlloc();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001514F File Offset: 0x0001334F
		public LuaType GetField(int index, string key)
		{
			return (LuaType)NativeMethods.lua_getfield(this._luaState, index, key);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001515E File Offset: 0x0001335E
		public LuaType GetField(LuaRegistry index, string key)
		{
			return (LuaType)NativeMethods.lua_getfield(this._luaState, (int)index, key);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001516D File Offset: 0x0001336D
		public LuaType GetGlobal(string name)
		{
			return (LuaType)NativeMethods.lua_getglobal(this._luaState, name);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001517B File Offset: 0x0001337B
		public LuaType GetInteger(int index, long i)
		{
			return (LuaType)NativeMethods.lua_geti(this._luaState, index, i);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001518A File Offset: 0x0001338A
		public bool GetInfo(string what, IntPtr ar)
		{
			return NativeMethods.lua_getinfo(this._luaState, what, ar) != 0;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001519C File Offset: 0x0001339C
		public bool GetInfo(string what, ref LuaDebug ar)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LuaDebug>(ar));
			bool result = false;
			try
			{
				Marshal.StructureToPtr<LuaDebug>(ar, intPtr, false);
				result = this.GetInfo(what, intPtr);
				ar = LuaDebug.FromIntPtr(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000151F8 File Offset: 0x000133F8
		public string GetLocal(IntPtr ar, int n)
		{
			return Marshal.PtrToStringAnsi(NativeMethods.lua_getlocal(this._luaState, ar, n));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001520C File Offset: 0x0001340C
		public string GetLocal(LuaDebug ar, int n)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LuaDebug>(ar));
			string result = string.Empty;
			try
			{
				Marshal.StructureToPtr<LuaDebug>(ar, intPtr, false);
				result = this.GetLocal(intPtr, n);
				ar = LuaDebug.FromIntPtr(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015260 File Offset: 0x00013460
		public bool GetMetaTable(int index)
		{
			return NativeMethods.lua_getmetatable(this._luaState, index) != 0;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015271 File Offset: 0x00013471
		public int GetStack(int level, IntPtr ar)
		{
			return NativeMethods.lua_getstack(this._luaState, level, ar);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015280 File Offset: 0x00013480
		public int GetStack(int level, ref LuaDebug ar)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LuaDebug>(ar));
			int result = 0;
			try
			{
				Marshal.StructureToPtr<LuaDebug>(ar, intPtr, false);
				result = this.GetStack(level, intPtr);
				ar = LuaDebug.FromIntPtr(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000152DC File Offset: 0x000134DC
		public LuaType GetTable(int index)
		{
			return (LuaType)NativeMethods.lua_gettable(this._luaState, index);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000152EA File Offset: 0x000134EA
		public LuaType GetTable(LuaRegistry index)
		{
			return (LuaType)NativeMethods.lua_gettable(this._luaState, (int)index);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000152F8 File Offset: 0x000134F8
		public int GetTop()
		{
			return NativeMethods.lua_gettop(this._luaState);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015305 File Offset: 0x00013505
		public int GetIndexedUserValue(int index, int nth)
		{
			return NativeMethods.lua_getiuservalue(this._luaState, index, nth);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015314 File Offset: 0x00013514
		public int GetUserValue(int index)
		{
			return this.GetIndexedUserValue(index, 1);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001531E File Offset: 0x0001351E
		public string GetUpValue(int functionIndex, int n)
		{
			return Marshal.PtrToStringAnsi(NativeMethods.lua_getupvalue(this._luaState, functionIndex, n));
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00015332 File Offset: 0x00013532
		public LuaHookFunction Hook
		{
			get
			{
				return NativeMethods.lua_gethook(this._luaState).ToLuaHookFunction();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00015344 File Offset: 0x00013544
		public int HookCount
		{
			get
			{
				return NativeMethods.lua_gethookcount(this._luaState);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00015351 File Offset: 0x00013551
		public LuaHookMask HookMask
		{
			get
			{
				return (LuaHookMask)NativeMethods.lua_gethookmask(this._luaState);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001535E File Offset: 0x0001355E
		public void Insert(int index)
		{
			NativeMethods.lua_rotate(this._luaState, index, 1);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001536D File Offset: 0x0001356D
		public bool IsBoolean(int index)
		{
			return this.Type(index) == LuaType.Boolean;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00015379 File Offset: 0x00013579
		public bool IsCFunction(int index)
		{
			return NativeMethods.lua_iscfunction(this._luaState, index) != 0;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001538A File Offset: 0x0001358A
		public bool IsFunction(int index)
		{
			return this.Type(index) == LuaType.Function;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00015396 File Offset: 0x00013596
		public bool IsInteger(int index)
		{
			return NativeMethods.lua_isinteger(this._luaState, index) != 0;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000153A7 File Offset: 0x000135A7
		public bool IsLightUserData(int index)
		{
			return this.Type(index) == LuaType.LightUserData;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000153B3 File Offset: 0x000135B3
		public bool IsNil(int index)
		{
			return this.Type(index) == LuaType.Nil;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000153BF File Offset: 0x000135BF
		public bool IsNone(int index)
		{
			return this.Type(index) == LuaType.None;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000153CB File Offset: 0x000135CB
		public bool IsNoneOrNil(int index)
		{
			return this.IsNone(index) || this.IsNil(index);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000153DF File Offset: 0x000135DF
		public bool IsNumber(int index)
		{
			return NativeMethods.lua_isnumber(this._luaState, index) != 0;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000153F0 File Offset: 0x000135F0
		public bool IsStringOrNumber(int index)
		{
			return NativeMethods.lua_isstring(this._luaState, index) != 0;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00015401 File Offset: 0x00013601
		public bool IsString(int index)
		{
			return this.Type(index) == LuaType.String;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001540D File Offset: 0x0001360D
		public bool IsTable(int index)
		{
			return this.Type(index) == LuaType.Table;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00015419 File Offset: 0x00013619
		public bool IsThread(int index)
		{
			return this.Type(index) == LuaType.Thread;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015425 File Offset: 0x00013625
		public bool IsUserData(int index)
		{
			return NativeMethods.lua_isuserdata(this._luaState, index) != 0;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00015436 File Offset: 0x00013636
		public bool IsYieldable
		{
			get
			{
				return NativeMethods.lua_isyieldable(this._luaState) != 0;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00015446 File Offset: 0x00013646
		public void PushLength(int index)
		{
			NativeMethods.lua_len(this._luaState, index);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00015454 File Offset: 0x00013654
		public LuaStatus Load(LuaReader reader, IntPtr data, string chunkName, string mode)
		{
			return (LuaStatus)NativeMethods.lua_load(this._luaState, reader.ToFunctionPointer(), data, chunkName, mode);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001546B File Offset: 0x0001366B
		public void NewTable()
		{
			NativeMethods.lua_createtable(this._luaState, 0, 0);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001547A File Offset: 0x0001367A
		public Lua NewThread()
		{
			return new Lua(NativeMethods.lua_newthread(this._luaState), this);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001548D File Offset: 0x0001368D
		public IntPtr NewIndexedUserData(int size, int uv)
		{
			return NativeMethods.lua_newuserdatauv(this._luaState, (UIntPtr)((ulong)((long)size)), uv);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000154A2 File Offset: 0x000136A2
		public IntPtr NewUserData(int size)
		{
			return this.NewIndexedUserData(size, 1);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000154AC File Offset: 0x000136AC
		public bool Next(int index)
		{
			return NativeMethods.lua_next(this._luaState, index) != 0;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000154BD File Offset: 0x000136BD
		public LuaStatus PCall(int arguments, int results, int errorFunctionIndex)
		{
			return (LuaStatus)NativeMethods.lua_pcallk(this._luaState, arguments, results, errorFunctionIndex, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000154D7 File Offset: 0x000136D7
		public LuaStatus PCallK(int arguments, int results, int errorFunctionIndex, int context, LuaKFunction k)
		{
			return (LuaStatus)NativeMethods.lua_pcallk(this._luaState, arguments, results, errorFunctionIndex, (IntPtr)context, k.ToFunctionPointer());
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000154F5 File Offset: 0x000136F5
		public void Pop(int n)
		{
			NativeMethods.lua_settop(this._luaState, -n - 1);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00015506 File Offset: 0x00013706
		public void PushBoolean(bool b)
		{
			NativeMethods.lua_pushboolean(this._luaState, b ? 1 : 0);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001551A File Offset: 0x0001371A
		public void PushCClosure(LuaFunction function, int n)
		{
			NativeMethods.lua_pushcclosure(this._luaState, function.ToFunctionPointer(), n);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001552E File Offset: 0x0001372E
		public void PushCFunction(LuaFunction function)
		{
			this.PushCClosure(function, 0);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00015538 File Offset: 0x00013738
		public void PushGlobalTable()
		{
			NativeMethods.lua_rawgeti(this._luaState, -1001000, 2L);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001554D File Offset: 0x0001374D
		public void PushInteger(long n)
		{
			NativeMethods.lua_pushinteger(this._luaState, n);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001555B File Offset: 0x0001375B
		public void PushLightUserData(IntPtr data)
		{
			NativeMethods.lua_pushlightuserdata(this._luaState, data);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001556C File Offset: 0x0001376C
		public void PushObject<T>(T obj)
		{
			if (obj == null)
			{
				this.PushNil();
				return;
			}
			GCHandle value = GCHandle.Alloc(obj);
			this.PushLightUserData(GCHandle.ToIntPtr(value));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000155A0 File Offset: 0x000137A0
		public void PushBuffer(byte[] buffer)
		{
			if (buffer == null)
			{
				this.PushNil();
				return;
			}
			NativeMethods.lua_pushlstring(this._luaState, buffer, (UIntPtr)((ulong)((long)buffer.Length)));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000155C4 File Offset: 0x000137C4
		public void PushString(string value)
		{
			if (value == null)
			{
				this.PushNil();
				return;
			}
			byte[] bytes = this.Encoding.GetBytes(value);
			this.PushBuffer(bytes);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000155EF File Offset: 0x000137EF
		public void PushString(string value, params object[] args)
		{
			this.PushString(string.Format(value, args));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000155FE File Offset: 0x000137FE
		public void PushNil()
		{
			NativeMethods.lua_pushnil(this._luaState);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001560B File Offset: 0x0001380B
		public void PushNumber(double number)
		{
			NativeMethods.lua_pushnumber(this._luaState, number);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015619 File Offset: 0x00013819
		public bool PushThread()
		{
			return NativeMethods.lua_pushthread(this._luaState) == 1;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015629 File Offset: 0x00013829
		public void PushCopy(int index)
		{
			NativeMethods.lua_pushvalue(this._luaState, index);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00015637 File Offset: 0x00013837
		public bool RawEqual(int index1, int index2)
		{
			return NativeMethods.lua_rawequal(this._luaState, index1, index2) != 0;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00015649 File Offset: 0x00013849
		public LuaType RawGet(int index)
		{
			return (LuaType)NativeMethods.lua_rawget(this._luaState, index);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00015657 File Offset: 0x00013857
		public LuaType RawGet(LuaRegistry index)
		{
			return (LuaType)NativeMethods.lua_rawget(this._luaState, (int)index);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00015665 File Offset: 0x00013865
		public LuaType RawGetInteger(int index, long n)
		{
			return (LuaType)NativeMethods.lua_rawgeti(this._luaState, index, n);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015674 File Offset: 0x00013874
		public LuaType RawGetInteger(LuaRegistry index, long n)
		{
			return (LuaType)NativeMethods.lua_rawgeti(this._luaState, (int)index, n);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015683 File Offset: 0x00013883
		public LuaType RawGetByHashCode(int index, object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "obj shouldn't be null");
			}
			return (LuaType)NativeMethods.lua_rawgetp(this._luaState, index, (IntPtr)obj.GetHashCode());
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000156AF File Offset: 0x000138AF
		public int RawLen(int index)
		{
			return (int)((uint)NativeMethods.lua_rawlen(this._luaState, index));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000156C2 File Offset: 0x000138C2
		public void RawSet(int index)
		{
			NativeMethods.lua_rawset(this._luaState, index);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000156D0 File Offset: 0x000138D0
		public void RawSet(LuaRegistry index)
		{
			NativeMethods.lua_rawset(this._luaState, (int)index);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000156DE File Offset: 0x000138DE
		public void RawSetInteger(int index, long i)
		{
			NativeMethods.lua_rawseti(this._luaState, index, i);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000156ED File Offset: 0x000138ED
		public void RawSetInteger(LuaRegistry index, long i)
		{
			NativeMethods.lua_rawseti(this._luaState, (int)index, i);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000156FC File Offset: 0x000138FC
		public void RawSetByHashCode(int index, object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "obj shouldn't be null");
			}
			NativeMethods.lua_rawsetp(this._luaState, index, (IntPtr)obj.GetHashCode());
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015728 File Offset: 0x00013928
		public void Register(string name, LuaFunction function)
		{
			this.PushCFunction(function);
			this.SetGlobal(name);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015738 File Offset: 0x00013938
		public void Remove(int index)
		{
			this.Rotate(index, -1);
			this.Pop(1);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00015749 File Offset: 0x00013949
		public void Replace(int index)
		{
			this.Copy(-1, index);
			this.Pop(1);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001575A File Offset: 0x0001395A
		public LuaStatus Resume(Lua from, int arguments, out int results)
		{
			return (LuaStatus)NativeMethods.lua_resume(this._luaState, (from != null) ? from._luaState : IntPtr.Zero, arguments, out results);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001577C File Offset: 0x0001397C
		public LuaStatus Resume(Lua from, int arguments)
		{
			int num;
			return (LuaStatus)NativeMethods.lua_resume(this._luaState, (from != null) ? from._luaState : IntPtr.Zero, arguments, out num);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000157A7 File Offset: 0x000139A7
		public int ResetThread()
		{
			return NativeMethods.lua_resetthread(this._luaState);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000157B4 File Offset: 0x000139B4
		public void Rotate(int index, int n)
		{
			NativeMethods.lua_rotate(this._luaState, index, n);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000157C3 File Offset: 0x000139C3
		public void SetAllocFunction(LuaAlloc alloc, ref IntPtr ud)
		{
			NativeMethods.lua_setallocf(this._luaState, alloc.ToFunctionPointer(), ud);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000157D8 File Offset: 0x000139D8
		public void SetField(int index, string key)
		{
			NativeMethods.lua_setfield(this._luaState, index, key);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000157E7 File Offset: 0x000139E7
		public void SetHook(LuaHookFunction hookFunction, LuaHookMask mask, int count)
		{
			NativeMethods.lua_sethook(this._luaState, hookFunction.ToFunctionPointer(), (int)mask, count);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000157FC File Offset: 0x000139FC
		public void SetGlobal(string name)
		{
			NativeMethods.lua_setglobal(this._luaState, name);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001580A File Offset: 0x00013A0A
		public void SetInteger(int index, long n)
		{
			NativeMethods.lua_seti(this._luaState, index, n);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015819 File Offset: 0x00013A19
		public string SetLocal(IntPtr ar, int n)
		{
			return Marshal.PtrToStringAnsi(NativeMethods.lua_setlocal(this._luaState, ar, n));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00015830 File Offset: 0x00013A30
		public string SetLocal(LuaDebug ar, int n)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LuaDebug>(ar));
			string result = string.Empty;
			try
			{
				Marshal.StructureToPtr<LuaDebug>(ar, intPtr, false);
				result = this.SetLocal(intPtr, n);
				ar = LuaDebug.FromIntPtr(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00015884 File Offset: 0x00013A84
		public void SetMetaTable(int index)
		{
			NativeMethods.lua_setmetatable(this._luaState, index);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00015892 File Offset: 0x00013A92
		public void SetTable(int index)
		{
			NativeMethods.lua_settable(this._luaState, index);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000158A0 File Offset: 0x00013AA0
		public void SetTop(int newTop)
		{
			NativeMethods.lua_settop(this._luaState, newTop);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000158AE File Offset: 0x00013AAE
		public string SetUpValue(int functionIndex, int n)
		{
			return Marshal.PtrToStringAnsi(NativeMethods.lua_setupvalue(this._luaState, functionIndex, n));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000158C2 File Offset: 0x00013AC2
		public void SetWarningFunction(LuaWarnFunction function, IntPtr userData)
		{
			NativeMethods.lua_setwarnf(this._luaState, function.ToFunctionPointer(), userData);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000158D6 File Offset: 0x00013AD6
		public void SetIndexedUserValue(int index, int nth)
		{
			NativeMethods.lua_setiuservalue(this._luaState, index, nth);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000158E5 File Offset: 0x00013AE5
		public void SetUserValue(int index)
		{
			this.SetIndexedUserValue(index, 1);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000158EF File Offset: 0x00013AEF
		public LuaStatus Status
		{
			get
			{
				return (LuaStatus)NativeMethods.lua_status(this._luaState);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000158FC File Offset: 0x00013AFC
		public bool StringToNumber(string s)
		{
			return NativeMethods.lua_stringtonumber(this._luaState, s) != UIntPtr.Zero;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00015914 File Offset: 0x00013B14
		public bool ToBoolean(int index)
		{
			return NativeMethods.lua_toboolean(this._luaState, index) != 0;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015925 File Offset: 0x00013B25
		public LuaFunction ToCFunction(int index)
		{
			return NativeMethods.lua_tocfunction(this._luaState, index).ToLuaFunction();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00015938 File Offset: 0x00013B38
		public void ToClose(int index)
		{
			NativeMethods.lua_toclose(this._luaState, index);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00015948 File Offset: 0x00013B48
		public long ToInteger(int index)
		{
			int num;
			return NativeMethods.lua_tointegerx(this._luaState, index, out num);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00015964 File Offset: 0x00013B64
		public long? ToIntegerX(int index)
		{
			int num;
			long value = NativeMethods.lua_tointegerx(this._luaState, index, out num);
			if (num != 0)
			{
				return new long?(value);
			}
			return null;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015993 File Offset: 0x00013B93
		public byte[] ToBuffer(int index)
		{
			return this.ToBuffer(index, true);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000159A0 File Offset: 0x00013BA0
		public byte[] ToBuffer(int index, bool callMetamethod)
		{
			UIntPtr value;
			IntPtr intPtr;
			if (callMetamethod)
			{
				intPtr = NativeMethods.luaL_tolstring(this._luaState, index, out value);
				this.Pop(1);
			}
			else
			{
				intPtr = NativeMethods.lua_tolstring(this._luaState, index, out value);
			}
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)((uint)value);
			if (num == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015A08 File Offset: 0x00013C08
		public string ToString(int index)
		{
			return this.ToString(index, true);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00015A14 File Offset: 0x00013C14
		public string ToString(int index, bool callMetamethod)
		{
			byte[] array = this.ToBuffer(index, callMetamethod);
			if (array == null)
			{
				return null;
			}
			return this.Encoding.GetString(array);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015A3C File Offset: 0x00013C3C
		public double ToNumber(int index)
		{
			int num;
			return NativeMethods.lua_tonumberx(this._luaState, index, out num);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015A58 File Offset: 0x00013C58
		public double? ToNumberX(int index)
		{
			int num;
			double value = NativeMethods.lua_tonumberx(this._luaState, index, out num);
			if (num != 0)
			{
				return new double?(value);
			}
			return null;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00015A87 File Offset: 0x00013C87
		public IntPtr ToPointer(int index)
		{
			return NativeMethods.lua_topointer(this._luaState, index);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00015A98 File Offset: 0x00013C98
		public Lua ToThread(int index)
		{
			IntPtr intPtr = NativeMethods.lua_tothread(this._luaState, index);
			if (intPtr == this._luaState)
			{
				return this;
			}
			return Lua.FromIntPtr(intPtr);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00015AC8 File Offset: 0x00013CC8
		public T ToObject<T>(int index, bool freeGCHandle = true)
		{
			if (this.IsNil(index) || !this.IsLightUserData(index))
			{
				return default(T);
			}
			IntPtr intPtr = this.ToUserData(index);
			if (intPtr == IntPtr.Zero)
			{
				return default(T);
			}
			GCHandle gchandle = GCHandle.FromIntPtr(intPtr);
			if (!gchandle.IsAllocated)
			{
				return default(T);
			}
			T result = (T)((object)gchandle.Target);
			if (freeGCHandle)
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00015B40 File Offset: 0x00013D40
		public IntPtr ToUserData(int index)
		{
			return NativeMethods.lua_touserdata(this._luaState, index);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00015B4E File Offset: 0x00013D4E
		public LuaType Type(int index)
		{
			return (LuaType)NativeMethods.lua_type(this._luaState, index);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015B5C File Offset: 0x00013D5C
		public string TypeName(LuaType type)
		{
			return Marshal.PtrToStringAnsi(NativeMethods.lua_typename(this._luaState, (int)type));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015B6F File Offset: 0x00013D6F
		public long UpValueId(int functionIndex, int n)
		{
			return (long)NativeMethods.lua_upvalueid(this._luaState, functionIndex, n);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015B83 File Offset: 0x00013D83
		public static int UpValueIndex(int i)
		{
			return -1001000 - i;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015B8C File Offset: 0x00013D8C
		public void UpValueJoin(int functionIndex1, int n1, int functionIndex2, int n2)
		{
			NativeMethods.lua_upvaluejoin(this._luaState, functionIndex1, n1, functionIndex2, n2);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00015B9E File Offset: 0x00013D9E
		public double Version()
		{
			return NativeMethods.lua_version(this._luaState);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015BAB File Offset: 0x00013DAB
		public void XMove(Lua to, int n)
		{
			if (to == null)
			{
				throw new ArgumentNullException("to", "to shouldn't be null");
			}
			NativeMethods.lua_xmove(this._luaState, to._luaState, n);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015BD2 File Offset: 0x00013DD2
		public int Yield(int results)
		{
			return NativeMethods.lua_yieldk(this._luaState, results, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00015BEC File Offset: 0x00013DEC
		public int YieldK(int results, int context, LuaKFunction continuation)
		{
			IntPtr k = continuation.ToFunctionPointer();
			return NativeMethods.lua_yieldk(this._luaState, results, (IntPtr)context, k);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00015C13 File Offset: 0x00013E13
		public void ArgumentCheck(bool condition, int argument, string message)
		{
			if (condition)
			{
				return;
			}
			this.ArgumentError(argument, message);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00015C22 File Offset: 0x00013E22
		public int ArgumentError(int argument, string message)
		{
			return NativeMethods.luaL_argerror(this._luaState, argument, message);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00015C31 File Offset: 0x00013E31
		public bool CallMetaMethod(int obj, string field)
		{
			return NativeMethods.luaL_callmeta(this._luaState, obj, field) != 0;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00015C43 File Offset: 0x00013E43
		public void CheckAny(int argument)
		{
			NativeMethods.luaL_checkany(this._luaState, argument);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015C51 File Offset: 0x00013E51
		public long CheckInteger(int argument)
		{
			return NativeMethods.luaL_checkinteger(this._luaState, argument);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00015C60 File Offset: 0x00013E60
		public byte[] CheckBuffer(int argument)
		{
			UIntPtr value;
			IntPtr intPtr = NativeMethods.luaL_checklstring(this._luaState, argument, out value);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)((uint)value);
			if (num == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015CB0 File Offset: 0x00013EB0
		public string CheckString(int argument)
		{
			byte[] array = this.CheckBuffer(argument);
			if (array == null)
			{
				return null;
			}
			return this.Encoding.GetString(array);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00015CD6 File Offset: 0x00013ED6
		public double CheckNumber(int argument)
		{
			return NativeMethods.luaL_checknumber(this._luaState, argument);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public int CheckOption(int argument, string def, string[] list)
		{
			return NativeMethods.luaL_checkoption(this._luaState, argument, def, list);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public void CheckStack(int newSize, string message)
		{
			NativeMethods.luaL_checkstack(this._luaState, newSize, message);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00015D03 File Offset: 0x00013F03
		public void CheckType(int argument, LuaType type)
		{
			NativeMethods.luaL_checktype(this._luaState, argument, (int)type);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00015D14 File Offset: 0x00013F14
		public T CheckObject<T>(int argument, string typeName, bool freeGCHandle = true)
		{
			if (this.IsNil(argument) || !this.IsLightUserData(argument))
			{
				return default(T);
			}
			IntPtr intPtr = this.CheckUserData(argument, typeName);
			if (intPtr == IntPtr.Zero)
			{
				return default(T);
			}
			GCHandle gchandle = GCHandle.FromIntPtr(intPtr);
			if (!gchandle.IsAllocated)
			{
				return default(T);
			}
			T result = (T)((object)gchandle.Target);
			if (freeGCHandle)
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015D8D File Offset: 0x00013F8D
		public IntPtr CheckUserData(int argument, string typeName)
		{
			return NativeMethods.luaL_checkudata(this._luaState, argument, typeName);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00015D9C File Offset: 0x00013F9C
		public bool DoFile(string file)
		{
			return this.LoadFile(file) != LuaStatus.OK || this.PCall(0, -1, 0) > LuaStatus.OK;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015DB5 File Offset: 0x00013FB5
		public bool DoString(string file)
		{
			return this.LoadString(file) != LuaStatus.OK || this.PCall(0, -1, 0) > LuaStatus.OK;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public int Error(string value, params object[] v)
		{
			string message = string.Format(value, v);
			return NativeMethods.luaL_error(this._luaState, message);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015DF1 File Offset: 0x00013FF1
		public int ExecResult(int stat)
		{
			return NativeMethods.luaL_execresult(this._luaState, stat);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015DFF File Offset: 0x00013FFF
		public int FileResult(int stat, string fileName)
		{
			return NativeMethods.luaL_fileresult(this._luaState, stat, fileName);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015E0E File Offset: 0x0001400E
		public LuaType GetMetaField(int obj, string field)
		{
			return (LuaType)NativeMethods.luaL_getmetafield(this._luaState, obj, field);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015E1D File Offset: 0x0001401D
		public LuaType GetMetaTable(string tableName)
		{
			return this.GetField(LuaRegistry.Index, tableName);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00015E2B File Offset: 0x0001402B
		public bool GetSubTable(int index, string name)
		{
			return NativeMethods.luaL_getsubtable(this._luaState, index, name) != 0;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015E3D File Offset: 0x0001403D
		public long Length(int index)
		{
			return NativeMethods.luaL_len(this._luaState, index);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015E4B File Offset: 0x0001404B
		public LuaStatus LoadBuffer(byte[] buffer, string name, string mode)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "buffer shouldn't be null");
			}
			return (LuaStatus)NativeMethods.luaL_loadbufferx(this._luaState, buffer, (UIntPtr)((ulong)((long)buffer.Length)), name, mode);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015E77 File Offset: 0x00014077
		public LuaStatus LoadBuffer(byte[] buffer, string name)
		{
			return this.LoadBuffer(buffer, name, null);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015E82 File Offset: 0x00014082
		public LuaStatus LoadBuffer(byte[] buffer)
		{
			return this.LoadBuffer(buffer, null, null);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015E90 File Offset: 0x00014090
		public LuaStatus LoadString(string chunk, string name)
		{
			byte[] bytes = this.Encoding.GetBytes(chunk);
			return this.LoadBuffer(bytes, name);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015EB2 File Offset: 0x000140B2
		public LuaStatus LoadString(string chunk)
		{
			return this.LoadString(chunk, null);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015EBC File Offset: 0x000140BC
		public LuaStatus LoadFile(string file, string mode)
		{
			return (LuaStatus)NativeMethods.luaL_loadfilex(this._luaState, file, mode);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015ECB File Offset: 0x000140CB
		public LuaStatus LoadFile(string file)
		{
			return this.LoadFile(file, null);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015ED5 File Offset: 0x000140D5
		public void NewLib(LuaRegister[] library)
		{
			this.NewLibTable(library);
			this.SetFuncs(library, 0);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015EE6 File Offset: 0x000140E6
		public void NewLibTable(LuaRegister[] library)
		{
			if (library == null)
			{
				throw new ArgumentNullException("library", "library shouldn't be null");
			}
			this.CreateTable(0, library.Length);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015F05 File Offset: 0x00014105
		public bool NewMetaTable(string name)
		{
			return NativeMethods.luaL_newmetatable(this._luaState, name) != 0;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015F16 File Offset: 0x00014116
		public void OpenLibs()
		{
			NativeMethods.luaL_openlibs(this._luaState);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015F23 File Offset: 0x00014123
		public long OptInteger(int argument, long d)
		{
			return NativeMethods.luaL_optinteger(this._luaState, argument, d);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015F32 File Offset: 0x00014132
		public byte[] OptBuffer(int index, byte[] def)
		{
			if (this.IsNoneOrNil(index))
			{
				return def;
			}
			return this.CheckBuffer(index);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015F46 File Offset: 0x00014146
		public string OptString(int index, string def)
		{
			if (this.IsNoneOrNil(index))
			{
				return def;
			}
			return this.CheckString(index);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015F5A File Offset: 0x0001415A
		public double OptNumber(int index, double def)
		{
			return NativeMethods.luaL_optnumber(this._luaState, index, def);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015F69 File Offset: 0x00014169
		public int Ref(LuaRegistry tableIndex)
		{
			return NativeMethods.luaL_ref(this._luaState, (int)tableIndex);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015F77 File Offset: 0x00014177
		public void RequireF(string moduleName, LuaFunction openFunction, bool global)
		{
			NativeMethods.luaL_requiref(this._luaState, moduleName, openFunction.ToFunctionPointer(), global ? 1 : 0);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015F92 File Offset: 0x00014192
		public void SetFuncs(LuaRegister[] library, int numberUpValues)
		{
			NativeMethods.luaL_setfuncs(this._luaState, library, numberUpValues);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015FA1 File Offset: 0x000141A1
		public void SetMetaTable(string name)
		{
			NativeMethods.luaL_setmetatable(this._luaState, name);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015FB0 File Offset: 0x000141B0
		public T TestObject<T>(int argument, string typeName, bool freeGCHandle = true)
		{
			if (this.IsNil(argument) || !this.IsLightUserData(argument))
			{
				return default(T);
			}
			IntPtr intPtr = this.TestUserData(argument, typeName);
			if (intPtr == IntPtr.Zero)
			{
				return default(T);
			}
			GCHandle gchandle = GCHandle.FromIntPtr(intPtr);
			if (!gchandle.IsAllocated)
			{
				return default(T);
			}
			T result = (T)((object)gchandle.Target);
			if (freeGCHandle)
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016029 File Offset: 0x00014229
		public IntPtr TestUserData(int argument, string typeName)
		{
			return NativeMethods.luaL_testudata(this._luaState, argument, typeName);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016038 File Offset: 0x00014238
		public void Traceback(Lua state, int level = 0)
		{
			this.Traceback(state, null, level);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016043 File Offset: 0x00014243
		public void Traceback(Lua state, string message, int level)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state", "state shouldn't be null");
			}
			NativeMethods.luaL_traceback(this._luaState, state._luaState, message, level);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001606C File Offset: 0x0001426C
		public string TypeName(int index)
		{
			LuaType type = this.Type(index);
			return this.TypeName(type);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016088 File Offset: 0x00014288
		public void Unref(LuaRegistry tableIndex, int reference)
		{
			NativeMethods.luaL_unref(this._luaState, (int)tableIndex, reference);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016097 File Offset: 0x00014297
		public void Warning(string message, bool toContinue)
		{
			NativeMethods.lua_warning(this._luaState, message, toContinue ? 1 : 0);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000160AC File Offset: 0x000142AC
		public void Where(int level)
		{
			NativeMethods.luaL_where(this._luaState, level);
		}

		// Token: 0x04000261 RID: 609
		private IntPtr _luaState;

		// Token: 0x04000262 RID: 610
		private readonly Lua _mainState;
	}
}
