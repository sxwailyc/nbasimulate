package com.ts.dt.dao;

import java.util.List;

import com.ts.dt.po.Player;

public interface ProfessionPlayerDao {

	public void save(Player player);

	public Player load(String id);

	public List<Player> getPlayerWithTeamId(long teamId);
}
