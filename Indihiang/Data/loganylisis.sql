/*琩高羆参璸*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.a_day = 13 group by page_access )
	order by  avgtime desc;
/*琩高hum╰参砐拜参璸*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hum%' group by page_access )
	order by  avgtime desc;
/*皐癸琘砐拜灿琩高*/	
select a.* from log_data a where a.page_access = '/hum/html/EveryDay_work-1.html'  and a.a_day = 11 order by time_taken desc;
/*琩高hr_sys╰参砐拜参璸*/
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hr_sys%'   group by page_access )
	order by  avgtime desc;
---------------------------------	
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hum%'  and t.a_day = 23  group by page_access )   --and t.page_access like '%every%html' 
	order by  avgtime desc;
	
select * from 
	(select t.page_access,count(*) times,avg(time_taken)  avgtime, max(time_taken)  maxtime ,min(time_taken) mintime from log_data t 
	where t.page_access like '%hr_sys%'   and t.a_day = 11 group by page_access )
	order by  avgtime desc;
	
select a.* from log_data a where a.page_access like '%SupportHuman%';--や穿
select a.* from log_data a where a.page_access like '%hum%'; --对
select a.* from log_data a where a.page_access like '%menjin%'; --箂皌ンΜ恨瞶

select a.a_time from log_data a where a.page_access = '/hum/ashx/Angular_Work/EveryDay_workaffirmlog.ashx'  and a.a_day = '23' group by a.a_time;
select a.* from log_data a where a.page_access = '/hum/ashx/Angular_Work/EveryDay_workaffirmlog.ashx'  and a.a_day = '23'  order by  a.time_taken desc;