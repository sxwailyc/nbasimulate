package com.ts.dt.monitor;

import com.dt.bottle.session.Session;
import com.dt.bottle.util.BottleUtil;
import com.ts.dt.engine.impl.MatchEngineImpl;
import com.ts.dt.po.MatchReq;
import com.ts.dt.pool.MatchReqPool;

public class MatchReqHandle extends Thread {

	@Override
	public void run() {
		// TODO Auto-generated method stub
		while (true) {
			MatchReq req = MatchReqPool.get();
			long matchId = new MatchEngineImpl().execute(req.getHomeTeamId(),
					req.getVisitingTeamId(), "CAR");

			Session session = BottleUtil.currentSession();
			req.setMatchId(matchId);
			session.beginTransaction();
			try {
				session.save(req);
			} catch (Exception e) {
				e.printStackTrace();
			}
			session.endTransaction();

		}

	}

}
