using System;
using System.Collections.Generic;
using NKC.Office;
using NKC.Templet.Office;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Office;
using NKM.Unit;
using UnityEngine;

namespace NKC.Util
{
	// Token: 0x02000810 RID: 2064
	public static class NKCTempletUtility
	{
		// Token: 0x060051CB RID: 20939 RVA: 0x0018D274 File Offset: 0x0018B474
		public static void PostJoin()
		{
			NKMMissionManager.PostJoin();
			NKMAttendanceManager.PostJoin();
			NKCLoginCutSceneManager.PostJoin();
			NKCTempletContainerUtil.InvokePostJoin();
			ShopTabTempletContainer.PostJoin();
			NKCPVPManager.PostJoin();
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0018D294 File Offset: 0x0018B494
		public static void CleanupAllTemplets()
		{
			NKCStringTable.Clear();
			NKCTempletContainerUtil.InvokeDrop();
			NKMOpenTagManager.Drop();
			NKMUnitMissionStepTemplet.Drop();
			NKMPotentialOptionTemplet.Drop();
			ShopTabTempletContainer.Drop();
			NKMOfficeGradeTemplet.Drop();
			NKMUnitExpTableContainer.Drop();
			NKCOfficeFurnitureInteractionTemplet.Drop();
			NKCOfficeUnitInteractionTemplet.Drop();
			NKCShopManager.Drop();
			NKCOfficeManager.Drop();
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0018D2D4 File Offset: 0x0018B4D4
		public static T PickRatio<T>(List<T> lstTargets, Func<T, int> ratioSelector) where T : class
		{
			if (lstTargets == null || lstTargets.Count == 0)
			{
				return default(T);
			}
			if (lstTargets.Count == 1)
			{
				return lstTargets[0];
			}
			int num = 0;
			foreach (T arg in lstTargets)
			{
				num += ratioSelector(arg);
			}
			int num2 = UnityEngine.Random.Range(0, num);
			foreach (T t in lstTargets)
			{
				num2 -= ratioSelector(t);
				if (num2 < 0)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0018D3B0 File Offset: 0x0018B5B0
		public static T PickRatio<T>(IEnumerable<T> lstTargets, Func<T, int> ratioSelector) where T : class
		{
			if (lstTargets == null)
			{
				return default(T);
			}
			int num = 0;
			foreach (T arg in lstTargets)
			{
				num += ratioSelector(arg);
			}
			int num2 = UnityEngine.Random.Range(0, num);
			foreach (T t in lstTargets)
			{
				num2 -= ratioSelector(t);
				if (num2 < 0)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0018D468 File Offset: 0x0018B668
		public static void InvokeJoin()
		{
			NKCTempletContainerUtil.InvokeJoin();
		}
	}
}
