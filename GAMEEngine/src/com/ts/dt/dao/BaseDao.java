package com.ts.dt.dao;


public interface BaseDao {
  public Object load(long ipkey);
  public void save(Persistable obj);
  public void update(Persistable obj);
  public void delete(Persistable obj);
}
