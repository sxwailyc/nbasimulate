package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.Player;

public interface PlayerDao {

	public Player load(String id) throws MatchException;

}
