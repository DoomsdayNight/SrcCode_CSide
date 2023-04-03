using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200078F RID: 1935
	public class NKCUIDevConsoleMenu : MonoBehaviour
	{
		// Token: 0x06004C2D RID: 19501 RVA: 0x0016C410 File Offset: 0x0016A610
		public void Init(List<ConsoleMainMenu> mainMenus, NKCUIDevConsoleMenu.ChangeMainMenu callMain)
		{
			using (List<ConsoleMainMenu>.Enumerator enumerator = mainMenus.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ConsoleMainMenu main = enumerator.Current;
					GameObject buttonObj = this.GetButtonObj(this.m_rectMainMenu);
					if (null != buttonObj)
					{
						this.SetUIStringKey(buttonObj, main.strKey, Color.black);
						NKCUIComStateButton component = buttonObj.GetComponent<NKCUIComStateButton>();
						if (null != component)
						{
							NKCUtil.SetBindFunction(component, delegate()
							{
								this.OnClickMainMenu(main.type);
								callMain(main.type);
							});
						}
					}
				}
			}
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0016C4D0 File Offset: 0x0016A6D0
		public void OnClickMainMenu(DEV_CONSOLE_MENU_TYPE newMain)
		{
			foreach (KeyValuePair<DEV_CONSOLE_MENU_TYPE, List<GameObject>> keyValuePair in this.m_dicSubMenus)
			{
				bool bValue = keyValuePair.Key == newMain;
				foreach (GameObject targetObj in keyValuePair.Value)
				{
					NKCUtil.SetGameobjectActive(targetObj, bValue);
				}
			}
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0016C568 File Offset: 0x0016A768
		public void Init(DEV_CONSOLE_MENU_TYPE mainType, List<ConsoleSubMenu> subMenus, NKCUIDevConsoleMenu.ChangeSubBtn callSub, NKCUIDevConsoleMenu.ChangeSubToggle callToggle)
		{
			using (List<ConsoleSubMenu>.Enumerator enumerator = subMenus.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ConsoleSubMenu slot = enumerator.Current;
					if (slot.stype == SUB_MENU_TYPE.BUTTON)
					{
						GameObject buttonObj = this.GetButtonObj(this.m_rectSubMenu);
						this.SetUIStringKey(buttonObj, slot.strKey, slot.bWarning ? Color.red : Color.black);
						this.AddSubMenuObject(mainType, buttonObj);
						NKCUIComStateButton component = buttonObj.GetComponent<NKCUIComStateButton>();
						if (null != component)
						{
							NKCUtil.SetBindFunction(component, delegate()
							{
								Debug.Log(string.Format("[DevConsoleMenu]Select Sub Menu : {0}", slot.type));
								callSub(slot.type);
							});
						}
						NKCUtil.SetGameobjectActive(buttonObj, false);
					}
					if (slot.stype == SUB_MENU_TYPE.CHECK_BOX)
					{
						GameObject toggleObj = this.GetToggleObj(this.m_rectSubMenu);
						this.SetUIStringKey(toggleObj, slot.strKey, Color.white);
						this.AddSubMenuObject(mainType, toggleObj);
						NKCUIComToggle toggle = toggleObj.GetComponent<NKCUIComToggle>();
						if (null != toggle)
						{
							NKCUtil.SetToggleValueChangedDelegate(toggle, delegate(bool x)
							{
								Debug.Log(string.Format("[DevConsoleMenu]ChangeSubMenuToggle {0} - {1}", slot.type, x));
								callToggle(slot.type, x);
								bool bSelect;
								if (this.IsForceSelectToggle(slot.type, out bSelect))
								{
									toggle.Select(bSelect, true, true);
								}
							});
						}
						NKCUtil.SetGameobjectActive(toggleObj, false);
					}
				}
			}
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0016C6FC File Offset: 0x0016A8FC
		private void AddSubMenuObject(DEV_CONSOLE_MENU_TYPE mainType, GameObject obj)
		{
			if (!this.m_dicSubMenus.ContainsKey(mainType))
			{
				this.m_dicSubMenus.Add(mainType, new List<GameObject>
				{
					obj
				});
				return;
			}
			this.m_dicSubMenus[mainType].Add(obj);
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0016C738 File Offset: 0x0016A938
		private bool IsForceSelectToggle(DEV_CONSOLE_SUB_MENU subMenu, out bool val)
		{
			val = false;
			if (subMenu == DEV_CONSOLE_SUB_MENU.SHOW_STRING_ID)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null)
				{
					val = gameOptionData.UseKeyStringView;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0016C768 File Offset: 0x0016A968
		public GameObject GetButtonObj(RectTransform rtParent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_pfbButton);
			if (null != gameObject)
			{
				NKCUtil.SetGameobjectActive(gameObject, true);
				gameObject.transform.SetParent(rtParent, false);
			}
			return gameObject;
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0016C7A0 File Offset: 0x0016A9A0
		public GameObject GetToggleObj(RectTransform rtParent)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_pfbCheckBox);
			if (null != gameObject)
			{
				NKCUtil.SetGameobjectActive(gameObject, true);
				gameObject.transform.SetParent(rtParent, false);
			}
			return gameObject;
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x0016C7D8 File Offset: 0x0016A9D8
		public void SetUIStringKey(GameObject targetObj, string strKey, Color _txtColor)
		{
			if (null == targetObj)
			{
				return;
			}
			if (string.IsNullOrEmpty(strKey))
			{
				return;
			}
			bool flag = strKey.Contains("SI_");
			Text componentInChildren = targetObj.GetComponentInChildren<Text>();
			if (!flag)
			{
				NKCUtil.SetLabelText(componentInChildren, strKey);
			}
			else
			{
				NKCUIComStringChanger componentInChildren2 = targetObj.GetComponentInChildren<NKCUIComStringChanger>();
				Text componentInChildren3 = targetObj.GetComponentInChildren<Text>();
				if (null != componentInChildren2 && componentInChildren3)
				{
					TargetStringInfoToChange item = default(TargetStringInfoToChange);
					item.m_Key = strKey;
					item.m_lbText = componentInChildren3;
					componentInChildren2.m_lstTargetStringInfoToChange.Add(item);
					componentInChildren2.Translate();
				}
			}
			NKCUtil.SetLabelTextColor(componentInChildren, _txtColor);
		}

		// Token: 0x04003BAE RID: 15278
		[Header("Dummy Object")]
		public GameObject m_pfbButton;

		// Token: 0x04003BAF RID: 15279
		public GameObject m_pfbCheckBox;

		// Token: 0x04003BB0 RID: 15280
		[Header("Menu Parent")]
		public RectTransform m_rectMainMenu;

		// Token: 0x04003BB1 RID: 15281
		public RectTransform m_rectSubMenu;

		// Token: 0x04003BB2 RID: 15282
		private Dictionary<DEV_CONSOLE_MENU_TYPE, List<GameObject>> m_dicSubMenus = new Dictionary<DEV_CONSOLE_MENU_TYPE, List<GameObject>>();

		// Token: 0x0200144F RID: 5199
		// (Invoke) Token: 0x0600A85D RID: 43101
		public delegate void ChangeMainMenu(DEV_CONSOLE_MENU_TYPE newMain);

		// Token: 0x02001450 RID: 5200
		// (Invoke) Token: 0x0600A861 RID: 43105
		public delegate void ChangeSubBtn(DEV_CONSOLE_SUB_MENU newMain);

		// Token: 0x02001451 RID: 5201
		// (Invoke) Token: 0x0600A865 RID: 43109
		public delegate void ChangeSubToggle(DEV_CONSOLE_SUB_MENU newMain, bool bSet);
	}
}
