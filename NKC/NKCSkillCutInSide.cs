using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200067B RID: 1659
	public class NKCSkillCutInSide
	{
		// Token: 0x060034FA RID: 13562 RVA: 0x0010B62C File Offset: 0x0010982C
		public void Load(List<NKCASEffect> listEffectLoadTemp, string midEffectName, string frontEffectName)
		{
			this.m_MidEffectName = midEffectName;
			this.m_FrontEffectName = frontEffectName;
			NKCASEffect item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, this.m_MidEffectName, this.m_MidEffectName, true);
			listEffectLoadTemp.Add(item);
			item = (NKCASEffect)NKCScenManager.GetScenManager().GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect, this.m_FrontEffectName, this.m_FrontEffectName, true);
			listEffectLoadTemp.Add(item);
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0010B6A0 File Offset: 0x001098A0
		public void Play(NKCEffectManager cNKCEffectManager, Sprite faceSprite, string unitName, string skillName)
		{
			if (this.m_SkillCutInMID == null)
			{
				this.m_SkillCutInMID = cNKCEffectManager.UseEffect(0, this.m_MidEffectName, this.m_MidEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUM_GAME_BATTLE_EFFECT, -1f, -1f, -1f, true, 1f, 0f, 0f, 0f, false, -1f, false, "", false, false, "BASE", 1f, true, false, 0f, -1f, false);
				this.m_SkillCutInFRONT = cNKCEffectManager.UseEffect(0, this.m_FrontEffectName, this.m_FrontEffectName, NKM_EFFECT_PARENT_TYPE.NEPT_NUF_AFTER_HUD_EFFECT, -1f, -1f, -1f, true, 1f, 0f, 0f, 0f, false, -1f, false, "", false, false, "BASE", 1f, true, false, 0f, -1f, false);
				if (this.m_SkillCutInMID != null)
				{
					GameObject gameObject = this.m_SkillCutInMID.m_EffectInstant.m_Instant.transform.Find("RENDER_GROUP/RENDER_CAM_CUTIN_PORTRAIT/SPRITE_PORTRAIT").gameObject;
					this.m_SkillCutInSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
				}
				if (this.m_SkillCutInFRONT != null)
				{
					GameObject gameObject2 = this.m_SkillCutInFRONT.m_EffectInstant.m_Instant.transform.Find("DESC/POS_TEXT_CHA_NAME/OFFSET_TEXT_CHA_NAME/TEXT_CHA_NAME").gameObject;
					this.m_SkillCutInTextUnitName = gameObject2.GetComponent<Text>();
					gameObject2 = this.m_SkillCutInFRONT.m_EffectInstant.m_Instant.transform.Find("DESC/POS_TEXT_SPELL_NAME/OFFSET_TEXT_SPELL_NAME/TEXT_SPELL_NAME").gameObject;
					this.m_SkillCutInTextSkillName = gameObject2.GetComponent<Text>();
				}
			}
			cNKCEffectManager.StopCutInEffect();
			if (this.m_SkillCutInMID != null)
			{
				this.m_SkillCutInTextUnitName.text = unitName;
				this.m_SkillCutInTextSkillName.text = skillName;
				this.m_SkillCutInSpriteRenderer.sprite = faceSprite;
				this.m_SkillCutInMID.ReStart();
			}
			if (this.m_SkillCutInFRONT != null)
			{
				this.m_SkillCutInFRONT.ReStart();
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0010B872 File Offset: 0x00109A72
		public void Stop()
		{
			if (this.m_SkillCutInMID != null)
			{
				this.m_SkillCutInMID.Stop(false);
				NKCASEffect skillCutInFRONT = this.m_SkillCutInFRONT;
				if (skillCutInFRONT == null)
				{
					return;
				}
				skillCutInFRONT.Stop(false);
			}
		}

		// Token: 0x040032FA RID: 13050
		public string m_MidEffectName;

		// Token: 0x040032FB RID: 13051
		public string m_FrontEffectName;

		// Token: 0x040032FC RID: 13052
		public NKCASEffect m_SkillCutInMID;

		// Token: 0x040032FD RID: 13053
		public NKCASEffect m_SkillCutInFRONT;

		// Token: 0x040032FE RID: 13054
		public SpriteRenderer m_SkillCutInSpriteRenderer;

		// Token: 0x040032FF RID: 13055
		public Text m_SkillCutInTextUnitName;

		// Token: 0x04003300 RID: 13056
		public Text m_SkillCutInTextSkillName;
	}
}
