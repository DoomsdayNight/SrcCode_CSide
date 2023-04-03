using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x020004B8 RID: 1208
	public enum NKM_STAT_TYPE : short
	{
		// Token: 0x04002265 RID: 8805
		[CountryDescription("없음", CountryCode.KOR)]
		NST_RANDOM = -1,
		// Token: 0x04002266 RID: 8806
		[CountryDescription("체력", CountryCode.KOR)]
		NST_HP,
		// Token: 0x04002267 RID: 8807
		[CountryDescription("공격력", CountryCode.KOR)]
		NST_ATK,
		// Token: 0x04002268 RID: 8808
		[CountryDescription("방어력", CountryCode.KOR)]
		NST_DEF,
		// Token: 0x04002269 RID: 8809
		[CountryDescription("치명타율", CountryCode.KOR)]
		NST_CRITICAL,
		// Token: 0x0400226A RID: 8810
		[CountryDescription("명중율", CountryCode.KOR)]
		NST_HIT,
		// Token: 0x0400226B RID: 8811
		[CountryDescription("회피율", CountryCode.KOR)]
		NST_EVADE,
		// Token: 0x0400226C RID: 8812
		[CountryDescription("체력 회복율", CountryCode.KOR)]
		NST_HP_REGEN_RATE,
		// Token: 0x0400226D RID: 8813
		[CountryDescription("치명타 피해 증가율", CountryCode.KOR)]
		NST_CRITICAL_DAMAGE_RATE,
		// Token: 0x0400226E RID: 8814
		[CountryDescription("치명타 저항율", CountryCode.KOR)]
		NST_CRITICAL_RESIST,
		// Token: 0x0400226F RID: 8815
		[CountryDescription("치명타 피해 증가 저항율", CountryCode.KOR)]
		NST_CRITICAL_DAMAGE_RESIST_RATE,
		// Token: 0x04002270 RID: 8816
		[CountryDescription("피해 감소율", CountryCode.KOR)]
		NST_DAMAGE_REDUCE_RATE,
		// Token: 0x04002271 RID: 8817
		[CountryDescription("이동속도 증가율", CountryCode.KOR)]
		NST_MOVE_SPEED_RATE,
		// Token: 0x04002272 RID: 8818
		[CountryDescription("공격속도 증가율", CountryCode.KOR)]
		NST_ATTACK_SPEED_RATE,
		// Token: 0x04002273 RID: 8819
		[CountryDescription("스킬 쿨타임 감소율", CountryCode.KOR)]
		NST_SKILL_COOL_TIME_REDUCE_RATE,
		// Token: 0x04002274 RID: 8820
		[CountryDescription("상태이상 저항율", CountryCode.KOR)]
		NST_CC_RESIST_RATE,
		// Token: 0x04002275 RID: 8821
		[CountryDescription("vs카운터에게 주는 피해 증가", CountryCode.KOR)]
		NST_UNIT_TYPE_COUNTER_DAMAGE_RATE,
		// Token: 0x04002276 RID: 8822
		[CountryDescription("vs카운터에게 받는 피해 감소", CountryCode.KOR)]
		NST_UNIT_TYPE_COUNTER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002277 RID: 8823
		[CountryDescription("vs솔저에게 주는 피해 증가", CountryCode.KOR)]
		NST_UNIT_TYPE_SOLDIER_DAMAGE_RATE,
		// Token: 0x04002278 RID: 8824
		[CountryDescription("vs솔저에게 받는 피해 감소", CountryCode.KOR)]
		NST_UNIT_TYPE_SOLDIER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002279 RID: 8825
		[CountryDescription("vs메카닉에게 주는 피해 증가", CountryCode.KOR)]
		NST_UNIT_TYPE_MECHANIC_DAMAGE_RATE,
		// Token: 0x0400227A RID: 8826
		[CountryDescription("vs메카닉에게 받는 피해 감소", CountryCode.KOR)]
		NST_UNIT_TYPE_MECHANIC_DAMAGE_REDUCE_RATE,
		// Token: 0x0400227B RID: 8827
		[CountryDescription("vs스트라이커에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_STRIKER_DAMAGE_RATE,
		// Token: 0x0400227C RID: 8828
		[CountryDescription("vs스트라이커에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_STRIKER_DAMAGE_REDUCE_RATE,
		// Token: 0x0400227D RID: 8829
		[CountryDescription("vs레인저에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_RANGER_DAMAGE_RATE,
		// Token: 0x0400227E RID: 8830
		[CountryDescription("vs레인저에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_RANGER_DAMAGE_REDUCE_RATE,
		// Token: 0x0400227F RID: 8831
		[CountryDescription("vs스나이퍼에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_SNIPER_DAMAGE_RATE,
		// Token: 0x04002280 RID: 8832
		[CountryDescription("vs스나이퍼에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_SNIPER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002281 RID: 8833
		[CountryDescription("vs디펜더에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_DEFFENDER_DAMAGE_RATE,
		// Token: 0x04002282 RID: 8834
		[CountryDescription("vs디펜더에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_DEFFENDER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002283 RID: 8835
		[CountryDescription("vs서포터에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_SUPPOERTER_DAMAGE_RATE,
		// Token: 0x04002284 RID: 8836
		[CountryDescription("vs서포터에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_SUPPOERTER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002285 RID: 8837
		[CountryDescription("vs시즈에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_SIEGE_DAMAGE_RATE,
		// Token: 0x04002286 RID: 8838
		[CountryDescription("vs시즈에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_SIEGE_DAMAGE_REDUCE_RATE,
		// Token: 0x04002287 RID: 8839
		[CountryDescription("vs타워에게 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_TOWER_DAMAGE_RATE,
		// Token: 0x04002288 RID: 8840
		[CountryDescription("vs타워에게 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_TOWER_DAMAGE_REDUCE_RATE,
		// Token: 0x04002289 RID: 8841
		[CountryDescription("vs지상유닛에게 주는 피해 증가", CountryCode.KOR)]
		NST_MOVE_TYPE_LAND_DAMAGE_RATE,
		// Token: 0x0400228A RID: 8842
		[CountryDescription("vs지상유닛에게 받는 피해 감소", CountryCode.KOR)]
		NST_MOVE_TYPE_LAND_DAMAGE_REDUCE_RATE,
		// Token: 0x0400228B RID: 8843
		[CountryDescription("vs공중유닛에게 주는 피해 증가", CountryCode.KOR)]
		NST_MOVE_TYPE_AIR_DAMAGE_RATE,
		// Token: 0x0400228C RID: 8844
		[CountryDescription("vs공중유닛에게 받는 피해 감소", CountryCode.KOR)]
		NST_MOVE_TYPE_AIR_DAMAGE_REDUCE_RATE,
		// Token: 0x0400228D RID: 8845
		[CountryDescription("장거리 공격으로 받는 피해 감소", CountryCode.KOR)]
		NST_LONG_RANGE_DAMAGE_REDUCE_RATE,
		// Token: 0x0400228E RID: 8846
		[CountryDescription("장거리 공격으로 주는 피해 추가", CountryCode.KOR)]
		NST_LONG_RANGE_DAMAGE_RATE,
		// Token: 0x0400228F RID: 8847
		[CountryDescription("단거리 공격으로 받는 피해 감소", CountryCode.KOR)]
		NST_SHORT_RANGE_DAMAGE_REDUCE_RATE,
		// Token: 0x04002290 RID: 8848
		[CountryDescription("단거리 공격으로 주는 피해 추가", CountryCode.KOR)]
		NST_SHORT_RANGE_DAMAGE_RATE,
		// Token: 0x04002291 RID: 8849
		[CountryDescription("힐 감소율", CountryCode.KOR)]
		NST_HEAL_REDUCE_RATE,
		// Token: 0x04002292 RID: 8850
		[CountryDescription("방어 관통율", CountryCode.KOR)]
		NST_DEF_PENETRATE_RATE,
		// Token: 0x04002293 RID: 8851
		[CountryDescription("방어막 강화율", CountryCode.KOR)]
		NST_BARRIER_REINFORCE_RATE,
		// Token: 0x04002294 RID: 8852
		[CountryDescription("스킬 데미지 강화율", CountryCode.KOR)]
		NST_SKILL_DAMAGE_RATE,
		// Token: 0x04002295 RID: 8853
		[CountryDescription("스킬 데미지 감소율", CountryCode.KOR)]
		NST_SKILL_DAMAGE_REDUCE_RATE,
		// Token: 0x04002296 RID: 8854
		[CountryDescription("궁극기 데미지 강화율", CountryCode.KOR)]
		NST_HYPER_SKILL_DAMAGE_RATE,
		// Token: 0x04002297 RID: 8855
		[CountryDescription("궁극기 데미지 감소율", CountryCode.KOR)]
		NST_HYPER_SKILL_DAMAGE_REDUCE_RATE,
		// Token: 0x04002298 RID: 8856
		[CountryDescription("침식체에게 주는 피해 증가", CountryCode.KOR)]
		NST_UNIT_TYPE_CORRUPTED_DAMAGE_RATE,
		// Token: 0x04002299 RID: 8857
		[CountryDescription("침식체에게 받는 피해 감소", CountryCode.KOR)]
		NST_UNIT_TYPE_CORRUPTED_DAMAGE_REDUCE_RATE,
		// Token: 0x0400229A RID: 8858
		[CountryDescription("리플레이서에게 주는 피해 증가", CountryCode.KOR)]
		NST_UNIT_TYPE_REPLACER_DAMAGE_RATE,
		// Token: 0x0400229B RID: 8859
		[CountryDescription("리플레이서에게 받는 피해 감소", CountryCode.KOR)]
		NST_UNIT_TYPE_REPLACER_DAMAGE_REDUCE_RATE,
		// Token: 0x0400229C RID: 8860
		[CountryDescription("상성으로 주는 피해 증가", CountryCode.KOR)]
		NST_ROLE_TYPE_DAMAGE_RATE,
		// Token: 0x0400229D RID: 8861
		[CountryDescription("상성으로 받는 피해 감소", CountryCode.KOR)]
		NST_ROLE_TYPE_DAMAGE_REDUCE_RATE,
		// Token: 0x0400229E RID: 8862
		[CountryDescription("성장형 공격력 증가율", CountryCode.KOR)]
		NST_HP_GROWN_ATK_RATE,
		// Token: 0x0400229F RID: 8863
		[CountryDescription("성장형 방어력 증가율", CountryCode.KOR)]
		NST_HP_GROWN_DEF_RATE,
		// Token: 0x040022A0 RID: 8864
		[CountryDescription("성장형 회피 증가율", CountryCode.KOR)]
		NST_HP_GROWN_EVADE_RATE,
		// Token: 0x040022A1 RID: 8865
		[CountryDescription("광역 피해 감소율", CountryCode.KOR)]
		NST_SPLASH_DAMAGE_REDUCE_RATE,
		// Token: 0x040022A2 RID: 8866
		[CountryDescription("디펜더 프로텍트 증가", CountryCode.KOR)]
		NST_DEFENDER_PROTECT_RATE,
		// Token: 0x040022A3 RID: 8867
		[CountryDescription("추가 피해 감소율", CountryCode.KOR)]
		NST_DAMAGE_INCREASE_DEFENCE,
		// Token: 0x040022A4 RID: 8868
		[CountryDescription("피해 감소 관통율", CountryCode.KOR)]
		NST_DAMAGE_REDUCE_PENETRATE,
		// Token: 0x040022A5 RID: 8869
		[CountryDescription("자기 추가 피해 감소율", CountryCode.KOR)]
		NST_DAMAGE_INCREASE_REDUCE,
		// Token: 0x040022A6 RID: 8870
		[CountryDescription("자기 피해 감소 관통율", CountryCode.KOR)]
		NST_DAMAGE_REDUCE_REDUCE,
		// Token: 0x040022A7 RID: 8871
		[CountryDescription("최대 피해 제한", CountryCode.KOR)]
		NST_DAMAGE_LIMIT_RATE_BY_HP,
		// Token: 0x040022A8 RID: 8872
		[CountryDescription("유효 타격 감소", CountryCode.KOR)]
		NST_ATTACK_COUNT_REDUCE,
		// Token: 0x040022A9 RID: 8873
		[CountryDescription("피해 내성", CountryCode.KOR)]
		NST_DAMAGE_RESIST,
		// Token: 0x040022AA RID: 8874
		[CountryDescription("배리어가 있는 적에게 공격 피해 감소", CountryCode.KOR)]
		NST_DAMAGE_REDUCE_RATE_AGAINST_BARRIER,
		// Token: 0x040022AB RID: 8875
		[CountryDescription("크리가 아닌 공격 데미지 비율", CountryCode.KOR)]
		NST_NON_CRITICAL_DAMAGE_TAKE_RATE,
		// Token: 0x040022AC RID: 8876
		[CountryDescription("주는 회복량 증가", CountryCode.KOR)]
		NST_HEAL_RATE,
		// Token: 0x040022AD RID: 8877
		[CountryDescription("넉백 저항", CountryCode.KOR)]
		NST_DAMAGE_BACK_RESIST,
		// Token: 0x040022AE RID: 8878
		[CountryDescription("전체 능력치 증가/감소", CountryCode.KOR)]
		NST_MAIN_STAT_RATE,
		// Token: 0x040022AF RID: 8879
		[CountryDescription("주는 피해 증감_Extra", CountryCode.KOR)]
		NST_EXTRA_ADJUST_DAMAGE_DEALT,
		// Token: 0x040022B0 RID: 8880
		[CountryDescription("받는 피해 증감_Extra", CountryCode.KOR)]
		NST_EXTRA_ADJUST_DAMAGE_RECEIVE,
		// Token: 0x040022B1 RID: 8881
		[CountryDescription("주는 피해 증감", CountryCode.KOR)]
		NST_ATTACK_DAMAGE_MODIFY_G2,
		// Token: 0x040022B2 RID: 8882
		[CountryDescription("코스트 반환", CountryCode.KOR)]
		NST_COST_RETURN_RATE,
		// Token: 0x040022B3 RID: 8883
		NST_RESERVE01,
		// Token: 0x040022B4 RID: 8884
		NST_RESERVE02,
		// Token: 0x040022B5 RID: 8885
		NST_RESERVE03,
		// Token: 0x040022B6 RID: 8886
		NST_RESERVE04,
		// Token: 0x040022B7 RID: 8887
		NST_RESERVE05,
		// Token: 0x040022B8 RID: 8888
		NST_END
	}
}
