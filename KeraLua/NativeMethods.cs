using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000093 RID: 147
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x06000553 RID: 1363
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_absindex(IntPtr luaState, int idx);

		// Token: 0x06000554 RID: 1364
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_arith(IntPtr luaState, int op);

		// Token: 0x06000555 RID: 1365
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_atpanic(IntPtr luaState, IntPtr panicf);

		// Token: 0x06000556 RID: 1366
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_callk(IntPtr luaState, int nargs, int nresults, IntPtr ctx, IntPtr k);

		// Token: 0x06000557 RID: 1367
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_checkstack(IntPtr luaState, int extra);

		// Token: 0x06000558 RID: 1368
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_close(IntPtr luaState);

		// Token: 0x06000559 RID: 1369
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_compare(IntPtr luaState, int index1, int index2, int op);

		// Token: 0x0600055A RID: 1370
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_concat(IntPtr luaState, int n);

		// Token: 0x0600055B RID: 1371
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_copy(IntPtr luaState, int fromIndex, int toIndex);

		// Token: 0x0600055C RID: 1372
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_createtable(IntPtr luaState, int elements, int records);

		// Token: 0x0600055D RID: 1373
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_dump(IntPtr luaState, IntPtr writer, IntPtr data, int strip);

		// Token: 0x0600055E RID: 1374
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_error(IntPtr luaState);

		// Token: 0x0600055F RID: 1375
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gc(IntPtr luaState, int what, int data);

		// Token: 0x06000560 RID: 1376
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gc(IntPtr luaState, int what, int data, int data2);

		// Token: 0x06000561 RID: 1377
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_getallocf(IntPtr luaState, ref IntPtr ud);

		// Token: 0x06000562 RID: 1378
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int lua_getfield(IntPtr luaState, int index, string k);

		// Token: 0x06000563 RID: 1379
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int lua_getglobal(IntPtr luaState, string name);

		// Token: 0x06000564 RID: 1380
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_gethook(IntPtr luaState);

		// Token: 0x06000565 RID: 1381
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gethookcount(IntPtr luaState);

		// Token: 0x06000566 RID: 1382
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gethookmask(IntPtr luaState);

		// Token: 0x06000567 RID: 1383
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_geti(IntPtr luaState, int index, long i);

		// Token: 0x06000568 RID: 1384
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int lua_getinfo(IntPtr luaState, string what, IntPtr ar);

		// Token: 0x06000569 RID: 1385
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_getiuservalue(IntPtr luaState, int idx, int n);

		// Token: 0x0600056A RID: 1386
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr lua_getlocal(IntPtr luaState, IntPtr ar, int n);

		// Token: 0x0600056B RID: 1387
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_getmetatable(IntPtr luaState, int index);

		// Token: 0x0600056C RID: 1388
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_getstack(IntPtr luaState, int level, IntPtr n);

		// Token: 0x0600056D RID: 1389
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gettable(IntPtr luaState, int index);

		// Token: 0x0600056E RID: 1390
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_gettop(IntPtr luaState);

		// Token: 0x0600056F RID: 1391
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr lua_getupvalue(IntPtr luaState, int funcIndex, int n);

		// Token: 0x06000570 RID: 1392
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_iscfunction(IntPtr luaState, int index);

		// Token: 0x06000571 RID: 1393
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_isinteger(IntPtr luaState, int index);

		// Token: 0x06000572 RID: 1394
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_isnumber(IntPtr luaState, int index);

		// Token: 0x06000573 RID: 1395
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_isstring(IntPtr luaState, int index);

		// Token: 0x06000574 RID: 1396
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_isuserdata(IntPtr luaState, int index);

		// Token: 0x06000575 RID: 1397
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_isyieldable(IntPtr luaState);

		// Token: 0x06000576 RID: 1398
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_len(IntPtr luaState, int index);

		// Token: 0x06000577 RID: 1399
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int lua_load(IntPtr luaState, IntPtr reader, IntPtr data, string chunkName, string mode);

		// Token: 0x06000578 RID: 1400
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_newstate(IntPtr allocFunction, IntPtr ud);

		// Token: 0x06000579 RID: 1401
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_newthread(IntPtr luaState);

		// Token: 0x0600057A RID: 1402
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_newuserdatauv(IntPtr luaState, UIntPtr size, int nuvalue);

		// Token: 0x0600057B RID: 1403
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_next(IntPtr luaState, int index);

		// Token: 0x0600057C RID: 1404
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_pcallk(IntPtr luaState, int nargs, int nresults, int errorfunc, IntPtr ctx, IntPtr k);

		// Token: 0x0600057D RID: 1405
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushboolean(IntPtr luaState, int value);

		// Token: 0x0600057E RID: 1406
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushcclosure(IntPtr luaState, IntPtr f, int n);

		// Token: 0x0600057F RID: 1407
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushinteger(IntPtr luaState, long n);

		// Token: 0x06000580 RID: 1408
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushlightuserdata(IntPtr luaState, IntPtr udata);

		// Token: 0x06000581 RID: 1409
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_pushlstring(IntPtr luaState, byte[] s, UIntPtr len);

		// Token: 0x06000582 RID: 1410
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushnil(IntPtr luaState);

		// Token: 0x06000583 RID: 1411
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushnumber(IntPtr luaState, double number);

		// Token: 0x06000584 RID: 1412
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_pushthread(IntPtr luaState);

		// Token: 0x06000585 RID: 1413
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_pushvalue(IntPtr luaState, int index);

		// Token: 0x06000586 RID: 1414
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_rawequal(IntPtr luaState, int index1, int index2);

		// Token: 0x06000587 RID: 1415
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_rawget(IntPtr luaState, int index);

		// Token: 0x06000588 RID: 1416
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_rawgeti(IntPtr luaState, int index, long n);

		// Token: 0x06000589 RID: 1417
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_rawgetp(IntPtr luaState, int index, IntPtr p);

		// Token: 0x0600058A RID: 1418
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern UIntPtr lua_rawlen(IntPtr luaState, int index);

		// Token: 0x0600058B RID: 1419
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_rawset(IntPtr luaState, int index);

		// Token: 0x0600058C RID: 1420
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_rawseti(IntPtr luaState, int index, long i);

		// Token: 0x0600058D RID: 1421
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_rawsetp(IntPtr luaState, int index, IntPtr p);

		// Token: 0x0600058E RID: 1422
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_resetthread(IntPtr luaState);

		// Token: 0x0600058F RID: 1423
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_resume(IntPtr luaState, IntPtr from, int nargs, out int results);

		// Token: 0x06000590 RID: 1424
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_rotate(IntPtr luaState, int index, int n);

		// Token: 0x06000591 RID: 1425
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_setallocf(IntPtr luaState, IntPtr f, IntPtr ud);

		// Token: 0x06000592 RID: 1426
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void lua_setfield(IntPtr luaState, int index, string key);

		// Token: 0x06000593 RID: 1427
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void lua_setglobal(IntPtr luaState, string key);

		// Token: 0x06000594 RID: 1428
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_sethook(IntPtr luaState, IntPtr f, int mask, int count);

		// Token: 0x06000595 RID: 1429
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_seti(IntPtr luaState, int index, long n);

		// Token: 0x06000596 RID: 1430
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_setiuservalue(IntPtr luaState, int index, int n);

		// Token: 0x06000597 RID: 1431
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr lua_setlocal(IntPtr luaState, IntPtr ar, int n);

		// Token: 0x06000598 RID: 1432
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_setmetatable(IntPtr luaState, int objIndex);

		// Token: 0x06000599 RID: 1433
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_settable(IntPtr luaState, int index);

		// Token: 0x0600059A RID: 1434
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_settop(IntPtr luaState, int newTop);

		// Token: 0x0600059B RID: 1435
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_setupvalue(IntPtr luaState, int funcIndex, int n);

		// Token: 0x0600059C RID: 1436
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_setwarnf(IntPtr luaState, IntPtr warningFunctionPtr, IntPtr ud);

		// Token: 0x0600059D RID: 1437
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_status(IntPtr luaState);

		// Token: 0x0600059E RID: 1438
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern UIntPtr lua_stringtonumber(IntPtr luaState, string s);

		// Token: 0x0600059F RID: 1439
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_toboolean(IntPtr luaState, int index);

		// Token: 0x060005A0 RID: 1440
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_tocfunction(IntPtr luaState, int index);

		// Token: 0x060005A1 RID: 1441
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_toclose(IntPtr luaState, int index);

		// Token: 0x060005A2 RID: 1442
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long lua_tointegerx(IntPtr luaState, int index, out int isNum);

		// Token: 0x060005A3 RID: 1443
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_tolstring(IntPtr luaState, int index, out UIntPtr strLen);

		// Token: 0x060005A4 RID: 1444
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double lua_tonumberx(IntPtr luaState, int index, out int isNum);

		// Token: 0x060005A5 RID: 1445
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_topointer(IntPtr luaState, int index);

		// Token: 0x060005A6 RID: 1446
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_tothread(IntPtr luaState, int index);

		// Token: 0x060005A7 RID: 1447
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_touserdata(IntPtr luaState, int index);

		// Token: 0x060005A8 RID: 1448
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_type(IntPtr luaState, int index);

		// Token: 0x060005A9 RID: 1449
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr lua_typename(IntPtr luaState, int type);

		// Token: 0x060005AA RID: 1450
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr lua_upvalueid(IntPtr luaState, int funcIndex, int n);

		// Token: 0x060005AB RID: 1451
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_upvaluejoin(IntPtr luaState, int funcIndex1, int n1, int funcIndex2, int n2);

		// Token: 0x060005AC RID: 1452
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double lua_version(IntPtr luaState);

		// Token: 0x060005AD RID: 1453
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void lua_warning(IntPtr luaState, string msg, int tocont);

		// Token: 0x060005AE RID: 1454
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void lua_xmove(IntPtr from, IntPtr to, int n);

		// Token: 0x060005AF RID: 1455
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int lua_yieldk(IntPtr luaState, int nresults, IntPtr ctx, IntPtr k);

		// Token: 0x060005B0 RID: 1456
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_argerror(IntPtr luaState, int arg, string message);

		// Token: 0x060005B1 RID: 1457
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_callmeta(IntPtr luaState, int obj, string e);

		// Token: 0x060005B2 RID: 1458
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_checkany(IntPtr luaState, int arg);

		// Token: 0x060005B3 RID: 1459
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long luaL_checkinteger(IntPtr luaState, int arg);

		// Token: 0x060005B4 RID: 1460
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr luaL_checklstring(IntPtr luaState, int arg, out UIntPtr len);

		// Token: 0x060005B5 RID: 1461
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double luaL_checknumber(IntPtr luaState, int arg);

		// Token: 0x060005B6 RID: 1462
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_checkoption(IntPtr luaState, int arg, string def, string[] list);

		// Token: 0x060005B7 RID: 1463
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void luaL_checkstack(IntPtr luaState, int sz, string message);

		// Token: 0x060005B8 RID: 1464
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_checktype(IntPtr luaState, int arg, int type);

		// Token: 0x060005B9 RID: 1465
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr luaL_checkudata(IntPtr luaState, int arg, string tName);

		// Token: 0x060005BA RID: 1466
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkversion_(IntPtr luaState, double ver, UIntPtr sz);

		// Token: 0x060005BB RID: 1467
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_error(IntPtr luaState, string message);

		// Token: 0x060005BC RID: 1468
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int luaL_execresult(IntPtr luaState, int stat);

		// Token: 0x060005BD RID: 1469
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_fileresult(IntPtr luaState, int stat, string fileName);

		// Token: 0x060005BE RID: 1470
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_getmetafield(IntPtr luaState, int obj, string e);

		// Token: 0x060005BF RID: 1471
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_getsubtable(IntPtr luaState, int index, string name);

		// Token: 0x060005C0 RID: 1472
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long luaL_len(IntPtr luaState, int index);

		// Token: 0x060005C1 RID: 1473
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_loadbufferx(IntPtr luaState, byte[] buff, UIntPtr sz, string name, string mode);

		// Token: 0x060005C2 RID: 1474
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_loadfilex(IntPtr luaState, string name, string mode);

		// Token: 0x060005C3 RID: 1475
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_newmetatable(IntPtr luaState, string name);

		// Token: 0x060005C4 RID: 1476
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr luaL_newstate();

		// Token: 0x060005C5 RID: 1477
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_openlibs(IntPtr luaState);

		// Token: 0x060005C6 RID: 1478
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long luaL_optinteger(IntPtr luaState, int arg, long d);

		// Token: 0x060005C7 RID: 1479
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double luaL_optnumber(IntPtr luaState, int arg, double d);

		// Token: 0x060005C8 RID: 1480
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int luaL_ref(IntPtr luaState, int registryIndex);

		// Token: 0x060005C9 RID: 1481
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void luaL_requiref(IntPtr luaState, string moduleName, IntPtr openFunction, int global);

		// Token: 0x060005CA RID: 1482
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_setfuncs(IntPtr luaState, [In] LuaRegister[] luaReg, int numUp);

		// Token: 0x060005CB RID: 1483
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern void luaL_setmetatable(IntPtr luaState, string tName);

		// Token: 0x060005CC RID: 1484
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr luaL_testudata(IntPtr luaState, int arg, string tName);

		// Token: 0x060005CD RID: 1485
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr luaL_tolstring(IntPtr luaState, int index, out UIntPtr len);

		// Token: 0x060005CE RID: 1486
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern IntPtr luaL_traceback(IntPtr luaState, IntPtr luaState2, string message, int level);

		// Token: 0x060005CF RID: 1487
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		internal static extern int luaL_typeerror(IntPtr luaState, int arg, string typeName);

		// Token: 0x060005D0 RID: 1488
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_unref(IntPtr luaState, int registryIndex, int reference);

		// Token: 0x060005D1 RID: 1489
		[DllImport("lua54", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void luaL_where(IntPtr luaState, int level);

		// Token: 0x040002B9 RID: 697
		private const string LuaLibraryName = "lua54";
	}
}
