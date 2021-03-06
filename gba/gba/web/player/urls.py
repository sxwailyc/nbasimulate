#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.player',
    # clients
     url(r'^$', 'views.index', name='free-players'),
     url(r'^free_player_min/$', 'views.index', {'min': True}, name='free-players-min'),
     url(r'^freeplayerbid/$', 'views.free_player_bid', name='free-player-bid'),
     url(r'^youthfreeplayerbid/$', 'views.youth_free_player_bid', name='youth-free-player-bid'),
     url(r'^freeplayer_detail/$', 'views.freeplayer_detail', name='free-player-detail'),
     url(r'^profession_player/$', 'views.profession_player', name='profession-player'),
     url(r'^profession_player_min/$', 'views.profession_player', {'min': True}, name='profession-player-min'),
     url(r'^profession_player_detail/$', 'views.profession_player_detail', name='profession-player-detail'),
     url(r'^youthfreeplayer/$', 'views.youth_free_player', name='youth-free-players'),
     url(r'^youthfreeplayermin/$', 'views.youth_free_player', {'min': True}, name='youth-free-players-min'),
     url(r'^youth_freeplayer_detail/$', 'views.youth_freeplayer_detail', name='youth-freeplayer-detail'),
     url(r'^youth_player/$', 'views.youth_player', name='youth-player'),
     url(r'^youth_player_detail/$', 'views.youth_player_detail', name='youth-player-detail'),
     url(r'^attention_player/$', 'views.attention_player', name='attention-player'),
     url(r'^attention_player_min/$', 'views.attention_player', {'min': True}, name='attention-player-min'),
     url(r'^add_attention_player/$', 'views.add_attention_player', name='add-attention-player'),
     url(r'^remove_attention_player/$', 'views.remove_attention_player', name='remove-attention-player'),
     url(r'^player_detail/$', 'views.player_detail', name='player-detail'),
     url(r'^player_update/$', 'views.player_update', name='player-update'),
     url(r'^draft_player/$', 'views.draft_player', name='draft-player'),
     url(r'^draft_player_min/$', 'views.draft_player', {'min': True}, name='draft-player-min'),
     url(r'^draft_player_bid/$', 'views.draft_player_bid', name='draft-player-bid'),
     url(r'^pro_player_renew/$', 'views.pro_player_renew', name='pro-player-renew'),
     url(r'^pro_player_sell/$', 'views.pro_player_sell', name='pro-player-sell'),
     url(r'^finish_draft/$', 'views.finish_draft', name='finish-draft'),
     url(r'^youth_player_termination/$', 'views.youth_player_termination', name='youth-player-termination'),
     url(r'^youth_player_sell/$', 'views.youth_player_sell', name='youth-player-sell'),
     url(r'^youth_player_promoted/$', 'views.youth_player_promoted', name='youth-player-promoted'),
)
