package com.ts.dt.dao.impl;

import com.ts.dt.dao.MatchNodosityMainDao;
import com.ts.dt.exception.MatchException;
import com.ts.dt.po.MatchNodosityDetail;
import com.ts.dt.po.MatchNodosityMain;
import com.ts.dt.po.MatchNodosityTacticalDetail;

public class MatchNodosityMainDaoImpl extends BaseDao implements MatchNodosityMainDao {

	public void save(MatchNodosityMain matchNodosityMain) throws MatchException {
		// TODO Auto-generated method stub
		super.save(matchNodosityMain);
	}

	public static void main(String[] args) throws MatchException {
		MatchNodosityMainDao matchNodosityMainDao = new MatchNodosityMainDaoImpl();
		MatchNodosityMain main = new MatchNodosityMain();
		MatchNodosityDetail detail = new MatchNodosityDetail();
		MatchNodosityTacticalDetail d = new MatchNodosityTacticalDetail();
		main.addMatchDetailLog(detail);
		main.addDetail(d);
		matchNodosityMainDao.save(main);

	}
}