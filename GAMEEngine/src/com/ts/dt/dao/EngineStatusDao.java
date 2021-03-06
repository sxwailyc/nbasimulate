package com.ts.dt.dao;

import com.ts.dt.exception.MatchException;
import com.ts.dt.po.EngineStatus;

public interface EngineStatusDao {

	public void save(EngineStatus engineStatus) throws MatchException;

	public void update(EngineStatus engineStatus) throws MatchException;

	public EngineStatus load(String name) throws MatchException;
}
