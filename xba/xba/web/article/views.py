#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response, redirect
from xba.model import Article, Comment

from xba.common import staticutil
from xba.common import listutil

def list(request, category, page=1):
    """文章列表"""
    page = int(page)
    if page < 1:
        page = 1
    pagesize = 20
    infos, total = Article.paging(page, pagesize, condition="status=1 and category='%s'" % category, order="created_time desc")
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    nextpage = page + 1l
    prevpage = page - 1
    
    response = render_to_response(request, "article/list.html", locals())
    path = "/article/%s/list/%s.html" % (category, page)
    staticutil.dump(response, path)
    return response
        
def detail(request, category, id):
    """文章详情"""
    article = Article.load(id=id)
    
    next_list = Article.query(condition="id>%s and category='%s'" % (id, article.category), order="id asc", limit=1)
    next = next_list[0] if next_list else None
   
    prev_list = Article.query(condition="id<%s and category='%s'" % (id, article.category), order="id desc", limit=1)
    prev = prev_list[0] if prev_list else None
    
    same_category_list = Article.query(condition="id<>%s and category='%s'" % (id, article.category), order="id desc", limit=20)
    
    sub_list = listutil.get_sub_list(same_category_list, 10)
    
    comment_list = Comment.query(condition="article_id=%s " % id, order="id desc")

    response = render_to_response(request, "article/detail.html", locals())
    path = "/article/%s/detail/%s.html" % (category, id)
    staticutil.dump(response, path)
    return response

def comment(request):
    
    article_id = request.REQUEST.get("id")
    username = request.REQUEST.get("username")
    content = request.REQUEST.get("content")
    category = request.REQUEST.get("category")
    
    if content:
        c = Comment()
        c.article_id = article_id
        c.username = username
        c.content = content
        c.persist()

    path = "/article/%s/detail/%s.html" % (category, article_id)
    staticutil.delete(path)

    url = "/article/%s/detail/%s.html" % (category, article_id)

    return redirect(url)
    
