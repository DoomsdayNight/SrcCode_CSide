using System;
using UnityEngine;

namespace NKC.Util
{
	// Token: 0x02000811 RID: 2065
	public static class NKCUIUtility
	{
		// Token: 0x060051D0 RID: 20944 RVA: 0x0018D470 File Offset: 0x0018B670
		public static Canvas FindCanvas(Transform t)
		{
			Transform transform = t;
			while (transform != null)
			{
				Canvas component = transform.GetComponent<Canvas>();
				if (component != null)
				{
					return component;
				}
				transform = transform.parent;
			}
			return null;
		}
	}
}
