!�˗�
!   boulder_corridor_run   LuaQ     dlmapchunk                 A@  @    A�  @    A�  @    A  @  �       map    xxxxxxxxxxxxxxxxxx    xA..^............B@    xxx,xxxxxxxxxxxxxx       @                                                          :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   b  [LuaQ     dlmain           A      A@  @ �  A�  @ �  A  @ @ A� @ � A  @ @ A� @ �  C ��  ��Æ� ć��  ��D��@E���E�������K F �@ � �JA  I�F�� �\@  E  �@ �   \@�E  �� �� 
A  	Ȋ�  \@  E  �@ �� 
A  	ȋ�  \@  E  �� �� 
A  	ȉ�  \@  E� �� \@  � $      depth    D:8-13    tags    luniq_boulder_trap    no_monster_gen    orient    float    kfeat    ^ = pressure plate trap    kitem    ^ = *    TriggerableFunction    new    func $   callback.boulder_grate_trap_stepped 	   repeated    data    targetname    target_run 
   gratename 
   grate_run    bouldername    boulder_run    add_triggerer    DgnTriggerer    type    pressure_plate    lua_marker    ^    ,    props_marker       �?   A    B 	   AB=floor     A                                                                        	   	   	   	   	      
   
   
   
   
   
   
                                                                                                tm    @          :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   Q   !   boulder_dual_corridor_run    �LuaQ     dlmapchunk           
      A@  @    A�  @    A@  @  �       map    xxxxxxxxxxxxxxxxxxx    .A.......^.......A.     
                                            :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des     �LuaQ     dlmain           D      A@  @ �  A�  @ �  A  @ �  A@ @ � A� @   A@ @ � A� @   @C ��  ��C��@D���  � ŉɀŊ� Ƌ�� ���K@F ŀ �@�JA  IǍ� �\@  E@ �� �   \@�E@ �� �  
A  	AH��  \@  E@ �� �  
A  	AH��  \@  E@ �� �  
A  	AH��  \@  E  � 	 \@  � %      depth 	   Lair:3-8    tags    luniq_boulder_trap    no_monster_gen 
   ruin_lair    orient    float    kfeat    ^ = pressure plate trap    kitem    ^ = *    TriggerableFunction    new    func $   callback.boulder_grate_trap_stepped 	   repeated    data    targetname    target_dual 
   gratename    grate_dual    bouldername    boulder_dual    add_triggerer    DgnTriggerer    type    pressure_plate    lua_marker    ^    ,    props_marker       �?   A    B 	   AB=floor     D                                                                                 	   	   	   	   	      
   
   
   
   
   
   
                                                                                                tm     C          :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   n   !   boulder_quad_collide   LuaQ     dlmapchunk           (      A@  @    A�  @    A�  @    A  @    A@ @    A@ @    A� @    A@ @    A@ @    A  @    A�  @    A� @    A  @  � 	      map       xxx@xxx     xxx.....xxx     xA.......Ax    xx.........xx    x...........x    @.....^.....@     xxx....xxxx 
      xxx@xx     (                                                                           	   	   	   
   
   
                                            :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   �  VLuaQ     dlmain           A      A@  @ �  A�  @ �  A  @ @ A� @ � A  @ @ �B ��  � Å��Æ��  �@D���D��@E�������K�E �� ˀ�JA  IAF�� �\@  E� �� �   \@�E� �� �  
A  	Aǈ�  \@  E� �� �  
A  	Aǉ�  \@  E� �� �  
A  	AǊ�  \@  E� �  \@ E@ �� \@  � #      depth 	   Lair:4-8    tags    luniq_boulder_trap    no_monster_gen    orient    float    kfeat    ^ = pressure plate trap    TriggerableFunction    new    func $   callback.boulder_grate_trap_stepped 	   repeated    data    targetname    target_quad 
   gratename    grate_quad    bouldername    boulder_quad    add_triggerer    DgnTriggerer    type    pressure_plate    lua_marker    ^    props_marker       �?   ,    A    A=floor    kitem    ^=*     A                                                                                 	   	   	   	   	   	   	   
   
   
   
                                                                                             tm    @          :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   �   !   boulder_indie   �LuaQ     dlmapchunk           @      A@  @    A�  @    A�  @    A  @    A@ @    A� @    A� @    A  @    A@ @    A� @    A� @    A  @    A@ @    A� @    A� @    A  @    A@ @    A� @    A� @    A  @    A@ @  �       map 	      xxxxx 
     xx222xx     xx21112xx     x211ab12x     xx2..12xx 
     xx...xx 	      xx.xx        x.xxxxxx        x......x        xxxxxx.x             x.x    xxxxxxx  x.x    xB....x  x.x    x.xxx.x  x.x    x.x x.x  x.x    x^x x.xxxx.x    x.x x......x    x.x xxxxxxxx    xAx    x.x    x@x     @                                                                           	   	   	   
   
   
                                                                                                                    :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   �  �LuaQ     dlmain           S      A@  @ �  A�  @ �  A  @ @ A� @ �  B ��  ��� Å��  ��C��@D���D�������K E �@ � �JA  I�E�� �\@  E  �@ �   \@�E  �� �� 
A  	ǈ�  \@  E  �@ �� 
A  	ǉ�  \@  E  �� �� 
A  	Ǉ�  \@  E� �  \@ E@ �� \@ E@ �� \@ E 	 �@	 \@ E 	 ��	 \@ E�	 F � �@
 ��
 �
 \@ E� �  \@ E@ �� \@  � /      depth    D:7-14, Snake:1-4    tags    luniq_boulder_trap    no_monster_gen    orient    float    TriggerableFunction    new    func $   callback.boulder_grate_trap_stepped 	   repeated    data    targetname    target_indie 
   gratename    grate_indie    bouldername    boulder_indie    add_triggerer    DgnTriggerer    type    pressure_plate    lua_marker    ^    ,    props_marker       �?   A    B    kfeat    ^ = pressure plate trap    subst 
   2 = x 1 2    1 = 1 2 .:5    kmons    1 = ball python 
   2 = adder    dgn    delayed_decay    _G    a    human skeleton    abAB=floor    kitem 9   b = whip w:50 / cursed whip w:10 / whip ego:reaching w:1     S                                                                                             	   	   	   	   
   
   
   
   
   
   
                                                                                                                                       tm    R          :D:/Spiele/stone_soup-tiles-0.11/dat/des/traps/boulders.des   �   