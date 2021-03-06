package com.ts.dt.match.helper;

import java.util.Iterator;
import java.util.List;

import com.ts.dt.context.MatchContext;
import com.ts.dt.dao.ProfessionPlayerDao;
import com.ts.dt.dao.impl.ProfessionPlayerDaoImpl;
import com.ts.dt.exception.MatchException;
import com.ts.dt.match.Controller;
import com.ts.dt.po.ActionDesc;
import com.ts.dt.po.Player;
import com.ts.dt.util.Logger;

public class SubstitutionHelper {

	public static void FoulOutSubstitution(MatchContext context) throws MatchException {

		Logger.info("start to substitution....");
		Controller foutOutController = context.getFoulOutController();
		String position = foutOutController.getPlayer().getPosition();
		String foutOutPlayerName = foutOutController.getPlayer().getName();
		long teamId = foutOutController.getPlayer().getTeamid();

		ProfessionPlayerDao playerDao = new ProfessionPlayerDaoImpl();

		List<Player> list = playerDao.getPlayerWithTeamId(teamId);

		Iterator<Player> iterator = list.iterator();

		Player onCourtPlayer = null;

		while (iterator.hasNext()) {
			Player player = iterator.next();
			if (context.onCourt(player.getNo())) {
				continue;
			} else {
				onCourtPlayer = choosePlayer(position, onCourtPlayer, player);
			}
		}
		// 将替换上场的球员添加到已上场列表
		context.addOnCourtPlayer(onCourtPlayer.getNo());
		foutOutController.setPlayer(onCourtPlayer);
		MatchInfoHelper.save(context, "<font color=\"red\">" + foutOutPlayerName + "犯满离场," + onCourtPlayer.getName() + "替换上场</font>");
		System.out.println(onCourtPlayer.getName() + "替换上场");
	}

	// 选择一个最适合的球员上场
	private static Player choosePlayer(String position, Player onCourtPlayer, Player player) {

		if (onCourtPlayer == null) {
			onCourtPlayer = player;
		} else {
			if (position.equals(player.getPosition())) {
				if (onCourtPlayer.getPosition() != position) {
					onCourtPlayer = player;
				} else if (player.getColligate() > onCourtPlayer.getColligate()) {
					onCourtPlayer = player;
				}
			} else if (onCourtPlayer.getPosition() != position && player.getColligate() > onCourtPlayer.getColligate()) {
				onCourtPlayer = player;
			}
		}

		return onCourtPlayer;
	}
}
