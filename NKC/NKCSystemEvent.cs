using System;
using NKC.UI;
using NKM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x020007B2 RID: 1970
	public class NKCSystemEvent : MonoBehaviour
	{
		// Token: 0x06004DCE RID: 19918 RVA: 0x0017777C File Offset: 0x0017597C
		public static void UI_SCEN_BG_DRAG(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				float num = NKCCamera.GetPosNowX(false) - pointerEventData.delta.x * 10f;
				float num2 = NKCCamera.GetPosNowY(false) - pointerEventData.delta.y * 10f;
				num = Mathf.Clamp(num, -100f, 100f);
				num2 = Mathf.Clamp(num2, -100f, 100f);
				NKCCamera.TrackingPos(1f, num, num2, -1f);
			}
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x001777F8 File Offset: 0x001759F8
		public void UI_TEAM_SHIP_CLICK()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_TEAM || NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_DUNGEON_ATK_READY)
			{
				NKCScenManager.GetScenManager().Get_SCEN_TEAM().UI_TEAM_SHIP_CLICK();
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY && NKCUIDeckViewer.IsInstanceOpen)
			{
				NKCUIDeckViewer.Instance.DeckViewShipClick();
			}
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0017784F File Offset: 0x00175A4F
		public void UI_UNIT_INFO_CLOSE()
		{
			NKCUIUnitInfo.CheckInstanceAndClose();
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x00177856 File Offset: 0x00175A56
		public void UI_BACK_TO_SCEN_HOME()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x00177864 File Offset: 0x00175A64
		public void UI_BASE_HANGER_OPEN()
		{
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x00177866 File Offset: 0x00175A66
		public void UI_BASE_FORGE_OPEN()
		{
			NKCScenManager.GetScenManager().Get_SCEN_BASE().OpenFactory();
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x00177877 File Offset: 0x00175A77
		public void UI_BASE_HANGER_CLOSE()
		{
			NKCUIUnitInfo.CheckInstanceAndClose();
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x00177880 File Offset: 0x00175A80
		public void UI_GAME_CAMERA_DRAG_BEGIN(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG_BEGIN(pointerEventData.position);
			}
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x001778AC File Offset: 0x00175AAC
		public void UI_GAME_CAMERA_DRAG(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG(pointerEventData.delta, pointerEventData.position);
			}
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x001778E0 File Offset: 0x00175AE0
		public void UI_GAME_CAMERA_DRAG_END(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_GAME_CAMERA_DRAG_END(pointerEventData.delta, pointerEventData.position);
			}
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x00177912 File Offset: 0x00175B12
		public static void UI_HUD_DECK_DOWN(int index)
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_DECK_DOWN(index);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x00177924 File Offset: 0x00175B24
		public static void UI_HUD_DECK_UP(int index)
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_DECK_UP(index);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x00177938 File Offset: 0x00175B38
		public static void UI_HUD_DECK_DRAG_BEGIN(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_DECK_DRAG_BEGIN(pointerEventData.pointerDrag.gameObject, pointerEventData.position);
			}
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x00177970 File Offset: 0x00175B70
		public static void UI_HUD_DECK_DRAG(BaseEventData cBaseEventData)
		{
			GameObject selectedObject = cBaseEventData.selectedObject;
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_DECK_DRAG(pointerEventData.pointerDrag.gameObject, pointerEventData.position, pointerEventData.position);
			}
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x001779B4 File Offset: 0x00175BB4
		public static void UI_HUD_DECK_DRAG_END(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_DECK_DRAG_END(pointerEventData.pointerDrag.gameObject, pointerEventData.position);
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x001779EC File Offset: 0x00175BEC
		public static void UI_HUD_SHIP_SKILL_DECK_DOWN(int index, BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_SHIP_SKILL_DECK_DOWN(index, pointerEventData.position);
			}
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00177A19 File Offset: 0x00175C19
		public static void UI_HUD_SHIP_SKILL_DECK_UP(int index)
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_SHIP_SKILL_DECK_UP(index);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00177A2C File Offset: 0x00175C2C
		public static void UI_HUD_SHIP_SKILL_DECK_DRAG_BEGIN(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_SHIP_SKILL_DECK_DRAG_BEGIN(pointerEventData.pointerDrag.gameObject, pointerEventData.position);
			}
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00177A64 File Offset: 0x00175C64
		public static void UI_HUD_SHIP_SKILL_DECK_DRAG(BaseEventData cBaseEventData)
		{
			GameObject selectedObject = cBaseEventData.selectedObject;
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_SHIP_SKILL_DECK_DRAG(pointerEventData.pointerDrag.gameObject, pointerEventData.position);
			}
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00177AA4 File Offset: 0x00175CA4
		public static void UI_HUD_SHIP_SKILL_DECK_DRAG_END(BaseEventData cBaseEventData)
		{
			PointerEventData pointerEventData = cBaseEventData as PointerEventData;
			if (pointerEventData != null)
			{
				NKCScenManager.GetScenManager().GetGameClient().UI_HUD_SHIP_SKILL_DECK_DRAG_END(pointerEventData.pointerDrag.gameObject, pointerEventData.position);
			}
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x00177ADB File Offset: 0x00175CDB
		public void UI_HUD_AUTO_RESPAWN()
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_AUTO_RESPAWN_TOGGLE();
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00177AEC File Offset: 0x00175CEC
		public void UI_HUD_ACTION_CAMERA()
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_ACTION_CAMERA_TOGGLE();
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00177AFD File Offset: 0x00175CFD
		public void UI_HUD_TRACK_CAMERA()
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_HUD_TRACK_CAMERA_TOGGLE();
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00177B0E File Offset: 0x00175D0E
		public void UI_GAME_NO_HP_DMG_MODE_TEAM_A()
		{
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x00177B10 File Offset: 0x00175D10
		public void UI_GAME_NO_HP_DMG_MODE_TEAM_B()
		{
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00177B12 File Offset: 0x00175D12
		public void UI_GAME_AI_DISABLE_TEAM_A()
		{
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00177B14 File Offset: 0x00175D14
		public void UI_GAME_AI_DISABLE_TEAM_B()
		{
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00177B16 File Offset: 0x00175D16
		public void UI_GAME_ALL_KILL()
		{
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x00177B18 File Offset: 0x00175D18
		public void UI_GAME_ALL_KILL_ENEMY()
		{
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00177B1A File Offset: 0x00175D1A
		public void UI_GAME_DEV_MENU()
		{
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00177B1C File Offset: 0x00175D1C
		public void UI_GAME_DEV_MENU_CLOSE()
		{
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00177B1E File Offset: 0x00175D1E
		public void UI_GAME_DEV_MENU_UNIT_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x00177B20 File Offset: 0x00175D20
		public void UI_GAME_DEV_MENU_MONSTER_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x00177B22 File Offset: 0x00175D22
		public void UI_GAME_DEV_MENU_SHIP_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x00177B24 File Offset: 0x00175D24
		public void UI_GAME_DEV_MENU_DUNGEON_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x00177B26 File Offset: 0x00175D26
		public void UI_GAME_DEV_MENU_OPER_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00177B28 File Offset: 0x00175D28
		public void UI_GAME_DEV_MENU_DUNGEON_LIST_CHANGED(int optionIndex)
		{
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x00177B2A File Offset: 0x00175D2A
		public void UI_GAME_DEV_MENU_DUNGEON_LIST_RELOAD()
		{
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00177B2C File Offset: 0x00175D2C
		public void UI_GAME_DEV_MENU_SHIP_CHANGE()
		{
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x00177B2E File Offset: 0x00175D2E
		public void UI_GAME_PAUSE()
		{
			NKCScenManager.GetScenManager().GetGameClient().UI_GAME_PAUSE();
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00177B3F File Offset: 0x00175D3F
		public void UI_GAME_DEV_FRAME_MOVE()
		{
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00177B41 File Offset: 0x00175D41
		public void UI_GAME_DEV_SKILL_NORMAL()
		{
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x00177B43 File Offset: 0x00175D43
		public void UI_GAME_DEV_SKILL_NORMAL_ENEMY()
		{
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00177B45 File Offset: 0x00175D45
		public void UI_GAME_DEV_SKILL_SPECIAL()
		{
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00177B47 File Offset: 0x00175D47
		public void UI_GAME_DEV_SKILL_SPECIAL_ENEMY()
		{
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00177B49 File Offset: 0x00175D49
		public void UI_GAME_DEV_MONSTER_AUTO_RESAPWN_TOGGLE(bool bSet)
		{
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00177B4B File Offset: 0x00175D4B
		public void UI_GAME_DEV_UNIT_REAL_TIME_SPAWN()
		{
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x00177B4D File Offset: 0x00175D4D
		public void UI_GAME_DEV_UNIT_REAL_TIME_SPAWN_ENEMY()
		{
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00177B4F File Offset: 0x00175D4F
		public void UI_GAME_DEV_RESET()
		{
		}
	}
}
