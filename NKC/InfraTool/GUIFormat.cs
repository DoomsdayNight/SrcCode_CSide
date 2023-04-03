using System;
using UnityEngine;

namespace NKC.InfraTool
{
	// Token: 0x02000892 RID: 2194
	public class GUIFormat
	{
		// Token: 0x0600575B RID: 22363 RVA: 0x001A3AA4 File Offset: 0x001A1CA4
		public static void BeginArea(Rect rect, Action action)
		{
			GUIStyle style = new GUIStyle(GUI.skin.box)
			{
				alignment = TextAnchor.MiddleLeft
			};
			GUILayout.BeginArea(rect, style);
			if (action != null)
			{
				action();
			}
			GUILayout.EndArea();
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x001A3ADD File Offset: 0x001A1CDD
		public static void Vertical(Action action, bool space = true)
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			if (action != null)
			{
				action();
			}
			GUILayout.EndVertical();
			if (space)
			{
				GUILayout.Space(2f);
			}
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001A3B04 File Offset: 0x001A1D04
		public static void Horizontal(Action action, bool space = true)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (action != null)
			{
				action();
			}
			GUILayout.EndHorizontal();
			if (space)
			{
				GUILayout.Space(2f);
			}
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x001A3B2C File Offset: 0x001A1D2C
		public static void ScrollView(Action action, float width, float height, bool space = true)
		{
			GUIStyle style = new GUIStyle(GUI.skin.box)
			{
				alignment = TextAnchor.UpperLeft
			};
			GUIFormat._scrollViewVector = GUILayout.BeginScrollView(GUIFormat._scrollViewVector, style, new GUILayoutOption[]
			{
				GUILayout.Width(width - 10f),
				GUILayout.Height(height / 2f - 100f)
			});
			if (action != null)
			{
				action();
			}
			GUILayout.EndScrollView();
			if (space)
			{
				GUILayout.Space(2f);
			}
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x001A3BA8 File Offset: 0x001A1DA8
		public static void Label(string str, GUIStyle style = null, int size = 30)
		{
			GUIStyle guistyle = (style != null) ? new GUIStyle(style) : new GUIStyle();
			guistyle.alignment = TextAnchor.UpperLeft;
			guistyle.fontSize = size;
			guistyle.normal.textColor = Color.white;
			GUILayout.Label(str, guistyle, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x001A3BF0 File Offset: 0x001A1DF0
		public static void Box(string str, int editorHeight, int boxGab)
		{
			GUILayout.Box(str, new GUIStyle(GUI.skin.box)
			{
				alignment = TextAnchor.UpperLeft,
				fontSize = 30,
				normal = 
				{
					textColor = Color.white
				}
			}, new GUILayoutOption[]
			{
				GUILayout.Height((float)editorHeight - GUIFormat.LastRect.y - (float)boxGab)
			});
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x001A3C54 File Offset: 0x001A1E54
		public static string TextField(string str, GUIStyle style = null, int size = 30)
		{
			GUIStyle guistyle = (style != null) ? new GUIStyle(style) : new GUIStyle();
			guistyle.alignment = TextAnchor.MiddleLeft;
			guistyle.fontSize = size;
			guistyle.normal.textColor = Color.white;
			return GUILayout.TextField(str, guistyle, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x001A3C9C File Offset: 0x001A1E9C
		public static bool Button(string str, TextAnchor anchor = TextAnchor.MiddleCenter, int size = 30, int buttonHeight = 50)
		{
			GUIStyle style = new GUIStyle(GUI.skin.button)
			{
				alignment = anchor,
				fontSize = size,
				normal = 
				{
					textColor = Color.white
				}
			};
			return GUILayout.Button(str, style, new GUILayoutOption[]
			{
				GUILayout.Height((float)buttonHeight)
			});
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001A3CF0 File Offset: 0x001A1EF0
		public static bool Toggle(bool boolean, string str)
		{
			GUIStyle style = new GUIStyle(GUI.skin.button)
			{
				alignment = TextAnchor.MiddleCenter,
				fontSize = GUIFormat.buttonFontSize,
				normal = 
				{
					textColor = Color.white
				}
			};
			return GUILayout.Toggle(boolean, str, style, new GUILayoutOption[]
			{
				GUILayout.Height(20f)
			});
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06005764 RID: 22372 RVA: 0x001A3D4C File Offset: 0x001A1F4C
		public static Rect LastRect
		{
			get
			{
				Rect lastRect = GUILayoutUtility.GetLastRect();
				if (lastRect.x == 0f || lastRect.y == 0f)
				{
					return GUIFormat._lastRect;
				}
				GUIFormat._lastRect = lastRect;
				return GUIFormat._lastRect;
			}
		}

		// Token: 0x04004530 RID: 17712
		private static Vector2 _scrollViewVector;

		// Token: 0x04004531 RID: 17713
		public static int buttonFontSize = 30;

		// Token: 0x04004532 RID: 17714
		private static Rect _lastRect;
	}
}
